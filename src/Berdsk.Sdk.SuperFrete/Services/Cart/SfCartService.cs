using System.Net.Http;
using System.Threading.Tasks;
using Berdsk.Sdk.SuperFrete.Services.Cart.Dtos;

namespace Berdsk.Sdk.SuperFrete.Services.Cart
{
    /// <summary>
    ///     Implementação do serviço de carrinho de fretes da SuperFrete.
    ///     Cria etiquetas de frete com status "pending" aguardando pagamento via checkout.
    /// </summary>
    public class SfCartService : SfBaseService, ISfCartService
    {
        /// <summary>
        ///     Inicializa uma nova instância de <see cref="SfCartService" />.
        /// </summary>
        /// <param name="httpClient">
        ///     Instância do <see cref="HttpClient" /> configurada pelo <c>SuperFreteClient</c>
        ///     com autenticação, URL base e headers padrão.
        /// </param>
        public SfCartService(HttpClient httpClient) : base(httpClient)
        {
        }

        /// <inheritdoc />
        public async Task<SfAddToCartResponse?> AddToCartAsync(SfAddToCartRequest request)
        {
            return await PostAsync<SfAddToCartResponse, SfAddToCartRequest>(SfEndpoints.Cart.Add, request);
        }
    }
}
