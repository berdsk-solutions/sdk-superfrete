using System;
using System.Text.Json.Serialization;
using Berdsk.Sdk.SuperFrete.Converters;

namespace Berdsk.Sdk.SuperFrete.Services.Tracking.Dtos
{
    /// <summary>
    ///     Detalhes do envio na SuperFrete: etiqueta, remetente, destinatário e dados do pedido.
    /// </summary>
    public class SfTrackingDetailsResponse
    {
        /// <summary>
        ///     Código da etiqueta/rastreio do envio.
        /// </summary>
        [JsonPropertyName("etiqueta")]
        public string? Label { get; set; }

        /// <summary>
        ///     Identificador único do usuário remetente na SuperFrete.
        /// </summary>
        [JsonPropertyName("uid")]
        public string? Uid { get; set; }

        /// <summary>
        ///     Nome do remetente.
        /// </summary>
        [JsonPropertyName("sender_name")]
        public string? SenderName { get; set; }

        /// <summary>
        ///     E-mail do remetente.
        /// </summary>
        [JsonPropertyName("email")]
        public string? Email { get; set; }

        /// <summary>
        ///     Telefone do remetente.
        /// </summary>
        [JsonPropertyName("phone_number")]
        public string? PhoneNumber { get; set; }

        /// <summary>
        ///     Número do pedido no sistema interno (Magento) da SuperFrete.
        /// </summary>
        [JsonPropertyName("order_number_magento")]
        public string? MagentoOrderNumber { get; set; }

        /// <summary>
        ///     Dados do pedido: endereços da etiqueta e ponto de postagem (Superpoint).
        /// </summary>
        [JsonPropertyName("order_data")]
        public SfTrackingOrderDataResponse? OrderData { get; set; }

        /// <summary>
        ///     Dados completos do pedido na API de pedidos da SuperFrete
        ///     (pagamento, serviço contratado, dados da transportadora, declaração de conteúdo).
        /// </summary>
        [JsonPropertyName("orders_api_data")]
        public SfTrackingOrdersApiDataResponse? OrdersApiData { get; set; }

        /// <summary>
        ///     Data/hora em que a consulta de rastreamento foi realizada (UTC).
        /// </summary>
        [JsonPropertyName("current_date")]
        [JsonConverter(typeof(SfDateTimeConverter))]
        public DateTime? CurrentDate { get; set; }

        /// <summary>
        ///     Nome do destinatário.
        /// </summary>
        [JsonPropertyName("recipient_name")]
        public string? RecipientName { get; set; }

        /// <summary>
        ///     Endereço completo do destinatário em formato de texto.
        /// </summary>
        [JsonPropertyName("recipient_address")]
        public string? RecipientAddress { get; set; }

        /// <summary>
        ///     Telefone do destinatário.
        /// </summary>
        [JsonPropertyName("recipient_phone")]
        public string? RecipientPhone { get; set; }

        /// <summary>
        ///     Modalidade de envio contratada (ex: <c>JADLOG ECONOMICO</c>, <c>LOGGI</c>).
        /// </summary>
        [JsonPropertyName("shipping_modality")]
        public string? ShippingModality { get; set; }
    }
}
