---
tags: [rastreamento, tracking, rastreio, eventos, entrega, comprovante, publico, sem-autenticacao]
---
# .Tracking: Rastreamento Público de Envios

O rastreamento usa uma **API separada e pública** (`https://rastreamento.superfrete.com/`) que **não exige autenticação**. Por isso, ele tem um cliente próprio: `SuperFreteTrackingClient` — **não** faz parte do `SuperFreteClient` e **não precisa de token**.

## Instanciação

```csharp
using Berdsk.Sdk.SuperFrete;

// Direto — sem token
var trackingClient = new SuperFreteTrackingClient(
    appName: "MinhaApp",          // opcional — User-Agent
    appVersion: "1.0.0",          // opcional
    contactEmail: "contato@meudominio.com" // opcional
);

// ASP.NET Core — Singleton via DI (obrigatório em produção)
builder.Services.AddSingleton<SuperFreteTrackingClient>(_ =>
    new SuperFreteTrackingClient(appName: "MinhaApp", appVersion: "1.0.0"));
```

## Métodos Disponíveis

| Método | Descrição | Entrada | DTO Saída |
| :--- | :--- | :--- | :--- |
| `GetTrackingAsync` | Rastreamento completo de um envio pelo código de rastreio. | `string trackingCode` | `SfTrackingResponse?` |

---

## Exemplo de Uso

```csharp
using Berdsk.Sdk.SuperFrete;
using Berdsk.Sdk.SuperFrete.Services.Tracking.Dtos;

var resultado = await trackingClient.Tracking.GetTrackingAsync("13191900413840");

if (resultado != null)
{
    // Status geral
    Console.WriteLine($"Atrasado: {resultado.IsDelayed} ({resultado.DelayDays} dias)");

    // Status e previsão da transportadora
    var provider = resultado.ProviderTracking;
    Console.WriteLine($"Transportadora: {provider?.Provider}");        // "jadlog", "loggi"...
    Console.WriteLine($"Status: {provider?.ShipmentStatus}");          // "Entregue", "Em trânsito"...
    Console.WriteLine($"Previsão: {provider?.EstimatedDelivery:d}");

    // Linha do tempo (eventos do mais recente para o mais antigo)
    foreach (var evento in provider?.Tracking?.Events ?? [])
    {
        Console.WriteLine($"[{evento.Date:g}] ({evento.TrackingOrigin}) {evento.Status} — {evento.Unit}");
    }

    // Dados do envio
    var tracking = resultado.Tracking;
    Console.WriteLine($"Etiqueta: {tracking?.Label}");
    Console.WriteLine($"Destinatário: {tracking?.RecipientName}");
    Console.WriteLine($"Modalidade: {tracking?.ShippingModality}");

    // Comprovante de entrega (foto/recibo), quando disponível
    foreach (var link in resultado.DeliveryProof?.Links ?? [])
    {
        Console.WriteLine($"Comprovante ({link.Rel}): {link.Href}");
    }
}
```

### Tratando Código Inexistente

```csharp
try
{
    var resultado = await trackingClient.Tracking.GetTrackingAsync(codigo);
}
catch (SfNotFoundException)
{
    Console.WriteLine("Código de rastreio não encontrado.");
}
```

`trackingCode` vazio ou em branco lança `ArgumentException` antes de chamar a API.

---

## Estrutura dos DTOs

Todos os DTOs estão em `Berdsk.Sdk.SuperFrete.Services.Tracking.Dtos`. Todos os campos são nullable — a API não documenta nulabilidade.

### `SfTrackingResponse` (raiz)

| Propriedade | Tipo | Descrição |
| :--- | :--- | :--- |
| `SuperFrete` | `bool?` | Envio feito pela SuperFrete. |
| `IsDelayed` | `bool?` | Entrega atrasada. |
| `DelayDays` | `int?` | Dias de atraso. |
| `ApplicationId` / `ApplicationName` / `ApplicationDisplayName` | `int?` / `string?` | Aplicação que originou a consulta. |
| `Tracking` | `SfTrackingDetailsResponse?` | Dados do envio na SuperFrete. |
| `ProviderTracking` | `SfProviderTrackingResponse?` | Rastreamento da transportadora. |
| `DeliveryProof` | `SfDeliveryProofResponse?` | Comprovantes de entrega. |
| `TicketHistory` | `List<SfTicketHistoryResponse>?` | Histórico de chamados na SuperFrete. |
| `CarrierReplyDeliveryForecasts` | `List<JsonElement>?` | **Schema desconhecido** — exposto como JSON bruto. |
| `TicketCreated` | `bool?` | Chamado criado automaticamente. Pode vir ausente. |

### `SfTrackingDetailsResponse` (campo `tracking`)

| Propriedade | Tipo | Descrição |
| :--- | :--- | :--- |
| `Label` | `string?` | Código da etiqueta (JSON: `etiqueta`). |
| `Uid` | `string?` | UID do remetente. |
| `SenderName` / `Email` / `PhoneNumber` | `string?` | Dados do remetente. |
| `MagentoOrderNumber` | `string?` | Nº do pedido interno (JSON: `order_number_magento`). |
| `OrderData` | `SfTrackingOrderDataResponse?` | Endereços da etiqueta + Superpoint. |
| `OrdersApiData` | `SfTrackingOrdersApiDataResponse?` | Pedido completo: pagamento, serviços, transportadora. |
| `CurrentDate` | `DateTime?` | Data/hora da consulta (UTC). |
| `RecipientName` / `RecipientAddress` / `RecipientPhone` | `string?` | Destinatário. |
| `ShippingModality` | `string?` | Ex: `JADLOG ECONOMICO`, `LOGGI`. |

