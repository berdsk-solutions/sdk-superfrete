using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Calculator.Dtos
{
    /// <summary>
    ///     Serviços adicionais contratados no cálculo de frete.
    /// </summary>
    public class SfCalculationAdditionalServicesResponse
    {
        /// <summary>
        ///     Indica se o Aviso de Recebimento (AR) foi incluído no cálculo.
        /// </summary>
        [JsonPropertyName("receipt")]
        public bool Receipt { get; set; }

        /// <summary>
        ///     Indica se o serviço de Mão Própria foi incluído no cálculo.
        /// </summary>
        [JsonPropertyName("own_hand")]
        public bool OwnHand { get; set; }
    }
}
