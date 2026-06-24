using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Checkout.Dtos
{
    /// <summary>
    ///     Detalhes da compra realizada durante o checkout.
    /// </summary>
    public class SfCheckoutPurchaseResponse
    {
        /// <summary>
        ///     Status da compra (ex: "paid").
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        /// <summary>
        ///     Lista de etiquetas pagas com detalhes de preço, desconto e rastreio.
        /// </summary>
        [JsonPropertyName("orders")]
        public SfCheckoutOrderResponse[]? Orders { get; set; }
    }
}
