using System.Text.Json.Serialization;
using Berdsk.Sdk.SuperFrete.Helpers;

namespace Berdsk.Sdk.SuperFrete.Services.Webhooks.Dtos
{
    /// <summary>
    ///     Payload recebido no endpoint configurado no webhook quando a SuperFrete dispara um evento.
    ///     Deserialize o body da requisição recebida nesta classe para processar a notificação.
    /// </summary>
    public class SfWebhookPayload
    {
        /// <summary>
        ///     Tipo do evento que originou a notificação.
        ///     Use as constantes de <see cref="SfWebhookEvent" /> para comparar o valor recebido.
        /// </summary>
        [JsonPropertyName("event")]
        public string? Event { get; set; }

        /// <summary>
        ///     Dados do pedido associados ao evento.
        /// </summary>
        [JsonPropertyName("data")]
        public SfWebhookPayloadData? Data { get; set; }
    }
}
