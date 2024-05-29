using Newtonsoft.Json;

namespace _7._NovaPoshta.Models;

public class AreaRequestDTO
{
    [JsonProperty(propertyName: "apiKey")]
    public string ApiKey { get; set; }
    [JsonProperty(propertyName: "modelName")]
    public string ModelName { get; set; }
    [JsonProperty(propertyName: "calledMethod")]
    public string CalledMethod { get; set; }
    [JsonProperty(propertyName: "methodProperties")]
    public AreaRequestPropertyDTO MethodProperties { get; set; }
}