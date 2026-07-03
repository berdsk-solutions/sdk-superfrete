using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Tracking.Dtos
{
    /// <summary>
    ///     Parâmetros adicionais de um evento do histórico de chamados.
    /// </summary>
    public class SfTicketHistoryParamsResponse
    {
        /// <summary>
        ///     Status de rastreamento associado ao evento (ex: <c>Entregue</c>).
        /// </summary>
        [JsonPropertyName("tracking_status")]
        public string? TrackingStatus { get; set; }
    }
}
