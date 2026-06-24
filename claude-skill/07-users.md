---
tags: [usuario, conta, saldo, enderecos, perfil]
---
# .Users: Dados do Usuário

O serviço `.Users` permite consultar informações do usuário autenticado pelo token de integração, incluindo dados de perfil, saldo disponível e endereços cadastrados na conta SuperFrete.

## Métodos Disponíveis

| Método | Descrição | DTO Entrada | DTO Saída |
| :--- | :--- | :--- | :--- |
| `GetUserInfoAsync` | Retorna os dados do usuário autenticado. | — | `SfUserResponse?` |
| `GetAddressesAsync` | Retorna os endereços cadastrados na conta. | — | `List<SfUserAddressResponse>?` |

---

## Exemplos de Uso

### Obter Dados do Usuário

Recupera o perfil completo do usuário, incluindo saldo e limites de envio.

- **DTO de Saída:** `SfUserResponse`

```csharp
var usuario = await client.Users.GetUserInfoAsync();

if (usuario != null)
{
    Console.WriteLine($"ID: {usuario.Id}");
    Console.WriteLine($"Nome: {usuario.Firstname} {usuario.Lastname}");
    Console.WriteLine($"Email: {usuario.Email}");
    Console.WriteLine($"Telefone: {usuario.Phone}");
    Console.WriteLine($"Documento: {usuario.Document}");

    // Saldo e limites
    Console.WriteLine($"Saldo disponível: R$ {usuario.Balance}");
    Console.WriteLine($"Envios total: {usuario.Limits?.Shipments}");
    Console.WriteLine($"Envios disponíveis: {usuario.Limits?.ShipmentsAvailable}");
}
```

---

### Verificar Saldo Antes do Checkout

Boa prática: verificar o saldo disponível antes de tentar fazer checkout para dar feedback antecipado ao usuário.

```csharp
var usuario = await client.Users.GetUserInfoAsync();
decimal saldo = usuario?.Balance ?? 0;

// Assumindo que você já calculou o preço total dos pedidos
decimal precoTotal = 35.90m;

if (saldo < precoTotal)
{
    Console.WriteLine($"Saldo insuficiente. Disponível: R$ {saldo:F2} | Necessário: R$ {precoTotal:F2}");
    Console.WriteLine("Recarregue seu saldo em: https://web.superfrete.com");
}
else
{
    // Prosseguir com o checkout
    var checkout = await client.Checkout.FinalizeOrderAsync(new SfCheckoutRequest
    {
        Orders = ["ord_abc123"]
    });
}
```

---

### Listar Endereços Cadastrados

Retorna todos os endereços salvos no perfil do usuário. Útil para pré-preencher o campo remetente (`From`) ao criar envios.

- **DTO de Saída:** `List<SfUserAddressResponse>`

```csharp
var enderecos = await client.Users.GetAddressesAsync();

if (enderecos == null || enderecos.Count == 0)
{
    Console.WriteLine("Nenhum endereço cadastrado.");
    return;
}

foreach (var endereco in enderecos)
{
    Console.WriteLine($"ID: {endereco.Id}");
    Console.WriteLine($"CEP: {endereco.PostalCode}");
    Console.WriteLine($"Logradouro: {endereco.Address}, {endereco.Number}");
    if (!string.IsNullOrEmpty(endereco.Complement))
        Console.WriteLine($"Complemento: {endereco.Complement}");
    Console.WriteLine($"Bairro: {endereco.District}");
    Console.WriteLine($"Cidade: {endereco.City} - {endereco.StateAbbr}");
    Console.WriteLine("---");
}
```

---

### Usar Endereço Salvo como Remetente

```csharp
var enderecos = await client.Users.GetAddressesAsync();
var enderecoPrincipal = enderecos?.FirstOrDefault(); // Primeiro endereço cadastrado

if (enderecoPrincipal != null)
{
    var cartRequest = new SfAddToCartRequest
    {
        From = new SfCartSenderRequest
        {
            Name = "Minha Loja",
            PostalCode = enderecoPrincipal.PostalCode ?? "",
            Address = enderecoPrincipal.Address ?? "",
            Number = enderecoPrincipal.Number ?? "",
            Complement = enderecoPrincipal.Complement,
            District = enderecoPrincipal.District ?? "",
            City = enderecoPrincipal.City ?? "",
            StateAbbr = enderecoPrincipal.StateAbbr ?? ""
        },
        // ... restante dos campos
    };
}
```

---

## Estrutura dos DTOs

### `SfUserResponse`

| Propriedade | Tipo | Descrição |
| :--- | :--- | :--- |
| `Id` | `string?` | ID único do usuário na SuperFrete. |
| `Firstname` | `string?` | Primeiro nome do usuário. |
| `Lastname` | `string?` | Sobrenome do usuário. |
| `Phone` | `string?` | Telefone cadastrado. |
| `Email` | `string?` | E-mail da conta. |
| `Document` | `string?` | CPF ou CNPJ do usuário. |
| `Balance` | `decimal?` | Saldo disponível em reais (R$). |
| `Limits` | `SfUserLimitsResponse?` | Limites de envio da conta. |

### `SfUserLimitsResponse`

| Propriedade | Tipo | Descrição |
| :--- | :--- | :--- |
| `Shipments` | `int?` | Total de envios permitidos no plano. |
| `ShipmentsAvailable` | `int?` | Envios ainda disponíveis no período. |

### `SfUserAddressResponse`

| Propriedade | Tipo | Descrição |
| :--- | :--- | :--- |
| `Id` | `string?` | ID único do endereço. |
| `PostalCode` | `string?` | CEP do endereço. |
| `Address` | `string?` | Logradouro (rua, avenida, etc.). |
| `Number` | `string?` | Número do imóvel. |
| `Complement` | `string?` | Complemento (apto, sala, etc.). |
| `District` | `string?` | Bairro. |
| `City` | `string?` | Cidade. |
| `StateAbbr` | `string?` | Sigla do estado (ex: `"SP"`, `"RJ"`). |

---

## Dicas para IAs ao utilizar .Users:

1. **Verificar saldo antes do checkout:** Sempre consulte `GetUserInfoAsync()` e verifique `Balance` antes de chamar `Checkout.FinalizeOrderAsync()`. Isso evita erros de saldo insuficiente e melhora a experiência do usuário.
2. **Endereços pré-cadastrados:** Use `GetAddressesAsync()` para popular o campo `From` no `SfAddToCartRequest`. O usuário provavelmente já tem o endereço de sua empresa cadastrado na SuperFrete.
3. **Limites de envio:** `ShipmentsAvailable` indica quantos envios ainda estão disponíveis no plano atual. Se for zero, o usuário precisará fazer upgrade antes de criar novos pedidos.
4. **ID do usuário:** O `Id` do usuário pode ser necessário em integrações avançadas, como suporte técnico ou auditoria de logs.
5. **Namespace:** `using Berdsk.Sdk.SuperFrete.Services.Users.Dtos;`.

---

[Anterior: Webhooks](./06-webhooks.md) | [Início](./00-comece-aqui.md) | [Próximo: ShippingServices](./08-shipping-services.md)
