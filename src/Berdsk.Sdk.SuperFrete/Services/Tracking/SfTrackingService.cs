using System;
using System.Net.Http;
using System.Threading.Tasks;
using Berdsk.Sdk.SuperFrete.Services.Tracking.Dtos;

namespace Berdsk.Sdk.SuperFrete.Services.Tracking
{
    /// <summary>
    ///     Implementação do serviço de rastreamento público da SuperFrete.
    /// </summary>
    public class SfTrackingService : SfBaseService, ISfTrackingService
    {
        /// <summary>
        ///     Inicializa uma nova instância de <see cref="SfTrackingService" />.
        /// </summary>
        /// <param name="httpClient">
        ///     Instância do <see cref="HttpClient" /> configurada pelo <c>SuperFreteTrackingClient</c>
        ///     com a URL base da API de rastreamento.
        /// </param>
        public SfTrackingService(HttpClient httpClient) : base(httpClient)
        {
        }

        /// <inheritdoc />
        public async Task<SfTrackingResponse?> GetTrackingAsync(string trackingCode)
        {
            if (string.IsNullOrWhiteSpace(trackingCode))
                throw new ArgumentException("O código de rastreio é obrigatório.", nameof(trackingCode));

            var url = string.Format(SfEndpoints.Tracking.ByCode, Uri.EscapeDataString(trackingCode));
            return await GetAsync<SfTrackingResponse>(url);
        }
    }
}
