using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Tracking.Dtos
{
    /// <summary>
    ///     Dados do envio na transportadora: identificadores, código de rastreio e informações de volume.
    /// </summary>
    public class SfTrackingCarrierDataResponse
    {
        /// <summary>
        ///     Código do envio na transportadora.
        /// </summary>
        [JsonPropertyName("code")]
        public string? Code { get; set; }

        /// <summary>
        ///     Identificador do envio na transportadora.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        /// <summary>
        ///     Código de rastreio do envio na transportadora.
        /// </summary>
        [JsonPropertyName("tracking_code")]
        public string? TrackingCode { get; set; }

        /// <summary>
        ///     Informações de impressão/roteirização do volume.
        /// </summary>
        [JsonPropertyName("volume_print")]
        public SfTrackingVolumePrintResponse? VolumePrint { get; set; }
    }
}
