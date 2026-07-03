using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Tracking.Dtos
{
    /// <summary>
    ///     Link para um comprovante de entrega.
    /// </summary>
    public class SfDeliveryProofLinkResponse
    {
        /// <summary>
        ///     Tipo do comprovante (ex: <c>pod</c>, <c>facade_photo</c>, <c>delivery_receipt</c>,
        ///     <c>delivery_receipt_image</c>).
        /// </summary>
        [JsonPropertyName("rel")]
        public string? Rel { get; set; }

        /// <summary>
        ///     URL do comprovante.
        /// </summary>
        [JsonPropertyName("href")]
        public string? Href { get; set; }
    }
}
