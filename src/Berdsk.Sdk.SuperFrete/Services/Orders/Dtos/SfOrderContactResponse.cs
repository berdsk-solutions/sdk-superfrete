using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Orders.Dtos
{
    /// <summary>
    ///     Dados de contato/endereço de remetente ou destinatário retornados
    ///     pela API nas informações de um pedido.
    /// </summary>
    public class SfOrderContactResponse
    {
        /// <summary>
        ///     Nome completo do remetente ou destinatário.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        ///     Rua/logradouro do endereço.
        /// </summary>
        [JsonPropertyName("address")]
        public string? Address { get; set; }

        /// <summary>
        ///     Complemento do endereço (ex: "Apto 42").
        /// </summary>
        [JsonPropertyName("complement")]
        public string? Complement { get; set; }

        /// <summary>
        ///     Número do endereço (campo de request; não retornado separadamente na resposta da API).
        /// </summary>
        [JsonPropertyName("number")]
        public string? Number { get; set; }

        /// <summary>
        ///     Número do local/endereço conforme retornado pela API (campo "location_number").
        /// </summary>
        [JsonPropertyName("location_number")]
        public string? LocationNumber { get; set; }

        /// <summary>
        ///     Bairro do endereço.
        /// </summary>
        [JsonPropertyName("district")]
        public string? District { get; set; }

        /// <summary>
        ///     Cidade do endereço.
        /// </summary>
        [JsonPropertyName("city")]
        public string? City { get; set; }

        /// <summary>
        ///     Sigla do estado em letras maiúsculas (ex: "SP", "RJ").
        /// </summary>
        [JsonPropertyName("state_abbr")]
        public string? StateAbbr { get; set; }

        /// <summary>
        ///     CEP com 8 dígitos numéricos (sem hífen).
        /// </summary>
        [JsonPropertyName("postal_code")]
        public string? PostalCode { get; set; }

        /// <summary>
        ///     Código do país (sempre "BR").
        /// </summary>
        [JsonPropertyName("country_id")]
        public string? CountryId { get; set; }

        /// <summary>
        ///     E-mail do destinatário (presente apenas no campo "to").
        /// </summary>
        [JsonPropertyName("email")]
        public string? Email { get; set; }

        /// <summary>
        ///     CPF ou CNPJ do remetente ou destinatário.
        /// </summary>
        [JsonPropertyName("document")]
        public string? Document { get; set; }
    }
}
