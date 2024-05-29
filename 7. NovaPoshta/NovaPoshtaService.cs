using System.Text;
using _7._NovaPoshta.Data;
using _7._NovaPoshta.Data.Entities;
using _7._NovaPoshta.Models;
using _7._NovaPoshta.Models.Settlements;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace _7._NovaPoshta;

public class NovaPoshtaService
{
    private readonly HttpClient _httpClient = new();
    private readonly string _url = @"https://api.novaposhta.ua/v2.0/json/";
    private readonly MyDataContext _dataContext = new();

    public NovaPoshtaService()
    {
        _dataContext.Database.Migrate();
    }

    public void GetAreas()
    {
        string key = "c575809eac6a20b014c93df30f7e4a1e";

        AreaRequestDTO areaRequest = new AreaRequestDTO
        {
            ApiKey = key,
            ModelName = "Address",
            CalledMethod = "getSettlementAreas",
            MethodProperties = new AreaRequestPropertyDTO
            {
                Page = "1",
                Ref = "",
            },
        };

        string json = JsonConvert.SerializeObject(areaRequest);
        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
        HttpResponseMessage response = _httpClient.PostAsync(_url, content).Result;
        if (response.IsSuccessStatusCode)
        {
            string responseData = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<AreaResponseDTO>(responseData);
            if (result.Data.Any())
            {
                foreach (var area in result.Data)
                {
                    var entity = _dataContext.Areas.SingleOrDefault(x => x.Ref == area.Ref);
                    if (entity is null)
                    {
                        entity = new AreaEntity
                        {
                            Name = area.Description,
                            Ref = area.Ref,
                        };
                        _dataContext.Areas.Add(entity);
                        _dataContext.SaveChanges();
                    }
                }
            }
        }
        else
        {
            Console.WriteLine($"Помилка запиту: {response.StatusCode}");
        }
    }

    public void GetSettlements()
    {
        string key = "c575809eac6a20b014c93df30f7e4a1e";
        int page = 1;
        while (true)
        {
            SettlementRequestDTO settlementRequest = new SettlementRequestDTO
            {
                ApiKey = key,
                ModelName = "AddressGeneral",
                CalledMethod = "getSettlements",
                MethodProperties = new SettlementRequestPropertyDTO
                {
                    Page = page.ToString(),
                    Limit = "1",
                    Warehouse = "1",
                },
            };
    
            string json = JsonConvert.SerializeObject(settlementRequest);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _httpClient.PostAsync(_url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                string responseData = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<SettlementResponseDTO>(responseData);
                if (result.Data.Any())
                {
                    foreach (var settlement in result.Data)
                    {
                        var entity = _dataContext.Settlements.SingleOrDefault(x => x.Ref == settlement.Ref);
                        if (entity is null)
                        {
                            entity = new SettlementEntity
                            {
                                Description = settlement.Description,
                                Ref = settlement.Ref,
                                AreaId = _dataContext.Areas.Where(x => x.Ref == settlement.Area).Select(x => x.Id)
                                    .SingleOrDefault(),
                            };
                            _dataContext.Settlements.Add(entity);
                        }
                    }
                    _dataContext.SaveChanges();

                    page++;
                }
                else
                {
                    return;
                }
            }
            else
            {
                Console.WriteLine($"Помилка запиту: {response.StatusCode}");
            }
        }
    }
}
