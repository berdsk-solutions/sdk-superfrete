using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Tracking.Dtos
{
    /// <summary>
    ///     Histórico de rastreamento consolidado do envio na transportadora.
    /// </summary>
    public class SfProviderTrackingDetailsResponse
    {
        /// <summary>
        ///     Código de rastreio do envio na transportadora.
        /// </summary>
        [JsonPropertyName("codigo")]
        public string? Code { get; set; }

        /// <summary>
        ///     Status atual do envio (ex: <c>Entregue</c>).
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        /// <summary>
        ///     Eventos de rastreamento, do mais recente para o mais antigo.
        /// </summary>
        [JsonPropertyName("eventos")]
        public List<SfTrackingEventResponse>? Events { get; set; }
    }
}
