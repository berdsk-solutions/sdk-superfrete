---
tags: [cotacao, frete, calculo, servicos, pac, sedex, jadlog, loggi]
---
# .Calculator: Cotação de Fretes

O serviço `.Calculator` permite cotar fretes de múltiplos serviços de transporte em uma única chamada. Ele retorna preços, prazos e detalhes de dimensões para cada serviço disponível entre o CEP de origem e destino.

## Métodos Disponíveis

| Método | Descrição | DTO Entrada | DTO Saída |
| :--- | :--- | :--- | :--- |
| `CalculateShippingAsync` | Cota fretes para um ou mais serviços. | `SfCalculateShippingRequest` | `List<SfCalculateShippingResponse>?` |

---

## Exemplos de Uso

### Cotação Completa (Recomendado para IAs)

Este é o exemplo mais completo, incluindo todos os campos opcionais para maior precisão no cálculo:

- **DTO de Entrada:** `SfCalculateShippingRequest`
- **DTO de Saída:** `List<SfCalculateShippingResponse>`

```csharp
using Berdsk.Sdk.SuperFrete.Helpers;
using Berdsk.Sdk.SuperFrete.Services.Calculator.Dtos;

var request = new SfCalculateShippingRequest
{
    // Origem e destino (apenas CEP é necessário)
    From = new SfCalculationOriginRequest
    {
        PostalCode = "01310-100" // CEP da Avenida Paulista
    },
    To = new SfCalculationDestinationRequest
    {
        PostalCode = "20040-020" // CEP do Rio de Janeiro
    },

    // Serviços a cotar (use o enum SfShippingServiceType)
    Services = [
        SfShippingServiceType.Pac,
        SfShippingServiceType.Sedex,
        SfShippingServiceType.Jadlog,
        SfShippingServiceType.MiniEnvios,
        SfShippingServiceType.Loggi
    ],

    // Dimensões do pacote
    Package = new SfCalculationPackageRequest
    {
        Height = 15,   // cm
        Width = 15,    // cm
        Length = 20,   // cm
        Weight = 0.5   // kg
    },

    // Serviços adicionais (opcional)
    Options = new SfCalculationOptionsRequest
    {
        OwnHand = false,           // Mão própria
        Receipt = false,           // Aviso de recebimento
        InsuranceValue = 50.00,    // Valor declarado (R$)
        UseInsuranceValue = true   // Usar valor declarado no cálculo do seguro
    }
};

var cotacoes = await client.Calculator.CalculateShippingAsync(request);

if (cotacoes == null || cotacoes.Count == 0)
{
    Console.WriteLine("Nenhum serviço disponível para esta rota.");
    return;
}

foreach (var servico in cotacoes)
{
    if (servico.HasError)
    {
        Console.WriteLine($"{servico.Name}: indisponível para esta rota.");
        continue;
    }

    Console.WriteLine($"{servico.Name}:");
    Console.WriteLine($"  Preço: R$ {servico.Price:F2}");
    Console.WriteLine($"  Desconto: R$ {servico.Discount:F2}");
    Console.WriteLine($"  Prazo: {servico.DeliveryTime} dias úteis");
    Console.WriteLine($"  Prazo range: {servico.DeliveryRange?.Min}-{servico.DeliveryRange?.Max} dias");
}
```

---

### Cotação Simples com Produto(s)

Ao invés de informar as dimensões do pacote final, informe a lista de produtos e a API calculará o volume automaticamente:

```csharp
var request = new SfCalculateShippingRequest
{
    From = new SfCalculationOriginRequest { PostalCode = "01310100" },
    To = new SfCalculationDestinationRequest { PostalCode = "20040020" },
    Services = [SfShippingServiceType.Pac, SfShippingServiceType.Sedex],
    Products = [
        new SfCalculationProductRequest
        {
            Quantity = 2,
            Height = 10,
            Width = 10,
            Length = 15,
            Weight = 0.3
        },
        new SfCalculationProductRequest
        {
            Quantity = 1,
            Height = 5,
            Width = 20,
            Length = 30,
            Weight = 0.8
        }
    ]
};

var cotacoes = await client.Calculator.CalculateShippingAsync(request);
```

---

### Selecionando o Serviço Mais Barato

