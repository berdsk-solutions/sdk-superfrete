using Newtonsoft.Json;

namespace Berdsk.Sdk.SuperFrete.Models;

/// <summary>
///     Requisição para o cálculo de frete.
/// </summary>
public class CalculatorRequest
{
    /// <summary>
    ///     CEP de origem.
    /// </summary>
    [JsonProperty("from")]
    public PostalAddress From { get; set; } = new();

    /// <summary>
    ///     CEP de destino.
    /// </summary>
    [JsonProperty("to")]
    public PostalAddress To { get; set; } = new();

    /// <summary>
    ///     Lista com os códigos dos serviços de entrega (ex: "1,2,17").
    /// </summary>
    [JsonProperty("services")]
    public string Services { get; set; } = "1,2,17";

    /// <summary>
    ///     Opções adicionais do cálculo.
    /// </summary>
    [JsonProperty("options")]
    public ShippingOptions Options { get; set; } = new();

    /// <summary>
    ///     Dimensões da caixa (se conhecidas).
    /// </summary>
    [JsonProperty("package")]
    public PackageDimensions? Package { get; set; }

    /// <summary>
    ///     Lista de produtos individuais (se as dimensões da caixa não forem conhecidas).
    /// </summary>
    [JsonProperty("products")]
    public List<ProductItem>? Products { get; set; }
}
