using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Tracking.Dtos
{
    /// <summary>
    ///     Serviço de frete calculado na cotação do pedido.
    /// </summary>
    public class SfTrackingCalculatedServiceResponse
    {
        /// <summary>
        ///     Valor de bônus aplicado.
        /// </summary>
        [JsonPropertyName("bonus")]
        public decimal? Bonus { get; set; }

        /// <summary>
        ///     Transportadora do serviço (ex: <c>jadlog</c>, <c>loggi</c>).
        /// </summary>
        [JsonPropertyName("carrier")]
        public string? Carrier { get; set; }

        /// <summary>
        ///     Código do serviço na transportadora (ex: <c>2002</c>, <c>FREIGHT_TYPE_ECONOMIC</c>).
        /// </summary>
        [JsonPropertyName("code")]
        public string? Code { get; set; }

        /// <summary>
        ///     Valor do seguro por valor declarado.
        /// </summary>
        [JsonPropertyName("declared_value_amount")]
        public decimal? DeclaredValueAmount { get; set; }

        /// <summary>
        ///     Prazo de entrega estimado, em dias úteis.
        /// </summary>
        [JsonPropertyName("delivery_time")]
        public int? DeliveryTime { get; set; }

        /// <summary>
        ///     CEP de destino.
        /// </summary>
        [JsonPropertyName("destinyPostCode")]
        public string? DestinationPostcode { get; set; }

        /// <summary>
        ///     Valor do desconto aplicado.
        /// </summary>
        [JsonPropertyName("discount_amount")]
        public decimal? DiscountAmount { get; set; }

        /// <summary>
        ///     Código do serviço usado para cálculo do desconto.
        /// </summary>
        [JsonPropertyName("discount_service_code")]
        public string? DiscountServiceCode { get; set; }

        /// <summary>
        ///     Indica se o serviço possui atendimento de balcão.
        /// </summary>
        [JsonPropertyName("has_counter_service")]
        public bool? HasCounterService { get; set; }

        /// <summary>
        ///     Indica se há desconto aplicado.
        /// </summary>
        [JsonPropertyName("has_discount")]
        public bool? HasDiscount { get; set; }

        /// <summary>
        ///     Indica se houve erro no cálculo do serviço.
        /// </summary>
        [JsonPropertyName("has_error")]
        public bool? HasError { get; set; }

        /// <summary>
        ///     Indica se há observações sobre o serviço.
        /// </summary>
        [JsonPropertyName("has_observation")]
        public bool? HasObservation { get; set; }

        /// <summary>
        ///     Indica se a entrega é domiciliar (no endereço do destinatário).
        /// </summary>
        [JsonPropertyName("home_delivery")]
        public bool? HomeDelivery { get; set; }

        /// <summary>
        ///     Indica se o serviço utiliza contrato da SuperFrete com a transportadora.
        /// </summary>
        [JsonPropertyName("isContract")]
        public bool? IsContract { get; set; }

        /// <summary>
        ///     Nível do serviço.
        /// </summary>
        [JsonPropertyName("level")]
        public int? Level { get; set; }

        /// <summary>
        ///     Prazo máximo de entrega, em dias úteis.
        /// </summary>
        [JsonPropertyName("max_delivery_time")]
        public int? MaxDeliveryTime { get; set; }

        /// <summary>
        ///     Prazo mínimo de entrega, em dias úteis.
        /// </summary>
        [JsonPropertyName("min_delivery_time")]
        public int? MinDeliveryTime { get; set; }

        /// <summary>
        ///     Nome do serviço (ex: <c>JADLOG Econômico</c>, <c>LOGGI</c>).
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        ///     CEP de origem.
        /// </summary>
        [JsonPropertyName("originPostCode")]
        public string? OriginPostcode { get; set; }

        /// <summary>
        ///     Valor real do desconto aplicado.
        /// </summary>
        [JsonPropertyName("real_discount_amount")]
        public decimal? RealDiscountAmount { get; set; }

        /// <summary>
        ///     Valor do serviço adicional de aviso de recebimento.
        /// </summary>
        [JsonPropertyName("receipt_notice_amount")]
        public decimal? ReceiptNoticeAmount { get; set; }

        /// <summary>
        ///     Indica se o serviço entrega aos sábados.
        /// </summary>
        [JsonPropertyName("saturday_delivery")]
        public bool? SaturdayDelivery { get; set; }

        /// <summary>
        ///     Valor do serviço adicional de mãos próprias.
        /// </summary>
        [JsonPropertyName("self_hand_amount")]
        public decimal? SelfHandAmount { get; set; }

        /// <summary>
        ///     Subtotal do frete, sem descontos.
        /// </summary>
        [JsonPropertyName("subtotal")]
        public decimal? Subtotal { get; set; }

        /// <summary>
        ///     Valor total do frete.
        /// </summary>
        [JsonPropertyName("total")]
        public decimal? Total { get; set; }

        /// <summary>
        ///     Valor total do frete com desconto aplicado.
        /// </summary>
        [JsonPropertyName("total_with_discount")]
        public decimal? TotalWithDiscount { get; set; }

        /// <summary>
        ///     Valor total do frete sem desconto.
        /// </summary>
        [JsonPropertyName("total_without_discount")]
        public decimal? TotalWithoutDiscount { get; set; }

        /// <summary>
        ///     Dimensões e peso do pacote informados na cotação.
        /// </summary>
        [JsonPropertyName("data")]
        public SfTrackingPackageDataResponse? Data { get; set; }
    }
}
