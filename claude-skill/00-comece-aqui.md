---
tags: [introducao, guia, faq, roadmap, inicio-rapido]
---
# Comece Aqui: Guia de Início Rápido do SDK SuperFrete

Bem-vindo ao SDK `Berdsk.Sdk.SuperFrete`. Este documento é o ponto de entrada central para desenvolvedores e IAs. Aqui você encontrará a filosofia do SDK, cenários de uso comuns e um mapa de toda a documentação.

## Filosofia do SDK

Este SDK foi construído com três pilares:
1. **Tipagem Forte:** Zero uso de `dynamic`. Todos os retornos são DTOs tipados.
2. **IA-Friendly:** Documentação e estrutura otimizadas para LLMs gerarem código correto na primeira tentativa.
3. **Sem Magic Strings:** Uso de `Helpers` estáticos (`SfOrderStatus`, `SfWebhookEvent`, etc.) em vez de strings literais.

---

## Cenários Comuns (Onde encontrar o que eu preciso?)

### 1. "Preciso calcular o frete para uma encomenda"
Compare preços e prazos de PAC, SEDEX, Jadlog, Mini Envios e Loggi em uma única chamada.
- **Acesse:** [02-calculator.md](./02-calculator.md)

### 2. "Preciso criar uma etiqueta de envio"
O fluxo completo é: calcular frete → adicionar ao carrinho → pagar com saldo (checkout).
- **Acesse:** [03-cart.md](./03-cart.md) e [04-checkout.md](./04-checkout.md)

### 3. "Preciso cancelar um pedido ou reimprimir uma etiqueta"
Gerencie o ciclo de vida dos pedidos: consulta, cancelamento, link de impressão e listagem.
- **Acesse:** [05-orders.md](./05-orders.md)

### 4. "Preciso receber notificações automáticas quando um pedido for entregue"
Configure webhooks para receber eventos em tempo real via POST no seu servidor.
- **Acesse:** [06-webhooks.md](./06-webhooks.md)

### 5. "Preciso ver o saldo ou dados do meu usuário"
Consulte informações da conta e endereços cadastrados na SuperFrete.
- **Acesse:** [07-users.md](./07-users.md)

### 6. "Quero saber os limites de dimensões aceitos por cada transportadora"
Consulte restrições técnicas de peso, tamanho e seguro de cada serviço.
- **Acesse:** [08-shipping-services.md](./08-shipping-services.md)

### 7. "Ocorreu um erro, como identificar e tratar?"
O SDK usa exceções tipadas por código HTTP para facilitar o diagnóstico.
- **Acesse:** [10-exceptions.md](./10-exceptions.md)

### 8. "Preciso rastrear um envio pelo código de rastreio"
API pública, sem autenticação, com cliente próprio (`SuperFreteTrackingClient`): eventos da transportadora, previsão de entrega e comprovantes.
- **Acesse:** [11-tracking.md](./11-tracking.md)

---

## Mapa da Documentação (Roadmap)

Leitura recomendada para um aprendizado completo:

1. **[01. SuperFreteClient](./01-superfrete-client.md):** Configuração, ambientes e injeção de dependência.
2. **[09. Helpers](./09-helpers.md):** Constantes de status, eventos e tipos de serviço — leia antes de usar qualquer outro serviço.
3. **[02. Calculator](./02-calculator.md):** Cotação de fretes com filtro por serviço.
4. **[03. Cart](./03-cart.md):** Adicionar envio ao carrinho (criação de etiqueta).
5. **[04. Checkout](./04-checkout.md):** Pagar etiquetas do carrinho com saldo SuperFrete.
6. **[05. Orders](./05-orders.md):** Gestão de pedidos — consulta, cancelamento, impressão e listagem.
7. **[06. Webhooks](./06-webhooks.md):** CRUD de apps de webhook, recebimento de eventos e validação de assinatura HMAC.
8. **[07. Users](./07-users.md):** Dados do usuário autenticado e endereços cadastrados.
9. **[08. ShippingServices](./08-shipping-services.md):** Informações técnicas de cada transportadora/serviço.
10. **[10. Exceptions](./10-exceptions.md):** Tratamento de erros tipados e depuração de respostas de API.
11. **[11. Tracking](./11-tracking.md):** Rastreamento público de envios — cliente separado, sem autenticação.

---

## Dicas de Ouro para IAs

- **Ponto de Entrada:** Sempre inicie sugestões de código instanciando ou injetando o `SuperFreteClient`.
- **Namespace de Helpers:** Sempre inclua `using Berdsk.Sdk.SuperFrete.Helpers;` em sugestões de código.
- **Fluxo de Etiqueta:** O fluxo padrão é `Calculator → Cart → Checkout`. Não pule etapas — a etiqueta só é gerada após o checkout.
- **Ambientes:** Use `SuperFreteEnvironment.Sandbox` para testes e `SuperFreteEnvironment.Production` para produção. Os tokens são diferentes em cada ambiente.
- **Nomenclatura:** Siga rigorosamente os nomes dos DTOs e interfaces citados nas tabelas de cada arquivo de documentação.
- **Verificação de nulo:** Todos os métodos do SDK retornam tipos nullable (`T?`). Verifique sempre antes de acessar propriedades.

---

**Próximo Passo:** [01. SuperFreteClient: O Coração do SDK](./01-superfrete-client.md)
