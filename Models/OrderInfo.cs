using Newtonsoft.Json;

namespace Berdsk.Sdk.SuperFrete.Models;

/// <summary>
///     Informações detalhadas de um pedido.
/// </summary>
public class OrderInfo : CartRequest
{
    [JsonProperty("id")] public string Id { get; set; } = string.Empty;

    [JsonProperty("protocol")] public string Protocol { get; set; } = string.Empty;

    [JsonProperty("format")] public string Format { get; set; } = string.Empty;

    [JsonProperty("delivery")] public int Delivery { get; set; }

    [JsonProperty("delivery_min")] public int DeliveryMin { get; set; }

    [JsonProperty("delivery_max")] public int DeliveryMax { get; set; }

    [JsonProperty("discount")] public decimal Discount { get; set; }

    [JsonProperty("price")] public decimal Price { get; set; }

    [JsonProperty("tracking")] public string? Tracking { get; set; }

    [JsonProperty("status")] public string Status { get; set; } = string.Empty;

    [JsonProperty("service_id")] public int ServiceId { get; set; }

    [JsonProperty("insurance_value")] public decimal? InsuranceValue { get; set; }

    [JsonProperty("generated_at")] public string GeneratedAt { get; set; } = string.Empty;

    [JsonProperty("posted_at")] public string? PostedAt { get; set; }

    [JsonProperty("created_at")] public string CreatedAt { get; set; } = string.Empty;

    [JsonProperty("updated_at")] public string UpdatedAt { get; set; } = string.Empty;

    [JsonProperty("print")] public PrintInfo Print { get; set; } = new();

    [JsonProperty("tags")] public List<OrderTag> Tags { get; set; } = new();
}
