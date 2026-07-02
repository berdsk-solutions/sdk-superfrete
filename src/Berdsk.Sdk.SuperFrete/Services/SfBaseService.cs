using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Berdsk.Sdk.SuperFrete.Converters;

namespace Berdsk.Sdk.SuperFrete.Services
{
    /// <summary>
    ///     Classe base abstrata para todos os serviços do SDK SuperFrete.
    ///     Centraliza a serialização JSON, o envio de requisições HTTP e o tratamento padronizado de erros.
    /// </summary>
    public abstract class SfBaseService
    {
        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters = { new SfDateTimeConverter() }
        };

        /// <summary>
        ///     Instância do <see cref="HttpClient" /> configurada com as credenciais e URL base da SuperFrete.
        /// </summary>
        protected readonly HttpClient HttpClient;

        /// <summary>
        ///     Inicializa uma nova instância de <see cref="SfBaseService" />.
        /// </summary>
        /// <param name="httpClient">
        ///     Instância do <see cref="HttpClient" /> previamente configurada pelo <c>SuperFreteClient</c>
        ///     com autenticação, URL base e headers padrão.
        /// </param>
        protected SfBaseService(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        /// <summary>
        ///     Envia uma requisição HTTP POST com corpo JSON e desserializa a resposta.
        /// </summary>
        /// <typeparam name="TResponse">Tipo esperado da resposta.</typeparam>
        /// <typeparam name="TRequest">Tipo do corpo da requisição.</typeparam>
        /// <param name="url">Caminho relativo do endpoint (ex: <c>api/v0/calculator</c>).</param>
        /// <param name="request">Objeto a ser serializado como corpo da requisição.</param>
        /// <param name="headers">Headers adicionais opcionais a serem incluídos apenas nesta requisição.</param>
        /// <returns>Resposta desserializada, ou <c>null</c> se a resposta for 204 No Content.</returns>
        protected async Task<TResponse?> PostAsync<TResponse, TRequest>(string url, TRequest request,
            IDictionary<string, string>? headers = null) where TResponse : class
        {
            using var httpRequest = new HttpRequestMessage(HttpMethod.Post, url);
            httpRequest.Content = JsonContent.Create(request, options: JsonOptions);

            if (headers != null)
                foreach (var header in headers)
                    httpRequest.Headers.Add(header.Key, header.Value);

            var response = await HttpClient.SendAsync(httpRequest);
            return await HandleResponseAsync<TResponse>(response);
        }

        /// <summary>
        ///     Envia uma requisição HTTP GET e desserializa a resposta.
        /// </summary>
        /// <typeparam name="T">Tipo esperado da resposta.</typeparam>
        /// <param name="url">Caminho relativo do endpoint, podendo incluir query string.</param>
        /// <returns>Resposta desserializada, ou <c>null</c> se a resposta for 204 No Content.</returns>
        protected async Task<T?> GetAsync<T>(string url) where T : class
        {
            var response = await HttpClient.GetAsync(url);
            return await HandleResponseAsync<T>(response);
        }

        /// <summary>
        ///     Envia uma requisição HTTP PUT com corpo JSON e desserializa a resposta.
        /// </summary>
        /// <typeparam name="TResponse">Tipo esperado da resposta.</typeparam>
        /// <typeparam name="TRequest">Tipo do corpo da requisição.</typeparam>
        /// <param name="url">Caminho relativo do endpoint.</param>
        /// <param name="request">Objeto a ser serializado como corpo da requisição.</param>
        /// <returns>Resposta desserializada, ou <c>null</c> se a resposta for 204 No Content.</returns>
        protected async Task<TResponse?> PutAsync<TResponse, TRequest>(string url, TRequest request)
            where TResponse : class
        {
            var response = await HttpClient.PutAsJsonAsync(url, request, JsonOptions);
            return await HandleResponseAsync<TResponse>(response);
        }

        /// <summary>
        ///     Envia uma requisição HTTP DELETE, com corpo JSON opcional, e desserializa a resposta.
        /// </summary>
        /// <typeparam name="T">Tipo esperado da resposta.</typeparam>
        /// <param name="url">Caminho relativo do endpoint.</param>
        /// <param name="request">Corpo opcional da requisição DELETE.</param>
        /// <returns>Resposta desserializada, ou <c>null</c> se a resposta for 204 No Content.</returns>
        protected async Task<T?> DeleteAsync<T>(string url, object? request = null) where T : class
        {
            var response = request == null
                ? await HttpClient.DeleteAsync(url)
                : await HttpClient.SendAsync(new HttpRequestMessage(HttpMethod.Delete, url)
                {
                    Content = JsonContent.Create(request, options: JsonOptions)
                });

            return await HandleResponseAsync<T>(response);
        }

        /// <summary>
        ///     Envia uma requisição HTTP PATCH com corpo JSON e desserializa a resposta.
        /// </summary>
        /// <typeparam name="TResponse">Tipo esperado da resposta.</typeparam>
        /// <typeparam name="TRequest">Tipo do corpo da requisição.</typeparam>
        /// <param name="url">Caminho relativo do endpoint.</param>
        /// <param name="request">Objeto a ser serializado como corpo da requisição.</param>
        /// <returns>Resposta desserializada, ou <c>null</c> se a resposta for 204 No Content.</returns>
        protected async Task<TResponse?> PatchAsync<TResponse, TRequest>(string url, TRequest request)
            where TResponse : class
        {
            var response = await HttpClient.PatchAsJsonAsync(url, request, JsonOptions);
            return await HandleResponseAsync<TResponse>(response);
        }

        private static async Task<T?> HandleResponseAsync<T>(HttpResponseMessage response) where T : class
        {
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                    return null;
                return await response.Content.ReadFromJsonAsync<T>(JsonOptions);
            }

            var rawBody = await response.Content.ReadAsStringAsync();
            SfErrorResponse? errorResponse = null;
            try
            {
                errorResponse = JsonSerializer.Deserialize<SfErrorResponse>(rawBody, JsonOptions);
            }
            catch
            {
                // Ignora erro de desserialização; o rawBody bruto será repassado para a exceção
            }

            throw response.StatusCode switch
            {
                HttpStatusCode.BadRequest => new SfBadRequestException(errorResponse, rawBody),
                HttpStatusCode.Unauthorized => new SfUnauthorizedException(errorResponse, rawBody),
                HttpStatusCode.Forbidden => new SfForbiddenException(errorResponse, rawBody),
                HttpStatusCode.NotFound => new SfNotFoundException(errorResponse, rawBody),
                HttpStatusCode.TooManyRequests => new SfTooManyRequestsException(errorResponse, rawBody),
                HttpStatusCode.InternalServerError => new SfInternalServerErrorException(errorResponse, rawBody),
                _ => new SuperFreteException(response.StatusCode, errorResponse, rawBody)
            };
        }
    }
}
