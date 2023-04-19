using HomeTask1.YAMLModels;
using YamlDotNet.Serialization;

namespace HomeTask1
{
    internal static class MetaLogger
    {
        public static void LogNewParsedFile(string outputFolder, int parsedLinesNumber, int errorsNumber, string filePath)
        {
            string metaLogPath = Path.Combine(outputFolder, "meta.log");
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }
            ISerializer serializer = new SerializerBuilder().Build();
            string logData;
            if (!File.Exists(metaLogPath))
            {
                MetaLogObj metaLogObj = new()
                {
                    ParsedFiles = 0,
                    ParsedLines = 0,
                    FoundErrors = 0,
                    InvalidFiles = new List<string>()
                };
                logData = serializer.Serialize(metaLogObj);
            }
            else
            {
                logData = File.ReadAllText(metaLogPath);
            }

            IDeserializer deserializer = new DeserializerBuilder().Build();
            MetaLogObj yamlObject = deserializer.Deserialize<MetaLogObj>(logData);
            yamlObject.ParsedFiles++;
            yamlObject.ParsedLines += parsedLinesNumber;
            if (errorsNumber > 0)
            {
                yamlObject.FoundErrors += errorsNumber;
                yamlObject.InvalidFiles ??= new List<string>();
                yamlObject.InvalidFiles = yamlObject.InvalidFiles.Prepend(filePath).ToList();
            }

            string yamlData = serializer.Serialize(yamlObject);

            File.WriteAllText(Path.Join(outputFolder, "meta.log"), yamlData);
        }
    }
}
