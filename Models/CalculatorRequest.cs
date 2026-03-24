using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Models;

/// <summary>
///     Requisiï¿½ï¿½o para o cï¿½lculo de frete.
/// </summary>
public class CalculatorRequest
{
    /// <summary>
    ///     CEP de origem.
    /// </summary>
    [JsonPropertyName("from")]
    public PostalAddress From { get; set; } = new();

    /// <summary>
    ///     CEP de destino.
    /// </summary>
    [JsonPropertyName("to")]
    public PostalAddress To { get; set; } = new();

    /// <summary>
    ///     Lista com os cï¿½digos dos serviï¿½os de entrega (ex: "1,2,17").
    /// </summary>
    [JsonPropertyName("services")]
    public string Services { get; set; } = "1,2,17";

    /// <summary>
    ///     Opï¿½ï¿½es adicionais do cï¿½lculo.
    /// </summary>
    [JsonPropertyName("options")]
    public ShippingOptions Options { get; set; } = new();

    /// <summary>
    ///     Dimensï¿½es da caixa (se conhecidas).
    /// </summary>
    [JsonPropertyName("package")]
    public PackageDimensions? Package { get; set; }

    /// <summary>
    ///     Lista de produtos individuais (se as dimensï¿½es da caixa nï¿½o forem conhecidas).
    /// </summary>
    [JsonPropertyName("products")]
    public List<ProductItem>? Products { get; set; }
}

