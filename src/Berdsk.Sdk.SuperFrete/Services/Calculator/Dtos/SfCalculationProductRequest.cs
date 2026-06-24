using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Calculator.Dtos
{
    /// <summary>
    ///     Dimensões individuais de um produto para o cálculo de frete.
    ///     Ao fornecer uma lista de produtos, a API calcula automaticamente
    ///     a caixa ideal para acomodar todos os itens.
    /// </summary>
    public class SfCalculationProductRequest
    {
        /// <summary>
        ///     Quantidade de unidades deste produto.
        ///     O padrão é <c>1</c>.
        /// </summary>
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; } = 1;

        /// <summary>
        ///     Altura do produto em centímetros.
        /// </summary>
        [JsonPropertyName("height")]
        public float Height { get; set; }

        /// <summary>
        ///     Largura do produto em centímetros.
        /// </summary>
        [JsonPropertyName("width")]
        public float Width { get; set; }

        /// <summary>
        ///     Comprimento do produto em centímetros.
        /// </summary>
        [JsonPropertyName("length")]
        public float Length { get; set; }

        /// <summary>
        ///     Peso do produto em quilogramas.
        /// </summary>
        [JsonPropertyName("weight")]
        public float Weight { get; set; }
    }
}
