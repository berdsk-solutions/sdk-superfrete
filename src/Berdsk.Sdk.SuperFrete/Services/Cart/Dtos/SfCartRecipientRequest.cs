using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Cart.Dtos
{
    /// <summary>
    ///     Dados do destinatário para criação de uma etiqueta de frete.
    /// </summary>
    public class SfCartRecipientRequest
    {
        /// <summary>
        ///     Nome completo do destinatário (até 50 caracteres).
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = default!;

        /// <summary>
        ///     Rua/logradouro do destinatário (até 50 caracteres).
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; } = default!;

        /// <summary>
        ///     Complemento do endereço do destinatário (até 20 caracteres, opcional).
        /// </summary>
        [JsonPropertyName("complement")]
        public string? Complement { get; set; }

        /// <summary>
        ///     Número do endereço do destinatário (até 10 caracteres, opcional).
        /// </summary>
        [JsonPropertyName("number")]
        public string? Number { get; set; }

        /// <summary>
        ///     Bairro do destinatário (até 50 caracteres).
        ///     Envie "NA" caso o bairro não seja conhecido.
        /// </summary>
        [JsonPropertyName("district")]
        public string District { get; set; } = default!;

        /// <summary>
        ///     Cidade do destinatário (até 50 caracteres).
        /// </summary>
        [JsonPropertyName("city")]
        public string City { get; set; } = default!;

        /// <summary>
        ///     Sigla do estado do destinatário em letras maiúsculas (ex: "SP", "RJ").
        /// </summary>
        [JsonPropertyName("state_abbr")]
        public string StateAbbr { get; set; } = default!;

        /// <summary>
        ///     CEP do destinatário com exatamente 8 dígitos numéricos (sem hífen).
        /// </summary>
        [JsonPropertyName("postal_code")]
        public string PostalCode { get; set; } = default!;

        /// <summary>
        ///     E-mail do destinatário para envio do código de rastreio (opcional).
        /// </summary>
        [JsonPropertyName("email")]
        public string? Email { get; set; }

        /// <summary>
        ///     CPF ou CNPJ do destinatário (obrigatório para emissão de DC-e).
        /// </summary>
        [JsonPropertyName("document")]
        public string Document { get; set; } = default!;
    }
}
