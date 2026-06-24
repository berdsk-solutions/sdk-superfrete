---
tags: [helpers, constantes, enums, status, eventos, servicos]
---
# Helpers e Constantes

O SDK disponibiliza um conjunto de classes estáticas e enums no namespace `Berdsk.Sdk.SuperFrete.Helpers` que funcionam como dicionários tipados de valores aceitos pela API SuperFrete. **Sempre utilize estas constantes** em vez de strings ou números literais para evitar erros de digitação e facilitar a manutenção.

```csharp
using Berdsk.Sdk.SuperFrete.Helpers;
```

---

## Enum: `SuperFreteEnvironment`

Define o ambiente de execução da API.

| Membro | URL Base | Uso |
| :--- | :--- | :--- |
| `SuperFreteEnvironment.Sandbox` | `https://sandbox.superfrete.com/` | Testes e desenvolvimento |
| `SuperFreteEnvironment.Production` | `https://api.superfrete.com/` | Ambiente de produção |

```csharp
var client = new SuperFreteClient(
    token: "...",
    environment: SuperFreteEnvironment.Production,
    // ...
);
```

---

## Enum: `SfShippingServiceType`

Identifica os serviços de transporte disponíveis na SuperFrete. O valor numérico do enum corresponde ao `Id` retornado pelo `Calculator` e ao valor esperado no campo `Service` do `Cart`.

| Membro | Valor | Serviço | Transportadora |
| :--- | :--- | :--- | :--- |
| `SfShippingServiceType.Pac` | `1` | PAC | Correios |
| `SfShippingServiceType.Sedex` | `2` | SEDEX | Correios |
| `SfShippingServiceType.Jadlog` | `3` | Jadlog | Jadlog |
| `SfShippingServiceType.MiniEnvios` | `17` | Mini Envios | Correios |
| `SfShippingServiceType.Loggi` | `31` | Loggi | Loggi |

```csharp
// Uso em cotação
Services = [SfShippingServiceType.Pac, SfShippingServiceType.Sedex]

// Uso no carrinho
Service = (int)SfShippingServiceType.Sedex // = 2

// Verificar tipo de serviço retornado
if (resposta.Id == (int)SfShippingServiceType.Jadlog) { /* ... */ }
```

---

## Classe Estática: `SfOrderStatus`

Status possíveis de um pedido ao longo do seu ciclo de vida.

| Constante | Valor | Significado |
| :--- | :--- | :--- |
| `SfOrderStatus.Pending` | `"pending"` | Pedido criado, aguardando pagamento. |
| `SfOrderStatus.Released` | `"released"` | Etiqueta gerada (pago com sucesso). |
| `SfOrderStatus.Posted` | `"posted"` | Postado na transportadora. |
| `SfOrderStatus.Delivered` | `"delivered"` | Entregue ao destinatário. |
| `SfOrderStatus.Canceled` | `"canceled"` | Pedido cancelado. |

```csharp
var pedido = await client.Orders.GetOrderInfoAsync("ord_abc");

switch (pedido?.Status)
{
    case SfOrderStatus.Pending:
        Console.WriteLine("Aguardando checkout.");
        break;
    case SfOrderStatus.Released:
        Console.WriteLine($"Pronto para postagem. Imprimir: {pedido.Print?.Url}");
        break;
    case SfOrderStatus.Posted:
        Console.WriteLine($"Em trânsito. Rastreio: {pedido.Tracking}");
        break;
    case SfOrderStatus.Delivered:
        Console.WriteLine("Entregue com sucesso!");
        break;
    case SfOrderStatus.Canceled:
        Console.WriteLine("Pedido cancelado.");
        break;
}
```

---

## Classe Estática: `SfWebhookEvent`

Tipos de eventos que podem ser notificados via webhook.

