using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Berdsk.Sdk.SuperFrete.Converters;

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
        ///     Tags do pedido. Permitem associar identificadores externos ao pedido, como o número do pedido da loja.
        ///     A API envia objeto indexado por posição quando há tags e array vazio quando não há —
        ///     ambos os formatos são normalizados para esta lista, que nunca é <c>null</c>.
        /// </summary>
        [JsonPropertyName("tags")]
        [JsonConverter(typeof(SfWebhookTagsConverter))]
        public List<SfWebhookTag> Tags { get; set; } = new List<SfWebhookTag>();

        /// <summary>
        ///     Data e hora de criação do pedido em UTC.
        ///     Aceita string ISO 8601 ou objeto Firestore Timestamp — convertidos automaticamente.
        /// </summary>
        [JsonPropertyName("created_at")]
        [JsonConverter(typeof(SfDateTimeConverter))]
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        ///     Data e hora em que o pagamento foi confirmado, em UTC. <c>null</c> se ainda não pago.
        /// </summary>
        [JsonPropertyName("paid_at")]
        [JsonConverter(typeof(SfDateTimeConverter))]
        public DateTime? PaidAt { get; set; }

        /// <summary>
        ///     Data e hora em que a etiqueta foi gerada, em UTC. <c>null</c> antes do evento <c>order.generated</c>.
        /// </summary>
        [JsonPropertyName("generated_at")]
        [JsonConverter(typeof(SfDateTimeConverter))]
        public DateTime? GeneratedAt { get; set; }

        /// <summary>
        ///     Data e hora em que o objeto foi postado na transportadora, em UTC. <c>null</c> antes de <c>order.posted</c>.
        /// </summary>
        [JsonPropertyName("posted_at")]
        [JsonConverter(typeof(SfDateTimeConverter))]
        public DateTime? PostedAt { get; set; }

        /// <summary>
        ///     Data e hora em que o objeto foi entregue ao destinatário, em UTC. <c>null</c> antes de <c>order.delivered</c>.
        /// </summary>
        [JsonPropertyName("delivered_at")]
        [JsonConverter(typeof(SfDateTimeConverter))]
        public DateTime? DeliveredAt { get; set; }

        /// <summary>
        ///     Data e hora em que o pedido foi cancelado, em UTC. <c>null</c> se não cancelado.
        /// </summary>
        [JsonPropertyName("canceled_at")]
        [JsonConverter(typeof(SfDateTimeConverter))]
        public DateTime? CanceledAt { get; set; }

        /// <summary>
        ///     Data e hora em que a etiqueta expirou, em UTC. <c>null</c> se não expirada.
        /// </summary>
        [JsonPropertyName("expired_at")]
        [JsonConverter(typeof(SfDateTimeConverter))]
        public DateTime? ExpiredAt { get; set; }

        /// <summary>
        ///     URL de rastreamento do objeto no portal da SuperFrete. <c>null</c> quando indisponível.
        /// </summary>
        [JsonPropertyName("tracking_url")]
        public string? TrackingUrl { get; set; }
    }
}
