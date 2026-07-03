using System;
using System.Text.Json.Serialization;
using Berdsk.Sdk.SuperFrete.Converters;

namespace Berdsk.Sdk.SuperFrete.Services.Tracking.Dtos
{
    /// <summary>
    ///     Evento individual do histórico de rastreamento do envio.
    /// </summary>
    public class SfTrackingEventResponse
    {
        /// <summary>
        ///     Data/hora do evento (UTC).
        /// </summary>
        [JsonPropertyName("data")]
        [JsonConverter(typeof(SfDateTimeConverter))]
        public DateTime? Date { get; set; }

        /// <summary>
        ///     Status do envio no momento do evento (ex: <c>Em trânsito</c>, <c>Entregue</c>).
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        /// <summary>
        ///     Unidade/local onde o evento ocorreu.
        /// </summary>
        [JsonPropertyName("unidade")]
        public string? Unit { get; set; }

        /// <summary>
        ///     Descrição detalhada do evento.
        /// </summary>
        [JsonPropertyName("descricao")]
        public string? Description { get; set; }

        /// <summary>
        ///     Origem do evento de rastreamento (ex: <c>jadlog</c>, <c>loggi</c>, <c>pegaki</c>).
        /// </summary>
        [JsonPropertyName("tracking_origin")]
        public string? TrackingOrigin { get; set; }
    }
}
