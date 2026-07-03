---
tags: [configuracao, autenticacao, singleton, injecao-de-dependencia, ambientes]
---
# SuperFreteClient: O Coração do SDK

O `SuperFreteClient` é a classe central e o ponto de entrada para todas as operações autenticadas do SDK `Berdsk.Sdk.SuperFrete`. Ele organiza os serviços da API SuperFrete em propriedades tipadas e garante autenticação consistente em todas as chamadas.

> **Exceção:** o rastreamento público usa uma API separada sem autenticação e tem cliente próprio, o `SuperFreteTrackingClient` — veja [11-tracking.md](./11-tracking.md).

## Como Instanciar

Forneça seu token de integração, o ambiente desejado e os dados da sua aplicação (necessários para o header `User-Agent`, obrigatório pela API SuperFrete).

```csharp
using Berdsk.Sdk.SuperFrete;
using Berdsk.Sdk.SuperFrete.Helpers;

var client = new SuperFreteClient(
    token: "SEU_TOKEN_AQUI",
    environment: SuperFreteEnvironment.Sandbox,
    appName: "MinhaApp",
    appVersion: "1.0.0",
    contactEmail: "contato@meudominio.com"
);
```

### Ambientes Disponíveis

| Enum | URL Base | Uso |
| :--- | :--- | :--- |
| `SuperFreteEnvironment.Sandbox` | `https://sandbox.superfrete.com/` | Testes e desenvolvimento |
| `SuperFreteEnvironment.Production` | `https://api.superfrete.com/` | Ambiente de produção real |

> **Como obter o token:** [Produção](https://web.superfrete.com/#/integrations) | [Sandbox](https://sandbox.superfrete.com/#/integrations)

---

## Melhores Práticas de Ciclo de Vida

> **Regra de Ouro:** O `SuperFreteClient` deve ser tratado como um **Singleton**.
>
> Ele encapsula um `HttpClient`. Instanciar o cliente repetidamente para cada requisição pode causar **Socket Exhaustion**, degradando a performance da aplicação em produção.

### Registro no Container de DI (ASP.NET Core)

```csharp
// Program.cs
builder.Services.AddSingleton<SuperFreteClient>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    return new SuperFreteClient(
        token: config["SuperFrete:Token"]!,
        environment: SuperFreteEnvironment.Production,
        appName: "MinhaApp",
        appVersion: "1.0.0",
        contactEmail: config["SuperFrete:ContactEmail"]!
    );
});
```

### Injeção no Controller ou Service

```csharp
public class FreteService
{
    private readonly SuperFreteClient _client;

    public FreteService(SuperFreteClient client)
    {
        _client = client;
    }

    public async Task<string> CotarFrete(string cepOrigem, string cepDestino)
    {
        var request = new SfCalculateShippingRequest
        {
            From = new SfCalculationOriginRequest { PostalCode = cepOrigem },
            To = new SfCalculationDestinationRequest { PostalCode = cepDestino },
            Services = [SfShippingServiceType.Pac, SfShippingServiceType.Sedex],
            Package = new SfCalculationPackageRequest { Height = 15, Width = 15, Length = 20, Weight = 0.5 }
        };

        var resultados = await _client.Calculator.CalculateShippingAsync(request);
        return resultados?.FirstOrDefault()?.Name ?? "Nenhum serviço disponível";
    }
}
```

---

## Estrutura de Serviços (Mapa de Propriedades)

Ao digitar `client.`, você terá acesso às seguintes áreas:

| Propriedade | Interface | Responsabilidade Principal |
| :--- | :--- | :--- |
| `.Calculator` | `ISfCalculatorService` | Cotar fretes de PAC, SEDEX, Jadlog, Mini Envios, Loggi. |
| `.ShippingServices` | `ISfShippingServicesService` | Consultar restrições técnicas e detalhes dos serviços disponíveis. |
| `.Cart` | `ISfCartService` | Adicionar envios ao carrinho (criação de etiqueta). |
| `.Checkout` | `ISfCheckoutService` | Pagar etiquetas do carrinho com saldo SuperFrete. |
| `.Orders` | `ISfOrderService` | Consultar, cancelar, imprimir e listar pedidos. |
| `.Webhooks` | `ISfWebhookService` | Criar, listar, atualizar e remover apps de webhook. |
| `.Users` | `ISfUserService` | Consultar dados e endereços do usuário autenticado. |

> Rastreamento **não** está aqui — é público e vive no `SuperFreteTrackingClient.Tracking` ([11-tracking.md](./11-tracking.md)).

---

## Configurações Avançadas

### HttpClient Customizado (Proxy, Timeout, Resiliência)

Se sua infraestrutura exige configurações específicas de proxy, timeout ou resiliência (ex: Polly), passe seu próprio `HttpClient`:

```csharp
var httpClient = new HttpClient
{
    Timeout = TimeSpan.FromSeconds(30)
};

var client = new SuperFreteClient(
    token: "SEU_TOKEN",
    environment: SuperFreteEnvironment.Production,
    appName: "MinhaApp",
    appVersion: "1.0.0",
    contactEmail: "dev@meudominio.com",
    httpClient: httpClient
);
```

### Configuração via appsettings.json

```json
{
  "SuperFrete": {
    "Token": "seu_token_aqui",
    "ContactEmail": "dev@meudominio.com"
  }
}
```

---

## Dicas para IAs ao gerar código com este SDK:

1. **Singleton obrigatório:** Sempre registre o `SuperFreteClient` como singleton. Nunca instancie dentro de loops ou métodos chamados frequentemente.
2. **Token seguro:** O token nunca deve ser hardcoded no código-fonte. Use `IConfiguration`, variáveis de ambiente ou `dotnet user-secrets`.
3. **Ambiente correto:** Use `SuperFreteEnvironment.Sandbox` em desenvolvimento e `SuperFreteEnvironment.Production` em produção. Tokens são específicos por ambiente.
4. **User-Agent obrigatório:** A SuperFrete exige o header `User-Agent`. O SDK monta automaticamente no formato `NomeDaApp/Versão (email)`. Preencha todos os três campos com dados reais.
5. **Tipagem Forte:** Todos os serviços retornam DTOs tipados. Nunca use `dynamic`, `object` ou `JsonElement` diretamente.
6. **Nullable:** Todos os métodos retornam `T?`. Sempre verifique nulo antes de acessar propriedades do resultado.

---

[Anterior: Início](./00-comece-aqui.md) | [Próximo: Calculator](./02-calculator.md)
