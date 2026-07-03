---
tags: [erros, excecoes, tratamento-de-erros, depuracao, http, validacao]
---
# Exceptions: Tratamento de Erros e Exceções

O SDK utiliza um sistema de exceções tipadas para facilitar a identificação e o tratamento de erros retornados pela API SuperFrete. Todas as exceções específicas herdam da classe base `SuperFreteException`.

## Hierarquia de Exceções

| Exceção | Código HTTP | Causa Provável |
| :--- | :--- | :--- |
| `SfBadRequestException` | 400 | Dados inválidos, CEP inexistente, saldo insuficiente. |
| `SfUnauthorizedException` | 401 | Token inválido, expirado ou ausente. |
| `SfForbiddenException` | 403 | Sem permissão para acessar o recurso. |
| `SfNotFoundException` | 404 | Pedido, webhook ou recurso não encontrado. |
| `SfTooManyRequestsException` | 429 | Rate limit atingido — muitas requisições em pouco tempo. |
| `SfInternalServerErrorException` | 500 | Erro interno nos servidores da SuperFrete. |
| `SuperFreteException` | Outros | Exceção genérica para códigos HTTP não mapeados acima. |

---

## Estrutura da Exceção Base (`SuperFreteException`)

Ao capturar uma `SuperFreteException`, você tem acesso a detalhes sobre o erro:

| Propriedade | Tipo | Descrição |
| :--- | :--- | :--- |
| `StatusCode` | `HttpStatusCode` | Código HTTP retornado pela API. |
| `ErrorResponse` | `SfErrorResponse?` | Corpo deserializado do erro (quando disponível). |
| `Message` | `string` | Mensagem descritiva do erro. |

### `SfErrorResponse`

| Propriedade | Tipo | Descrição |
| :--- | :--- | :--- |
| `Message` | `string?` | Descrição textual do erro retornada pela API. |
| `Code` | `string?` | Código do erro (quando fornecido pela API). |
| `Errors` | `Dictionary<string, string[]>?` | Erros por campo (comum em erros de validação 400). |

---

## Exemplos de Uso

### 1. Tratamento Genérico (Recomendado como fallback)

Ideal para logs e respostas rápidas ao usuário. Captura qualquer erro da API.

```csharp
try
{
    var cotacoes = await client.Calculator.CalculateShippingAsync(request);
}
catch (SuperFreteException ex)
{
    Console.WriteLine($"Erro na API SuperFrete: {ex.Message}");
    Console.WriteLine($"Status HTTP: {(int)ex.StatusCode}");

    if (ex.ErrorResponse?.Errors != null)
    {
        foreach (var campo in ex.ErrorResponse.Errors)
        {
            Console.WriteLine($"Campo [{campo.Key}]: {string.Join(", ", campo.Value)}");
        }
    }
}
```

---

### 2. Captura Específica por Código HTTP

Útil para fluxos de negócio que variam conforme o tipo de erro.

```csharp
try
{
    var resultado = await client.Cart.AddToCartAsync(cartRequest);
}
catch (SfUnauthorizedException)
{
    Console.WriteLine("Token inválido ou expirado. Gere um novo token na SuperFrete.");
}
catch (SfBadRequestException ex)
{
    Console.WriteLine($"Dados inválidos: {ex.ErrorResponse?.Message}");
    // Pode indicar saldo insuficiente, CEP inválido, etc.
}
catch (SfNotFoundException)
{
    Console.WriteLine("Serviço ou recurso não encontrado.");
}
catch (SfTooManyRequestsException)
{
    Console.WriteLine("Muitas requisições. Aguarde alguns segundos e tente novamente.");
    // Implemente retry com backoff exponencial
}
catch (SfInternalServerErrorException)
{
    Console.WriteLine("Erro interno na SuperFrete. Tente novamente em alguns minutos.");
}
catch (SuperFreteException ex)
{
    // Fallback para qualquer outro erro da API
    Console.WriteLine($"Erro inesperado [{(int)ex.StatusCode}]: {ex.Message}");
}
```

---

### 3. Fluxo Completo com Tratamento de Erros

