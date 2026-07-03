using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Berdsk.Sdk.SuperFrete.Converters;

namespace Berdsk.Sdk.SuperFrete.Services.Tracking.Dtos
{
    /// <summary>
    ///     Comprovantes de entrega disponibilizados pela transportadora (fotos, recibos).
    /// </summary>
    public class SfDeliveryProofResponse
    {
        /// <summary>
        ///     Links para os comprovantes de entrega (foto da fachada, recibo, comprovante de posse etc.).
        /// </summary>
        [JsonPropertyName("links")]
        public List<SfDeliveryProofLinkResponse>? Links { get; set; }

        /// <summary>
        ///     Fonte/API de onde os comprovantes foram obtidos.
        /// </summary>
        [JsonPropertyName("source")]
        public string? Source { get; set; }

        /// <summary>
        ///     Transportadora que forneceu os comprovantes.
        /// </summary>
        [JsonPropertyName("carrier")]
        public string? Carrier { get; set; }

        /// <summary>
        ///     Data/hora em que os comprovantes foram capturados (UTC).
        /// </summary>
        [JsonPropertyName("captured_at")]
        [JsonConverter(typeof(SfDateTimeConverter))]
        public DateTime? CapturedAt { get; set; }

        /// <summary>
        ///     Nome de quem recebeu o pacote, quando informado pela transportadora.
        /// </summary>
        [JsonPropertyName("receiverName")]
        public string? ReceiverName { get; set; }

        /// <summary>
        ///     Documento de quem recebeu o pacote, quando informado pela transportadora.
        /// </summary>
        [JsonPropertyName("receiverDocument")]
        public string? ReceiverDocument { get; set; }

        /// <summary>
        ///     Descrição do local da entrega, quando informada pela transportadora.
        /// </summary>
        [JsonPropertyName("locationDescription")]
        public string? LocationDescription { get; set; }
    }
}
