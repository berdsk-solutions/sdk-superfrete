using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Berdsk.Sdk.SuperFrete.Services.Orders.Dtos;

namespace Berdsk.Sdk.SuperFrete.Services.Orders
{
    /// <summary>
    ///     Implementação do serviço de pedidos e etiquetas da SuperFrete.
    ///     Permite consultar, cancelar, listar e obter links de impressão de etiquetas.
    /// </summary>
    public class SfOrderService : SfBaseService, ISfOrderService
    {
        /// <summary>
        ///     Inicializa uma nova instância de <see cref="SfOrderService" />.
        /// </summary>
        /// <param name="httpClient">
        ///     Instância do <see cref="HttpClient" /> configurada pelo <c>SuperFreteClient</c>
        ///     com autenticação, URL base e headers padrão.
        /// </param>
        public SfOrderService(HttpClient httpClient) : base(httpClient)
        {
        }

        /// <inheritdoc />
        public async Task<SfOrderInfoResponse?> GetOrderInfoAsync(string orderId)
        {
            return await GetAsync<SfOrderInfoResponse>(string.Format(SfEndpoints.Orders.Info, orderId));
        }

        /// <inheritdoc />
        public async Task<Dictionary<string, SfCancelOrderResultResponse>?> CancelOrderAsync(
            SfCancelOrderRequest request)
        {
            return await PostAsync<Dictionary<string, SfCancelOrderResultResponse>, SfCancelOrderRequest>(
                SfEndpoints.Orders.Cancel, request);
        }

        /// <inheritdoc />
        public async Task<SfPrintLinkResponse?> GetPrintLinkAsync(SfPrintLinkRequest request)
        {
            return await PostAsync<SfPrintLinkResponse, SfPrintLinkRequest>(SfEndpoints.Orders.Print, request);
        }

        /// <inheritdoc />
        public async Task<SfListOrdersResponse?> ListOrdersAsync(SfListOrdersRequest? request = null)
        {
            var url = BuildListOrdersUrl(request);
            return await GetAsync<SfListOrdersResponse>(url);
        }

        /// <summary>
        ///     Constrói a URL de listagem de pedidos com os query parameters baseados nos filtros fornecidos.
        /// </summary>
        /// <param name="request">Filtros opcionais de paginação e ordenação.</param>
        /// <returns>URL completa com query string, ou somente o endpoint base se não houver filtros.</returns>
        private static string BuildListOrdersUrl(SfListOrdersRequest? request)
        {
            if (request == null) return SfEndpoints.Orders.List;

            var queryParams = new List<string>();

            if (!string.IsNullOrEmpty(request.Status))
                queryParams.Add($"status={Uri.EscapeDataString(request.Status)}");
            if (request.Page.HasValue)
                queryParams.Add($"page={request.Page.Value}");
            if (request.PerPage.HasValue)
                queryParams.Add($"per_page={request.PerPage.Value}");
            if (!string.IsNullOrEmpty(request.Order))
                queryParams.Add($"order={Uri.EscapeDataString(request.Order)}");
            if (!string.IsNullOrEmpty(request.SortBy))
                queryParams.Add($"sort_by={Uri.EscapeDataString(request.SortBy)}");

            return queryParams.Count == 0
                ? SfEndpoints.Orders.List
                : $"{SfEndpoints.Orders.List}?{string.Join("&", queryParams)}";
        }
    }
}
