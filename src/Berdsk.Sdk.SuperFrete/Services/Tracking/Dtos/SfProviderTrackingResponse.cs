using System;
using System.Text.Json.Serialization;
using Berdsk.Sdk.SuperFrete.Converters;

namespace Berdsk.Sdk.SuperFrete.Services.Tracking.Dtos
{
    /// <summary>
    ///     Rastreamento reportado pela transportadora responsável pelo envio.
    /// </summary>
    public class SfProviderTrackingResponse
    {
        /// <summary>
        ///     Transportadora que reportou o rastreamento (ex: <c>jadlog</c>, <c>loggi</c>).
        /// </summary>
        [JsonPropertyName("provider")]
        public string? Provider { get; set; }

        /// <summary>
        ///     Indica se a consulta à transportadora foi bem-sucedida.
        /// </summary>
        [JsonPropertyName("success")]
        public bool? Success { get; set; }

        /// <summary>
        ///     Identificador do envio na transportadora.
        /// </summary>
        [JsonPropertyName("shipmentId")]
        public string? ShipmentId { get; set; }

        /// <summary>
        ///     Status atual do envio na transportadora (ex: <c>Entregue</c>).
        /// </summary>
        [JsonPropertyName("shipmentStatus")]
        public string? ShipmentStatus { get; set; }

        /// <summary>
        ///     Previsão de entrega informada pela transportadora (UTC).
        /// </summary>
        [JsonPropertyName("previsaoEntrega")]
        [JsonConverter(typeof(SfDateTimeConverter))]
        public DateTime? EstimatedDelivery { get; set; }

        /// <summary>
        ///     Histórico de eventos de rastreamento do envio.
        /// </summary>
        [JsonPropertyName("tracking")]
        public SfProviderTrackingDetailsResponse? Tracking { get; set; }

        /// <summary>
        ///     Indica se o selo de "em atraso" deve ser exibido na página de rastreio.
        /// </summary>
        [JsonPropertyName("show_em_atraso_badge")]
        public bool? ShowDelayedBadge { get; set; }
    }
}
