using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Berdsk.Sdk.SuperFrete.Models;

namespace Berdsk.Sdk.SuperFrete;

/// <summary>
///     Cliente para interação com a API do SuperFrete.
/// </summary>
public class SuperFreteClient : IDisposable
{
    private readonly HttpClient _httpClient;

    /// <summary>
    ///     Inicializa uma nova instância do cliente SuperFrete.
    /// </summary>
    /// <param name="token">Token de autenticação (Bearer).</param>
    /// <param name="environment">Ambiente (Sandbox ou Produção).</param>
    /// <param name="appName">Nome da sua aplicação para o User-Agent.</param>
    /// <param name="appVersion">Versão da sua aplicação.</param>
    /// <param name="contactEmail">E-mail de contato técnico.</param>
    public SuperFreteClient(string token, SuperFreteEnvironment environment, string appName, string appVersion,
        string contactEmail)
    {
        var token1 = token;
        var userAgent = $"{appName} {appVersion} ({contactEmail})";

        var baseUrl = environment == SuperFreteEnvironment.Sandbox
            ? "https://sandbox.superfrete.com/"
            : "https://api.superfrete.com/";

        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(baseUrl)
        };

        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token1);
        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(userAgent);
    }

    /// <summary>
    ///     Libera os recursos do cliente.
    /// </summary>
    public void Dispose()
    {
        _httpClient.Dispose();
    }

    #region Calculator & Services

    /// <summary>
    ///     Calcula o valor do frete para uma determinada encomenda.
    /// </summary>
    /// <param name="request">Dados para o cálculo.</param>
    /// <returns>Lista de serviços de frete disponíveis.</returns>
    public async Task<List<ShippingServiceResult>> CalculateShippingAsync(CalculatorRequest request)
    {
        return await PostAsync<List<ShippingServiceResult>>("api/v0/calculator", request);
    }

    /// <summary>
    ///     Retorna os detalhes técnicos de cada serviço dos Correios.
    /// </summary>
    /// <returns>Dicionário com informações dos serviços indexados pelo código.</returns>
    public async Task<Dictionary<string, ServiceInfo>> GetServicesInfoAsync()
    {
        return await GetAsync<Dictionary<string, ServiceInfo>>("api/v0/services/info");
    }

    #endregion

    #region Cart & Orders

    /// <summary>
    ///     Envia os detalhes de um pedido para a SuperFrete e gera uma etiqueta de frete (status pending).
    /// </summary>
    /// <param name="request">Dados do frete.</param>
    /// <returns>Resposta com o ID da etiqueta gerada.</returns>
    public async Task<CartResponse> AddToCartAsync(CartRequest request)
    {
        return await PostAsync<CartResponse>("api/v0/cart", request);
    }

    /// <summary>
    ///     Realiza o pagamento de etiquetas de frete utilizando o saldo disponível.
    /// </summary>
    /// <param name="orders">Lista de IDs de pedidos/etiquetas.</param>
    /// <returns>Detalhes da compra e status das etiquetas.</returns>
    public async Task<CheckoutResponse> CheckoutAsync(List<string> orders)
    {
        var request = new CheckoutRequest { Orders = orders };
        return await PostAsync<CheckoutResponse>("api/v0/checkout", request);
    }

    /// <summary>
    ///     Recupera informações detalhadas sobre uma etiqueta de frete específica.
    /// </summary>
    /// <param name="id">ID único da etiqueta.</param>
    /// <returns>Dados detalhados do pedido.</returns>
    public async Task<OrderInfo> GetOrderInfoAsync(string id)
    {
        return await GetAsync<OrderInfo>($"api/v0/order/info/{id}");
    }

    /// <summary>
    ///     Retorna a URL da etiqueta em formato PDF para impressão.
    /// </summary>
    /// <param name="orders">Lista de IDs de pedidos.</param>
    /// <returns>URL para acesso ao PDF.</returns>
    public async Task<string> GetPrintLinkAsync(List<string> orders)
    {
        var request = new { orders };
        var response = await PostAsync<PrintInfo>("api/v0/tag/print", request);
        return response.Url;
    }

    /// <summary>
    ///     Cancela uma etiqueta de frete, desde que ainda não tenha sido postada.
    /// </summary>
    /// <param name="id">ID da etiqueta.</param>
    /// <param name="reason">Motivo do cancelamento.</param>
    /// <returns>Status do cancelamento indexado pelo ID.</returns>
    public async Task<CancelOrderResponse> CancelOrderAsync(string id, string reason = "Cancelado pelo usuário")
    {
        var request = new CancelOrderRequest
        {
            Order = new CancelOrderInfo { Id = id, Description = reason }
        };
        return await PostAsync<CancelOrderResponse>("api/v0/order/cancel", request);
    }

    #endregion

    #region User & Webhooks

    /// <summary>
    ///     Retorna informações da conta na SuperFrete a partir do token de autenticação.
    /// </summary>
    /// <returns>Dados do usuário e saldo.</returns>
    public async Task<UserInfo> GetUserInfoAsync()
    {
        return await GetAsync<UserInfo>("api/v0/user");
    }

    /// <summary>
    ///     Cadastra um webhook para receber notificações de eventos.
    /// </summary>
    /// <param name="request">Dados do webhook.</param>
    /// <returns>Informações do webhook criado.</returns>
    public async Task<WebhookResponse> CreateWebhookAsync(WebhookRequest request)
    {
        return await PostAsync<WebhookResponse>("api/v0/webhook", request);
    }

    /// <summary>
    ///     Retorna todos os Webhook Apps cadastrados na conta.
    /// </summary>
    /// <returns>Lista de webhooks.</returns>
    public async Task<List<WebhookResponse>> ListWebhooksAsync()
    {
        return await GetAsync<List<WebhookResponse>>("api/v0/webhook");
    }

    /// <summary>
    ///     Atualiza as informações de um Webhook App já cadastrado.
    /// </summary>
    /// <param name="id">ID do webhook.</param>
    /// <param name="request">Dados para atualização.</param>
    /// <returns>Dados atualizados do webhook.</returns>
    public async Task<WebhookResponse> UpdateWebhookAsync(string id, WebhookRequest request)
    {
        return await PutAsync<WebhookResponse>($"api/v0/webhook/{id}", request);
    }

    /// <summary>
    ///     Remove um Webhook App cadastrado.
    /// </summary>
    /// <param name="id">ID do webhook.</param>
    /// <returns>Verdadeiro se removido com sucesso.</returns>
    public async Task<bool> DeleteWebhookAsync(string id)
    {
        var response = await _httpClient.DeleteAsync($"api/v0/webhook/{id}");
        return response.IsSuccessStatusCode;
    }

    #endregion

    #region HTTP Helpers

    private async Task<T> GetAsync<T>(string endpoint)
    {
        var response = await _httpClient.GetAsync(endpoint);
        return await HandleResponseAsync<T>(response);
    }

    private async Task<T> PostAsync<T>(string endpoint, object data)
    {
        var json = JsonConvert.SerializeObject(data, new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        });
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(endpoint, content);
        return await HandleResponseAsync<T>(response);
    }

    private async Task<T> PutAsync<T>(string endpoint, object data)
    {
        var json = JsonConvert.SerializeObject(data, new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        });
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync(endpoint, content);
        return await HandleResponseAsync<T>(response);
    }

    private async Task<T> HandleResponseAsync<T>(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            var errorMessage = $"Erro na API SuperFrete: {response.StatusCode}";
            string? errorCode = null;

            try
            {
                var errorObj = JsonConvert.DeserializeObject<dynamic>(content);
                if (errorObj != null)
                {
                    errorMessage = errorObj.message ?? errorMessage;
                    errorCode = errorObj.code;
                }
            }
            catch
            {
                /* Ignorar erro de parse no erro */
            }

            throw new SuperFreteException(errorMessage, errorCode);
        }

        if (string.IsNullOrWhiteSpace(content))
            return default!;

        return JsonConvert.DeserializeObject<T>(content)!;
    }

    #endregion
}
