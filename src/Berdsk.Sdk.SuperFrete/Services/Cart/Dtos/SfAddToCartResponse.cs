using System.Text.Json.Serialization;
using Berdsk.Sdk.SuperFrete.Helpers;

namespace Berdsk.Sdk.SuperFrete.Services.Cart.Dtos
{
    /// <summary>
    ///     Resposta retornada pela API ao adicionar um frete ao carrinho da SuperFrete.
    /// </summary>
    public class SfAddToCartResponse
    {
        /// <summary>
        ///     Identificador único da etiqueta criada.
        ///     Utilize este ID para realizar o pagamento via
        ///     <c>ISfCheckoutService.FinalizeOrderAsync</c>.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = default!;

        /// <summary>
        ///     Preço total do frete em reais.
        /// </summary>
        [JsonPropertyName("price")]
        public float Price { get; set; }

        /// <summary>
        ///     Status atual da etiqueta.
        ///     Use as constantes de <see cref="SfOrderStatus" /> para comparar o valor.
        /// </summary>
        [JsonPropertyName("status")]
        public string Status { get; set; } = default!;
    }
}
