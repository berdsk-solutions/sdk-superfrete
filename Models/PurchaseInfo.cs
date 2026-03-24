using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Models;

public class PurchaseInfo
{
    [JsonPropertyName("status")] public string Status { get; set; } = string.Empty;

    [JsonPropertyName("orders")] public List<PurchaseOrder> Orders { get; set; } = new();
}

