---
tags: [servicos, transportadoras, restricoes, dimensoes, pac, sedex, jadlog, loggi]
---
# .ShippingServices: Informações Técnicas dos Serviços

O serviço `.ShippingServices` permite consultar os detalhes técnicos de cada serviço de transporte disponível na SuperFrete, incluindo restrições de peso, dimensões, valor de seguro e formatos de embalagem aceitos.

## Métodos Disponíveis

| Método | Descrição | DTO Entrada | DTO Saída |
| :--- | :--- | :--- | :--- |
| `GetServicesInfoAsync` | Retorna informações técnicas de todos os serviços. | — | `Dictionary<string, SfServiceInfoResponse>?` |

---

## Exemplos de Uso

### Listar Todos os Serviços e Suas Restrições

- **DTO de Saída:** `Dictionary<string, SfServiceInfoResponse>` — a chave é o ID numérico do serviço como string.

```csharp
using Berdsk.Sdk.SuperFrete.Services.ShippingServices.Dtos;

var servicos = await client.ShippingServices.GetServicesInfoAsync();

if (servicos == null || servicos.Count == 0)
{
    Console.WriteLine("Nenhum serviço disponível.");
    return;
}

foreach (var (serviceId, info) in servicos)
{
    Console.WriteLine($"=== {info.Name} (ID: {serviceId}) ===");
    Console.WriteLine($"Transportadora: {info.Company?.Name}");

    if (info.Restrictions != null)
    {
        var rest = info.Restrictions;

        Console.WriteLine($"Seguro máximo: R$ {rest.InsuranceValue?.Max}");

        if (rest.Formats?.Package != null)
        {
            var pkg = rest.Formats.Package;
            Console.WriteLine("Restrições do pacote:");
            Console.WriteLine($"  Peso: {pkg.Weight?.Min}kg – {pkg.Weight?.Max}kg");
            Console.WriteLine($"  Largura: {pkg.Width?.Min}cm – {pkg.Width?.Max}cm");
            Console.WriteLine($"  Altura: {pkg.Height?.Min}cm – {pkg.Height?.Max}cm");
            Console.WriteLine($"  Comprimento: {pkg.Length?.Min}cm – {pkg.Length?.Max}cm");
            Console.WriteLine($"  Soma máx. das dimensões: {pkg.Sum?.Max}cm");
        }
    }

    Console.WriteLine();
}
```

---

### Consultar um Serviço Específico por ID

```csharp
using Berdsk.Sdk.SuperFrete.Helpers;

var servicos = await client.ShippingServices.GetServicesInfoAsync();

// Buscar SEDEX (ID = 2)
var sedexId = ((int)SfShippingServiceType.Sedex).ToString();

if (servicos != null && servicos.TryGetValue(sedexId, out var sedex))
{
    Console.WriteLine($"Serviço: {sedex.Name}");
    Console.WriteLine($"Peso máximo: {sedex.Restrictions?.Formats?.Package?.Weight?.Max}kg");
    Console.WriteLine($"Logo: {sedex.Company?.Picture}");
}
```

---

### Validar se uma Encomenda Cabe em um Serviço

```csharp
var servicos = await client.ShippingServices.GetServicesInfoAsync();

double pesoKg = 1.5;
double altCm = 20;
double largCm = 30;
double compCm = 40;

foreach (var (serviceId, info) in servicos ?? [])
{
    var pkg = info.Restrictions?.Formats?.Package;
    if (pkg == null) continue;

    bool pesoOk = pesoKg >= (pkg.Weight?.Min ?? 0) && pesoKg <= (pkg.Weight?.Max ?? double.MaxValue);
    bool altOk = altCm >= (pkg.Height?.Min ?? 0) && altCm <= (pkg.Height?.Max ?? double.MaxValue);
    bool largOk = largCm >= (pkg.Width?.Min ?? 0) && largCm <= (pkg.Width?.Max ?? double.MaxValue);
    bool compOk = compCm >= (pkg.Length?.Min ?? 0) && compCm <= (pkg.Length?.Max ?? double.MaxValue);

    if (pesoOk && altOk && largOk && compOk)
    {
        Console.WriteLine($"{info.Name} (ID {serviceId}): ACEITA esta encomenda");
    }
    else
    {
        Console.WriteLine($"{info.Name} (ID {serviceId}): NÃO aceita esta encomenda");
    }
}
```

