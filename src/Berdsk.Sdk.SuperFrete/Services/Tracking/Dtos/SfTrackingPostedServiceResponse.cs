using System;
using System.Text.Json.Serialization;
using Berdsk.Sdk.SuperFrete.Converters;

namespace Berdsk.Sdk.SuperFrete.Services.Tracking.Dtos
{
    /// <summary>
    ///     Serviço de frete efetivamente postado, com valores conferidos na postagem.
    /// </summary>
    public class SfTrackingPostedServiceResponse
    {
        /// <summary>
        ///     Valor de bônus aplicado.
        /// </summary>
        [JsonPropertyName("bonus")]
        public decimal? Bonus { get; set; }

        /// <summary>
        ///     Código do serviço na transportadora (ex: <c>2002</c>, <c>FREIGHT_TYPE_ECONOMIC</c>).
        /// </summary>
        [JsonPropertyName("code")]
        public string? Code { get; set; }

        /// <summary>
        ///     Data/hora da postagem (UTC).
        /// </summary>
        [JsonPropertyName("date")]
        [JsonConverter(typeof(SfDateTimeConverter))]
        public DateTime? Date { get; set; }

        /// <summary>
        ///     Valor do seguro por valor declarado.
        /// </summary>
        [JsonPropertyName("declared_value_amount")]
        public decimal? DeclaredValueAmount { get; set; }

        /// <summary>
        ///     Valor do desconto aplicado.
        /// </summary>
        [JsonPropertyName("discount_amount")]
        public decimal? DiscountAmount { get; set; }

        /// <summary>
        ///     Valor real do desconto aplicado.
        /// </summary>
        [JsonPropertyName("real_discount_amount")]
        public decimal? RealDiscountAmount { get; set; }

        /// <summary>
        ///     Valor total do frete.
        /// </summary>
        [JsonPropertyName("total")]
        public decimal? Total { get; set; }

        /// <summary>
        ///     Valor total do frete com desconto aplicado.
        /// </summary>
        [JsonPropertyName("total_with_discount")]
        public decimal? TotalWithDiscount { get; set; }

        /// <summary>
        ///     Valor total do frete sem desconto.
        /// </summary>
        [JsonPropertyName("total_without_discount")]
        public decimal? TotalWithoutDiscount { get; set; }

        /// <summary>
        ///     Indica se o pacote foi postado em um ponto (PUDO) diferente do selecionado.
        /// </summary>
        [JsonPropertyName("wasPostedAtWrongPudo")]
        public bool? WasPostedAtWrongPudo { get; set; }

        /// <summary>
        ///     Indica se o pacote foi devolvido ao remetente.
        /// </summary>
        [JsonPropertyName("wasReturnedToSender")]
        public bool? WasReturnedToSender { get; set; }

        /// <summary>
        ///     Valor extra cobrado por postagem em ponto (PUDO) incorreto.
        /// </summary>
        [JsonPropertyName("wrongPudoExtraAmount")]
        public decimal? WrongPudoExtraAmount { get; set; }

        /// <summary>
        ///     Dimensões e peso do pacote conferidos na postagem.
        /// </summary>
        [JsonPropertyName("data")]
        public SfTrackingPackageDataResponse? Data { get; set; }
    }
}
