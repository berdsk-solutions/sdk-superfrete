using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.ShippingServices.Dtos
{
    /// <summary>
    ///     Informações técnicas de um serviço de entrega da SuperFrete,
    ///     incluindo tipo, abrangência, restrições e serviços adicionais disponíveis.
    /// </summary>
    public class SfServiceInfoResponse
    {
        /// <summary>
        ///     Nome do serviço de entrega (ex: "PAC", "SEDEX").
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        ///     Tipo do serviço (ex: <c>"express"</c>).
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        /// <summary>
        ///     Abrangência do serviço (ex: <c>"interstate"</c> para cobertura nacional).
        /// </summary>
        [JsonPropertyName("range")]
        public string? Range { get; set; }

        /// <summary>
        ///     Restrições técnicas do serviço, como limites de dimensões, peso e valor de seguro.
        /// </summary>
        [JsonPropertyName("restrictions")]
        public SfServiceRestrictionsResponse? Restrictions { get; set; }

        /// <summary>
        ///     Lista de dados obrigatórios para utilizar o serviço (ex: <c>["names", "addresses"]</c>).
        /// </summary>
        [JsonPropertyName("requirements")]
        public string[]? Requirements { get; set; }

        /// <summary>
        ///     Lista de serviços adicionais disponíveis para contratação (ex: <c>["AR", "MP", "VD"]</c>).
        /// </summary>
        [JsonPropertyName("optionals")]
        public string[]? Optionals { get; set; }

        /// <summary>
        ///     Dados da transportadora responsável pelo serviço.
        /// </summary>
        [JsonPropertyName("company")]
        public SfServiceCompanyResponse? Company { get; set; }
    }
}
