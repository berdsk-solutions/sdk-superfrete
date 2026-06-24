using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Calculator.Dtos
{
    /// <summary>
    ///     Dimensões e peso da embalagem para o cálculo de frete.
    ///     Use este objeto quando as dimensões da caixa já são conhecidas.
    ///     Alternativamente, use <see cref="SfCalculationProductRequest" /> para que a API calcule a caixa ideal.
    /// </summary>
    public class SfCalculationPackageRequest
    {
        /// <summary>
        ///     Altura da embalagem em centímetros.
        /// </summary>
        [JsonPropertyName("height")]
        public float Height { get; set; }

        /// <summary>
        ///     Largura da embalagem em centímetros.
        /// </summary>
        [JsonPropertyName("width")]
        public float Width { get; set; }

        /// <summary>
        ///     Comprimento da embalagem em centímetros.
        /// </summary>
        [JsonPropertyName("length")]
        public float Length { get; set; }

        /// <summary>
        ///     Peso da embalagem em quilogramas.
        /// </summary>
        [JsonPropertyName("weight")]
        public float Weight { get; set; }
    }
}
