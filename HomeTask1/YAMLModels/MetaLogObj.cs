using YamlDotNet.Serialization;

namespace HomeTask1.YAMLModels
{
    public class MetaLogObj
    {
        [YamlMember(Alias = "parsed_lines")]
        public int ParsedLines { get; set; }
        [YamlMember(Alias = "parsed_files")]
        public int ParsedFiles { get; set; }
        [YamlMember(Alias = "found_errors")]
        public int FoundErrors { get; set; }
        [YamlMember(Alias = "invalid_files")]
        public List<string>? InvalidFiles { get; set; }
    }
}
