using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Webhooks.Dtos
{
    /// <summary>
    ///     Dados do pedido/etiqueta contidos em uma notificação de webhook da SuperFrete.
    /// </summary>
    public class SfWebhookNotificationData
    {
        /// <summary>
        ///     Protocolo interno do pedido na plataforma SuperFrete.
        /// </summary>
        [JsonPropertyName("protocol")]
        public string? Protocol { get; set; }

        /// <summary>
        ///     Identificador único da etiqueta (UUID).
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        /// <summary>
        ///     Status atual do pedido no momento do evento.
        ///     Use <see cref="Berdsk.Sdk.SuperFrete.Helpers.SfOrderStatus" /> para comparação.
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        /// <summary>
        ///     Código de rastreamento da encomenda junto à transportadora.
        /// </summary>
        [JsonPropertyName("tracking")]
        public string? Tracking { get; set; }

        /// <summary>
        ///     Código de rastreamento próprio, quando disponível.
        /// </summary>
        [JsonPropertyName("self_tracking")]
        public string? SelfTracking { get; set; }

        /// <summary>
        ///     Identificador do usuário proprietário da etiqueta.
        /// </summary>
        [JsonPropertyName("user_id")]
        public string? UserId { get; set; }

        /// <summary>
        ///     Tags do pedido com identificadores e URLs das plataformas de origem.
        /// </summary>
        [JsonPropertyName("tags")]
        public SfWebhookNotificationTagData[]? Tags { get; set; }

        /// <summary>
        ///     Data e hora de criação da etiqueta (ISO 8601).
        /// </summary>
        [JsonPropertyName("created_at")]
        public string? CreatedAt { get; set; }

        /// <summary>
        ///     Data e hora em que o pagamento foi efetuado (ISO 8601).
        /// </summary>
        [JsonPropertyName("paid_at")]
        public string? PaidAt { get; set; }

        /// <summary>
        ///     Data e hora em que a etiqueta de frete foi gerada (ISO 8601).
        /// </summary>
        [JsonPropertyName("generated_at")]
        public string? GeneratedAt { get; set; }

        /// <summary>
        ///     Data e hora em que o objeto foi postado na transportadora (ISO 8601).
        /// </summary>
        [JsonPropertyName("posted_at")]
        public string? PostedAt { get; set; }

        /// <summary>
        ///     Data e hora em que o objeto foi entregue ao destinatário (ISO 8601).
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
        ///     URL de rastreamento do objeto no portal da SuperFrete.
        /// </summary>
        [JsonPropertyName("tracking_url")]
        public string? TrackingUrl { get; set; }
    }
}
