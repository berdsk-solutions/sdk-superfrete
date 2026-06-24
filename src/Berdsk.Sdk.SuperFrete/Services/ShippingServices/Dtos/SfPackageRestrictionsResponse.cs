using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.ShippingServices.Dtos
{
    /// <summary>
    ///     Restrições de dimensões e peso para um formato de pacote específico.
    /// </summary>
    public class SfPackageRestrictionsResponse
    {
        /// <summary>
        ///     Limites de peso aceitos pelo serviço, em quilogramas.
        /// </summary>
        [JsonPropertyName("weight")]
        public SfMinMaxRangeResponse? Weight { get; set; }

        /// <summary>
        ///     Limites de largura do pacote, em centímetros.
        /// </summary>
        [JsonPropertyName("width")]
        public SfMinMaxRangeResponse? Width { get; set; }

        /// <summary>
        ///     Limites de altura do pacote, em centímetros.
        /// </summary>
        [JsonPropertyName("height")]
        public SfMinMaxRangeResponse? Height { get; set; }

        /// <summary>
        ///     Limites de comprimento do pacote, em centímetros.
        /// </summary>
        [JsonPropertyName("length")]
        public SfMinMaxRangeResponse? Length { get; set; }

        /// <summary>
        ///     Soma máxima das dimensões (altura + largura + comprimento) em centímetros.
        /// </summary>
        [JsonPropertyName("sum")]
        public float Sum { get; set; }
    }
}
