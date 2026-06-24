using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Webhooks.Dtos
{
    /// <summary>
    ///     Tag associada a um pedido, utilizada para vincular identificadores externos (ex: número do pedido da loja).
    /// </summary>
    public class SfWebhookTag
    {
        /// <summary>
        ///     Nome da tag (ex: <c>order_id</c>).
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        ///     Valor da tag (ex: <c>order-1555</c>).
        /// </summary>
        [JsonPropertyName("value")]
        public string? Value { get; set; }
    }
}
