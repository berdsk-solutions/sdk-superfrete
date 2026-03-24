using Newtonsoft.Json;

namespace Berdsk.Sdk.SuperFrete.Models;

/// <summary>
///     Resposta da criação de frete no carrinho.
/// </summary>
public class CartResponse
{
    /// <summary>
    ///     ID da etiqueta gerada.
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    ///     Status da etiqueta (ex: pending).
    /// </summary>
    [JsonProperty("status")]
    public string Status { get; set; } = string.Empty;
}
