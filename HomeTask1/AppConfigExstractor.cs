using System.Configuration;

namespace HomeTask1
{
    internal static class AppConfigExstractor
    {
        internal static string ExtractDirectoryPath(string appConfigKey)
        {
            string? directoryPath = ConfigurationManager.AppSettings[appConfigKey];
            if (directoryPath == null)
            {
                throw new ArgumentNullException($"Configuration \"${appConfigKey}\" is missing in App.config file.");
            }
            if (!Directory.Exists(directoryPath))
            {
                throw new DirectoryNotFoundException($"Configuration \"{appConfigKey}\" directory {directoryPath} doesn't exist.");
            }
            return directoryPath;
        }
    }
}
