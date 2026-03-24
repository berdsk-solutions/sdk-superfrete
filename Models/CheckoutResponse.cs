using Newtonsoft.Json;

namespace Berdsk.Sdk.SuperFrete.Models;

/// <summary>
///     Resposta do checkout.
/// </summary>
public class CheckoutResponse
{
    [JsonProperty("success")] public bool Success { get; set; }

    [JsonProperty("purchase")] public PurchaseInfo Purchase { get; set; } = new();
}
