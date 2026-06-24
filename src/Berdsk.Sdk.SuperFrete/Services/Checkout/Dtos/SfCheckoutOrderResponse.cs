using System.Text.Json.Serialization;
using Berdsk.Sdk.SuperFrete.Helpers;

namespace Berdsk.Sdk.SuperFrete.Services.Checkout.Dtos
{
    /// <summary>
    ///     Dados de uma etiqueta após a realização do checkout.
    /// </summary>
    public class SfCheckoutOrderResponse
    {
        /// <summary>
        ///     Identificador único da etiqueta paga.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = default!;

        /// <summary>
        ///     Valor total do frete em reais.
        /// </summary>
        [JsonPropertyName("price")]
        public float Price { get; set; }

        /// <summary>
        ///     Valor do desconto aplicado em reais.
        /// </summary>
        [JsonPropertyName("discount")]
        public float Discount { get; set; }

        /// <summary>
        ///     Código numérico do serviço de frete utilizado.
        ///     Use <see cref="SfShippingServiceType" /> para interpretar o valor.
        /// </summary>
        [JsonPropertyName("service_id")]
        public int ServiceId { get; set; }

        /// <summary>
        ///     Código de rastreio da encomenda.
        ///     Disponível a partir do status <see cref="SfOrderStatus.Released" />.
        /// </summary>
        [JsonPropertyName("tracking")]
        public string? Tracking { get; set; }

        /// <summary>
        ///     Dados para impressão da etiqueta em PDF.
        /// </summary>
        [JsonPropertyName("print")]
        public SfCheckoutPrintResponse? Print { get; set; }
    }
}
