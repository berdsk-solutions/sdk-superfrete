using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Webhooks.Dtos
{
    /// <summary>
    ///     Tag associada a um pedido recebida na notificação de webhook da SuperFrete.
    /// </summary>
    public class SfWebhookNotificationTagData
    {
        /// <summary>
        ///     Identificador do pedido na plataforma de origem (ex: número do pedido da loja).
        /// </summary>
        [JsonPropertyName("tag")]
        public string? Tag { get; set; }

        /// <summary>
        ///     URL da plataforma de origem associada à tag.
        /// </summary>
        [JsonPropertyName("url")]
        public string? Url { get; set; }
    }
}
