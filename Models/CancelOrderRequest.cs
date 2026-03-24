using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Models;

/// <summary>
///     Requisição para cancelamento de pedido.
/// </summary>
public class CancelOrderRequest
{
    [JsonPropertyName("order")] public CancelOrderInfo Order { get; set; } = new();
}

