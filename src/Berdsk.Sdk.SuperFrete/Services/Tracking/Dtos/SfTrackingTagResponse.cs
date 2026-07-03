using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Tracking.Dtos
{
    /// <summary>
    ///     Endereços de origem e destino impressos na etiqueta do envio.
    /// </summary>
    public class SfTrackingTagResponse
    {
        /// <summary>
        ///     Endereço e contato do remetente (origem).
        /// </summary>
        [JsonPropertyName("origin")]
        public SfTrackingAddressResponse? Origin { get; set; }

        /// <summary>
        ///     Endereço e contato do destinatário (destino).
        /// </summary>
        [JsonPropertyName("destiny")]
        public SfTrackingAddressResponse? Destination { get; set; }
    }
}
