---
name: superfrete
description: Integração completa com a SuperFrete via SDK Berdsk.Sdk.SuperFrete. Use este skill para gerar código C# correto na primeira tentativa — cobre instalação, todos os serviços, DTOs, helpers, webhooks e tratamento de erros.
---

# Skill: Integração SuperFrete (.NET)

## Documentação Detalhada por Domínio

Consulte os arquivos abaixo para referência completa de cada serviço:

| Arquivo | Conteúdo |
|---|---|
| [00-comece-aqui.md](./00-comece-aqui.md) | Visão geral, cenários comuns e roadmap de leitura |
| [01-superfrete-client.md](./01-superfrete-client.md) | Instanciação, ambientes, Singleton e injeção de dependência |
| [02-calculator.md](./02-calculator.md) | Cotação de fretes — todos os DTOs e exemplos |
| [03-cart.md](./03-cart.md) | Adicionar envio ao carrinho — DTOs completos |
| [04-checkout.md](./04-checkout.md) | Pagar etiquetas e fluxo completo Calculator→Cart→Checkout |
| [05-orders.md](./05-orders.md) | Consultar, cancelar, imprimir e listar pedidos |
| [06-webhooks.md](./06-webhooks.md) | CRUD de webhook apps, payload real e validação HMAC |
| [07-users.md](./07-users.md) | Dados do usuário e endereços |
| [08-shipping-services.md](./08-shipping-services.md) | Restrições técnicas por transportadora |
| [09-helpers.md](./09-helpers.md) | Todas as constantes — SfOrderStatus, SfWebhookEvent, SfShippingServiceType, etc. |
| [10-exceptions.md](./10-exceptions.md) | Hierarquia de exceções e padrões de retry |

---

## Instalação

```bash
dotnet add package Berdsk.Sdk.SuperFrete
```

## Namespaces

```csharp
using Berdsk.Sdk.SuperFrete;
using Berdsk.Sdk.SuperFrete.Helpers;
using Berdsk.Sdk.SuperFrete.Services.Calculator.Dtos;
using Berdsk.Sdk.SuperFrete.Services.Cart.Dtos;
using Berdsk.Sdk.SuperFrete.Services.Checkout.Dtos;
using Berdsk.Sdk.SuperFrete.Services.Orders.Dtos;
using Berdsk.Sdk.SuperFrete.Services.Webhooks.Dtos;
using Berdsk.Sdk.SuperFrete.Services.Users.Dtos;
using Berdsk.Sdk.SuperFrete.Services.ShippingServices.Dtos;
```

---

## Mapa de Serviços

| Propriedade do cliente | Interface | Responsabilidade |
|---|---|---|
| `client.Calculator` | `ISfCalculatorService` | Cotar fretes |
| `client.ShippingServices` | `ISfShippingServicesService` | Restrições técnicas por serviço |
| `client.Cart` | `ISfCartService` | Adicionar envio ao carrinho |
| `client.Checkout` | `ISfCheckoutService` | Pagar etiquetas com saldo |
| `client.Orders` | `ISfOrderService` | Consultar, cancelar, imprimir, listar |
| `client.Webhooks` | `ISfWebhookService` | CRUD de webhook apps |
| `client.Users` | `ISfUserService` | Dados e endereços do usuário |

---

## Instanciação Rápida

```csharp
// Direto
var client = new SuperFreteClient(
    token: "SEU_TOKEN",
    environment: SuperFreteEnvironment.Sandbox,
    appName: "MinhaApp",
    appVersion: "1.0.0",
    contactEmail: "contato@meudominio.com"
);

// ASP.NET Core — Singleton via DI (obrigatório em produção)
builder.Services.AddSingleton<SuperFreteClient>(sp =>
    new SuperFreteClient(
        token: sp.GetRequiredService<IConfiguration>()["SuperFrete:Token"]!,
        environment: SuperFreteEnvironment.Production,
        appName: "MinhaApp", appVersion: "1.0.0",
        contactEmail: sp.GetRequiredService<IConfiguration>()["SuperFrete:ContactEmail"]!
    ));
```

> Detalhes completos → [01-superfrete-client.md](./01-superfrete-client.md)

---

## Helpers — Referência Rápida

