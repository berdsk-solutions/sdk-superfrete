using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Models;

/// <summary>
///     Representa um produto individual para cálculo de frete.
/// </summary>
public class ProductItem
{
    /// <summary>
    ///     Quantidade deste produto.
    /// </summary>
    [JsonPropertyName("quantity")]
    public int Quantity { get; set; } = 1;

    /// <summary>
    ///     Peso do produto em quilogramas (kg).
    /// </summary>
    [JsonPropertyName("weight")]
    public double Weight { get; set; }

    /// <summary>
    ///     Altura do produto em centímetros (cm).
    /// </summary>
    [JsonPropertyName("height")]
    public double Height { get; set; }

    /// <summary>
    ///     Largura do produto em centímetros (cm).
    /// </summary>
    [JsonPropertyName("width")]
    public double Width { get; set; }

    /// <summary>
    ///     Comprimento do produto em centímetros (cm).
    /// </summary>
    [JsonPropertyName("length")]
    public double Length { get; set; }
}

