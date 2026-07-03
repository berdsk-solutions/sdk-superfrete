using System;
using System.Text.Json.Serialization;
using Berdsk.Sdk.SuperFrete.Converters;

namespace Berdsk.Sdk.SuperFrete.Services.Tracking.Dtos
{
    /// <summary>
    ///     Evento do histórico de chamados (tickets) da SuperFrete para o envio.
    /// </summary>
    public class SfTicketHistoryResponse
    {
        /// <summary>
        ///     Identificador do evento.
        /// </summary>
        [JsonPropertyName("id")]
        public long? Id { get; set; }

        /// <summary>
        ///     Identificador numérico interno do pedido associado.
        /// </summary>
        [JsonPropertyName("order_id")]
        public long? OrderId { get; set; }

        /// <summary>
        ///     Tipo do evento (ex: <c>ticket_history.ticket_created</c>,
        ///     <c>ticket_history.occurrence_status</c>).
        /// </summary>
        [JsonPropertyName("event_type")]
        public string? EventType { get; set; }

        /// <summary>
        ///     Parâmetros adicionais do evento.
        /// </summary>
        [JsonPropertyName("params")]
        public SfTicketHistoryParamsResponse? Params { get; set; }

        /// <summary>
        ///     Identificador da aplicação que registrou o evento.
        /// </summary>
        [JsonPropertyName("application_id")]
        public int? ApplicationId { get; set; }

        /// <summary>
        ///     Nome interno da aplicação que registrou o evento.
        /// </summary>
        [JsonPropertyName("application_name")]
        public string? ApplicationName { get; set; }

        /// <summary>
        ///     Nome de exibição da aplicação que registrou o evento.
        /// </summary>
        [JsonPropertyName("application_display_name")]
        public string? ApplicationDisplayName { get; set; }

        /// <summary>
        ///     Título do evento.
        /// </summary>
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        /// <summary>
        ///     Subtítulo do evento (pode conter Markdown).
        /// </summary>
        [JsonPropertyName("subtitle")]
        public string? Subtitle { get; set; }

        /// <summary>
        ///     Mensagem do evento.
        /// </summary>
        [JsonPropertyName("message")]
        public string? Message { get; set; }

        /// <summary>
        ///     Data/hora de criação do evento (UTC).
        /// </summary>
        [JsonPropertyName("created_at")]
        [JsonConverter(typeof(SfDateTimeConverter))]
        public DateTime? CreatedAt { get; set; }
    }
}
