using Berdsk.Sdk.SuperFrete.Helpers;

namespace Berdsk.Sdk.SuperFrete.Services.Orders.Dtos
{
    /// <summary>
    ///     Parâmetros de filtragem e paginação para listagem de etiquetas.
    ///     Todos os campos são opcionais; omita para retornar a primeira página com os padrões da API.
    /// </summary>
    public class SfListOrdersRequest
    {
        /// <summary>
        ///     Filtra as etiquetas por status.
        ///     Use as constantes de <see cref="SfOrderStatus" /> para os valores aceitos
        ///     (ex: <see cref="SfOrderStatus.Pending" />, <see cref="SfOrderStatus.Released" />).
        /// </summary>
        public string? Status { get; set; }

        /// <summary>
        ///     Número da página a ser retornada (começando em 1).
        /// </summary>
        public int? Page { get; set; }

        /// <summary>
        ///     Quantidade de resultados por página.
        /// </summary>
        public int? PerPage { get; set; }

        /// <summary>
        ///     Direção da ordenação dos resultados.
        ///     Use <see cref="SfSortOrder.Ascending" /> ou <see cref="SfSortOrder.Descending" />.
        /// </summary>
        public string? Order { get; set; }

        /// <summary>
        ///     Campo pelo qual os resultados serão ordenados.
        ///     Use <see cref="SfSortBy.CreatedAt" /> ou <see cref="SfSortBy.UpdatedAt" />.
        /// </summary>
        public string? SortBy { get; set; }
    }
}
