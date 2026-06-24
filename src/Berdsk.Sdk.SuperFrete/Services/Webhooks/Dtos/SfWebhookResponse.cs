using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Webhooks.Dtos
{
    /// <summary>
    ///     Representa um webhook app da SuperFrete.
    ///     Utilizado como resposta de criação, atualização e listagem de webhooks.
    /// </summary>
    public class SfWebhookResponse
    {
        /// <summary>
        ///     Identificador único do webhook na plataforma SuperFrete.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        /// <summary>
        ///     Nome identificador do webhook.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        ///     URL configurada para receber as notificações de eventos.
        /// </summary>
        [JsonPropertyName("url")]
        public string? Url { get; set; }

        /// <summary>
        ///     Token secreto gerado pela SuperFrete para validação HMAC-SHA256 das notificações recebidas.
        ///     Retornado apenas na criação do webhook — armazene-o de forma segura.
        /// </summary>
        [JsonPropertyName("secret_token")]
        public string? SecretToken { get; set; }

        /// <summary>
        ///     Lista de eventos monitorados por este webhook.
        ///     Use <see cref="Berdsk.Sdk.SuperFrete.Helpers.SfWebhookEvent" /> para comparação dos valores.
        /// </summary>
        [JsonPropertyName("events")]
        public string[]? Events { get; set; }

        /// <summary>
        ///     Indica se o webhook está ativo e recebendo notificações.
        /// </summary>
        [JsonPropertyName("is_active")]
        public bool IsActive { get; set; }

        /// <summary>
        ///     Data e hora de criação do webhook no formato ISO 8601.
        /// </summary>
        [JsonPropertyName("created_at")]
        public string? CreatedAt { get; set; }

        /// <summary>
        ///     Data e hora da última atualização do webhook no formato ISO 8601.
        ///     Será <c>null</c> se o webhook nunca foi atualizado.
        /// </summary>
        [JsonPropertyName("updated_at")]
        public string? UpdatedAt { get; set; }
    }
}
