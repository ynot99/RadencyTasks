using CsvHelper;
using CsvHelper.Configuration;
using HomeTask1;
using HomeTask1.JSONModels;
using System.Globalization;
using System.Text.Json;

// BAD do not just handle strings!
string usersCsvDataFolderPath; // convert to input model
string processedUsersCsvDataFolderPath; // convert to output model
try
{
    usersCsvDataFolderPath = AppConfigExstractor.ExtractDirectoryPath("UsersCSVDataFolder");
    processedUsersCsvDataFolderPath = AppConfigExstractor.ExtractDirectoryPath("ProcessedUsersCSVDataFolder");
}
catch (ArgumentNullException ex)
{
    Console.WriteLine(ex.Message);
    return;
}
catch (DirectoryNotFoundException ex)
{
    Console.WriteLine(ex.Message);
    return;
}
Input input = new(usersCsvDataFolderPath);

FileSystemWatcher watcher = new(input.Path)
{
    Filter = "*.csv",
    IncludeSubdirectories = true,
    EnableRaisingEvents = true
};

// TODO it is very risky move, it needs to be tested,
// if path cannot be removed from this list, we're screwed
List<string> pathsInProcess = new();

watcher.Changed += async (sender, e) =>
{
    if (e.ChangeType != WatcherChangeTypes.Changed)
    {
        return;
    }

    // MARK maybe not necessary to use Task.Run here
    await Task.Run(() =>
    {
        if (pathsInProcess.Find(pathInP => pathInP == e.FullPath) != null)
        {
            Console.WriteLine($"Cannot process a file {e.FullPath}, it is already being processed.");
            return;
        }
        pathsInProcess.Add(e.FullPath);
        // Wait for the file to be released by another process.
        while (IsFileLocked(e.FullPath))
        {
            Console.WriteLine("The file is being used, waiting to try again...");
            Thread.Sleep(100);
        }

        CsvConfiguration config = new(CultureInfo.InvariantCulture)
        {
            TrimOptions = TrimOptions.Trim,
            MissingFieldFound = null,
        };

        using (StreamReader reader = new(e.FullPath))
        using (CsvReader csv = new(reader, config))
        {
            Console.WriteLine($"The file \"{e.FullPath}\" is being processed {DateTime.Now}");

            Dictionary<string, CityObj> cityDictionary = new();

            csv.Context.RegisterClassMap<MyCsvClassMap>();

            Payer payer = new();
            IEnumerable<Payer> records = csv.EnumerateRecords(payer);
            foreach (Payer record in records)
            {
                // TODO count as a non valid record
                if (record.Address == null || record.Service == null) continue;
                string city = record.Address.Split(',')[0];
                if (!cityDictionary.ContainsKey(city))
                {
                    cityDictionary.Add(city, new CityObj
                    {
                        City = city,
                        Total = 0,
                        ServiceDictionary = new Dictionary<string, ServiceObj>()
                    });
                }

                cityDictionary[city].ServiceDictionary ??= new Dictionary<string, ServiceObj>();

                if (!cityDictionary[city].ServiceDictionary.ContainsKey(record.Service))
                {
                    cityDictionary[city].ServiceDictionary.Add(record.Service, new ServiceObj()
                    {
                        Name = record.Service,
                        Total = 0,
                        Payers = new List<PayerObj>()
                    }
                    );
                    cityDictionary[city].Total++;
                }

                cityDictionary[city].ServiceDictionary[record.Service].Payers.Add(new PayerObj()
                {
                    Name = $"{record.FirstName} {record.LastName}",
                    AccountNumber = record.AccountNumber,
                    Date = record.Date,
                    Payment = record.Payment
                });
                cityDictionary[city].ServiceDictionary[record.Service].Total++;
            }

            Console.WriteLine($"Serializing to JSON... {DateTime.Now}");

            const string fileName = "ProcessedCSV.json";
            using FileStream createStream
                    = File.Create(Path.Combine(processedUsersCsvDataFolderPath, fileName));

            JsonSerializer.Serialize(createStream, cityDictionary.Values.ToList());
            Console.WriteLine(Path.Combine(processedUsersCsvDataFolderPath, fileName));
        }
        pathsInProcess.Remove(e.FullPath);
        Console.WriteLine($"The file \"{e.FullPath}\" has done processing {DateTime.Now}");
        Console.WriteLine($"============================================================");
    });
};

Console.WriteLine("Press enter to exit.");
Console.ReadLine();

static bool IsFileLocked(string filePath)
{
    try
    {
        using FileStream stream = new(filePath, FileMode.Open, FileAccess.Read, FileShare.None);
        // The file is not locked.
        stream.Close();
        return false;
    }
    catch (IOException)
    {
        // The file is locked.
        return true;
    }
}
