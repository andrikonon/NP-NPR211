using Newtonsoft.Json;

namespace _7._NovaPoshta.Models;

public class AreaResponseDTO
{
    [JsonProperty(propertyName: "success")]
    public bool Success { get; set; }
    [JsonProperty(propertyName: "data")] 
    public IEnumerable<AreaItemResponseDTO> Data { get; set; }
}