---

## Estrutura dos DTOs

### `SfServiceInfoResponse` (valor do Dictionary)

| Propriedade | Tipo | Descrição |
| :--- | :--- | :--- |
| `Name` | `string?` | Nome do serviço (ex: `"PAC"`, `"SEDEX"`). |
| `Type` | `string?` | Tipo interno do serviço. |
| `Range` | `SfMinMaxRangeResponse?` | Faixa de IDs do serviço. |
| `Restrictions` | `SfServiceRestrictionsResponse?` | Restrições de seguro e formatos. |
| `Requirements` | `object?` | Requisitos do serviço (variam por transportadora). |
| `Optionals` | `object?` | Serviços opcionais disponíveis. |
| `Company` | `SfServiceCompanyResponse?` | Dados da transportadora. |

### `SfServiceRestrictionsResponse`

| Propriedade | Tipo | Descrição |
| :--- | :--- | :--- |
| `InsuranceValue` | `SfMinMaxRangeResponse?` | Valor mínimo e máximo de seguro aceito (R$). |
| `Formats` | `SfServiceFormatsResponse?` | Formatos de embalagem disponíveis. |

### `SfServiceFormatsResponse`

| Propriedade | Tipo | Descrição |
| :--- | :--- | :--- |
| `Package` | `SfPackageRestrictionsResponse?` | Restrições para o formato caixa/pacote. |

### `SfPackageRestrictionsResponse`

| Propriedade | Tipo | Descrição |
| :--- | :--- | :--- |
| `Weight` | `SfMinMaxRangeResponse?` | Faixa de peso aceita (kg). |
| `Width` | `SfMinMaxRangeResponse?` | Faixa de largura aceita (cm). |
| `Height` | `SfMinMaxRangeResponse?` | Faixa de altura aceita (cm). |
| `Length` | `SfMinMaxRangeResponse?` | Faixa de comprimento aceito (cm). |
| `Sum` | `SfMinMaxRangeResponse?` | Soma máxima das dimensões (altura + largura + comprimento). |

### `SfMinMaxRangeResponse`

| Propriedade | Tipo | Descrição |
| :--- | :--- | :--- |
| `Min` | `double?` | Valor mínimo. |
| `Max` | `double?` | Valor máximo. |

### `SfServiceCompanyResponse`

| Propriedade | Tipo | Descrição |
| :--- | :--- | :--- |
| `Name` | `string?` | Nome da transportadora. |
| `Picture` | `string?` | URL da logo da transportadora. |

---

## Serviços Disponíveis

| ID | Enum | Nome | Transportadora |
| :--- | :--- | :--- | :--- |
| `1` | `SfShippingServiceType.Pac` | PAC | Correios |
| `2` | `SfShippingServiceType.Sedex` | SEDEX | Correios |
| `3` | `SfShippingServiceType.Jadlog` | Jadlog | Jadlog |
| `17` | `SfShippingServiceType.MiniEnvios` | Mini Envios | Correios |
| `31` | `SfShippingServiceType.Loggi` | Loggi | Loggi |

---

## Dicas para IAs ao utilizar .ShippingServices:

1. **Chave do Dictionary:** A chave do dicionário retornado é o ID do serviço como string (ex: `"1"` para PAC, `"2"` para SEDEX). Use `((int)SfShippingServiceType.Pac).ToString()` para construir a chave de forma tipada.
2. **Cache dos dados:** As informações de serviços raramente mudam. Considere cachear o resultado de `GetServicesInfoAsync()` por algumas horas para evitar chamadas desnecessárias.
3. **Validação preventiva:** Use este serviço para validar dimensões e peso **antes** de chamar `Calculator.CalculateShippingAsync()`. Isso evita erros e melhora a experiência do usuário ao detectar incompatibilidades antecipadamente.
4. **`Sum`:** O campo `Sum.Max` representa a soma máxima de `altura + largura + comprimento`. Alguns serviços têm limite mesmo que as dimensões individuais sejam válidas.
5. **Namespace:** `using Berdsk.Sdk.SuperFrete.Services.ShippingServices.Dtos;` e `using Berdsk.Sdk.SuperFrete.Helpers;`.

---

[Anterior: Users](./07-users.md) | [Início](./00-comece-aqui.md) | [Próximo: Helpers](./09-helpers.md)
