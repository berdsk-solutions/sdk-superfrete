using System.Threading.Tasks;
using Berdsk.Sdk.SuperFrete.Services.Cart.Dtos;
using Berdsk.Sdk.SuperFrete.Services.Checkout;

namespace Berdsk.Sdk.SuperFrete.Services.Cart
{
    /// <summary>
    ///     Define as operações do serviço de carrinho de fretes da SuperFrete.
    /// </summary>
    public interface ISfCartService
    {
        /// <summary>
        ///     Envia os detalhes de um frete para a SuperFrete, criando uma etiqueta com status "pending".
        /// </summary>
        /// <remarks>
        ///     Após criar a etiqueta, realize o pagamento via
        ///     <see cref="ISfCheckoutService.FinalizeOrderAsync" />
        ///     ou diretamente no painel SuperFrete.
        /// </remarks>
        /// <param name="request">Dados completos do frete a ser criado.</param>
        /// <returns>Dados da etiqueta criada, incluindo o ID para pagamento.</returns>
        Task<SfAddToCartResponse?> AddToCartAsync(SfAddToCartRequest request);
    }
}
