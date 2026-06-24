using System.Threading.Tasks;
using Berdsk.Sdk.SuperFrete.Services.Checkout.Dtos;

namespace Berdsk.Sdk.SuperFrete.Services.Checkout
{
    /// <summary>
    ///     Define as operações do serviço de checkout da SuperFrete.
    /// </summary>
    public interface ISfCheckoutService
    {
        /// <summary>
        ///     Realiza o pagamento de etiquetas usando o saldo disponível na conta SuperFrete.
        ///     Após o pagamento bem-sucedido, o status das etiquetas é atualizado para "released".
        /// </summary>
        /// <remarks>Requer saldo suficiente na carteira SuperFrete.</remarks>
        /// <param name="request">IDs das etiquetas a pagar.</param>
        /// <returns>Resultado do checkout com detalhes das etiquetas pagas.</returns>
        Task<SfCheckoutResponse?> FinalizeOrderAsync(SfCheckoutRequest request);
    }
}
