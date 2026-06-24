using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Calculator.Dtos
{
    /// <summary>
    ///     Representa o destino do cálculo de frete.
    ///     Aceita CEP nos formatos XXXXX-XXX ou XXXXXXXX.
    /// </summary>
    public class SfCalculationDestinationRequest
    {
        /// <summary>
        ///     CEP de destino da entrega.
        ///     Aceita os formatos XXXXX-XXX ou XXXXXXXX.
        /// </summary>
        [JsonPropertyName("postal_code")]
        public string PostalCode { get; set; } = default!;
    }
}