```csharp
public async Task<string?> GerarEtiquetaAsync(string cepOrigem, string cepDestino, double pesoKg)
{
    try
    {
        // Cotação
        var cotacoes = await client.Calculator.CalculateShippingAsync(new SfCalculateShippingRequest
        {
            From = new SfCalculationOriginRequest { PostalCode = cepOrigem },
            To = new SfCalculationDestinationRequest { PostalCode = cepDestino },
            Services = [SfShippingServiceType.Pac, SfShippingServiceType.Sedex],
            Package = new SfCalculationPackageRequest { Height = 15, Width = 15, Length = 20, Weight = pesoKg }
        });

        var melhorOpcao = cotacoes?.Where(c => !c.HasError).OrderBy(c => c.Price).FirstOrDefault();
        if (melhorOpcao == null) return null;

        // Carrinho
        var pedido = await client.Cart.AddToCartAsync(new SfAddToCartRequest
        {
            From = new SfCartSenderRequest { /* ... */ },
            To = new SfCartRecipientRequest { /* ... */ },
            Service = melhorOpcao.Id,
            Volumes = [new SfCartVolumeRequest { Height = 15, Width = 15, Length = 20, Weight = pesoKg }],
            Platform = "MinhaLoja/1.0.0"
        });

        // Checkout
        var checkout = await client.Checkout.FinalizeOrderAsync(new SfCheckoutRequest
        {
            Orders = [pedido!.Id!]
        });

        return checkout?.Purchase?.Orders?.FirstOrDefault()?.Print?.Url;
    }
    catch (SfBadRequestException ex)
    {
        // Saldo insuficiente ou dados inválidos
        throw new InvalidOperationException($"Falha ao gerar etiqueta: {ex.ErrorResponse?.Message}", ex);
    }
    catch (SfUnauthorizedException)
    {
        throw new UnauthorizedAccessException("Token SuperFrete inválido ou expirado.");
    }
    catch (SuperFreteException ex)
    {
        // Log e re-throw para tratamento upstream
        Console.Error.WriteLine($"Erro SuperFrete [{(int)ex.StatusCode}]: {ex.Message}");
        throw;
    }
}
```

---

### 4. Retry com Backoff para Rate Limit

```csharp
const int maxTentativas = 3;
int tentativa = 0;
SfCalculateShippingResponse[]? resultado = null;

while (tentativa < maxTentativas)
{
    try
    {
        var cotacoes = await client.Calculator.CalculateShippingAsync(request);
        resultado = cotacoes?.ToArray();
        break;
    }
    catch (SfTooManyRequestsException)
    {
        tentativa++;
        if (tentativa >= maxTentativas) throw;
        await Task.Delay(TimeSpan.FromSeconds(Math.Pow(2, tentativa))); // 2s, 4s, 8s
    }
}
```

---

## Dicas para IAs ao lidar com Exceptions:

1. **Sempre use try-catch:** Ao sugerir código de integração com o SDK, envolva as chamadas em blocos `try-catch`. A API SuperFrete pode retornar erros mesmo para chamadas bem formadas (ex: saldo insuficiente, CEP inválido, rate limit).
2. **`SfBadRequestException` (400):** Este é o erro mais comum em produção. Pode indicar: CEP inválido, saldo insuficiente para checkout, dimensões fora do limite do serviço, dados obrigatórios ausentes.
3. **`SfUnauthorizedException` (401):** O token está errado, expirado ou foi revogado. Oriente o usuário a gerar um novo token no painel da SuperFrete.
4. **`SfTooManyRequestsException` (429):** Implemente retry com backoff exponencial. Nunca faça loop infinito de retentativas.
5. **`SfInternalServerErrorException` (500):** Erro do lado da SuperFrete. Não há nada a fazer no código. Registre nos logs e notifique se persistir.
6. **`ErrorResponse?.Errors`:** Para erros 400, sempre inspecione `ex.ErrorResponse?.Errors` — ele contém os campos específicos que causaram a rejeição.
7. **Namespace:** `using Berdsk.Sdk.SuperFrete;` (a exceção base `SuperFreteException` e as subclasses estão no namespace raiz do SDK).

---

[Anterior: Helpers](./09-helpers.md) | [Início](./00-comece-aqui.md) | [Próximo: Tracking](./11-tracking.md)
