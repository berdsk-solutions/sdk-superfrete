using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Cart.Dtos
{
    /// <summary>
    ///     Dados da nota fiscal da encomenda.
    ///     Use quando <see cref="SfCartOptionsRequest.NonCommercial" /> for <c>false</c> ou <c>null</c>
    ///     (envio com nota fiscal em vez de declaração de conteúdo).
    /// </summary>
    public class SfCartInvoiceRequest
    {
        /// <summary>
        ///     Número da nota fiscal com exatamente 44 dígitos.
        ///     Envie uma string vazia caso não possua nota fiscal.
        /// </summary>
        [JsonPropertyName("number")]
        public string Number { get; set; } = default!;

        /// <summary>
        ///     Chave de acesso ou identificador da nota fiscal (opcional).
        /// </summary>
        [JsonPropertyName("key")]
        public string? Key { get; set; }
    }
}
