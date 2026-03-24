using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Models;

/// <summary>
///     InformaÃ§Ãµes detalhadas do remetente ou destinatÃ¡rio.
/// </summary>
public class ContactInfo
{
    /// <summary>
    ///     Nome completo (precisa ter nome e sobrenome).
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     Rua do endereÃ§o.
    /// </summary>
    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;

    /// <summary>
    ///     Complemento do endereÃ§o.
    /// </summary>
    [JsonPropertyName("complement")]
    public string? Complement { get; set; }

    /// <summary>
    ///     NÃºmero do endereÃ§o.
    /// </summary>
    [JsonPropertyName("number")]
    public string? Number { get; set; }

    /// <summary>
    ///     Bairro.
    /// </summary>
    [JsonPropertyName("district")]
    public string District { get; set; } = string.Empty;

    /// <summary>
    ///     Cidade.
    /// </summary>
    [JsonPropertyName("city")]
    public string City { get; set; } = string.Empty;

    /// <summary>
    ///     CÃ³digo do estado (ex: SP).
    /// </summary>
    [JsonPropertyName("state_abbr")]
    public string StateAbbr { get; set; } = string.Empty;

    /// <summary>
    ///     CEP.
    /// </summary>
    [JsonPropertyName("postal_code")]
    public string PostalCode { get; set; } = string.Empty;

    /// <summary>
    ///     CPF ou CNPJ.
    /// </summary>
    [JsonPropertyName("document")]
    public string? Document { get; set; }

    /// <summary>
    ///     E-mail (opcional para destinatÃ¡rio).
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>
    ///     CÃ³digo do paÃ­s (sempre "BR").
    /// </summary>
    [JsonPropertyName("country_id")]
    public string CountryId { get; set; } = "BR";
}

