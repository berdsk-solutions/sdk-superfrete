using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Tracking.Dtos
{
    /// <summary>
    ///     Informações de impressão/roteirização do volume na transportadora.
    ///     Campos além do código de barras variam por transportadora.
    /// </summary>
    public class SfTrackingVolumePrintResponse
    {
        /// <summary>
        ///     Código de barras do volume.
        /// </summary>
        [JsonPropertyName("barCode")]
        public string? BarCode { get; set; }

        /// <summary>
        ///     Unidade de destino do volume.
        /// </summary>
        [JsonPropertyName("destinationUnit")]
        public string? DestinationUnit { get; set; }

        /// <summary>
        ///     Código da última milha (last mile) da rota.
        /// </summary>
        [JsonPropertyName("lastMile")]
        public string? LastMile { get; set; }

        /// <summary>
        ///     Posição do volume na roteirização.
        /// </summary>
        [JsonPropertyName("position")]
        public string? Position { get; set; }

        /// <summary>
        ///     Prioridade do volume na roteirização.
        /// </summary>
        [JsonPropertyName("priority")]
        public int? Priority { get; set; }

        /// <summary>
        ///     Código da via/estrada da rota.
        /// </summary>
        [JsonPropertyName("road")]
        public string? Road { get; set; }

        /// <summary>
        ///     Código da rota do volume.
        /// </summary>
        [JsonPropertyName("route")]
        public string? Route { get; set; }

        /// <summary>
        ///     Sequência do volume dentro do envio.
        /// </summary>
        [JsonPropertyName("volumeSequence")]
        public int? VolumeSequence { get; set; }
    }
}
