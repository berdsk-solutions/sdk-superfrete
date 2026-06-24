---
tags: [checkout, pagamento, saldo, etiqueta, pedido]
---
# .Checkout: Pagar Etiquetas com Saldo

O serviço `.Checkout` finaliza o fluxo de criação de etiquetas. Ele debita o saldo da conta SuperFrete e libera a geração das etiquetas de envio para os pedidos informados. Este é o terceiro e último passo no fluxo de criação de etiquetas.

## Métodos Disponíveis

| Método | Descrição | DTO Entrada | DTO Saída |
| :--- | :--- | :--- | :--- |
| `FinalizeOrderAsync` | Paga um ou mais pedidos do carrinho com saldo. | `SfCheckoutRequest` | `SfCheckoutResponse?` |

---

## Exemplos de Uso

### Checkout Simples (1 pedido)

- **DTO de Entrada:** `SfCheckoutRequest`
- **DTO de Saída:** `SfCheckoutResponse`

```csharp
using Berdsk.Sdk.SuperFrete.Services.Checkout.Dtos;

// orderId obtido em Cart.AddToCartAsync().Id
var request = new SfCheckoutRequest
{
    Orders = ["ord_abc123xyz456"]
};

var resultado = await client.Checkout.FinalizeOrderAsync(request);

if (resultado?.Success == true)
{
    Console.WriteLine("Checkout realizado com sucesso!");
    Console.WriteLine($"Status da compra: {resultado.Purchase?.Status}");

    foreach (var pedido in resultado.Purchase?.Orders ?? [])
    {
        Console.WriteLine($"Pedido: {pedido.Id}");
        Console.WriteLine($"Preço: R$ {pedido.Price}");
        Console.WriteLine($"Desconto: R$ {pedido.Discount}");
        Console.WriteLine($"Rastreamento: {pedido.Tracking}");
        Console.WriteLine($"Link de impressão: {pedido.Print?.Url}");
    }
}
```

---

### Checkout em Lote (Múltiplos Pedidos)

É possível pagar vários pedidos do carrinho em uma única chamada, reduzindo o custo de transações:

```csharp
// Lista de IDs obtidos em chamadas anteriores ao Cart.AddToCartAsync
var orderIds = new[]
{
    "ord_abc123",
    "ord_def456",
    "ord_ghi789"
};

var request = new SfCheckoutRequest { Orders = orderIds };
var resultado = await client.Checkout.FinalizeOrderAsync(request);

if (resultado?.Success == true)
{
    foreach (var pedido in resultado.Purchase?.Orders ?? [])
    {
        // Abrir URL de impressão
        Console.WriteLine($"Imprimir etiqueta {pedido.Id}: {pedido.Print?.Url}");
    }
}
```

---

### Fluxo Completo: Calculator → Cart → Checkout

```csharp
// Passo 1: Cotar
var cotacoes = await client.Calculator.CalculateShippingAsync(new SfCalculateShippingRequest
{
    From = new SfCalculationOriginRequest { PostalCode = "01310100" },
    To = new SfCalculationDestinationRequest { PostalCode = "20040020" },
    Services = [SfShippingServiceType.Pac, SfShippingServiceType.Sedex],
    Package = new SfCalculationPackageRequest { Height = 15, Width = 15, Length = 20, Weight = 0.5 }
});

var maisBarato = cotacoes?.Where(c => !c.HasError).OrderBy(c => c.Price).FirstOrDefault();
if (maisBarato == null) return;

// Passo 2: Adicionar ao Carrinho
var carrinho = await client.Cart.AddToCartAsync(new SfAddToCartRequest
{
    From = new SfCartSenderRequest { Name = "Loja", PostalCode = "01310100", Address = "Av. Paulista", Number = "1", District = "Bela Vista", City = "São Paulo", StateAbbr = "SP" },
    To = new SfCartRecipientRequest { Name = "Cliente", PostalCode = "20040020", Address = "Rua Centro", Number = "10", District = "Centro", City = "Rio de Janeiro", StateAbbr = "RJ", Document = "98765432100" },
    Service = maisBarato.Id,
    Volumes = [new SfCartVolumeRequest { Height = 15, Width = 15, Length = 20, Weight = 0.5 }],
    Platform = "MinhaLoja/1.0.0"
});

if (carrinho?.Id == null) return;

// Passo 3: Checkout
var checkout = await client.Checkout.FinalizeOrderAsync(new SfCheckoutRequest
{
    Orders = [carrinho.Id]
});

if (checkout?.Success == true)
{
    var etiqueta = checkout.Purchase?.Orders?.FirstOrDefault();
    Console.WriteLine($"Etiqueta gerada! Rastreio: {etiqueta?.Tracking}");
    Console.WriteLine($"Imprimir: {etiqueta?.Print?.Url}");
}
```

---

## Estrutura dos DTOs

### `SfCheckoutRequest`

| Propriedade | Tipo | Obrigatório | Descrição |
| :--- | :--- | :--- | :--- |
| `Orders` | `string[]` | Sim | IDs dos pedidos a pagar (obtidos no `Cart.AddToCartAsync`). |

### `SfCheckoutResponse`

| Propriedade | Tipo | Descrição |
| :--- | :--- | :--- |
| `Success` | `bool?` | `true` se o checkout foi concluído com sucesso. |
| `Purchase` | `SfCheckoutPurchaseResponse?` | Detalhes da compra. |

### `SfCheckoutPurchaseResponse`

| Propriedade | Tipo | Descrição |
| :--- | :--- | :--- |
| `Status` | `string?` | Status da compra. |
| `Orders` | `SfCheckoutOrderResponse[]?` | Lista de pedidos pagos. |

### `SfCheckoutOrderResponse`

| Propriedade | Tipo | Descrição |
| :--- | :--- | :--- |
| `Id` | `string?` | ID do pedido. |
| `Price` | `decimal?` | Valor pago. |
| `Discount` | `decimal?` | Desconto aplicado. |
| `ServiceId` | `int?` | ID do serviço de transporte. |
| `Tracking` | `string?` | Código de rastreamento. |
| `Print` | `SfCheckoutPrintResponse?` | Objeto com a URL de impressão da etiqueta. |

### `SfCheckoutPrintResponse`

| Propriedade | Tipo | Descrição |
| :--- | :--- | :--- |
| `Url` | `string?` | URL para download/impressão da etiqueta em PDF. |

---

## Dicas para IAs ao utilizar .Checkout:

1. **Saldo insuficiente:** Se o usuário não tiver saldo suficiente na conta SuperFrete, a API retornará um erro (geralmente `400`). Verifique o saldo em `Users.GetUserInfoAsync()` antes do checkout.
2. **Múltiplos pedidos em lote:** Sempre que possível, agrupe pedidos em um único checkout. Isso é mais eficiente do que chamar `FinalizeOrderAsync` individualmente para cada pedido.
3. **Guardar o Tracking:** Após o checkout bem-sucedido, persista o `Tracking` retornado em `SfCheckoutOrderResponse.Tracking`. Esse código é necessário para rastrear a encomenda.
4. **URL de impressão:** A propriedade `Print.Url` contém o link para o PDF da etiqueta. Abra no browser ou redirecione o usuário para download.
5. **`Success = false`:** Verifique sempre `resultado?.Success == true` antes de acessar `Purchase`. Um checkout mal-sucedido retorna `Success = false`.
6. **Namespace:** `using Berdsk.Sdk.SuperFrete.Services.Checkout.Dtos;`.

---

[Anterior: Cart](./03-cart.md) | [Início](./00-comece-aqui.md) | [Próximo: Orders](./05-orders.md)
