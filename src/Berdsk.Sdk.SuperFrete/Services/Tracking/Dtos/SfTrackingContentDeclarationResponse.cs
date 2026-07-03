using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Tracking.Dtos
{
    /// <summary>
    ///     Item da declaração de conteúdo do envio.
    ///     A API retorna quantidade e valor como string — a leitura aceita número ou string.
    /// </summary>
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public class SfTrackingContentDeclarationResponse
    {
        /// <summary>
        ///     Identificador do item.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        /// <summary>
        ///     Descrição do item.
        /// </summary>
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        /// <summary>
        ///     Quantidade do item.
        /// </summary>
        [JsonPropertyName("qty")]
        public int? Quantity { get; set; }

        /// <summary>
        ///     Valor unitário do item.
        /// </summary>
        [JsonPropertyName("value")]
        public decimal? Value { get; set; }
    }
}
