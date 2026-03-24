using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Models;

/// <summary>
///     Informações detalhadas de um pedido.
/// </summary>
public class OrderInfo : CartRequest
{
    [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;

    [JsonPropertyName("protocol")] public string Protocol { get; set; } = string.Empty;

    [JsonPropertyName("format")] public string Format { get; set; } = string.Empty;

    [JsonPropertyName("delivery")] public int Delivery { get; set; }

    [JsonPropertyName("delivery_min")] public int DeliveryMin { get; set; }

    [JsonPropertyName("delivery_max")] public int DeliveryMax { get; set; }

    [JsonPropertyName("discount")] public decimal Discount { get; set; }

    [JsonPropertyName("price")] public decimal Price { get; set; }

    [JsonPropertyName("tracking")] public string? Tracking { get; set; }

    [JsonPropertyName("status")] public string Status { get; set; } = string.Empty;

    [JsonPropertyName("service_id")] public int ServiceId { get; set; }

    [JsonPropertyName("insurance_value")] public decimal? InsuranceValue { get; set; }

    [JsonPropertyName("generated_at")] public string GeneratedAt { get; set; } = string.Empty;

    [JsonPropertyName("posted_at")] public string? PostedAt { get; set; }

    [JsonPropertyName("created_at")] public string CreatedAt { get; set; } = string.Empty;

    [JsonPropertyName("updated_at")] public string UpdatedAt { get; set; } = string.Empty;

    [JsonPropertyName("print")] public PrintInfo Print { get; set; } = new();

    [JsonPropertyName("tags")] public List<OrderTag> Tags { get; set; } = new();
}

