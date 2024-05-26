using Newtonsoft.Json;

namespace _7._NovaPoshta.Models;

public class AreaRequestPropertyDTO
{
    [JsonProperty(propertyName: "Page")]
    public string Page { get; set; }
    [JsonProperty(propertyName: "Ref")]
    public string Ref { get; set; }
}