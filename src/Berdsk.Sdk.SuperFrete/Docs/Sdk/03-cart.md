---
tags: [carrinho, etiqueta, envio, remetente, destinatario, frete]
---
# .Cart: Adicionar Frete ao Carrinho

O serviço `.Cart` permite adicionar um envio ao carrinho da SuperFrete, gerando um pedido com status `pending`. Este é o segundo passo no fluxo de criação de etiquetas. Após adicionar ao carrinho, utilize o `.Checkout` para pagar e gerar a etiqueta.

## Métodos Disponíveis

| Método | Descrição | DTO Entrada | DTO Saída |
| :--- | :--- | :--- | :--- |
| `AddToCartAsync` | Adiciona um envio ao carrinho. | `SfAddToCartRequest` | `SfAddToCartResponse?` |

---

## Fluxo de Criação de Etiqueta

```
1. Calculator.CalculateShippingAsync() → obtém o Id do serviço e preço
2. Cart.AddToCartAsync()               → cria o pedido (status: pending)
3. Checkout.FinalizeOrderAsync()       → paga e gera a etiqueta (status: released)
```

---

## Exemplos de Uso

### Adicionar ao Carrinho (Exemplo Completo)

- **DTO de Entrada:** `SfAddToCartRequest`
- **DTO de Saída:** `SfAddToCartResponse`

```csharp
using Berdsk.Sdk.SuperFrete.Helpers;
using Berdsk.Sdk.SuperFrete.Services.Cart.Dtos;

var request = new SfAddToCartRequest
{
    // Remetente
    From = new SfCartSenderRequest
    {
        Name = "Loja Exemplo Ltda",
        PostalCode = "01310100",
        Address = "Avenida Paulista",
        Number = "1000",
        Complement = "Sala 42",
        District = "Bela Vista",
        City = "São Paulo",
        StateAbbr = "SP",
        Document = "12345678000195" // CNPJ ou CPF do remetente (opcional)
    },

    // Destinatário
    To = new SfCartRecipientRequest
    {
        Name = "João da Silva",
        PostalCode = "20040020",
        Address = "Rua da Assembleia",
        Number = "10",
        Complement = "Apto 301",
        District = "Centro",
        City = "Rio de Janeiro",
        StateAbbr = "RJ",
        Email = "joao@exemplo.com",   // Opcional
        Document = "98765432100"       // CPF ou CNPJ do destinatário
    },

    // ID do serviço obtido na cotação (Calculator.Id)
    Service = (int)SfShippingServiceType.Sedex, // 2

    // Volumes/pacotes
    Volumes = [
        new SfCartVolumeRequest
        {
            Height = 15,
            Width = 20,
            Length = 30,
            Weight = 1.5
        }
    ],

    // Plataforma de integração
    Platform = "MinhaLoja/1.0.0",

    // Opções adicionais (opcional)
    Options = new SfCartOptionsRequest
    {
        InsuranceValue = 150.00,  // Valor declarado para seguro
        Receipt = false,          // Aviso de recebimento
        OwnHand = false,          // Mão própria
        NonCommercial = false,    // Não comercial

        // Nota fiscal (opcional)
        Invoice = new SfCartInvoiceRequest
        {
            Number = "1234",
            Key = "35240101234567890001550010001234567890123456" // Chave NF-e (opcional)
        },

        // Tags para rastreamento interno (opcional)
        Tags = [
            new SfCartTagRequest
            {
                Tag = "pedido-001",
                Url = "https://meusite.com/pedidos/001"
            }
        ]
    }
};

var resultado = await client.Cart.AddToCartAsync(request);

if (resultado != null)
{
    Console.WriteLine($"Pedido criado! ID: {resultado.Id}");
    Console.WriteLine($"Preço: R$ {resultado.Price}");
    Console.WriteLine($"Status: {resultado.Status}"); // "pending"

    // Guarde o resultado.Id para usar no Checkout
}
```

---

### Exemplo Mínimo (Sem Opções)

Para casos simples sem nota fiscal ou tags:

