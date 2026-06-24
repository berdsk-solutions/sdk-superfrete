using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Calculator.Dtos
{
    /// <summary>
    ///     Detalhes de um pacote individual retornados pelo cálculo de frete.
    /// </summary>
    public class SfCalculationPackageResultResponse
    {
        /// <summary>
        ///     Preço do frete para este pacote (já com desconto SuperFrete aplicado).
        /// </summary>
        [JsonPropertyName("price")]
        public float Price { get; set; }

        /// <summary>
        ///     Valor do desconto aplicado pela SuperFrete neste pacote.
        /// </summary>
        [JsonPropertyName("discount")]
        public string? Discount { get; set; }

        /// <summary>
        ///     Formato do pacote retornado pela transportadora (ex: <c>"box"</c>, <c>"package"</c>).
        /// </summary>
        [JsonPropertyName("format")]
        public string? Format { get; set; }

        /// <summary>
        ///     Dimensões do pacote calculadas pela transportadora.
        /// </summary>
        [JsonPropertyName("dimensions")]
        public SfCalculationPackageDimensionsResponse? Dimensions { get; set; }

        /// <summary>
        ///     Peso do pacote em quilogramas (retornado como string pela API).
        /// </summary>
        [JsonPropertyName("weight")]
        public string? Weight { get; set; }

        /// <summary>
        ///     Valor do seguro aplicado a este pacote.
        /// </summary>
        [JsonPropertyName("insurance_value")]
        public float InsuranceValue { get; set; }
    }
}
