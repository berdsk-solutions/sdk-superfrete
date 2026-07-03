using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Tracking.Dtos
{
    /// <summary>
    ///     Endereço e dados de contato de origem ou destino da etiqueta.
    /// </summary>
    public class SfTrackingAddressResponse
    {
        /// <summary>
        ///     Nome completo do contato.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        ///     Primeiro nome do contato.
        /// </summary>
        [JsonPropertyName("firstname")]
        public string? FirstName { get; set; }

        /// <summary>
        ///     Sobrenome do contato.
        /// </summary>
        [JsonPropertyName("lastname")]
        public string? LastName { get; set; }

        /// <summary>
        ///     Logradouro (rua, avenida etc.).
        /// </summary>
        [JsonPropertyName("street")]
        public string? Street { get; set; }

        /// <summary>
        ///     Número do endereço.
        /// </summary>
        [JsonPropertyName("number")]
        public string? Number { get; set; }

        /// <summary>
        ///     Complemento do endereço.
        /// </summary>
        [JsonPropertyName("complement")]
        public string? Complement { get; set; }

        /// <summary>
        ///     Bairro.
        /// </summary>
        [JsonPropertyName("district")]
        public string? District { get; set; }

        /// <summary>
        ///     Cidade.
        /// </summary>
        [JsonPropertyName("city")]
        public string? City { get; set; }

        /// <summary>
        ///     Unidade federativa (UF), ex: <c>SP</c>.
        /// </summary>
        [JsonPropertyName("region")]
        public string? Region { get; set; }

        /// <summary>
        ///     CEP do endereço.
        /// </summary>
        [JsonPropertyName("postcode")]
        public string? Postcode { get; set; }

        /// <summary>
        ///     E-mail do contato.
        /// </summary>
        [JsonPropertyName("email")]
        public string? Email { get; set; }

        /// <summary>
        ///     Telefone do contato.
        /// </summary>
        [JsonPropertyName("phone")]
        public string? Phone { get; set; }

        /// <summary>
        ///     Telefone do contato (campo alternativo retornado pela API).
        /// </summary>
        [JsonPropertyName("phone_number")]
        public string? PhoneNumber { get; set; }
    }
}
