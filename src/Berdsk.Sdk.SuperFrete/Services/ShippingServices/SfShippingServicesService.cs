using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Berdsk.Sdk.SuperFrete.Services.ShippingServices.Dtos;

namespace Berdsk.Sdk.SuperFrete.Services.ShippingServices
{
    /// <summary>
    ///     Implementação do serviço de informações técnicas dos serviços de entrega da SuperFrete.
    /// </summary>
    public class SfShippingServicesService : SfBaseService, ISfShippingServicesService
    {
        /// <summary>
        ///     Inicializa uma nova instância de <see cref="SfShippingServicesService" />.
        /// </summary>
        /// <param name="httpClient">Instância do <see cref="HttpClient" /> configurada pelo <c>SuperFreteClient</c>.</param>
        public SfShippingServicesService(HttpClient httpClient) : base(httpClient)
        {
        }

        /// <inheritdoc />
        public async Task<Dictionary<string, SfServiceInfoResponse>?> GetServicesInfoAsync()
        {
            return await GetAsync<Dictionary<string, SfServiceInfoResponse>>(SfEndpoints.ShippingServices.Info);
        }
    }
}
