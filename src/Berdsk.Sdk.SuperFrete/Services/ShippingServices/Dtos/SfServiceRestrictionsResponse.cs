using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.ShippingServices.Dtos
{
    /// <summary>
    ///     Restrições técnicas de um serviço de entrega, incluindo limites de valor de seguro
    ///     e restrições de formato e dimensões de embalagem.
    /// </summary>
    public class SfServiceRestrictionsResponse
    {
        /// <summary>
        ///     Limites mínimo e máximo do valor declarado para seguro (em reais).
        /// </summary>
        [JsonPropertyName("insurance_value")]
        public SfMinMaxRangeResponse? InsuranceValue { get; set; }

        /// <summary>
        ///     Restrições por formato de embalagem (ex: caixa, rolo).
        /// </summary>
        [JsonPropertyName("formats")]
        public SfServiceFormatsResponse? Formats { get; set; }
    }
}
