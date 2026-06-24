using System.Text.Json.Serialization;
using Berdsk.Sdk.SuperFrete.Helpers;

namespace Berdsk.Sdk.SuperFrete.Services.Orders.Dtos
{
    /// <summary>
    ///     Informações detalhadas de uma etiqueta de frete retornadas pela API SuperFrete.
    /// </summary>
    public class SfOrderInfoResponse
    {
        /// <summary>
        ///     Identificador único da etiqueta.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = default!;

        /// <summary>
        ///     Protocolo interno da etiqueta (geralmente igual ao ID).
        /// </summary>
        [JsonPropertyName("protocol")]
        public string? Protocol { get; set; }

        /// <summary>
        ///     Formato da embalagem (ex: "box").
        /// </summary>
        [JsonPropertyName("format")]
        public string? Format { get; set; }

        /// <summary>
        ///     Prazo de entrega em dias úteis.
        /// </summary>
        [JsonPropertyName("delivery")]
        public int Delivery { get; set; }

        /// <summary>
        ///     Prazo mínimo de entrega em dias úteis.
        /// </summary>
        [JsonPropertyName("delivery_min")]
        public int DeliveryMin { get; set; }

        /// <summary>
        ///     Prazo máximo de entrega em dias úteis.
        /// </summary>
        [JsonPropertyName("delivery_max")]
        public int DeliveryMax { get; set; }

        /// <summary>
        ///     Valor do desconto aplicado ao frete em reais.
        /// </summary>
        [JsonPropertyName("discount")]
        public float Discount { get; set; }

        /// <summary>
        ///     Altura da embalagem em centímetros.
        /// </summary>
        [JsonPropertyName("height")]
        public float Height { get; set; }

        /// <summary>
        ///     Largura da embalagem em centímetros.
        /// </summary>
        [JsonPropertyName("width")]
        public float Width { get; set; }

        /// <summary>
        ///     Comprimento da embalagem em centímetros.
        /// </summary>
        [JsonPropertyName("length")]
        public float Length { get; set; }

        /// <summary>
        ///     Peso da embalagem em quilogramas.
        /// </summary>
        [JsonPropertyName("weight")]
        public float Weight { get; set; }

        /// <summary>
        ///     Dados do remetente da encomenda.
        /// </summary>
        [JsonPropertyName("from")]
        public SfOrderContactResponse? From { get; set; }

        /// <summary>
        ///     Dados do destinatário da encomenda.
        /// </summary>
        [JsonPropertyName("to")]
        public SfOrderContactResponse? To { get; set; }

        /// <summary>
        ///     Número da nota fiscal.
        ///     <c>null</c> quando a encomenda foi emitida como declaração de conteúdo.
        /// </summary>
        [JsonPropertyName("invoice")]
        public string? Invoice { get; set; }

        /// <summary>
        ///     Indica se o serviço de Mão Própria foi contratado.
        /// </summary>
        [JsonPropertyName("own_hand")]
        public bool OwnHand { get; set; }

        /// <summary>
        ///     Indica se o serviço de Aviso de Recebimento (AR) foi contratado.
        /// </summary>
        [JsonPropertyName("receipt")]
        public bool Receipt { get; set; }

        /// <summary>
        ///     Valor total do frete em reais.
        /// </summary>
        [JsonPropertyName("price")]
        public float Price { get; set; }

        /// <summary>
        ///     Código de rastreio da encomenda junto à transportadora.
        ///     Disponível a partir do status <see cref="SfOrderStatus.Released" />.
        /// </summary>
        [JsonPropertyName("tracking")]
        public string? Tracking { get; set; }

        /// <summary>
        ///     Status atual da etiqueta.
        ///     Use as constantes de <see cref="SfOrderStatus" /> para comparar o valor.
        /// </summary>
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        /// <summary>
        ///     Código numérico do serviço de frete utilizado.
        ///     Use <see cref="SfShippingServiceType" /> para interpretar o valor.
        /// </summary>
        [JsonPropertyName("service_id")]
        public int ServiceId { get; set; }

        /// <summary>
        ///     Lista de produtos declarados no conteúdo da encomenda.
        /// </summary>
        [JsonPropertyName("products")]
        public SfOrderProductResponse[]? Products { get; set; }

        /// <summary>
        ///     Valor do seguro contratado em reais.
        ///     <c>null</c> quando nenhum seguro adicional foi contratado.
        /// </summary>
        [JsonPropertyName("insurance_value")]
        public float? InsuranceValue { get; set; }

        /// <summary>
        ///     Data e hora de geração da etiqueta pela transportadora (ISO 8601 UTC).
        /// </summary>
        [JsonPropertyName("generated_at")]
        public string? GeneratedAt { get; set; }

        /// <summary>
        ///     Data e hora de postagem na transportadora (ISO 8601 UTC).
        ///     <c>null</c> até a encomenda ser postada.
        /// </summary>
        [JsonPropertyName("posted_at")]
        public string? PostedAt { get; set; }

        /// <summary>
        ///     Data e hora de criação da etiqueta (ISO 8601 UTC).
        /// </summary>
        [JsonPropertyName("created_at")]
        public string? CreatedAt { get; set; }

        /// <summary>
        ///     Data e hora da última atualização da etiqueta (ISO 8601 UTC).
        /// </summary>
        [JsonPropertyName("updated_at")]
        public string? UpdatedAt { get; set; }

        /// <summary>
        ///     Dados para impressão da etiqueta em PDF.
        /// </summary>
        [JsonPropertyName("print")]
        public SfOrderPrintResponse? Print { get; set; }

        /// <summary>
        ///     Tags de rastreio que referenciam o pedido original na plataforma do lojista.
        /// </summary>
        [JsonPropertyName("tags")]
        public SfOrderTagResponse[]? Tags { get; set; }
    }
}
