using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Calculator.Dtos
{
    /// <summary>
    ///     Opções adicionais para o cálculo de frete da SuperFrete.
    /// </summary>
    public class SfCalculationOptionsRequest
    {
        /// <summary>
        ///     Indica se o serviço de Mão Própria deve ser incluído no cálculo.
        ///     Quando <c>true</c>, a entrega só pode ser recebida pelo destinatário.
        /// </summary>
        [JsonPropertyName("own_hand")]
        public bool? OwnHand { get; set; }

        /// <summary>
        ///     Indica se o Aviso de Recebimento (AR) deve ser incluído no cálculo.
        ///     Quando <c>true</c>, o remetente recebe confirmação de entrega.
        /// </summary>
        [JsonPropertyName("receipt")]
        public bool? Receipt { get; set; }

        /// <summary>
        ///     Valor declarado da mercadoria para fins de seguro (em reais).
        /// </summary>
        [JsonPropertyName("insurance_value")]
        public float? InsuranceValue { get; set; }

        /// <summary>
        ///     Indica se o valor declarado (<see cref="InsuranceValue" />) deve ser utilizado
        ///     no cálculo do seguro da encomenda.
        /// </summary>
        [JsonPropertyName("use_insurance_value")]
        public bool? UseInsuranceValue { get; set; }
    }
}
