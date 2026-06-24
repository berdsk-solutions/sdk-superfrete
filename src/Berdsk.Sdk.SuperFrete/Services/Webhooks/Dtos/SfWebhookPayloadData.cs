using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Webhooks.Dtos
{
    /// <summary>
    ///     Dados do pedido contidos no payload de uma notificação de webhook da SuperFrete.
    /// </summary>
    public class SfWebhookPayloadData
    {
        /// <summary>
        ///     Identificador único do pedido na plataforma SuperFrete.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        /// <summary>
        ///     Identificador do pedido (igual ao <see cref="Id" /> na API atual).
        /// </summary>
        [JsonPropertyName("order_id")]
        public string? OrderId { get; set; }

        /// <summary>
        ///     Protocolo interno do pedido. <c>null</c> em alguns eventos.
        /// </summary>
        [JsonPropertyName("protocol")]
        public string? Protocol { get; set; }

        /// <summary>
        ///     Status atual do pedido no momento do evento.
        ///     Use <see cref="Berdsk.Sdk.SuperFrete.Helpers.SfOrderStatus" /> para comparação.
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        /// <summary>
        ///     Código de rastreamento junto à transportadora. Disponível a partir do evento <c>order.generated</c>.
        /// </summary>
        [JsonPropertyName("tracking")]
        public string? Tracking { get; set; }

        /// <summary>
        ///     Código de rastreamento próprio, quando disponível.
        /// </summary>
        [JsonPropertyName("self_tracking")]
        public string? SelfTracking { get; set; }

        /// <summary>
        ///     Identificador do usuário proprietário do pedido na SuperFrete.
        /// </summary>
        [JsonPropertyName("user_id")]
        public string? UserId { get; set; }

        /// <summary>
        ///     Tags do pedido indexadas por posição (chave numérica em string: "0", "1", ...).
        ///     Permitem associar identificadores externos ao pedido, como o número do pedido da loja.
        /// </summary>
        [JsonPropertyName("tags")]
        public Dictionary<string, SfWebhookTag>? Tags { get; set; }

        /// <summary>
        ///     Data e hora de criação do pedido (ISO 8601).
        /// </summary>
        [JsonPropertyName("created_at")]
        public string? CreatedAt { get; set; }

        /// <summary>
        ///     Data e hora em que o pagamento foi confirmado (ISO 8601). <c>null</c> se ainda não pago.
        /// </summary>
        [JsonPropertyName("paid_at")]
        public string? PaidAt { get; set; }

        /// <summary>
        ///     Data e hora em que a etiqueta foi gerada (ISO 8601). <c>null</c> antes do evento <c>order.generated</c>.
        /// </summary>
        [JsonPropertyName("generated_at")]
        public string? GeneratedAt { get; set; }

        /// <summary>
        ///     Data e hora em que o objeto foi postado na transportadora (ISO 8601). <c>null</c> antes de <c>order.posted</c>.
        /// </summary>
        [JsonPropertyName("posted_at")]
        public string? PostedAt { get; set; }

        /// <summary>
        ///     Data e hora em que o objeto foi entregue ao destinatário (ISO 8601). <c>null</c> antes de <c>order.delivered</c>.
        /// </summary>
        [JsonPropertyName("delivered_at")]
        public string? DeliveredAt { get; set; }

        /// <summary>
        ///     Data e hora em que o pedido foi cancelado (ISO 8601). <c>null</c> se não cancelado.
        /// </summary>
        [JsonPropertyName("canceled_at")]
        public string? CanceledAt { get; set; }

        /// <summary>
        ///     Data e hora em que a etiqueta expirou (ISO 8601). <c>null</c> se não expirada.
        /// </summary>
        [JsonPropertyName("expired_at")]
        public string? ExpiredAt { get; set; }

        /// <summary>
        ///     URL de rastreamento do objeto no portal da SuperFrete. <c>null</c> quando indisponível.
        /// </summary>
        [JsonPropertyName("tracking_url")]
        public string? TrackingUrl { get; set; }
    }
}
