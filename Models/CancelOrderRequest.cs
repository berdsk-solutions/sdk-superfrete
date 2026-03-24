using Newtonsoft.Json;

namespace Berdsk.Sdk.SuperFrete.Models;

/// <summary>
///     Requisição para cancelamento de pedido.
/// </summary>
public class CancelOrderRequest
{
    [JsonProperty("order")] public CancelOrderInfo Order { get; set; } = new();
}
