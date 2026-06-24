using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Orders.Dtos
{
    /// <summary>
    ///     Dados necessários para solicitar o cancelamento de uma etiqueta de frete.
    ///     Somente etiquetas que ainda não foram postadas podem ser canceladas.
    ///     O valor pago é estornado para a carteira SuperFrete.
    /// </summary>
    public class SfCancelOrderRequest
    {
        /// <summary>
        ///     Dados do pedido a ser cancelado, incluindo ID e motivo (obrigatório).
        /// </summary>
        [JsonPropertyName("order")]
        public SfCancelOrderItemRequest Order { get; set; } = default!;
    }
}
