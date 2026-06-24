using System.Collections.Generic;
using System.Threading.Tasks;
using Berdsk.Sdk.SuperFrete.Services.Webhooks.Dtos;

namespace Berdsk.Sdk.SuperFrete.Services.Webhooks
{
    /// <summary>
    ///     Interface do serviço de webhooks da SuperFrete.
    ///     Permite criar, listar, atualizar e remover Webhook Apps para recebimento de notificações de eventos.
    /// </summary>
    public interface ISfWebhookService
    {
        /// <summary>
        ///     Cria um novo webhook app para receber notificações de eventos.
        /// </summary>
        /// <remarks>
        ///     O <c>secret_token</c> retornado na resposta deve ser armazenado de forma segura.
        ///     Utilize-o para validar a autenticidade das notificações recebidas via HMAC-SHA256.
        /// </remarks>
        /// <param name="request">Dados do webhook a criar.</param>
        /// <returns>Dados do webhook criado, incluindo o <c>secret_token</c> gerado.</returns>
        Task<SfWebhookResponse?> CreateWebhookAsync(SfCreateWebhookRequest request);

        /// <summary>
        ///     Lista todos os webhooks cadastrados na conta autenticada.
        /// </summary>
        /// <returns>Lista de webhooks cadastrados.</returns>
        Task<List<SfWebhookResponse>?> ListWebhooksAsync();

        /// <summary>
        ///     Atualiza um webhook existente. Apenas os campos informados (não nulos) são atualizados.
        /// </summary>
        /// <param name="webhookId">Identificador único do webhook a atualizar.</param>
        /// <param name="request">Campos a atualizar.</param>
        /// <returns>Dados atualizados do webhook.</returns>
        Task<SfWebhookResponse?> UpdateWebhookAsync(string webhookId, SfUpdateWebhookRequest request);

        /// <summary>
        ///     Remove um webhook. A remoção é definitiva e o webhook deixa de receber notificações imediatamente.
        /// </summary>
        /// <param name="webhookId">Identificador único do webhook a remover.</param>
        Task DeleteWebhookAsync(string webhookId);
    }
}
