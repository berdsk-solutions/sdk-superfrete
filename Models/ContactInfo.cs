using Newtonsoft.Json;

namespace Berdsk.Sdk.SuperFrete.Models;

/// <summary>
///     Informações detalhadas do remetente ou destinatário.
/// </summary>
public class ContactInfo
{
    /// <summary>
    ///     Nome completo (precisa ter nome e sobrenome).
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     Rua do endereço.
    /// </summary>
    [JsonProperty("address")]
    public string Address { get; set; } = string.Empty;

    /// <summary>
    ///     Complemento do endereço.
    /// </summary>
    [JsonProperty("complement")]
    public string? Complement { get; set; }

    /// <summary>
    ///     Número do endereço.
    /// </summary>
    [JsonProperty("number")]
    public string? Number { get; set; }

    /// <summary>
    ///     Bairro.
    /// </summary>
    [JsonProperty("district")]
    public string District { get; set; } = string.Empty;

    /// <summary>
    ///     Cidade.
    /// </summary>
    [JsonProperty("city")]
    public string City { get; set; } = string.Empty;

    /// <summary>
    ///     Código do estado (ex: SP).
    /// </summary>
    [JsonProperty("state_abbr")]
    public string StateAbbr { get; set; } = string.Empty;

    /// <summary>
    ///     CEP.
    /// </summary>
    [JsonProperty("postal_code")]
    public string PostalCode { get; set; } = string.Empty;

    /// <summary>
    ///     CPF ou CNPJ.
    /// </summary>
    [JsonProperty("document")]
    public string? Document { get; set; }

    /// <summary>
    ///     E-mail (opcional para destinatário).
    /// </summary>
    [JsonProperty("email")]
    public string? Email { get; set; }

    /// <summary>
    ///     Código do país (sempre "BR").
    /// </summary>
    [JsonProperty("country_id")]
    public string CountryId { get; set; } = "BR";
}
