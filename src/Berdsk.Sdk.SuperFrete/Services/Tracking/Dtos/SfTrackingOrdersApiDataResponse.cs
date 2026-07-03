using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Tracking.Dtos
{
    /// <summary>
    ///     Dados completos do pedido na API de pedidos da SuperFrete, retornados junto ao rastreamento.
    /// </summary>
    public class SfTrackingOrdersApiDataResponse
    {
        /// <summary>
        ///     Status do pedido na SuperFrete (ex: <c>completed</c>).
        ///     Consulte <c>SfOrderStatus</c> para os valores conhecidos.
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        /// <summary>
        ///     Identificador único do usuário remetente na SuperFrete.
        /// </summary>
        [JsonPropertyName("uid")]
        public string? Uid { get; set; }

        /// <summary>
        ///     Identificador do pedido na SuperFrete.
        /// </summary>
        [JsonPropertyName("order_id")]
        public string? OrderId { get; set; }

        /// <summary>
        ///     Dados do pedido: endereços da etiqueta e ponto de postagem (Superpoint).
        /// </summary>
        [JsonPropertyName("data")]
        public SfTrackingOrderDataResponse? Data { get; set; }

        /// <summary>
        ///     Dados de pagamento do pedido.
        /// </summary>
        [JsonPropertyName("payment")]
        public SfTrackingPaymentResponse? Payment { get; set; }

        /// <summary>
        ///     Serviço de frete calculado na cotação.
        /// </summary>
        [JsonPropertyName("service_calculated")]
        public SfTrackingCalculatedServiceResponse? CalculatedService { get; set; }

        /// <summary>
        ///     Serviço de frete efetivamente postado (valores conferidos na postagem).
        /// </summary>
        [JsonPropertyName("service_posted")]
        public SfTrackingPostedServiceResponse? PostedService { get; set; }

        /// <summary>
        ///     Dados do envio na transportadora (código, rastreio, informações de volume).
        /// </summary>
        [JsonPropertyName("carrier_data")]
        public SfTrackingCarrierDataResponse? CarrierData { get; set; }

        /// <summary>
        ///     Dados do pedido no sistema interno (Magento) da SuperFrete.
        /// </summary>
        [JsonPropertyName("magento_data")]
        public SfTrackingMagentoDataResponse? MagentoData { get; set; }

        /// <summary>
        ///     Itens da declaração de conteúdo do envio.
        /// </summary>
        [JsonPropertyName("content_declaration")]
        public List<SfTrackingContentDeclarationResponse>? ContentDeclaration { get; set; }
    }
}
