using System.Collections.Generic;
using System.Threading.Tasks;
using Berdsk.Sdk.SuperFrete.Services.ShippingServices.Dtos;

namespace Berdsk.Sdk.SuperFrete.Services.ShippingServices
{
    /// <summary>
    ///     Define as operações do serviço de informações técnicas dos serviços de entrega da SuperFrete.
    /// </summary>
    public interface ISfShippingServicesService
    {
        /// <summary>
        ///     Retorna os detalhes técnicos de cada serviço de entrega disponível.
        ///     Inclui limites de dimensões, peso, valores de seguro e serviços adicionais.
        /// </summary>
        /// <returns>
        ///     Dicionário com informações de cada serviço,
        ///     onde a chave é o código do serviço (ex: <c>"1"</c> para PAC).
        /// </returns>
        Task<Dictionary<string, SfServiceInfoResponse>?> GetServicesInfoAsync();
    }
}
