using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Models;

/// <summary>
///     Requisição para finalizar pedidos (checkout).
/// </summary>
public class CheckoutRequest
{
    /// <summary>
    ///     Lista de IDs de pedidos para pagamento.
    /// </summary>
    [JsonPropertyName("orders")]
    public List<string> Orders { get; set; } = new();
}