```csharp
var cotacoes = await client.Calculator.CalculateShippingAsync(request);

var maisBarato = cotacoes?
    .Where(c => !c.HasError && c.Price > 0)
    .OrderBy(c => c.Price)
    .FirstOrDefault();

if (maisBarato != null)
{
    Console.WriteLine($"Opção mais barata: {maisBarato.Name} — R$ {maisBarato.Price:F2}");
    Console.WriteLine($"ID do serviço: {maisBarato.Id}"); // Necessário para AddToCartAsync
}
```

---

### Selecionando o Serviço Mais Rápido

```csharp
var maiRapido = cotacoes?
    .Where(c => !c.HasError && c.DeliveryTime > 0)
    .OrderBy(c => c.DeliveryTime)
    .ThenBy(c => c.Price)
    .FirstOrDefault();
```

---

## Estrutura dos DTOs

### DTO de Entrada: `SfCalculateShippingRequest`

| Propriedade | Tipo | Obrigatório | Descrição |
| :--- | :--- | :--- | :--- |
| `From` | `SfCalculationOriginRequest` | Sim | CEP de origem. |
| `To` | `SfCalculationDestinationRequest` | Sim | CEP de destino. |
| `Services` | `SfShippingServiceType[]` | Sim | Serviços a cotar. Convertido para `"1,2,17"` automaticamente. |
| `Package` | `SfCalculationPackageRequest` | Condicional | Dimensões do pacote (use `Package` **ou** `Products`). |
| `Products` | `SfCalculationProductRequest[]?` | Condicional | Lista de produtos (alternativa a `Package`). |
| `Options` | `SfCalculationOptionsRequest?` | Não | Serviços adicionais e seguro. |

### DTO de Saída: `SfCalculateShippingResponse`

| Propriedade | Tipo | Descrição |
| :--- | :--- | :--- |
| `Id` | `int` | ID numérico do serviço (use para `SfAddToCartRequest.Service`). |
| `Name` | `string?` | Nome do serviço (ex: `"PAC"`, `"SEDEX"`). |
| `Price` | `decimal` | Preço total com desconto SuperFrete aplicado. |
| `Discount` | `decimal` | Valor do desconto obtido via SuperFrete. |
| `Currency` | `string?` | Moeda (ex: `"BRL"`). |
| `DeliveryTime` | `int` | Prazo em dias úteis. |
| `DeliveryRange` | `SfCalculationDeliveryRangeResponse?` | Faixa de prazo (min/max). |
| `Packages` | `SfCalculationPackageResultResponse[]?` | Detalhes dos pacotes calculados. |
| `AdditionalServices` | `SfCalculationAdditionalServicesResponse?` | Flags de mão própria e aviso de recebimento. |
| `Company` | `SfCalculationCompanyResponse?` | Dados da transportadora (id, nome, logo). |
| `HasError` | `bool` | `true` se o serviço não está disponível para esta rota. |

---

## Dicas para IAs ao utilizar .Calculator:

1. **`Id` do serviço:** O campo `Id` retornado em `SfCalculateShippingResponse` é o mesmo valor de `SfShippingServiceType` (ex: PAC=1, SEDEX=2). Use-o diretamente em `SfAddToCartRequest.Service` no próximo passo.
2. **Filtrar erros:** Sempre filtre `HasError == false` antes de exibir resultados ao usuário. Serviços indisponíveis para uma rota retornam `HasError = true` sem lançar exceção.
3. **`Package` vs `Products`:** Use `Package` quando já conhece as dimensões finais do pacote. Use `Products` para que a API calcule o volume com base nos itens individuais.
4. **CEP sem formatação:** O SDK aceita CEP com ou sem hífen (`"01310-100"` ou `"01310100"`).
5. **`InsuranceValue`:** Se `UseInsuranceValue = true`, o valor do seguro é calculado sobre `InsuranceValue`. Se `false`, o seguro é calculado sobre o valor declarado padrão.
6. **Namespace correto:** `using Berdsk.Sdk.SuperFrete.Services.Calculator.Dtos;` e `using Berdsk.Sdk.SuperFrete.Helpers;`.

---

[Anterior: SuperFreteClient](./01-superfrete-client.md) | [Início](./00-comece-aqui.md) | [Próximo: Cart](./03-cart.md)
