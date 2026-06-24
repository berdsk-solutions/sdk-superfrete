using System.Text.Json.Serialization;
using Berdsk.Sdk.SuperFrete.Helpers;

namespace Berdsk.Sdk.SuperFrete.Services.Webhooks.Dtos
{
    /// <summary>
    ///     Payload recebido pelo endpoint configurado no webhook quando um evento é disparado pela SuperFrete.
    ///     Utilize o campo <see cref="Event" /> para determinar qual ação foi executada e processar
    ///     os dados em <see cref="Data" /> adequadamente.
    /// </summary>
    public class SfWebhookNotificationPayload
    {
        /// <summary>
        ///     Tipo do evento que originou a notificação.
        ///     Use as constantes de <see cref="SfWebhookEvent" /> para comparar o valor recebido.
        /// </summary>
        [JsonPropertyName("event")]
        public string? Event { get; set; }

        /// <summary>
        ///     Dados do pedido/etiqueta associados ao evento disparado.
        /// </summary>
        [JsonPropertyName("data")]
        public SfWebhookNotificationData? Data { get; set; }
    }
}
