using Newtonsoft.Json;

namespace _7._NovaPoshta.Models.Settlements;

public class SettlementRequestDTO
{
    [JsonProperty(propertyName: "apiKey")]
    public string ApiKey { get; set; }
    [JsonProperty(propertyName: "modelName")]
    public string ModelName { get; set; }
    [JsonProperty(propertyName: "calledMethod")]
    public string CalledMethod { get; set; }
    [JsonProperty(propertyName: "methodProperties")]
    public SettlementRequestPropertyDTO MethodProperties { get; set; }
}