### `SfProviderTrackingResponse` (campo `provider_tracking`)

| Propriedade | Tipo | Descrição |
| :--- | :--- | :--- |
| `Provider` | `string?` | Transportadora (`jadlog`, `loggi`...). |
| `Success` | `bool?` | Consulta à transportadora bem-sucedida. |
| `ShipmentId` / `ShipmentStatus` | `string?` | Identificador e status na transportadora. |
| `EstimatedDelivery` | `DateTime?` | Previsão de entrega (JSON: `previsaoEntrega`). |
| `Tracking` | `SfProviderTrackingDetailsResponse?` | `Code`, `Status` e lista `Events`. |
| `ShowDelayedBadge` | `bool?` | Exibir selo "em atraso" (JSON: `show_em_atraso_badge`). |

### `SfTrackingEventResponse` (itens de `Events`)

| Propriedade | Tipo | Descrição |
| :--- | :--- | :--- |
| `Date` | `DateTime?` | Data/hora do evento (JSON: `data`). |
| `Status` | `string?` | Ex: `Em trânsito`, `Entregue`. |
| `Unit` | `string?` | Local do evento (JSON: `unidade`). |
| `Description` | `string?` | Descrição (JSON: `descricao`). |
| `TrackingOrigin` | `string?` | Origem: `jadlog`, `loggi`, `pegaki`. |

### `SfDeliveryProofResponse` (campo `delivery_proof`)

| Propriedade | Tipo | Descrição |
| :--- | :--- | :--- |
| `Links` | `List<SfDeliveryProofLinkResponse>?` | `Rel` (`pod`, `facade_photo`, `delivery_receipt`, `delivery_receipt_image`) + `Href`. |
| `Source` / `Carrier` | `string?` | Origem dos comprovantes. |
| `CapturedAt` | `DateTime?` | Data da captura (UTC). |
| `ReceiverName` / `ReceiverDocument` / `LocationDescription` | `string?` | Recebedor — só algumas transportadoras (ex: Loggi). |

### `SfTrackingOrdersApiDataResponse` (campo `orders_api_data`)

| Propriedade | Tipo | Descrição |
| :--- | :--- | :--- |
| `Status` | `string?` | Status do pedido — compare com `SfOrderStatus`. |
| `OrderId` | `string?` | ID do pedido na SuperFrete. |
| `Payment` | `SfTrackingPaymentResponse?` | Créditos e forma de pagamento. |
| `CalculatedService` | `SfTrackingCalculatedServiceResponse?` | Serviço cotado (JSON: `service_calculated`). |
| `PostedService` | `SfTrackingPostedServiceResponse?` | Serviço postado, com valores conferidos (JSON: `service_posted`). |
| `CarrierData` | `SfTrackingCarrierDataResponse?` | Códigos na transportadora + `VolumePrint`. |
| `MagentoData` | `SfTrackingMagentoDataResponse?` | Nº do pedido interno. |
| `ContentDeclaration` | `List<SfTrackingContentDeclarationResponse>?` | Itens declarados (`Quantity`, `Value`, `Description`). |

DTOs auxiliares: `SfTrackingOrderDataResponse` (`Tag` + `SuperPoint`), `SfTrackingTagResponse` (`Origin`/`Destination` como `SfTrackingAddressResponse`), `SfTrackingSuperPointResponse` (ponto Pegaki/PUDO com `Geolocation`), `SfTrackingPackageDataResponse` (dimensões/peso), `SfTrackingVolumePrintResponse`, `SfTicketHistoryResponse` + `SfTicketHistoryParamsResponse`.

---

## Dicas para IAs ao utilizar .Tracking:

1. **Cliente separado:** Use `SuperFreteTrackingClient` — nunca tente acessar rastreamento via `SuperFreteClient`. Não passe token; a API é pública.
2. **Namespace:** `using Berdsk.Sdk.SuperFrete;` e `using Berdsk.Sdk.SuperFrete.Services.Tracking.Dtos;`.
3. **Datas normalizadas:** Todos os campos de data são `DateTime?` em UTC. O `SfDateTimeConverter` trata automaticamente ISO 8601, formato brasileiro `dd/MM/yyyy HH:mm:ss`, Firestore Timestamp e unix epoch — a API de rastreamento mistura todos esses formatos.
4. **Dimensões como string:** A API retorna dimensões ora como número, ora como string (`"31"`). Os DTOs já aceitam ambos — não é necessário tratamento manual.
5. **Eventos ordenados:** `Events` vem do mais recente para o mais antigo. O primeiro item é o status atual.
6. **`CarrierReplyDeliveryForecasts`:** Schema desconhecido (sempre observado vazio). É `List<JsonElement>?` — inspecione `GetRawText()` se precisar do conteúdo.
7. **Status em português:** `ShipmentStatus` e `Events[].Status` vêm em português da transportadora (`"Entregue"`, `"Em trânsito"`) — não há helper de constantes para eles; trate como texto de exibição.
8. **Comprovante de entrega:** `DeliveryProof.Links` pode conter foto (`pod`/`facade_photo`) e recibo (`delivery_receipt`). URLs podem expirar — não armazene.
9. **Erros:** mesmo mapeamento de exceções do SDK (`SfNotFoundException` para código inexistente etc.).

---

[Anterior: Exceptions](./10-exceptions.md) | [Início](./00-comece-aqui.md)
