using Newtonsoft.Json;

namespace Berdsk.Sdk.SuperFrete.Models;

/// <summary>
///     Produto para declaração de conteúdo.
/// </summary>
public class DeclarationProduct
{
    /// <summary>
    ///     Nome do produto.
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     Quantidade do produto.
    /// </summary>
    [JsonProperty("quantity")]
    public int Quantity { get; set; }

    /// <summary>
    ///     Valor unitário de cada produto.
    /// </summary>
    [JsonProperty("unitary_value")]
    public decimal UnitaryValue { get; set; }
}
