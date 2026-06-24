using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Calculator.Dtos
{
    /// <summary>
    ///     Faixa de prazo de entrega estimado pelo serviço de frete.
    /// </summary>
    public class SfCalculationDeliveryRangeResponse
    {
        /// <summary>
        ///     Prazo mínimo de entrega em dias úteis.
        /// </summary>
        [JsonPropertyName("min")]
        public int Min { get; set; }

        /// <summary>
        ///     Prazo máximo de entrega em dias úteis.
        /// </summary>
        [JsonPropertyName("max")]
        public int Max { get; set; }
    }
}
