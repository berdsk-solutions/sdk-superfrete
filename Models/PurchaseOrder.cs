using Newtonsoft.Json;

namespace Berdsk.Sdk.SuperFrete.Models;

public class PurchaseOrder
{
    [JsonProperty("id")] public string Id { get; set; } = string.Empty;

    [JsonProperty("price")] public decimal Price { get; set; }

    [JsonProperty("discount")] public decimal Discount { get; set; }

    [JsonProperty("service_id")] public int ServiceId { get; set; }

    [JsonProperty("tracking")] public string? Tracking { get; set; }

    [JsonProperty("print")] public PrintInfo Print { get; set; } = new();
}
