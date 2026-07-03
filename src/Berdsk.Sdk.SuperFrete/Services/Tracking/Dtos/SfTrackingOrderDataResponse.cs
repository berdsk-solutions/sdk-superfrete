using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Tracking.Dtos
{
    /// <summary>
    ///     Dados do pedido associados ao envio: endereços da etiqueta e ponto de postagem (Superpoint).
    /// </summary>
    public class SfTrackingOrderDataResponse
    {
        /// <summary>
        ///     Endereços de origem e destino impressos na etiqueta.
        /// </summary>
        [JsonPropertyName("tag")]
        public SfTrackingTagResponse? Tag { get; set; }

        /// <summary>
        ///     Ponto de postagem/retirada (Superpoint) utilizado no envio.
        /// </summary>
        [JsonPropertyName("superpoint")]
        public SfTrackingSuperPointResponse? SuperPoint { get; set; }
    }
}
