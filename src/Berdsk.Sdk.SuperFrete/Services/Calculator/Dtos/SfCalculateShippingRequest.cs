using System.Text.Json.Serialization;
using Berdsk.Sdk.SuperFrete.Helpers;

namespace Berdsk.Sdk.SuperFrete.Services.Calculator.Dtos
{
    /// <summary>
    ///     Dados necessários para solicitar o cálculo de frete na SuperFrete.
    /// </summary>
    /// <remarks>
    ///     Forneça as dimensões da encomenda via <see cref="Package" /> ou <see cref="Products" />, nunca ambos.
    ///     Ao usar <see cref="Products" />, a API calcula a caixa ideal — utilize as dimensões retornadas
    ///     ao gerar a etiqueta de postagem.
    /// </remarks>
    public class SfCalculateShippingRequest
    {
        /// <summary>
        ///     CEP de origem da mercadoria.
        /// </summary>
        [JsonPropertyName("from")]
        public SfCalculationOriginRequest From { get; set; } = default!;

        /// <summary>
        ///     CEP de destino da entrega.
        /// </summary>
        [JsonPropertyName("to")]
        public SfCalculationDestinationRequest To { get; set; } = default!;

        /// <summary>
        ///     Serviços de entrega a cotar. Serializado como string separada por vírgulas (ex: "1,2,17").
        ///     Quando <c>null</c>, a API retorna cotações de todos os serviços disponíveis.
        /// </summary>
        [JsonPropertyName("services")]
        [JsonConverter(typeof(SfShippingServicesJsonConverter))]
        public SfShippingServiceType[]? Services { get; set; }

        /// <summary>
        ///     Opções adicionais do cálculo: mão própria, aviso de recebimento e seguro.
        /// </summary>
        [JsonPropertyName("options")]
        public SfCalculationOptionsRequest? Options { get; set; }

        /// <summary>
        ///     Dimensões e peso da embalagem.
        ///     Use este campo quando a caixa de envio já é conhecida.
        ///     Não utilize em conjunto com <see cref="Products" />.
        /// </summary>
        [JsonPropertyName("package")]
        public SfCalculationPackageRequest? Package { get; set; }

        /// <summary>
        ///     Lista de produtos a serem enviados.
        ///     Quando fornecida, a API calcula automaticamente a caixa ideal.
        ///     Não utilize em conjunto com <see cref="Package" />.
        /// </summary>
        [JsonPropertyName("products")]
        public SfCalculationProductRequest[]? Products { get; set; }
    }
}
