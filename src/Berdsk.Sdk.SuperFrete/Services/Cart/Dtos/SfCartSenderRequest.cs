using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Cart.Dtos
{
    /// <summary>
    ///     Dados do remetente para criação de uma etiqueta de frete.
    /// </summary>
    public class SfCartSenderRequest
    {
        /// <summary>
        ///     Nome completo do remetente (até 50 caracteres).
        ///     Deve conter nome e sobrenome, ou no formato "Loja NomeLoja".
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = default!;

        /// <summary>
        ///     Rua/logradouro do remetente (até 50 caracteres).
        /// </summary>
        [JsonPropertyName("address")]
        public string Address { get; set; } = default!;

        /// <summary>
        ///     Complemento do endereço do remetente (até 20 caracteres, opcional).
        /// </summary>
        [JsonPropertyName("complement")]
        public string? Complement { get; set; }

        /// <summary>
        ///     Número do endereço do remetente (até 10 caracteres).
        ///     Envie uma string vazia caso não haja número.
        /// </summary>
        [JsonPropertyName("number")]
        public string? Number { get; set; }

        /// <summary>
        ///     Bairro do remetente (até 60 caracteres).
        ///     Envie "NA" caso o bairro não seja conhecido.
        /// </summary>
        [JsonPropertyName("district")]
        public string District { get; set; } = default!;

        /// <summary>
        ///     Cidade do remetente (até 50 caracteres).
        /// </summary>
        [JsonPropertyName("city")]
        public string City { get; set; } = default!;

        /// <summary>
        ///     Sigla do estado do remetente em letras maiúsculas (ex: "SP", "RJ").
        /// </summary>
        [JsonPropertyName("state_abbr")]
        public string StateAbbr { get; set; } = default!;

        /// <summary>
        ///     CEP do remetente com exatamente 8 dígitos numéricos (sem hífen).
        /// </summary>
        [JsonPropertyName("postal_code")]
        public string PostalCode { get; set; } = default!;

        /// <summary>
        ///     CPF ou CNPJ do remetente (opcional).
        ///     Se omitido, utiliza o documento cadastrado na conta SuperFrete.
        /// </summary>
        [JsonPropertyName("document")]
        public string? Document { get; set; }
    }
}
