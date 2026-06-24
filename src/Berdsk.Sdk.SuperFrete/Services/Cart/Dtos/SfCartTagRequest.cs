using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Cart.Dtos
{
    /// <summary>
    ///     Tag de identificação do pedido na plataforma de origem.
    ///     Permite rastrear a etiqueta de volta ao pedido original na plataforma do lojista.
    /// </summary>
    public class SfCartTagRequest
    {
        /// <summary>
        ///     Identificador único do pedido na plataforma de origem (ex: número do pedido).
        /// </summary>
        [JsonPropertyName("tag")]
        public string? Tag { get; set; }

        /// <summary>
        ///     URL da plataforma de origem associada ao pedido (ex: link do pedido na loja).
        /// </summary>
        [JsonPropertyName("url")]
        public string? Url { get; set; }
    }
}
