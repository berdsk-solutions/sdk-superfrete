using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Tracking.Dtos
{
    /// <summary>
    ///     Resposta completa da API pública de rastreamento da SuperFrete
    ///     (<c>GET public/tracking/{tracking_code}</c>).
    /// </summary>
    public class SfTrackingResponse
    {
        /// <summary>
        ///     Indica se o envio foi realizado pela SuperFrete.
        /// </summary>
        [JsonPropertyName("superfrete")]
        public bool? SuperFrete { get; set; }

        /// <summary>
        ///     Indica se a entrega está atrasada em relação à previsão.
        /// </summary>
        [JsonPropertyName("is_delayed")]
        public bool? IsDelayed { get; set; }

        /// <summary>
        ///     Quantidade de dias de atraso da entrega, quando aplicável.
        /// </summary>
        [JsonPropertyName("delay_days")]
        public int? DelayDays { get; set; }

        /// <summary>
        ///     Identificador da aplicação que originou a consulta de rastreamento.
        /// </summary>
        [JsonPropertyName("application_id")]
        public int? ApplicationId { get; set; }

        /// <summary>
        ///     Nome interno da aplicação que originou a consulta (ex: <c>tracking-page</c>).
        /// </summary>
        [JsonPropertyName("application_name")]
        public string? ApplicationName { get; set; }

        /// <summary>
        ///     Nome de exibição da aplicação que originou a consulta.
        /// </summary>
        [JsonPropertyName("application_display_name")]
        public string? ApplicationDisplayName { get; set; }

        /// <summary>
        ///     Detalhes do envio na SuperFrete: etiqueta, remetente, destinatário e dados do pedido.
        /// </summary>
        [JsonPropertyName("tracking")]
        public SfTrackingDetailsResponse? Tracking { get; set; }

        /// <summary>
        ///     Rastreamento reportado pela transportadora responsável (Jadlog, Loggi, Correios etc.).
        /// </summary>
        [JsonPropertyName("provider_tracking")]
        public SfProviderTrackingResponse? ProviderTracking { get; set; }

        /// <summary>
        ///     Comprovantes de entrega disponibilizados pela transportadora (fotos, recibos).
        /// </summary>
        [JsonPropertyName("delivery_proof")]
        public SfDeliveryProofResponse? DeliveryProof { get; set; }

        /// <summary>
        ///     Histórico de eventos de chamados (tickets) abertos na SuperFrete para este envio.
        /// </summary>
        [JsonPropertyName("ticket_history")]
        public List<SfTicketHistoryResponse>? TicketHistory { get; set; }

        /// <summary>
        ///     Previsões de entrega respondidas pela transportadora.
        ///     <b>Atenção:</b> o formato dos itens desta lista não é documentado pela SuperFrete;
        ///     por isso os elementos são expostos como <see cref="JsonElement" /> brutos.
        /// </summary>
        [JsonPropertyName("carrier_reply_delivery_forecasts")]
        public List<JsonElement>? CarrierReplyDeliveryForecasts { get; set; }

        /// <summary>
        ///     Indica se um chamado (ticket) foi criado automaticamente na SuperFrete para este envio.
        /// </summary>
        [JsonPropertyName("ticket_created")]
        public bool? TicketCreated { get; set; }
    }
}
