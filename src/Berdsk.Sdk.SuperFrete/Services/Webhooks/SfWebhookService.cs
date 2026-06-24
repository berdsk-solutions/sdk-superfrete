using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Berdsk.Sdk.SuperFrete.Services.Webhooks.Dtos;

namespace Berdsk.Sdk.SuperFrete.Services.Webhooks
{
    /// <summary>
    ///     Implementação do serviço de webhooks da SuperFrete.
    /// </summary>
    public class SfWebhookService : SfBaseService, ISfWebhookService
    {
        /// <summary>
        ///     Inicializa uma nova instância de <see cref="SfWebhookService" />.
        /// </summary>
        /// <param name="httpClient">Instância do <see cref="HttpClient" /> configurada pelo <c>SuperFreteClient</c>.</param>
        public SfWebhookService(HttpClient httpClient) : base(httpClient)
        {
        }

        /// <inheritdoc />
        public async Task<SfWebhookResponse?> CreateWebhookAsync(SfCreateWebhookRequest request)
        {
            return await PostAsync<SfWebhookResponse, SfCreateWebhookRequest>(SfEndpoints.Webhooks.Base, request);
        }

        /// <inheritdoc />
        public async Task<List<SfWebhookResponse>?> ListWebhooksAsync()
        {
            return await GetAsync<List<SfWebhookResponse>>(SfEndpoints.Webhooks.Base);
        }

        /// <inheritdoc />
        public async Task<SfWebhookResponse?> UpdateWebhookAsync(string webhookId, SfUpdateWebhookRequest request)
        {
            var url = string.Format(SfEndpoints.Webhooks.ById, webhookId);
            return await PutAsync<SfWebhookResponse, SfUpdateWebhookRequest>(url, request);
        }

        /// <inheritdoc />
        public async Task DeleteWebhookAsync(string webhookId)
        {
            var url = string.Format(SfEndpoints.Webhooks.ById, webhookId);
            await DeleteAsync<SfWebhookResponse>(url);
        }
    }
}
