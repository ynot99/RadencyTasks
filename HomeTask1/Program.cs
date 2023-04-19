using HomeTask1;
using HomeTask1.JSONModels;


Console.WriteLine("Type \"start\" to start the program");
while (Console.ReadLine() != "start") { }

string usersCsvDataFolderPath;
string processedUsersCsvDataFolderPath;
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

FileSystemWatcher watcher = new(usersCsvDataFolderPath)
{
    Filters = { "*.csv", "*.txt" },
    IncludeSubdirectories = true,
    EnableRaisingEvents = true
};

List<string> pathsInProcess = new();

watcher.Changed += async (_, e) =>
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
        int attempts = 0;
        while (attempts <= 500)
        {
            try
            {
                using FileStream stream = new(e.FullPath, FileMode.Open, FileAccess.Read, FileShare.None);
                stream.Close();
                break;
            }
            catch (IOException)
            {
                attempts++;
                Console.WriteLine($"The file {e.FullPath} is being used, waiting to try again...");
                Thread.Sleep(100);
            }
        }

        CsvProcessor csvProcessor = new(e.FullPath);
        Console.WriteLine($"The file \"{e.FullPath}\" is being processed {DateTime.Now}");
        IEnumerable<CityObj>? csvCityObjs = csvProcessor.Process();
        Console.WriteLine($"The file \"{e.FullPath}\" is being written to json {DateTime.Now}");
        JsonCityObjsWriter jsonCityObjsWriter = new(processedUsersCsvDataFolderPath);
        jsonCityObjsWriter.Write(csvCityObjs);
        pathsInProcess.Remove(e.FullPath);
        Console.WriteLine($"The file \"{e.FullPath}\" has done processing {DateTime.Now}");
        MetaLogger.LogNewParsedFile(
            jsonCityObjsWriter.OutputFolder,
            csvProcessor.ParsedLines,
            csvProcessor.InvalidLines,
            csvProcessor.CsvFilePath);
        Console.WriteLine("meta.log updated");
        Console.WriteLine("============================================================");
    });
};

Console.WriteLine("Type \"exit\" to stop the program");
while (Console.ReadLine() != "exit") { }

