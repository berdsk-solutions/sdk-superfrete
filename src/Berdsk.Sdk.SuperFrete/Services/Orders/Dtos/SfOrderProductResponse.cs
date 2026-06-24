using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Orders.Dtos
{
    /// <summary>
    ///     Produto declarado no conteúdo da encomenda, conforme retornado pela API.
    /// </summary>
    /// <remarks>
    ///     Atenção: os campos <see cref="Quantity" /> e <see cref="UnitaryValue" /> são
    ///     retornados como strings pela API SuperFrete.
    /// </remarks>
    public class SfOrderProductResponse
    {
        /// <summary>
        ///     Nome do produto declarado.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        ///     Quantidade de unidades do produto (retornada como string pela API).
        /// </summary>
        [JsonPropertyName("quantity")]
        public string? Quantity { get; set; }

        /// <summary>
        ///     Valor unitário do produto em reais (retornado como string pela API).
        /// </summary>
        [JsonPropertyName("unitary_value")]
        public string? UnitaryValue { get; set; }
    }
}
