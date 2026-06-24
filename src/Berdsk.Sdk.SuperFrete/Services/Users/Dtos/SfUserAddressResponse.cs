using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Users.Dtos
{
    /// <summary>
    ///     Endereço cadastrado na conta SuperFrete do usuário autenticado.
    /// </summary>
    public class SfUserAddressResponse
    {
        /// <summary>
        ///     Identificador único do endereço.
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        /// <summary>
        ///     Rótulo/apelido do endereço (ex: <c>Casa</c>, <c>Loja</c>).
        /// </summary>
        [JsonPropertyName("label")]
        public string? Label { get; set; }

        /// <summary>
        ///     Nome completo do responsável pelo endereço.
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        ///     CEP do endereço (apenas dígitos).
        /// </summary>
        [JsonPropertyName("postal_code")]
        public string? PostalCode { get; set; }

        /// <summary>
        ///     Logradouro (rua, avenida etc.).
        /// </summary>
        [JsonPropertyName("address")]
        public string? Address { get; set; }

        /// <summary>
        ///     Número do logradouro.
        /// </summary>
        [JsonPropertyName("number")]
        public string? Number { get; set; }

        /// <summary>
        ///     Complemento do endereço (apto, sala etc.).
        /// </summary>
        [JsonPropertyName("complement")]
        public string? Complement { get; set; }

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
        ///     Sigla do estado (UF) do endereço (ex: <c>SP</c>, <c>RJ</c>).
        /// </summary>
        [JsonPropertyName("state_abbr")]
        public string? StateAbbr { get; set; }

        /// <summary>
        ///     Número de telefone de contato associado ao endereço.
        /// </summary>
        [JsonPropertyName("phone")]
        public string? Phone { get; set; }

        /// <summary>
        ///     Indica se este é o endereço padrão do usuário.
        /// </summary>
        [JsonPropertyName("is_primary")]
        public bool? IsPrimary { get; set; }
    }
}
