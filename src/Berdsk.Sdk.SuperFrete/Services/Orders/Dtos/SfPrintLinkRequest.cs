using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Orders.Dtos
{
    /// <summary>
    ///     Dados necessários para obter o link de impressão PDF de uma ou mais etiquetas.
    /// </summary>
    public class SfPrintLinkRequest
    {
        /// <summary>
        ///     Identificadores das etiquetas a serem incluídas no PDF de impressão.
        /// </summary>
        [JsonPropertyName("orders")]
        public string[] Orders { get; set; } = default!;
    }
}
