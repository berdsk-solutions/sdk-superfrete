using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Models;

public class PurchaseOrder
{
    [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;

    [JsonPropertyName("price")] public decimal Price { get; set; }

    [JsonPropertyName("discount")] public decimal Discount { get; set; }

    [JsonPropertyName("service_id")] public int ServiceId { get; set; }

    [JsonPropertyName("tracking")] public string? Tracking { get; set; }

    [JsonPropertyName("print")] public PrintInfo Print { get; set; } = new();
}