| Constante | Valor | Quando é disparado |
| :--- | :--- | :--- |
| `SfWebhookEvent.OrderCreated` | `"order.created"` | Pedido criado no carrinho. |
| `SfWebhookEvent.OrderReleased` | `"order.released"` | Pedido pago (etiqueta gerada). |
| `SfWebhookEvent.OrderGenerated` | `"order.generated"` | Etiqueta processada pela transportadora. |
| `SfWebhookEvent.OrderPosted` | `"order.posted"` | Pedido postado na transportadora. |
| `SfWebhookEvent.OrderDelivered` | `"order.delivered"` | Pedido entregue ao destinatário. |
| `SfWebhookEvent.OrderCancelled` | `"order.cancelled"` | Pedido cancelado. |

```csharp
// Criando webhook com eventos específicos
Events = [
    SfWebhookEvent.OrderPosted,
    SfWebhookEvent.OrderDelivered,
    SfWebhookEvent.OrderCancelled
]

// Processando evento recebido
if (payload.Event == SfWebhookEvent.OrderDelivered)
{
    // Marcar pedido como entregue no sistema
}
```

---

## Classe Estática: `SfSortOrder`

Define a direção de ordenação na listagem de pedidos.

| Constante | Valor | Descrição |
| :--- | :--- | :--- |
| `SfSortOrder.Ascending` | `"asc"` | Crescente (mais antigo primeiro). |
| `SfSortOrder.Descending` | `"desc"` | Decrescente (mais recente primeiro). |

```csharp
var lista = await client.Orders.ListOrdersAsync(new SfListOrdersRequest
{
    Order = SfSortOrder.Descending // Mais recentes primeiro
});
```

---

## Classe Estática: `SfSortBy`

Define o campo de ordenação na listagem de pedidos.

| Constante | Valor | Descrição |
| :--- | :--- | :--- |
| `SfSortBy.CreatedAt` | `"created_at"` | Ordenar pela data de criação. |
| `SfSortBy.UpdatedAt` | `"updated_at"` | Ordenar pela data de última atualização. |

```csharp
var lista = await client.Orders.ListOrdersAsync(new SfListOrdersRequest
{
    SortBy = SfSortBy.UpdatedAt,
    Order = SfSortOrder.Descending
});
```

---

## Resumo dos Helpers por Contexto de Uso

| Serviço | Helper | Onde Usar |
| :--- | :--- | :--- |
| `SuperFreteClient` | `SuperFreteEnvironment` | Parâmetro `environment` na instanciação. |
| `Calculator`, `Cart` | `SfShippingServiceType` | `Services[]` na cotação; `Service` no carrinho. |
| `Orders`, `Webhooks` | `SfOrderStatus` | Comparar `Status` de pedidos retornados. |
| `Webhooks` | `SfWebhookEvent` | `Events[]` na criação; `Event` no payload recebido. |
| `Orders` (listagem) | `SfSortOrder`, `SfSortBy` | Parâmetros `Order` e `SortBy` em `SfListOrdersRequest`. |

---

## Dicas para IAs ao utilizar Helpers:

1. **Nunca use strings literais:** Em vez de `"pending"`, use `SfOrderStatus.Pending`. Em vez de `"order.delivered"`, use `SfWebhookEvent.OrderDelivered`. Erros de digitação são a causa mais comum de bugs silenciosos.
2. **`SfShippingServiceType` como int:** Para usar no campo `Service` do carrinho, faça o cast explícito: `(int)SfShippingServiceType.Sedex`. O JSON de saída serializa o array de serviços automaticamente como string `"1,2,17"` — o converter `SfShippingServicesJsonConverter` faz isso internamente.
3. **Always include the using:** Todos os helpers estão em `using Berdsk.Sdk.SuperFrete.Helpers;`. Inclua sempre esse namespace nas sugestões de código.
4. **Extensível:** Se um novo serviço de transporte for adicionado pela SuperFrete, o enum `SfShippingServiceType` deverá ser atualizado com o novo ID. Verifique a documentação oficial da API para manter sincronizado.

---

[Anterior: ShippingServices](./08-shipping-services.md) | [Início](./00-comece-aqui.md) | [Próximo: Exceptions](./10-exceptions.md)
