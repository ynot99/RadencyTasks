using Newtonsoft.Json;

namespace HomeTask1.JSONModels
{
    internal class CityObj
    {
        public string? City { get; set; }
        [JsonIgnore]
        internal Dictionary<string, ServiceObj>? ServiceDictionary { get; set; }
        [JsonProperty("Services")]
        public List<ServiceObj> Services => ServiceDictionary?.Values.ToList() ?? new List<ServiceObj>();
        public decimal Total { get; set; }
    }
}
