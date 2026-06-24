using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Orders.Dtos
{
    /// <summary>
    ///     Resposta paginada retornada pela API ao listar etiquetas da conta.
    /// </summary>
    public class SfListOrdersResponse
    {
        /// <summary>
        ///     Lista de etiquetas retornadas na página atual.
        /// </summary>
        [JsonPropertyName("data")]
        public SfOrderInfoResponse[]? Data { get; set; }

        /// <summary>
        ///     Número total de etiquetas disponíveis (em todas as páginas).
        /// </summary>
        [JsonPropertyName("total")]
        public int? Total { get; set; }

        /// <summary>
        ///     Quantidade de resultados por página conforme definido na requisição.
        /// </summary>
        [JsonPropertyName("per_page")]
        public int? PerPage { get; set; }

        /// <summary>
        ///     Número da página atual retornada.
        /// </summary>
        [JsonPropertyName("current_page")]
        public int? CurrentPage { get; set; }

        /// <summary>
        ///     Número da última página disponível para a consulta realizada.
        /// </summary>
        [JsonPropertyName("last_page")]
        public int? LastPage { get; set; }
    }
}
