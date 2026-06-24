using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Calculator.Dtos
{
    /// <summary>
    ///     Dimensões do pacote retornadas pelo cálculo de frete.
    /// </summary>
    /// <remarks>
    ///     A API da SuperFrete retorna as dimensões como strings (ex: <c>"10"</c>).
    ///     Converta para numérico conforme necessário ao utilizar esses valores.
    /// </remarks>
    public class SfCalculationPackageDimensionsResponse
    {
        /// <summary>
        ///     Altura do pacote em centímetros (retornada como string pela API).
        /// </summary>
        [JsonPropertyName("height")]
        public string? Height { get; set; }

        /// <summary>
        ///     Largura do pacote em centímetros (retornada como string pela API).
        /// </summary>
        [JsonPropertyName("width")]
        public string? Width { get; set; }

        /// <summary>
        ///     Comprimento do pacote em centímetros (retornada como string pela API).
        /// </summary>
        [JsonPropertyName("length")]
        public string? Length { get; set; }
    }
}
