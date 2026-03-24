using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Models;

/// <summary>
///     Produto para declaração de conteúdo.
/// </summary>
public class DeclarationProduct
{
    /// <summary>
    ///     Nome do produto.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     Quantidade do produto.
    /// </summary>
    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    /// <summary>
    ///     Valor unitário de cada produto.
    /// </summary>
    [JsonPropertyName("unitary_value")]
    public decimal UnitaryValue { get; set; }
}

