using System.Net.Http.Headers;
using Berdsk.Sdk.SuperFrete.Helpers;
using Berdsk.Sdk.SuperFrete.Services.Calculator;
using Berdsk.Sdk.SuperFrete.Services.Cart;
using Berdsk.Sdk.SuperFrete.Services.Checkout;
using Berdsk.Sdk.SuperFrete.Services.Orders;
using Berdsk.Sdk.SuperFrete.Services.ShippingServices;
using Berdsk.Sdk.SuperFrete.Services.Users;
using Berdsk.Sdk.SuperFrete.Services.Webhooks;

namespace Berdsk.Sdk.SuperFrete
{
    /// <summary>
    ///     Ponto de entrada principal do SDK SuperFrete.
    ///     Agrega todos os serviços disponíveis e configura o <see cref="HttpClient" /> com autenticação e URL base.
    /// </summary>
    /// <remarks>
    ///     <b>Atenção:</b> Para evitar <b>Socket Exhaustion</b> (esgotamento de portas), evite criar múltiplas instâncias
    ///     de <see cref="SuperFreteClient" /> ou <see cref="HttpClient" /> manualmente.
    ///     É altamente recomendado injetar o <see cref="HttpClient" /> via Dependency Injection (DI)
    ///     ou reutilizá-lo como uma instância estática (Singleton).
    /// </remarks>
    public class SuperFreteClient
    {
        private const string SandboxBaseUrl = "https://sandbox.superfrete.com/";
        private const string ProductionBaseUrl = "https://api.superfrete.com/";

        /// <summary>
        ///     Inicializa uma nova instância do <see cref="SuperFreteClient" />.
        /// </summary>
        /// <param name="token">
        ///     Token de autenticação Bearer obtido no painel da SuperFrete.
        /// </param>
        /// <param name="environment">
        ///     Ambiente de execução da API. Padrão: <see cref="SuperFreteEnvironment.Production" />.
        /// </param>
        /// <param name="appName">
        ///     Nome da aplicação integradora, incluído no header <c>User-Agent</c> para identificação.
        /// </param>
        /// <param name="appVersion">
        ///     Versão da aplicação integradora, incluída no header <c>User-Agent</c>.
        /// </param>
        /// <param name="contactEmail">
        ///     E-mail de contato técnico, incluído no header <c>User-Agent</c> para suporte.
        /// </param>
        /// <param name="httpClient">
        ///     Instância opcional de <see cref="HttpClient" /> a ser reutilizada.
        ///     Recomendado via Dependency Injection para evitar Socket Exhaustion.
        ///     Se <c>null</c>, uma nova instância será criada internamente.
        /// </param>
        public SuperFreteClient(
            string token,
            SuperFreteEnvironment environment = SuperFreteEnvironment.Production,
            string? appName = null,
            string? appVersion = null,
            string? contactEmail = null,
            HttpClient? httpClient = null)
        {
            var baseUrl = environment == SuperFreteEnvironment.Sandbox ? SandboxBaseUrl : ProductionBaseUrl;
            var client = httpClient ?? new HttpClient();

            if (client.BaseAddress == null)
                client.BaseAddress = new Uri(baseUrl);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            var userAgent = BuildUserAgent(appName, appVersion, contactEmail);
            if (userAgent != null)
                client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", userAgent);

            Calculator = new SfCalculatorService(client);
            ShippingServices = new SfShippingServicesService(client);
            Cart = new SfCartService(client);
            Checkout = new SfCheckoutService(client);
            Orders = new SfOrderService(client);
            Webhooks = new SfWebhookService(client);
            Users = new SfUserService(client);
        }

        /// <summary>
        ///     Serviço de cotação de fretes — calcula preços e prazos para múltiplos serviços de entrega.
        /// </summary>
        public ISfCalculatorService Calculator { get; }

        /// <summary>
        ///     Serviço de informações técnicas dos serviços de entrega disponíveis na SuperFrete
        ///     (limites de dimensões, pesos, restrições de conteúdo etc.).
        /// </summary>
        public ISfShippingServicesService ShippingServices { get; }

        /// <summary>
        ///     Serviço de carrinho de fretes — adiciona fretes ao carrinho gerando etiquetas com status <c>pending</c>.
        /// </summary>
        public ISfCartService Cart { get; }

        /// <summary>
        ///     Serviço de checkout — realiza o pagamento de etiquetas com saldo disponível em conta.
        /// </summary>
        public ISfCheckoutService Checkout { get; }

        /// <summary>
        ///     Serviço de pedidos e etiquetas — consulta, cancelamento, impressão e listagem.
        /// </summary>
        public ISfOrderService Orders { get; }

        /// <summary>
        ///     Serviço de webhooks — criação, listagem, atualização e remoção de Webhook Apps.
        /// </summary>
        public ISfWebhookService Webhooks { get; }

        /// <summary>
        ///     Serviço de usuário — informações da conta autenticada e endereços cadastrados.
        /// </summary>
        public ISfUserService Users { get; }

        private static string? BuildUserAgent(string? appName, string? appVersion, string? contactEmail)
        {
            if (appName == null) return null;
            var agent = string.IsNullOrEmpty(appVersion) ? appName : $"{appName}/{appVersion}";
            return string.IsNullOrEmpty(contactEmail) ? agent : $"{agent} ({contactEmail})";
        }
    }
}