```csharp
// Serviços de transporte
SfShippingServiceType.Pac        // 1
SfShippingServiceType.Sedex      // 2
SfShippingServiceType.Jadlog     // 3
SfShippingServiceType.MiniEnvios // 17
SfShippingServiceType.Loggi      // 31

// Status de pedidos
SfOrderStatus.Pending    // "pending"
SfOrderStatus.Released   // "released"
SfOrderStatus.Posted     // "posted"
SfOrderStatus.Delivered  // "delivered"
SfOrderStatus.Canceled   // "canceled"

// Eventos de webhook
SfWebhookEvent.OrderCreated    // "order.created"
SfWebhookEvent.OrderReleased   // "order.released"
SfWebhookEvent.OrderGenerated  // "order.generated"
SfWebhookEvent.OrderPosted     // "order.posted"
SfWebhookEvent.OrderDelivered  // "order.delivered"
SfWebhookEvent.OrderCancelled  // "order.cancelled"

// Ordenação
SfSortOrder.Ascending / SfSortOrder.Descending
SfSortBy.CreatedAt / SfSortBy.UpdatedAt
```

> Detalhes completos → [09-helpers.md](./09-helpers.md)

---

## Fluxo Principal: Criar Etiqueta

```
Calculator.CalculateShippingAsync()  →  obter Id e preço do serviço
Cart.AddToCartAsync()                →  criar pedido (status: pending)
Checkout.FinalizeOrderAsync()        →  pagar e gerar etiqueta (status: released)
```

> Fluxo completo com código → [04-checkout.md](./04-checkout.md)

---

## Webhooks — Pontos Críticos

- **Payload real** é `SfWebhookPayload` → `SfWebhookPayloadData`
- **`tags`** é `Dictionary<string, SfWebhookTag>` com chaves numéricas em string (`"0"`, `"1"`...) — **não é array**
- **Todos os campos de data** são `DateTime?` em UTC — o `SfDateTimeConverter` detecta automaticamente string ISO 8601, objeto Firestore Timestamp (`_seconds`/`_nanoseconds`) ou unix epoch
- **`SecretToken`** retornado **apenas na criação** — armazene imediatamente em variável de ambiente
- **Header de validação:** `X-ME-Signature` — valide com HMAC-SHA256 usando `CryptographicOperations.FixedTimeEquals`
- **Sempre retorne HTTP 200** — a SuperFrete reenvia até 5× em caso de falha (intervalo de 15 min)
- **`Tracking`** só fica disponível a partir do evento `order.generated`

> Implementação completa → [06-webhooks.md](./06-webhooks.md)

---

## Tratamento de Erros — Referência Rápida

| Exceção | HTTP | Causa comum |
|---|---|---|
| `SfBadRequestException` | 400 | CEP inválido, saldo insuficiente, campo obrigatório ausente |
| `SfUnauthorizedException` | 401 | Token inválido ou expirado |
| `SfForbiddenException` | 403 | Sem permissão para o recurso |
| `SfNotFoundException` | 404 | Pedido ou webhook não encontrado |
| `SfTooManyRequestsException` | 429 | Rate limit — use retry com backoff exponencial |
| `SfInternalServerErrorException` | 500 | Erro nos servidores da SuperFrete |
| `SuperFreteException` | outros | Base — captura qualquer erro não mapeado |

> Exemplos de catch, retry e `ErrorResponse.Errors` → [10-exceptions.md](./10-exceptions.md)

---

## Regras Invioláveis

1. **Zero magic strings** — use sempre `SfOrderStatus.X`, `SfWebhookEvent.X`, `(int)SfShippingServiceType.X`
2. **Nullable** — todos os métodos retornam `T?`; cheque nulo antes de acessar propriedades
3. **Singleton** — nunca instancie `SuperFreteClient` por request ou dentro de loops
4. **System.Text.Json** — nunca Newtonsoft; o SDK já configura `PropertyNameCaseInsensitive` e `WhenWritingNull`
5. **Filtrar `HasError`** — sempre filtre `!c.HasError` nos resultados do Calculator antes de usar
6. **`Print.Url` expira** — nunca armazene; gere sempre via `Orders.GetPrintLinkAsync`
7. **Cancelamento** — só funciona nos status `pending` ou `released`
8. **`tags` no webhook** — é dicionário com chaves `"0"`, `"1"`, não array
