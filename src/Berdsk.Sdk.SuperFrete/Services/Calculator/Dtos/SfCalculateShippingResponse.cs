using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Calculator.Dtos
{
    /// <summary>
    ///     Resultado do cálculo de frete para um serviço de entrega específico.
    ///     A API retorna uma lista contendo um item por serviço consultado.
    /// </summary>
    public class SfCalculateShippingResponse
    {
        /// <summary>
        ///     Código do serviço de entrega (ex: 1 = PAC, 2 = SEDEX).
        ///     Corresponde ao valor inteiro de <see cref="Berdsk.Sdk.SuperFrete.Helpers.SfShippingServiceType" />.
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        ///     Nome do serviço de entrega (ex: "PAC", "SEDEX").
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        ///     Preço final do frete com desconto SuperFrete aplicado, em reais.
        /// </summary>
        [JsonPropertyName("price")]
        public float Price { get; set; }

        /// <summary>
        ///     Valor total do desconto aplicado pela SuperFrete sobre o preço de tabela.
        /// </summary>
        [JsonPropertyName("discount")]
        public string? Discount { get; set; }

        /// <summary>
        ///     Moeda utilizada no cálculo. Sempre <c>"R$"</c> para a API da SuperFrete.
        /// </summary>
        [JsonPropertyName("currency")]
        public string? Currency { get; set; }

        /// <summary>
        ///     Prazo de entrega estimado em dias úteis.
        /// </summary>
        [JsonPropertyName("delivery_time")]
        public int DeliveryTime { get; set; }

        /// <summary>
        ///     Faixa de prazo de entrega estimado, com valores mínimo e máximo em dias úteis.
        /// </summary>
        [JsonPropertyName("delivery_range")]
        public SfCalculationDeliveryRangeResponse? DeliveryRange { get; set; }

        /// <summary>
        ///     Lista de pacotes que compõem o envio, com dimensões e preços individuais.
        /// </summary>
        [JsonPropertyName("packages")]
        public SfCalculationPackageResultResponse[]? Packages { get; set; }

        /// <summary>
        ///     Serviços adicionais (Mão Própria, Aviso de Recebimento) contratados neste cálculo.
        /// </summary>
        [JsonPropertyName("additional_services")]
        public SfCalculationAdditionalServicesResponse? AdditionalServices { get; set; }

        /// <summary>
        ///     Dados da transportadora responsável por este serviço de entrega.
        /// </summary>
        [JsonPropertyName("company")]
        public SfCalculationCompanyResponse? Company { get; set; }

        /// <summary>
        ///     Indica se houve erro ao calcular o frete para este serviço específico.
        ///     Quando <c>true</c>, os demais campos podem estar incompletos ou ausentes.
        /// </summary>
        [JsonPropertyName("has_error")]
        public bool HasError { get; set; }
    }
}
