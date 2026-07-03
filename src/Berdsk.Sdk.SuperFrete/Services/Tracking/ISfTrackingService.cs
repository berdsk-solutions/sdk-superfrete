using System.Threading.Tasks;
using Berdsk.Sdk.SuperFrete.Services.Tracking.Dtos;

namespace Berdsk.Sdk.SuperFrete.Services.Tracking
{
    /// <summary>
    ///     Serviço de rastreamento público da SuperFrete.
    ///     Consulta o rastreamento completo de um envio pelo código de rastreio, sem necessidade de autenticação.
    /// </summary>
    public interface ISfTrackingService
    {
        /// <summary>
        ///     Consulta o rastreamento completo de um envio pelo código de rastreio.
        /// </summary>
        /// <param name="trackingCode">Código de rastreio do envio (etiqueta).</param>
        /// <returns>
        ///     Rastreamento completo do envio: dados do pedido, eventos da transportadora,
        ///     comprovantes de entrega e histórico de chamados; ou <c>null</c> se a resposta não tiver corpo.
        /// </returns>
        /// <exception cref="SfNotFoundException">Lançada quando o código de rastreio não é encontrado.</exception>
        /// <exception cref="SuperFreteException">Lançada para os demais erros retornados pela API.</exception>
        Task<SfTrackingResponse?> GetTrackingAsync(string trackingCode);
    }
}
