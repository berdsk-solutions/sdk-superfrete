using Newtonsoft.Json;

namespace Berdsk.Sdk.SuperFrete.Models;

public class PurchaseInfo
{
    [JsonProperty("status")] public string Status { get; set; } = string.Empty;

    [JsonProperty("orders")] public List<PurchaseOrder> Orders { get; set; } = new();
}
