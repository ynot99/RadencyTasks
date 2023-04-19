using HomeTask1.JSONModels;
using System.Text.Json;

namespace HomeTask1;

internal class JsonCityObjsWriter
{
    public string OutputFolder { get; }

    public JsonCityObjsWriter(string outputFolder)
    {
        OutputFolder = Path.Combine(outputFolder, DateTime.Now.ToString("MM-dd-yyyy"));
    }

    public void Write(IEnumerable<CityObj>? cityObjs)
    {
        // BAD it will also work with outputwhateverhereis.json
        if (!Directory.Exists(OutputFolder))
        {
            Directory.CreateDirectory(OutputFolder);
        }
        int outputFilesLength = Directory.GetFiles(OutputFolder, "output*.json").Length;

        string fileName = $"output{outputFilesLength + 1}.json";
        using FileStream createStream = File.Create(Path.Combine(OutputFolder, fileName));

        if (cityObjs != null)
        {
            JsonSerializer.Serialize(createStream, cityObjs);
        }
    }
}
