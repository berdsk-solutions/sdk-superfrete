using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Cart.Dtos
{
    /// <summary>
    ///     Opções adicionais para a criação de uma etiqueta de frete.
    /// </summary>
    public class SfCartOptionsRequest
    {
        /// <summary>
        ///     Valor declarado para seguro da encomenda em reais (sem incluir o valor do frete).
        ///     Opcional; quando omitido, nenhum seguro adicional é contratado.
        /// </summary>
        [JsonPropertyName("insurance_value")]
        public float? InsuranceValue { get; set; }

        /// <summary>
        ///     Indica se o serviço de Aviso de Recebimento (AR) deve ser contratado.
        /// </summary>
        [JsonPropertyName("receipt")]
        public bool? Receipt { get; set; }

        /// <summary>
        ///     Indica se o serviço de Mão Própria deve ser contratado.
        /// </summary>
        [JsonPropertyName("own_hand")]
        public bool? OwnHand { get; set; }

        /// <summary>
        ///     Define o tipo de documento da encomenda.
        ///     <c>true</c> para declaração de conteúdo; <c>false</c> ou <c>null</c> para nota fiscal.
        ///     Jadlog e Loggi aceitam apenas declaração de conteúdo (<c>true</c>).
        /// </summary>
        [JsonPropertyName("non_commercial")]
        public bool? NonCommercial { get; set; }

        /// <summary>
        ///     Dados da nota fiscal da encomenda.
        ///     Obrigatório quando <see cref="NonCommercial" /> for <c>false</c> ou <c>null</c>.
        /// </summary>
        [JsonPropertyName("invoice")]
        public SfCartInvoiceRequest? Invoice { get; set; }

        /// <summary>
        ///     Tags de identificação do pedido na plataforma de origem.
        ///     Permite associar a etiqueta ao pedido original do lojista.
        /// </summary>
        [JsonPropertyName("tags")]
        public SfCartTagRequest[]? Tags { get; set; }
    }
}
