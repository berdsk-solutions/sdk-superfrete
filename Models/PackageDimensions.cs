using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Models;

/// <summary>
///     Representa as dimensões e peso de um volume ou pacote.
/// </summary>
public class PackageDimensions
{
    /// <summary>
    ///     Altura em centímetros (cm).
    /// </summary>
    [JsonPropertyName("height")]
    public double Height { get; set; }

    /// <summary>
    ///     Largura em centímetros (cm).
    /// </summary>
    [JsonPropertyName("width")]
    public double Width { get; set; }

    /// <summary>
    ///     Comprimento em centímetros (cm).
    /// </summary>
    [JsonPropertyName("length")]
    public double Length { get; set; }

    /// <summary>
    ///     Peso em quilogramas (kg).
    /// </summary>
    [JsonPropertyName("weight")]
    public double Weight { get; set; }
}

