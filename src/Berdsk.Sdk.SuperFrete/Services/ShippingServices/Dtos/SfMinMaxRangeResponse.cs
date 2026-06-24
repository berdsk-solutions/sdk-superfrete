using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.ShippingServices.Dtos
{
    /// <summary>
    ///     Representa um intervalo de valores mínimo e máximo para uma restrição de serviço
    ///     (ex: limites de peso, dimensões ou valor de seguro).
    /// </summary>
    public class SfMinMaxRangeResponse
    {
        /// <summary>
        ///     Valor mínimo permitido.
        /// </summary>
        [JsonPropertyName("min")]
        public float Min { get; set; }

        /// <summary>
        ///     Valor máximo permitido.
        /// </summary>
        [JsonPropertyName("max")]
        public float Max { get; set; }
    }
}
