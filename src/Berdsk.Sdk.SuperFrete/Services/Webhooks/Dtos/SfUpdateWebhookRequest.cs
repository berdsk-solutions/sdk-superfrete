using System.Text.Json.Serialization;
using Berdsk.Sdk.SuperFrete.Helpers;

namespace Berdsk.Sdk.SuperFrete.Services.Webhooks.Dtos
{
    /// <summary>
    ///     Dados para atualização parcial de um webhook existente na SuperFrete.
    ///     Apenas os campos informados (não nulos) serão atualizados.
    /// </summary>
    public class SfUpdateWebhookRequest
    {
        /// <summary>
        ///     Novo nome do webhook. Quando <c>null</c>, o nome atual é mantido.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        ///     Nova URL de notificação. Quando <c>null</c>, a URL atual é mantida.
        ///     O endpoint deve estar acessível publicamente e aceitar requisições POST.
        /// </summary>
        [JsonPropertyName("url")]
        public string? Url { get; set; }

        /// <summary>
        ///     Nova lista de eventos a monitorar. Quando <c>null</c>, a lista atual é mantida.
        ///     Use as constantes de <see cref="SfWebhookEvent" /> para os valores aceitos.
        /// </summary>
        [JsonPropertyName("events")]
        public string[]? Events { get; set; }

        /// <summary>
        ///     Ativa (<c>true</c>) ou desativa (<c>false</c>) o webhook.
        ///     Quando <c>null</c>, o estado atual é mantido.
        /// </summary>
        [JsonPropertyName("is_active")]
        public bool? IsActive { get; set; }
    }
}
