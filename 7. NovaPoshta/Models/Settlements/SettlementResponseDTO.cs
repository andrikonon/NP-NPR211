using Newtonsoft.Json;

namespace _7._NovaPoshta.Models;

public class SettlementResponseDTO
{
    [JsonProperty(propertyName: "success")]
    public bool Success { get; set; }
    [JsonProperty(propertyName: "data")] 
    public IEnumerable<SettlementItemResponseDTO> Data { get; set; }
}