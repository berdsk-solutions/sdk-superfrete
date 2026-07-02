---
tags: [webhooks, notificacoes, eventos, seguranca, hmac, assinatura]
---
# .Webhooks: Notificações de Eventos em Tempo Real

O serviço `.Webhooks` permite gerenciar "Webhook Apps" na SuperFrete. Um Webhook App é um endpoint no seu servidor que recebe notificações automáticas quando eventos ocorrem nos seus pedidos (criação, pagamento, postagem, entrega, cancelamento).

## Métodos Disponíveis

| Método | Descrição | DTO Entrada | DTO Saída |
| :--- | :--- | :--- | :--- |
| `CreateWebhookAsync` | Registra um novo app de webhook. | `SfCreateWebhookRequest` | `SfWebhookResponse?` |
| `ListWebhooksAsync` | Lista todos os apps de webhook cadastrados. | — | `List<SfWebhookResponse>?` |
| `UpdateWebhookAsync` | Atualiza um app de webhook existente. | `string webhookId`, `SfUpdateWebhookRequest` | `SfWebhookResponse?` |
| `DeleteWebhookAsync` | Remove um app de webhook. | `string webhookId` | `Task` |

---

## Exemplos de Uso

### Criar um Webhook App

Cadastra um endpoint no seu servidor para receber notificações de eventos.

- **DTO de Entrada:** `SfCreateWebhookRequest`
- **DTO de Saída:** `SfWebhookResponse`

```csharp
using Berdsk.Sdk.SuperFrete.Helpers;
using Berdsk.Sdk.SuperFrete.Services.Webhooks.Dtos;

var request = new SfCreateWebhookRequest
{
    Name = "Notificações da Loja",
    Url = "https://meusite.com/webhooks/superfrete",
    Events = [
        SfWebhookEvent.OrderCreated,
        SfWebhookEvent.OrderReleased,
        SfWebhookEvent.OrderGenerated,
        SfWebhookEvent.OrderPosted,
        SfWebhookEvent.OrderDelivered,
        SfWebhookEvent.OrderCancelled
    ]
};

var webhook = await client.Webhooks.CreateWebhookAsync(request);

if (webhook != null)
{
    Console.WriteLine($"Webhook criado! ID: {webhook.Id}");
    Console.WriteLine($"Nome: {webhook.Name}");
    Console.WriteLine($"URL: {webhook.Url}");
    Console.WriteLine($"Ativo: {webhook.IsActive}");

    // IMPORTANTE: O secret_token só é retornado na criação!
    // Armazene-o com segurança para validar as assinaturas HMAC.
    Console.WriteLine($"Secret Token: {webhook.SecretToken}");
}
```

> **Atenção:** O campo `SecretToken` (`secret_token`) é retornado **apenas na criação** do webhook. Guarde-o com segurança no seu servidor (variável de ambiente ou vault). Ele será usado para validar a autenticidade das notificações recebidas.

---

### Listar Webhooks

```csharp
var webhooks = await client.Webhooks.ListWebhooksAsync();

foreach (var hook in webhooks ?? [])
{
    Console.WriteLine($"[{(hook.IsActive == true ? "ATIVO" : "INATIVO")}] {hook.Id} — {hook.Name} → {hook.Url}");
    Console.WriteLine($"  Eventos: {string.Join(", ", hook.Events ?? [])}");
}
```

---

### Atualizar um Webhook

Atualiza a URL, nome, eventos ou status ativo de um webhook existente. Todos os campos são opcionais — envie apenas o que deseja alterar.

- **DTO de Entrada:** `SfUpdateWebhookRequest`

```csharp
var update = new SfUpdateWebhookRequest
{
    Url = "https://meusite.com/webhooks/superfrete-v2",
    IsActive = true,
    Events = [SfWebhookEvent.OrderDelivered, SfWebhookEvent.OrderCancelled]
};

var atualizado = await client.Webhooks.UpdateWebhookAsync("webhook-id-aqui", update);
Console.WriteLine($"Webhook atualizado: {atualizado?.Url}");
```

---

### Desativar Temporariamente

```csharp
await client.Webhooks.UpdateWebhookAsync("webhook-id", new SfUpdateWebhookRequest
{
    IsActive = false
});
```

---

### Deletar um Webhook

```csharp
await client.Webhooks.DeleteWebhookAsync("webhook-id-aqui");
Console.WriteLine("Webhook removido com sucesso.");
```

---

