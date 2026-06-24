using System.Collections.Generic;
using System.Threading.Tasks;
using Berdsk.Sdk.SuperFrete.Services.Orders.Dtos;

namespace Berdsk.Sdk.SuperFrete.Services.Orders
{
    /// <summary>
    ///     Define as operações do serviço de pedidos e etiquetas da SuperFrete.
    /// </summary>
    public interface ISfOrderService
    {
        /// <summary>
        ///     Obtém informações detalhadas de uma etiqueta pelo ID.
        /// </summary>
        /// <param name="orderId">ID da etiqueta retornado pela API de carrinho.</param>
        /// <returns>Informações completas da etiqueta, ou <c>null</c> se não encontrada.</returns>
        Task<SfOrderInfoResponse?> GetOrderInfoAsync(string orderId);

        /// <summary>
        ///     Cancela uma etiqueta. Só é possível cancelar etiquetas não postadas.
        ///     O valor é estornado para a carteira SuperFrete.
        /// </summary>
        /// <param name="request">Dados do cancelamento (ID e motivo).</param>
        /// <returns>
        ///     Dicionário onde a chave é o ID da etiqueta e o valor indica se foi cancelado.
        /// </returns>
        Task<Dictionary<string, SfCancelOrderResultResponse>?> CancelOrderAsync(SfCancelOrderRequest request);

        /// <summary>
        ///     Obtém a URL do PDF de impressão para as etiquetas informadas.
        /// </summary>
        /// <param name="request">IDs das etiquetas para impressão.</param>
        /// <returns>URL do PDF gerado com as etiquetas solicitadas.</returns>
        Task<SfPrintLinkResponse?> GetPrintLinkAsync(SfPrintLinkRequest request);

        /// <summary>
        ///     Lista as etiquetas da conta com filtros e paginação.
        /// </summary>
        /// <param name="request">Filtros e parâmetros de paginação (todos opcionais).</param>
        /// <returns>Página de etiquetas correspondente aos filtros aplicados.</returns>
        Task<SfListOrdersResponse?> ListOrdersAsync(SfListOrdersRequest? request = null);
    }
}
