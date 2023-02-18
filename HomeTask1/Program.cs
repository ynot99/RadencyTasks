using CsvHelper;
using CsvHelper.Configuration;
using HomeTask1;
using System.Globalization;

string usersCSVDataFolderPath;
string processedUsersCSVDataFolderPath;
try
{
    usersCSVDataFolderPath = AppConfigExstractor.ExtractDirectoryPath("UsersCSVDataFolder");
    processedUsersCSVDataFolderPath = AppConfigExstractor.ExtractDirectoryPath("ProcessedUsersCSVDataFolder");
}
catch (ArgumentNullException e)
{
    Console.WriteLine(e.Message);
    return;
}
catch (DirectoryNotFoundException e)
{
    Console.WriteLine(e.Message);
    return;
}

FileSystemWatcher watcher = new(usersCSVDataFolderPath)
{
    Filter = "*.csv",
    IncludeSubdirectories = true,
    EnableRaisingEvents = true
};

// TODO it is very risky move, it needs to be tested,
// if path cannot be remove from this list, we're screwed
List<string> pathsInProcess = new();

watcher.Changed += async (sender, e) =>
{
    if (e.ChangeType != WatcherChangeTypes.Changed)
    {
        return;
    }
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

            csv.Context.RegisterClassMap<MyCsvClassMap>();
            csv.Read();
            csv.ReadHeader();
            while (csv.Read())
            {
                Payer? record = csv.GetRecord<Payer>();
                if (record == null)
                {
                    Console.WriteLine("WOAH!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    continue;
                }
            }
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
