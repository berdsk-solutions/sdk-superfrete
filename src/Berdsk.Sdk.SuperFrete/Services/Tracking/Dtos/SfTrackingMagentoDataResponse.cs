using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Tracking.Dtos
{
    /// <summary>
    ///     Dados do pedido no sistema interno (Magento) da SuperFrete.
    /// </summary>
    public class SfTrackingMagentoDataResponse
    {
        /// <summary>
        ///     Número do pedido no Magento.
        /// </summary>
        [JsonPropertyName("order_number")]
        public string? OrderNumber { get; set; }
    }
}
