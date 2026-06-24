using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Orders.Dtos
{
    /// <summary>
    ///     Dados do pedido a ser cancelado.
    /// </summary>
    public class SfCancelOrderItemRequest
    {
        /// <summary>
        ///     Identificador único da etiqueta a ser cancelada (obrigatório).
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = default!;

        /// <summary>
        ///     Motivo do cancelamento da etiqueta (obrigatório).
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; } = default!;
    }
}
