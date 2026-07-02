---
tags: [pedidos, cancelamento, impressao, rastreamento, listagem, etiqueta]
---
# .Orders: Gestão de Pedidos

O serviço `.Orders` permite consultar, cancelar, imprimir e listar pedidos criados na SuperFrete. É o principal serviço de pós-venda do SDK, cobrindo todo o ciclo de vida de um envio após o checkout.

## Métodos Disponíveis

| Método | Descrição | DTO Entrada | DTO Saída |
| :--- | :--- | :--- | :--- |
| `GetOrderInfoAsync` | Obtém detalhes completos de um pedido. | `string orderId` | `SfOrderInfoResponse?` |
| `CancelOrderAsync` | Cancela um pedido por ID. | `SfCancelOrderRequest` | `Dictionary<string, SfCancelOrderResultResponse>?` |
| `GetPrintLinkAsync` | Obtém link de impressão em PDF para um ou mais pedidos. | `SfPrintLinkRequest` | `SfPrintLinkResponse?` |
| `ListOrdersAsync` | Lista pedidos com filtros opcionais. | `SfListOrdersRequest?` | `SfListOrdersResponse?` |

---

## Exemplos de Uso

### Consultar Detalhes de um Pedido

Recupera todos os dados de um pedido específico, incluindo remetente, destinatário, status e rastreamento.

- **DTO de Saída:** `SfOrderInfoResponse`

```csharp
var pedido = await client.Orders.GetOrderInfoAsync("ord_abc123xyz456");

if (pedido != null)
{
    Console.WriteLine($"ID: {pedido.Id}");
    Console.WriteLine($"Protocolo: {pedido.Protocol}");
    Console.WriteLine($"Status: {pedido.Status}");      // Compare com SfOrderStatus
    Console.WriteLine($"Rastreamento: {pedido.Tracking}");
    Console.WriteLine($"Preço: {pedido.Price}");
    Console.WriteLine($"Serviço ID: {pedido.ServiceId}");

    // Datas do ciclo de vida
    Console.WriteLine($"Criado em: {pedido.CreatedAt}");
    Console.WriteLine($"Postado em: {pedido.PostedAt}");
    Console.WriteLine($"Entregue em: {pedido.Delivery}");

    // Remetente e destinatário
    Console.WriteLine($"De: {pedido.From?.Name} ({pedido.From?.City}/{pedido.From?.StateAbbr})");
    Console.WriteLine($"Para: {pedido.To?.Name} ({pedido.To?.City}/{pedido.To?.StateAbbr})");

    // Link de impressão (se disponível)
    if (pedido.Print?.Url != null)
        Console.WriteLine($"Etiqueta: {pedido.Print.Url}");
}
```

---

### Verificar Status com Helper

```csharp
using Berdsk.Sdk.SuperFrete.Helpers;

var pedido = await client.Orders.GetOrderInfoAsync("ord_abc123");

if (pedido?.Status == SfOrderStatus.Delivered)
{
    Console.WriteLine("Pedido entregue com sucesso!");
}
else if (pedido?.Status == SfOrderStatus.Posted)
{
    Console.WriteLine($"Em trânsito. Rastrear: {pedido.Tracking}");
}
else if (pedido?.Status == SfOrderStatus.Canceled)
{
    Console.WriteLine("Pedido cancelado.");
}
```

---

### Cancelar um Pedido

> **Atenção:** Pedidos já postados nos Correios podem não ser canceláveis. Verifique o status antes de tentar cancelar.

- **DTO de Entrada:** `SfCancelOrderRequest`
- **DTO de Saída:** `Dictionary<string, SfCancelOrderResultResponse>?` — chave é o `orderId`

```csharp
using Berdsk.Sdk.SuperFrete.Services.Orders.Dtos;

var request = new SfCancelOrderRequest
{
    Order = new SfCancelOrderItemRequest
    {
        Id = "ord_abc123xyz456",
        Description = "Cliente solicitou cancelamento"
    }
};

var resultado = await client.Orders.CancelOrderAsync(request);

if (resultado != null)
{
    foreach (var kvp in resultado)
    {
        string orderId = kvp.Key;
        bool cancelado = kvp.Value.Canceled;
        Console.WriteLine($"Pedido {orderId}: {(cancelado ? "cancelado" : "não cancelado")}");
    }
}
```

---

### Obter Link de Impressão

Gera uma URL para download do PDF com as etiquetas dos pedidos informados. Útil para reimprimir etiquetas já geradas.

- **DTO de Entrada:** `SfPrintLinkRequest`
- **DTO de Saída:** `SfPrintLinkResponse`

```csharp
var request = new SfPrintLinkRequest
{
    Orders = ["ord_abc123", "ord_def456"] // Imprime múltiplas etiquetas em um PDF
};

var link = await client.Orders.GetPrintLinkAsync(request);

if (link?.Url != null)
{
    Console.WriteLine($"Download das etiquetas: {link.Url}");
    // Redirecionar o usuário para link.Url para download do PDF
}
```

---

### Listar Pedidos

Lista todos os pedidos da conta com suporte a filtros de status, paginação e ordenação.

- **DTO de Entrada:** `SfListOrdersRequest?` (todos os campos são opcionais)
- **DTO de Saída:** `SfListOrdersResponse`

