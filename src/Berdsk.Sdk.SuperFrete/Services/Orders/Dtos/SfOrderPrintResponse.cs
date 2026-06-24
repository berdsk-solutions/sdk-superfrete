using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Orders.Dtos
{
    /// <summary>
    ///     Informações de impressão da etiqueta retornadas nas informações de um pedido.
    /// </summary>
    public class SfOrderPrintResponse
    {
        /// <summary>
        ///     URL do PDF da etiqueta para impressão.
        ///     Disponível após a geração da etiqueta pela transportadora.
        /// </summary>
        [JsonPropertyName("url")]
        public string? Url { get; set; }
    }
}
