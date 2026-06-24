using System.Net.Http;
using System.Threading.Tasks;
using Berdsk.Sdk.SuperFrete.Services.Checkout.Dtos;

namespace Berdsk.Sdk.SuperFrete.Services.Checkout
{
    /// <summary>
    ///     Implementação do serviço de checkout da SuperFrete.
    ///     Realiza o pagamento de etiquetas utilizando o saldo disponível na conta.
    /// </summary>
    public class SfCheckoutService : SfBaseService, ISfCheckoutService
    {
        /// <summary>
        ///     Inicializa uma nova instância de <see cref="SfCheckoutService" />.
        /// </summary>
        /// <param name="httpClient">
        ///     Instância do <see cref="HttpClient" /> configurada pelo <c>SuperFreteClient</c>
        ///     com autenticação, URL base e headers padrão.
        /// </param>
        public SfCheckoutService(HttpClient httpClient) : base(httpClient)
        {
        }

        /// <inheritdoc />
        public async Task<SfCheckoutResponse?> FinalizeOrderAsync(SfCheckoutRequest request)
        {
            return await PostAsync<SfCheckoutResponse, SfCheckoutRequest>(SfEndpoints.Checkout.Finalize, request);
        }
    }
}
