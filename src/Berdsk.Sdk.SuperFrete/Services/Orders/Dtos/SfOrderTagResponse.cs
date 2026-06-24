using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Orders.Dtos
{
    /// <summary>
    ///     Tag de rastreio associada à etiqueta, referenciando o pedido original na plataforma de origem.
    /// </summary>
    public class SfOrderTagResponse
    {
        /// <summary>
        ///     Identificador do pedido na plataforma de origem.
        /// </summary>
        [JsonPropertyName("tag")]
        public string? Tag { get; set; }

        /// <summary>
        ///     URL da plataforma de origem associada ao pedido.
        /// </summary>
        [JsonPropertyName("url")]
        public string? Url { get; set; }
    }
}
