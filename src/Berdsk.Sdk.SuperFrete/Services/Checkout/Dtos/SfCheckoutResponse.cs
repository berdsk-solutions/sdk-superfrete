using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Checkout.Dtos
{
    /// <summary>
    ///     Resposta retornada pela API após a realização do checkout.
    /// </summary>
    public class SfCheckoutResponse
    {
        /// <summary>
        ///     Indica se o pagamento foi processado com sucesso.
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        /// <summary>
        ///     Detalhes da compra realizada, incluindo as etiquetas pagas.
        /// </summary>
        [JsonPropertyName("purchase")]
        public SfCheckoutPurchaseResponse? Purchase { get; set; }
    }
}
