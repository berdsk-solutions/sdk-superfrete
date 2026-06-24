using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Checkout.Dtos
{
    /// <summary>
    ///     Informações de impressão de uma etiqueta retornada pelo checkout.
    /// </summary>
    public class SfCheckoutPrintResponse
    {
        /// <summary>
        ///     URL do PDF da etiqueta para impressão.
        ///     Disponível após a geração da etiqueta pela transportadora.
        /// </summary>
        [JsonPropertyName("url")]
        public string? Url { get; set; }
    }
}
