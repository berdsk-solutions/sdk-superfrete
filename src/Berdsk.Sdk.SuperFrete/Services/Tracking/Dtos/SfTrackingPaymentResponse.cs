using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Tracking.Dtos
{
    /// <summary>
    ///     Dados de pagamento do pedido associado ao envio.
    /// </summary>
    public class SfTrackingPaymentResponse
    {
        /// <summary>
        ///     Valor cobrado no cartão de crédito.
        /// </summary>
        [JsonPropertyName("amount_charged_to_credit_card")]
        public decimal? AmountChargedToCreditCard { get; set; }

        /// <summary>
        ///     Valor de crédito em conta a ser utilizado.
        /// </summary>
        [JsonPropertyName("credit_to_be_used")]
        public decimal? CreditToBeUsed { get; set; }

        /// <summary>
        ///     Valor de crédito efetivamente aplicado no pagamento.
        /// </summary>
        [JsonPropertyName("applied_credit_amount")]
        public decimal? AppliedCreditAmount { get; set; }

        /// <summary>
        ///     Indica se o pagamento utilizou cartão de crédito.
        /// </summary>
        [JsonPropertyName("use_credit_card")]
        public bool? UseCreditCard { get; set; }

        /// <summary>
        ///     Indica se o pagamento utilizou saldo em conta.
        /// </summary>
        [JsonPropertyName("use_store_credit")]
        public bool? UseStoreCredit { get; set; }
    }
}
