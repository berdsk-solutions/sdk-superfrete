using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Berdsk.Sdk.SuperFrete.Services.Calculator.Dtos;

namespace Berdsk.Sdk.SuperFrete.Services.Calculator
{
    /// <summary>
    ///     Implementação do serviço de cotação de fretes da SuperFrete.
    /// </summary>
    public class SfCalculatorService : SfBaseService, ISfCalculatorService
    {
        /// <summary>
        ///     Inicializa uma nova instância de <see cref="SfCalculatorService" />.
        /// </summary>
        /// <param name="httpClient">Instância do <see cref="HttpClient" /> configurada pelo <c>SuperFreteClient</c>.</param>
        public SfCalculatorService(HttpClient httpClient) : base(httpClient)
        {
        }

        /// <inheritdoc />
        public async Task<List<SfCalculateShippingResponse>?> CalculateShippingAsync(SfCalculateShippingRequest request)
        {
            return await PostAsync<List<SfCalculateShippingResponse>, SfCalculateShippingRequest>(
                SfEndpoints.Calculator.Calculate, request);
        }
    }
}
