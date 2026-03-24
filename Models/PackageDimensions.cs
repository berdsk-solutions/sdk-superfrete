using Newtonsoft.Json;

namespace Berdsk.Sdk.SuperFrete.Models;

/// <summary>
///     Representa as dimensões e peso de um volume ou pacote.
/// </summary>
public class PackageDimensions
{
    /// <summary>
    ///     Altura em centímetros (cm).
    /// </summary>
    [JsonProperty("height")]
    public double Height { get; set; }

    /// <summary>
    ///     Largura em centímetros (cm).
    /// </summary>
    [JsonProperty("width")]
    public double Width { get; set; }

    /// <summary>
    ///     Comprimento em centímetros (cm).
    /// </summary>
    [JsonProperty("length")]
    public double Length { get; set; }

    /// <summary>
    ///     Peso em quilogramas (kg).
    /// </summary>
    [JsonProperty("weight")]
    public double Weight { get; set; }
}
