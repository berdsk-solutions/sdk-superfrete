using System.Text.Json.Serialization;
using Berdsk.Sdk.SuperFrete.Helpers;

namespace Berdsk.Sdk.SuperFrete.Services.Cart.Dtos
{
    /// <summary>
    ///     Dados completos para criação de uma etiqueta de frete na SuperFrete.
    ///     Após a criação bem-sucedida, a etiqueta fica com status
    ///     <see cref="SfOrderStatus.Pending" /> aguardando pagamento.
    /// </summary>
    public class SfAddToCartRequest
    {
        /// <summary>
        ///     Dados do remetente da encomenda (obrigatório).
        /// </summary>
        [JsonPropertyName("from")]
        public SfCartSenderRequest From { get; set; } = default!;

        /// <summary>
        ///     Dados do destinatário da encomenda (obrigatório).
        /// </summary>
        [JsonPropertyName("to")]
        public SfCartRecipientRequest To { get; set; } = default!;

        /// <summary>
        ///     Código numérico do serviço de frete (obrigatório).
        ///     Use os valores de <see cref="SfShippingServiceType" />:
        ///     PAC=1, SEDEX=2, Jadlog=3, MiniEnvios=17, Loggi=31.
        /// </summary>
        [JsonPropertyName("service")]
        public int Service { get; set; }

        /// <summary>
        ///     Lista de produtos declarados no conteúdo da encomenda.
        ///     Utilizado quando <see cref="SfCartOptionsRequest.NonCommercial" /> for <c>true</c>.
        /// </summary>
        [JsonPropertyName("products")]
        public SfCartProductRequest[]? Products { get; set; }

        /// <summary>
        ///     Dimensões físicas do pacote (obrigatório).
        ///     Recomenda-se utilizar os valores retornados pela API de cotação de frete.
        /// </summary>
        [JsonPropertyName("volumes")]
        public SfCartVolumeRequest Volumes { get; set; } = default!;

        /// <summary>
        ///     Opções adicionais do frete, como seguro, Aviso de Recebimento, Mão Própria e tags.
        /// </summary>
        [JsonPropertyName("options")]
        public SfCartOptionsRequest? Options { get; set; }

        /// <summary>
        ///     Nome da plataforma ou integração que está gerando a etiqueta (obrigatório).
        ///     Exemplo: "MinhaLoja", "MeuERP".
        /// </summary>
        [JsonPropertyName("platform")]
        public string Platform { get; set; } = default!;
    }
}