```csharp
using Berdsk.Sdk.SuperFrete.Helpers;

// Listar pedidos pendentes
var request = new SfListOrdersRequest
{
    Status = SfOrderStatus.Pending,    // Filtra por status
    Page = 1,
    PerPage = 20,
    Order = SfSortOrder.Descending,    // Mais recentes primeiro
    SortBy = SfSortBy.CreatedAt
};

var lista = await client.Orders.ListOrdersAsync(request);

if (lista != null)
{
    Console.WriteLine($"Total de pedidos: {lista.Total}");
    Console.WriteLine($"Página {lista.CurrentPage} de {lista.LastPage}");

    foreach (var pedido in lista.Data ?? [])
    {
        Console.WriteLine($"[{pedido.Status}] {pedido.Id} — {pedido.Tracking} — R$ {pedido.Price}");
    }
}
```

### Listar Todos os Pedidos (sem filtros)

```csharp
var todos = await client.Orders.ListOrdersAsync(); // Sem filtros = lista todos
```

### Paginação

```csharp
int paginaAtual = 1;
SfListOrdersResponse? pagina;

do
{
    pagina = await client.Orders.ListOrdersAsync(new SfListOrdersRequest { Page = paginaAtual, PerPage = 50 });

    foreach (var pedido in pagina?.Data ?? [])
    {
        // Processar pedido
    }

    paginaAtual++;
} while (pagina != null && paginaAtual <= pagina.LastPage);
```

---

## Estrutura dos DTOs

### `SfOrderInfoResponse`

| Propriedade | Tipo | Descrição |
| :--- | :--- | :--- |
| `Id` | `string?` | Identificador único do pedido. |
| `Protocol` | `string?` | Protocolo SuperFrete. |
| `Status` | `string?` | Status atual. Compare com `SfOrderStatus`. |
| `Tracking` | `string?` | Código de rastreamento dos Correios/transportadora. |
| `Price` | `string?` | Valor pago pelo frete. |
| `Discount` | `string?` | Desconto aplicado. |
| `ServiceId` | `int?` | ID do serviço de transporte. |
| `From` | `SfOrderContactResponse?` | Dados completos do remetente. |
| `To` | `SfOrderContactResponse?` | Dados completos do destinatário. |
| `Products` | `SfOrderProductResponse[]?` | Produtos declarados no envio. |
| `Print` | `SfOrderPrintResponse?` | Link de impressão da etiqueta. |
| `Tags` | `SfOrderTagResponse[]?` | Tags de rastreamento interno. |
| `CreatedAt` | `DateTime?` | Data/hora de criação em UTC. |
| `PostedAt` | `DateTime?` | Data/hora de postagem em UTC. |
| `GeneratedAt` | `DateTime?` | Data/hora de geração da etiqueta em UTC. |
| `Delivery` | `string?` | Data/hora de entrega. |

### `SfListOrdersRequest`

| Propriedade | Tipo | Descrição |
| :--- | :--- | :--- |
| `Status` | `string?` | Filtro por status (use `SfOrderStatus`). |
| `Page` | `int?` | Número da página (padrão: 1). |
| `PerPage` | `int?` | Itens por página (padrão: API decide). |
| `Order` | `string?` | Direção da ordenação (use `SfSortOrder`). |
| `SortBy` | `string?` | Campo de ordenação (use `SfSortBy`). |

### `SfListOrdersResponse`

| Propriedade | Tipo | Descrição |
| :--- | :--- | :--- |
| `Data` | `SfOrderInfoResponse[]?` | Lista de pedidos da página atual. |
| `Total` | `int?` | Total de pedidos no filtro. |
| `PerPage` | `int?` | Itens por página. |
| `CurrentPage` | `int?` | Página atual. |
| `LastPage` | `int?` | Última página disponível. |

---

## Dicas para IAs ao utilizar .Orders:

1. **Status com Helper:** Sempre use `SfOrderStatus.Pending`, `SfOrderStatus.Released`, etc. para comparar o status retornado. Nunca compare com strings literais.
2. **Cancelamento:** Só é possível cancelar pedidos nos status `pending` ou `released`. Pedidos `posted` ou `delivered` geralmente não podem ser cancelados.
3. **Resposta do cancelamento:** A API retorna um `Dictionary<string, SfCancelOrderResultResponse>` onde a chave é o `orderId`. Isso permite cancelar múltiplos pedidos em uma chamada (via lógica manual) e verificar o resultado individualmente.
4. **Reimpressão:** Use `GetPrintLinkAsync` para reimprimir etiquetas já geradas. O link expira após um período — não armazene a URL, sempre gere uma nova.
5. **Listagem paginada:** Para exportar todos os pedidos, implemente paginação com `LastPage`. Nunca assuma que todos os pedidos cabem na primeira página.
6. **`SortBy` e `Order`:** Use `SfSortBy.CreatedAt` com `SfSortOrder.Descending` para listar os pedidos mais recentes primeiro.
7. **Namespace:** `using Berdsk.Sdk.SuperFrete.Services.Orders.Dtos;` e `using Berdsk.Sdk.SuperFrete.Helpers;`.

---

[Anterior: Checkout](./04-checkout.md) | [Início](./00-comece-aqui.md) | [Próximo: Webhooks](./06-webhooks.md)
