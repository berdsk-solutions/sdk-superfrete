using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Models;

/// <summary>
///     Resposta do checkout.
/// </summary>
public class CheckoutResponse
{
    [JsonPropertyName("success")] public bool Success { get; set; }

    [JsonPropertyName("purchase")] public PurchaseInfo Purchase { get; set; } = new();
}

