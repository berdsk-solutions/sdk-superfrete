using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Tracking.Dtos
{
    /// <summary>
    ///     Coordenadas geográficas de um ponto de postagem/retirada.
    /// </summary>
    public class SfTrackingGeolocationResponse
    {
        /// <summary>
        ///     Latitude.
        /// </summary>
        [JsonPropertyName("lat")]
        public double? Latitude { get; set; }

        /// <summary>
        ///     Longitude.
        /// </summary>
        [JsonPropertyName("lon")]
        public double? Longitude { get; set; }
    }
}
