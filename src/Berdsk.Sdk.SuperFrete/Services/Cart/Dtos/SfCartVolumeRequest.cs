using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Cart.Dtos
{
    /// <summary>
    ///     Dimensões físicas do pacote a ser enviado.
    ///     Utilize os valores retornados pela API de cotação de frete para garantir consistência.
    /// </summary>
    public class SfCartVolumeRequest
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
