using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Calculator.Dtos
{
    /// <summary>
    ///     Representa a origem do cálculo de frete.
    ///     Aceita CEP nos formatos XXXXX-XXX ou XXXXXXXX.
    /// </summary>
    public class SfCalculationOriginRequest
    {
        /// <summary>
        ///     CEP de origem da mercadoria.
        ///     Aceita os formatos XXXXX-XXX ou XXXXXXXX.
        /// </summary>
        [JsonPropertyName("postal_code")]
        public string PostalCode { get; set; } = default!;
    }
}
