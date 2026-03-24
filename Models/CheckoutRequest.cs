using Newtonsoft.Json;

namespace Berdsk.Sdk.SuperFrete.Models;

/// <summary>
///     Requisição para finalizar pedidos (checkout).
/// </summary>
public class CheckoutRequest
{
    /// <summary>
    ///     Lista de IDs de pedidos para pagamento.
    /// </summary>
    [JsonProperty("orders")]
    public List<string> Orders { get; set; } = new();
}
