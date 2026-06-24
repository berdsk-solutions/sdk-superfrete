using System.Collections.Generic;
using System.Threading.Tasks;
using Berdsk.Sdk.SuperFrete.Services.Users.Dtos;

namespace Berdsk.Sdk.SuperFrete.Services.Users
{
    /// <summary>
    ///     Interface do serviço de informações do usuário autenticado na SuperFrete.
    /// </summary>
    public interface ISfUserService
    {
        /// <summary>
        ///     Retorna as informações da conta autenticada, incluindo dados cadastrais, saldo disponível e limites de frete.
        /// </summary>
        /// <returns>Dados detalhados do usuário e sua conta.</returns>
        Task<SfUserResponse?> GetUserInfoAsync();

        /// <summary>
        ///     Retorna os endereços cadastrados na conta do usuário autenticado.
        /// </summary>
        /// <returns>Lista de endereços cadastrados na conta SuperFrete.</returns>
        Task<List<SfUserAddressResponse>?> GetAddressesAsync();
    }
}
