using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete
{
    /// <summary>
    ///     Representa a resposta de erro retornada pela API SuperFrete.
    /// </summary>
    public class SfErrorResponse
    {
        /// <summary>
        ///     Mensagem geral do erro retornada pela API.
        /// </summary>
        [JsonPropertyName("message")]
        public string? Message { get; set; }

        /// <summary>
        ///     Código do erro retornado pela API.
        /// </summary>
        [JsonPropertyName("code")]
        public string? Code { get; set; }

        /// <summary>
        ///     Dicionário de erros de validação, onde a chave é o campo e o valor é uma lista de mensagens de erro.
        /// </summary>
        [JsonPropertyName("errors")]
        public Dictionary<string, string[]>? Errors { get; set; }
    }

    /// <summary>
    ///     Exceção base lançada quando a API SuperFrete retorna um erro HTTP.
    /// </summary>
    public class SuperFreteException : Exception
    {
        /// <summary>
        ///     Inicializa uma nova instância de <see cref="SuperFreteException" />.
        /// </summary>
        /// <param name="statusCode">Código de status HTTP retornado pela API.</param>
        /// <param name="errorResponse">Objeto de erro deserializado da resposta da API, se disponível.</param>
        /// <param name="rawBody">Corpo bruto da resposta HTTP, útil para diagnóstico quando a deserialização falha.</param>
        public SuperFreteException(HttpStatusCode statusCode, SfErrorResponse? errorResponse, string? rawBody = null)
            : base(errorResponse?.Message ?? $"SuperFrete API error with status code {statusCode}")
        {
            StatusCode = statusCode;
            ErrorResponse = errorResponse;
            RawBody = rawBody;
        }

        /// <summary>
        ///     Código de status HTTP retornado pela API.
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        ///     Objeto de resposta de erro detalhado deserializado da API, se disponível.
        /// </summary>
        public SfErrorResponse? ErrorResponse { get; }

        /// <summary>
        ///     Corpo bruto da resposta HTTP em caso de erro — útil para diagnóstico quando a deserialização falha.
        /// </summary>
        public string? RawBody { get; }
    }

    /// <summary>
    ///     Exceção lançada quando a API retorna status 400 (Bad Request).
    ///     Indica que a requisição contém dados inválidos ou mal-formatados.
    /// </summary>
    public class SfBadRequestException : SuperFreteException
    {
        /// <summary>
        ///     Inicializa uma nova instância de <see cref="SfBadRequestException" />.
        /// </summary>
        /// <param name="errorResponse">Objeto de erro deserializado da resposta da API, se disponível.</param>
        /// <param name="rawBody">Corpo bruto da resposta HTTP.</param>
        public SfBadRequestException(SfErrorResponse? errorResponse, string? rawBody = null)
            : base(HttpStatusCode.BadRequest, errorResponse, rawBody)
        {
        }
    }

    /// <summary>
    ///     Exceção lançada quando a API retorna status 401 (Unauthorized).
    ///     Indica que o token de autenticação é inválido ou não foi fornecido.
    /// </summary>
    public class SfUnauthorizedException : SuperFreteException
    {
        /// <summary>
        ///     Inicializa uma nova instância de <see cref="SfUnauthorizedException" />.
        /// </summary>
        /// <param name="errorResponse">Objeto de erro deserializado da resposta da API, se disponível.</param>
        /// <param name="rawBody">Corpo bruto da resposta HTTP.</param>
        public SfUnauthorizedException(SfErrorResponse? errorResponse, string? rawBody = null)
            : base(HttpStatusCode.Unauthorized, errorResponse, rawBody)
        {
        }
    }

    /// <summary>
    ///     Exceção lançada quando a API retorna status 403 (Forbidden).
    ///     Indica que o token não possui permissão para executar a operação solicitada.
    /// </summary>
    public class SfForbiddenException : SuperFreteException
    {
        /// <summary>
        ///     Inicializa uma nova instância de <see cref="SfForbiddenException" />.
        /// </summary>
        /// <param name="errorResponse">Objeto de erro deserializado da resposta da API, se disponível.</param>
        /// <param name="rawBody">Corpo bruto da resposta HTTP.</param>
        public SfForbiddenException(SfErrorResponse? errorResponse, string? rawBody = null)
            : base(HttpStatusCode.Forbidden, errorResponse, rawBody)
        {
        }
    }

    /// <summary>
    ///     Exceção lançada quando a API retorna status 404 (Not Found).
    ///     Indica que o recurso solicitado não foi encontrado.
    /// </summary>
    public class SfNotFoundException : SuperFreteException
    {
        /// <summary>
        ///     Inicializa uma nova instância de <see cref="SfNotFoundException" />.
        /// </summary>
        /// <param name="errorResponse">Objeto de erro deserializado da resposta da API, se disponível.</param>
        /// <param name="rawBody">Corpo bruto da resposta HTTP.</param>
        public SfNotFoundException(SfErrorResponse? errorResponse, string? rawBody = null)
            : base(HttpStatusCode.NotFound, errorResponse, rawBody)
        {
        }
    }

    /// <summary>
    ///     Exceção lançada quando a API retorna status 429 (Too Many Requests).
    ///     Indica que o limite de requisições por período foi excedido. Implemente backoff exponencial antes de tentar novamente.
    /// </summary>
    public class SfTooManyRequestsException : SuperFreteException
    {
        /// <summary>
        ///     Inicializa uma nova instância de <see cref="SfTooManyRequestsException" />.
        /// </summary>
        /// <param name="errorResponse">Objeto de erro deserializado da resposta da API, se disponível.</param>
        /// <param name="rawBody">Corpo bruto da resposta HTTP.</param>
        public SfTooManyRequestsException(SfErrorResponse? errorResponse, string? rawBody = null)
            : base(HttpStatusCode.TooManyRequests, errorResponse, rawBody)
        {
        }
    }

    /// <summary>
    ///     Exceção lançada quando a API retorna status 500 (Internal Server Error).
    ///     Indica um erro inesperado no servidor da SuperFrete.
    /// </summary>
    public class SfInternalServerErrorException : SuperFreteException
    {
        /// <summary>
        ///     Inicializa uma nova instância de <see cref="SfInternalServerErrorException" />.
        /// </summary>
        /// <param name="errorResponse">Objeto de erro deserializado da resposta da API, se disponível.</param>
        /// <param name="rawBody">Corpo bruto da resposta HTTP.</param>
        public SfInternalServerErrorException(SfErrorResponse? errorResponse, string? rawBody = null)
            : base(HttpStatusCode.InternalServerError, errorResponse, rawBody)
        {
        }
    }
}
