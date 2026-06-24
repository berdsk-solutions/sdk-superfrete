using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Checkout.Dtos
{
    /// <summary>
    ///     Dados necessários para realizar o checkout e pagar etiquetas com saldo da conta SuperFrete.
    /// </summary>
    public class SfCheckoutRequest
    {
        /// <summary>
        ///     Identificadores das etiquetas a serem pagas.
        ///     Os IDs são retornados pela API de carrinho ao criar cada etiqueta.
        /// </summary>
        [JsonPropertyName("orders")]
        public string[] Orders { get; set; } = default!;
    }
}
