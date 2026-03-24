# Berdsk.Sdk.SuperFrete

![SuperFrete](resources/superfrete.png)

A **Berdsk.Sdk.SuperFrete** é uma biblioteca .NET não oficial, desenvolvida pela **Berdsk**, para facilitar a integração com a API da [SuperFrete](https://superfrete.com/).

Com este SDK, você pode calcular fretes, gerenciar carrinhos, emitir etiquetas e gerenciar webhooks de forma simples e fluída em suas aplicações .NET.

---

## 🚀 Recursos

- 📦 **Cálculo de Frete:** Consulte preços e prazos de diversos serviços dos Correios.
- 🛒 **Gestão de Carrinho:** Adicione envios ao carrinho da SuperFrete.
- 💳 **Checkout:** Realize o pagamento de etiquetas utilizando seu saldo.
- 📄 **Impressão:** Obtenha links para impressão de etiquetas em PDF.
- ⚓ **Webhooks:** Configure e gerencie notificações de eventos.
- 🛡️ **Seguro e Simples:** Tipagem forte para todas as requisições e respostas.

---

## 📦 Instalação

Instale o pacote via NuGet:

```bash
dotnet add package Berdsk.Sdk.SuperFrete
```

---

## 🛠️ Como Usar

### Inicializando o Cliente

```csharp
using Berdsk.Sdk.SuperFrete;
using Berdsk.Sdk.SuperFrete.Models;

var client = new SuperFreteClient(
    token: "SEU_TOKEN_AQUI",
    environment: SuperFreteEnvironment.Sandbox,
    appName: "MinhaApp",
    appVersion: "1.0.0",
    contactEmail: "contato@meudominio.com"
);
```

### Calculando Frete

```csharp
var request = new CalculatorRequest
{
    From = "01001000",
    To = "20040000",
    Services = "1,2,17", // SEDEX, PAC, Mini Envios
    Package = new PackageDimensions
    {
        Weight = 0.5,
        Width = 15,
        Height = 10,
        Length = 20
    }
};

var results = await client.CalculateShippingAsync(request);

foreach (var service in results)
{
    Console.WriteLine($"{service.Name}: R$ {service.Price} - Prazo: {service.DeliveryTime} dias");
}
```

---

## 📄 Licença

Este projeto está licenciado sob a [Licença MIT](LICENSE).

---

## 🏢 Sobre a Berdsk

![Berdsk](resources/berdsk-brand.png)

A **Berdsk** foca em criar soluções tecnológicas eficientes e SDKs de alta qualidade para o ecossistema .NET.

Visite nosso site: [berdsk.com.br](https://berdsk.com.br)

---

> **Aviso:** Esta é uma biblioteca independente e não possui vínculo oficial com a SuperFrete. Todos os direitos da marca SuperFrete pertencem aos seus respectivos proprietários.
