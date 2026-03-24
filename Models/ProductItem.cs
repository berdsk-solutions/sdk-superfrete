using Newtonsoft.Json;

namespace Berdsk.Sdk.SuperFrete.Models;

/// <summary>
///     Representa um produto individual para cálculo de frete.
/// </summary>
public class ProductItem
{
    /// <summary>
    ///     Quantidade deste produto.
    /// </summary>
    [JsonProperty("quantity")]
    public int Quantity { get; set; } = 1;

    /// <summary>
    ///     Peso do produto em quilogramas (kg).
    /// </summary>
    [JsonProperty("weight")]
    public double Weight { get; set; }

    /// <summary>
    ///     Altura do produto em centímetros (cm).
    /// </summary>
    [JsonProperty("height")]
    public double Height { get; set; }

    /// <summary>
    ///     Largura do produto em centímetros (cm).
    /// </summary>
    [JsonProperty("width")]
    public double Width { get; set; }

    /// <summary>
    ///     Comprimento do produto em centímetros (cm).
    /// </summary>
    [JsonProperty("length")]
    public double Length { get; set; }
}
