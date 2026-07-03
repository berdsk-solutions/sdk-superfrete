using System;
using System.Net.Http;
using Berdsk.Sdk.SuperFrete.Services.Tracking;

namespace Berdsk.Sdk.SuperFrete
{
    /// <summary>
    ///     Cliente da API pública de rastreamento da SuperFrete (<c>https://rastreamento.superfrete.com/</c>).
    ///     Esta API não exige autenticação — nenhum token é necessário.
    /// </summary>
    /// <remarks>
    ///     <b>Atenção:</b> Para evitar <b>Socket Exhaustion</b> (esgotamento de portas), evite criar múltiplas instâncias
    ///     de <see cref="SuperFreteTrackingClient" /> ou <see cref="HttpClient" /> manualmente.
    ///     É altamente recomendado injetar o <see cref="HttpClient" /> via Dependency Injection (DI)
    ///     ou reutilizá-lo como uma instância estática (Singleton).
    /// </remarks>
    public class SuperFreteTrackingClient
    {
        private const string TrackingBaseUrl = "https://rastreamento.superfrete.com/";

        /// <summary>
        ///     Inicializa uma nova instância do <see cref="SuperFreteTrackingClient" />.
        /// </summary>
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
        public SuperFreteTrackingClient(
            string? appName = null,
            string? appVersion = null,
            string? contactEmail = null,
            HttpClient? httpClient = null)
        {
            var client = httpClient ?? new HttpClient();

            if (client.BaseAddress == null)
                client.BaseAddress = new Uri(TrackingBaseUrl);

            if (!client.DefaultRequestHeaders.Contains("Accept"))
                client.DefaultRequestHeaders.Add("Accept", "application/json");

            var userAgent = BuildUserAgent(appName, appVersion, contactEmail);
            if (userAgent != null)
                client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", userAgent);

            Tracking = new SfTrackingService(client);
        }

        /// <summary>
        ///     Serviço de rastreamento — consulta o rastreamento completo de um envio pelo código de rastreio.
        /// </summary>
        public ISfTrackingService Tracking { get; }

        private static string? BuildUserAgent(string? appName, string? appVersion, string? contactEmail)
        {
            if (appName == null) return null;
            var agent = string.IsNullOrEmpty(appVersion) ? appName : $"{appName}/{appVersion}";
            return string.IsNullOrEmpty(contactEmail) ? agent : $"{agent} ({contactEmail})";
        }
    }
}
