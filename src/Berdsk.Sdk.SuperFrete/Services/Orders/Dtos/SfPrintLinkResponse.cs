using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Orders.Dtos
{
    /// <summary>
    ///     Resposta retornada pela API com o link para impressão das etiquetas em PDF.
    /// </summary>
    public class SfPrintLinkResponse
    {
        /// <summary>
        ///     URL do PDF contendo as etiquetas solicitadas para impressão.
        /// </summary>
        [JsonPropertyName("url")]
        public string? Url { get; set; }
    }
}
