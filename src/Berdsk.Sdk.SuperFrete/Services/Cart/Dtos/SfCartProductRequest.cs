using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Cart.Dtos
{
    /// <summary>
    ///     Produto a ser declarado no conteúdo da encomenda.
    ///     Utilizado quando a etiqueta é emitida como declaração de conteúdo
    ///     (em vez de nota fiscal).
    /// </summary>
    public class SfCartProductRequest
    {
        /// <summary>
        ///     Nome do produto declarado.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = default!;

        /// <summary>
        ///     Quantidade de unidades do produto.
        /// </summary>
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        /// <summary>
        ///     Valor unitário do produto em reais.
        /// </summary>
        [JsonPropertyName("unitary_value")]
        public decimal UnitaryValue { get; set; }
    }
}