## Recebendo e Validando Notificações

Quando um evento ocorre, a SuperFrete faz uma requisição `POST` para a URL configurada com o payload da notificação e um header de segurança `X-ME-Signature`.

### Estrutura do Payload Recebido

O payload é deserializável para `SfWebhookPayload`:

```json
{
  "event": "order.generated",
  "data": {
    "id": "ClmHZOg0p9CWbpFwKsLm",
    "order_id": "ClmHZOg0p9CWbpFwKsLm",
    "protocol": null,
    "status": "generated",
    "tracking": "AK038659733BR",
    "self_tracking": null,
    "user_id": "2F8TXAcSyLbSYefDlW4q2jaaciO2",
    "tags": { "0": { "name": "order_id", "value": "order-1555" } },
    "created_at": "2026-06-24T22:41:58.325Z",
    "paid_at": "2026-06-24T22:42:18.395Z",
    "generated_at": "2026-06-24T22:42:24.318Z",
    "posted_at": null,
    "delivered_at": null,
    "canceled_at": null,
    "expired_at": null,
    "tracking_url": null
  }
}
```

### Exemplo de Controller ASP.NET Core para Receber Webhooks

```csharp
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Berdsk.Sdk.SuperFrete.Helpers;
using Berdsk.Sdk.SuperFrete.Services.Webhooks.Dtos;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("webhooks")]
public class SuperFreteWebhookController : ControllerBase
{
    private readonly string _secretToken; // Obtido da variável de ambiente

    public SuperFreteWebhookController(IConfiguration config)
    {
        _secretToken = config["SuperFrete:WebhookSecret"]!;
    }

    [HttpPost("superfrete")]
    public async Task<IActionResult> ReceberNotificacao()
    {
        // Ler o corpo RAW da requisição
        using var reader = new StreamReader(Request.Body);
        var payloadRaw = await reader.ReadToEndAsync();

        // Validar assinatura HMAC-SHA256
        var assinaturaRecebida = Request.Headers["X-ME-Signature"].ToString();
        if (!ValidarAssinatura(payloadRaw, assinaturaRecebida))
        {
            return Unauthorized("Assinatura inválida.");
        }

        // Deserializar o payload
        var payload = JsonSerializer.Deserialize<SfWebhookPayload>(payloadRaw,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (payload == null) return BadRequest();

        // Processar o evento
        switch (payload.Event)
        {
            case SfWebhookEvent.OrderDelivered:
                Console.WriteLine($"Pedido {payload.Data?.Id} entregue!");
                // Atualizar status no banco de dados
                break;

            case SfWebhookEvent.OrderPosted:
                Console.WriteLine($"Pedido {payload.Data?.Id} postado. Rastreio: {payload.Data?.Tracking}");
                break;

            case SfWebhookEvent.OrderCancelled:
                Console.WriteLine($"Pedido {payload.Data?.Id} cancelado.");
                break;

            case SfWebhookEvent.OrderGenerated:
                Console.WriteLine($"Etiqueta gerada para {payload.Data?.Id}. URL de rastreio: {payload.Data?.TrackingUrl}");
                break;
        }

        return Ok(); // Sempre responda 200 para confirmar recebimento
    }

    private bool ValidarAssinatura(string payload, string assinaturaRecebida)
    {
        if (string.IsNullOrEmpty(assinaturaRecebida)) return false;

        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_secretToken));
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
        var assinaturaCalculada = Convert.ToHexString(hash).ToLowerInvariant();

        return CryptographicOperations.FixedTimeEquals(
            Encoding.UTF8.GetBytes(assinaturaCalculada),
            Encoding.UTF8.GetBytes(assinaturaRecebida)
        );
    }
}
```

---

## Estrutura dos DTOs

### `SfCreateWebhookRequest`

| Propriedade | Tipo | Obrigatório | Descrição |
| :--- | :--- | :--- | :--- |
| `Name` | `string` | Sim | Nome descritivo do webhook app. |
| `Url` | `string` | Sim | URL do endpoint que receberá as notificações (deve ser `POST`). |
| `Events` | `string[]` | Sim | Lista de eventos a monitorar (use `SfWebhookEvent`). |

### `SfUpdateWebhookRequest`

| Propriedade | Tipo | Obrigatório | Descrição |
| :--- | :--- | :--- | :--- |
| `Name` | `string?` | Não | Novo nome. |
| `Url` | `string?` | Não | Nova URL. |
| `Events` | `string[]?` | Não | Nova lista de eventos. |
| `IsActive` | `bool?` | Não | `true` para ativar, `false` para desativar. |