```csharp
var request = new SfAddToCartRequest
{
    From = new SfCartSenderRequest
    {
        Name = "Remetente",
        PostalCode = "01310100",
        Address = "Av. Paulista",
        Number = "1",
        District = "Bela Vista",
        City = "São Paulo",
        StateAbbr = "SP"
    },
    To = new SfCartRecipientRequest
    {
        Name = "Destinatário",
        PostalCode = "20040020",
        Address = "Rua Exemplo",
        Number = "100",
        District = "Centro",
        City = "Rio de Janeiro",
        StateAbbr = "RJ",
        Document = "98765432100"
    },
    Service = (int)SfShippingServiceType.Pac,
    Volumes = [new SfCartVolumeRequest { Height = 10, Width = 15, Length = 20, Weight = 0.5 }],
    Platform = "MinhaLoja/1.0.0"
};

var resultado = await client.Cart.AddToCartAsync(request);
```

---

### Adicionando Produtos ao Carrinho

Quando o envio possui produtos declarados:

```csharp
var request = new SfAddToCartRequest
{
    From = new SfCartSenderRequest { /* ... */ },
    To = new SfCartRecipientRequest { /* ... */ },
    Service = (int)SfShippingServiceType.Jadlog,
    Products = [
        new SfCartProductRequest
        {
            Name = "Camiseta Azul",
            Quantity = 2,
            UnitaryValue = 49.90
        },
        new SfCartProductRequest
        {
            Name = "Calça Jeans",
            Quantity = 1,
            UnitaryValue = 120.00
        }
    ],
    Volumes = [new SfCartVolumeRequest { Height = 20, Width = 30, Length = 40, Weight = 1.2 }],
    Platform = "MinhaLoja/1.0.0",
    Options = new SfCartOptionsRequest
    {
        InsuranceValue = 220.00 // Soma dos produtos
    }
};
```

---

## Estrutura dos DTOs

### `SfAddToCartRequest`

| Propriedade | Tipo | Obrigatório | Descrição |
| :--- | :--- | :--- | :--- |
| `From` | `SfCartSenderRequest` | Sim | Dados do remetente. |
| `To` | `SfCartRecipientRequest` | Sim | Dados do destinatário. |
| `Service` | `int` | Sim | ID do serviço (use `(int)SfShippingServiceType.X`). |
| `Volumes` | `SfCartVolumeRequest[]` | Sim | Dimensões do(s) pacote(s). |
| `Platform` | `string` | Sim | Identificação da plataforma/sistema. |
| `Products` | `SfCartProductRequest[]?` | Não | Produtos para declaração. |
| `Options` | `SfCartOptionsRequest?` | Não | Seguro, mão própria, NF-e, tags. |

### `SfAddToCartResponse`

| Propriedade | Tipo | Descrição |
| :--- | :--- | :--- |
| `Id` | `string?` | ID do pedido criado. Use no `SfCheckoutRequest`. |
| `Price` | `string?` | Preço a pagar pelo frete. |
| `Status` | `string?` | Status inicial: `SfOrderStatus.Pending` (`"pending"`). |

---

## Dicas para IAs ao utilizar .Cart:

1. **Fluxo obrigatório:** O `.Cart` não gera etiqueta — ele apenas cria o pedido. Após `AddToCartAsync`, é **obrigatório** chamar `Checkout.FinalizeOrderAsync` para gerar a etiqueta.
2. **`Service` como int:** O campo `Service` recebe o ID numérico do serviço. Use `(int)SfShippingServiceType.Sedex` ou o valor `Id` retornado pelo `Calculator`.
3. **`Document` do destinatário:** CPF (11 dígitos) ou CNPJ (14 dígitos), sem pontuação.
4. **Múltiplos volumes:** O campo `Volumes` é um array. Para envios com várias caixas, adicione um `SfCartVolumeRequest` para cada caixa.
5. **`InsuranceValue`:** Defina sempre que o conteúdo tiver valor declarado. Isso afeta o cálculo do seguro.
6. **`Platform`:** Use um identificador do seu sistema (ex: `"MinhaLoja/2.1.0"`). Facilita o suporte da SuperFrete.
7. **Guardar o `Id`:** Salve o `resultado.Id` retornado — ele é necessário para o Checkout e para consultas futuras.
8. **Namespace:** `using Berdsk.Sdk.SuperFrete.Services.Cart.Dtos;` e `using Berdsk.Sdk.SuperFrete.Helpers;`.

---

[Anterior: Calculator](./02-calculator.md) | [Início](./00-comece-aqui.md) | [Próximo: Checkout](./04-checkout.md)
