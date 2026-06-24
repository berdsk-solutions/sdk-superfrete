using System.Text.Json.Serialization;
using Berdsk.Sdk.SuperFrete.Helpers;

namespace Berdsk.Sdk.SuperFrete.Services.Webhooks.Dtos
{
    /// <summary>
    ///     Dados necessários para criar um novo webhook app na SuperFrete.
    /// </summary>
    public class SfCreateWebhookRequest
    {
        /// <summary>
        ///     Nome identificador do webhook (obrigatório).
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = default!;

        /// <summary>
        ///     URL que receberá as notificações via HTTP POST (obrigatório).
        ///     O endpoint deve estar acessível publicamente e aceitar requisições POST.
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; } = default!;

        /// <summary>
        ///     Lista de eventos a monitorar.
        ///     Use as constantes de <see cref="SfWebhookEvent" /> para os valores aceitos.
        ///     Quando não informado, todos os eventos serão monitorados.
        /// </summary>
        [JsonPropertyName("events")]
        public string[]? Events { get; set; }
    }
}
