using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Berdsk.Sdk.SuperFrete.Services.Users.Dtos;

namespace Berdsk.Sdk.SuperFrete.Services.Users
{
    /// <summary>
    ///     Implementação do serviço de informações do usuário autenticado na SuperFrete.
    /// </summary>
    public class SfUserService : SfBaseService, ISfUserService
    {
        /// <summary>
        ///     Inicializa uma nova instância de <see cref="SfUserService" />.
        /// </summary>
        /// <param name="httpClient">Instância do <see cref="HttpClient" /> configurada pelo <c>SuperFreteClient</c>.</param>
        public SfUserService(HttpClient httpClient) : base(httpClient)
        {
        }

        /// <inheritdoc />
        public async Task<SfUserResponse?> GetUserInfoAsync()
        {
            return await GetAsync<SfUserResponse>(SfEndpoints.Users.Info);
        }

        /// <inheritdoc />
        public async Task<List<SfUserAddressResponse>?> GetAddressesAsync()
        {
            return await GetAsync<List<SfUserAddressResponse>>(SfEndpoints.Users.Addresses);
        }
    }
}
