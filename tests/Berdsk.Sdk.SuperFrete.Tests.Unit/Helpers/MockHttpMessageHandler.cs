using System.Net;
using System.Text;

namespace Berdsk.Sdk.SuperFrete.Tests.Unit.Helpers
{
    /// <summary>
    ///     Handler HTTP mock para simular respostas da API SuperFrete em testes unitários.
    /// </summary>
    public class MockHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpStatusCode _statusCode;
        private readonly string _content;

        /// <summary>
        ///     Última requisição recebida pelo handler — útil para verificar URL, método e corpo enviados.
        /// </summary>
        public HttpRequestMessage? LastRequest { get; private set; }

        /// <summary>
        ///     Inicializa o mock com o status HTTP e o corpo de resposta desejados.
        /// </summary>
        /// <param name="statusCode">Código de status HTTP a retornar.</param>
        /// <param name="content">Corpo JSON da resposta.</param>
        public MockHttpMessageHandler(HttpStatusCode statusCode, string content)
        {
            _statusCode = statusCode;
            _content = content;
        }

        /// <inheritdoc />
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            LastRequest = request;
            return Task.FromResult(new HttpResponseMessage(_statusCode)
            {
                Content = new StringContent(_content, Encoding.UTF8, "application/json")
            });
        }
    }
}