### `SfWebhookResponse`

| Propriedade | Tipo | Descrição |
| :--- | :--- | :--- |
| `Id` | `string?` | ID do webhook app. |
| `Name` | `string?` | Nome do webhook app. |
| `Url` | `string?` | URL do endpoint configurado. |
| `SecretToken` | `string?` | Token secreto HMAC (retornado **apenas na criação**). |
| `Events` | `string[]?` | Eventos configurados. |
| `IsActive` | `bool` | Se o webhook está ativo. |
| `CreatedAt` | `DateTime?` | Data de criação em UTC (aceita ISO 8601 ou Firestore Timestamp automaticamente). |
| `UpdatedAt` | `DateTime?` | Data da última atualização em UTC (aceita ISO 8601 ou Firestore Timestamp automaticamente). |

### `SfWebhookPayload`

| Propriedade | Tipo | Descrição |
| :--- | :--- | :--- |
| `Event` | `string?` | Tipo do evento. Compare com `SfWebhookEvent`. |
| `Data` | `SfWebhookPayloadData?` | Dados do pedido que gerou o evento. |

### `SfWebhookPayloadData`

| Propriedade | Tipo | Descrição |
| :--- | :--- | :--- |
| `Id` | `string?` | ID do pedido. |
| `OrderId` | `string?` | ID do pedido (mesmo valor que `Id` na API atual). |
| `Protocol` | `string?` | Protocolo interno. |
| `Status` | `string?` | Status atual. Compare com `SfOrderStatus`. |
| `Tracking` | `string?` | Código de rastreamento (disponível a partir de `order.generated`). |
| `SelfTracking` | `string?` | Código de rastreamento próprio. |
| `UserId` | `string?` | ID do usuário proprietário. |
| `Tags` | `Dictionary<string, SfWebhookTag>?` | Tags indexadas por posição numérica em string (`"0"`, `"1"`...). |
| `CreatedAt` | `DateTime?` | Data de criação em UTC. |
| `PaidAt` | `DateTime?` | Data de pagamento. |
| `GeneratedAt` | `DateTime?` | Data de geração da etiqueta. |
| `PostedAt` | `DateTime?` | Data de postagem. |
| `DeliveredAt` | `DateTime?` | Data de entrega. |
| `CanceledAt` | `DateTime?` | Data de cancelamento. |
| `ExpiredAt` | `DateTime?` | Data de expiração. |
| `TrackingUrl` | `string?` | URL de rastreamento público. |

### `SfWebhookTag`

| Propriedade | Tipo | Descrição |
| :--- | :--- | :--- |
| `Name` | `string?` | Nome da tag (ex: `order_id`). |
| `Value` | `string?` | Valor da tag (ex: `order-1555`). |

---

## Dicas para IAs ao utilizar .Webhooks:

1. **`SecretToken` único:** O `secret_token` só é retornado na **criação** do webhook. Salve-o imediatamente em uma variável de ambiente (`SuperFrete__WebhookSecret`) ou vault. Se perder, precisará recriar o webhook.
2. **Validação obrigatória:** Sempre valide o header `X-ME-Signature` antes de processar qualquer payload. Use `CryptographicOperations.FixedTimeEquals` para evitar timing attacks.
3. **Resposta 200 sempre:** Seu endpoint deve retornar HTTP 200 mesmo que não consiga processar o evento imediatamente. A SuperFrete reenvia a notificação em caso de falha (até 5 tentativas com intervalo de 15 minutos).
4. **Idempotência:** Um mesmo evento pode chegar mais de uma vez. Use o `data.Id` (ID do pedido) para verificar se já foi processado antes de agir novamente.
5. **Eventos com `SfWebhookEvent`:** Use sempre as constantes do helper (ex: `SfWebhookEvent.OrderDelivered`) em vez de strings literais no `switch`.
6. **`tracking` e `tracking_url`:** Só ficam disponíveis a partir do evento `order.generated`. Em eventos anteriores esses campos são `null`.
7. **Namespace:** `using Berdsk.Sdk.SuperFrete.Services.Webhooks.Dtos;` e `using Berdsk.Sdk.SuperFrete.Helpers;`.

---

[Anterior: Orders](./05-orders.md) | [Início](./00-comece-aqui.md) | [Próximo: Users](./07-users.md)
