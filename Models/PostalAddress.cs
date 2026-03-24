using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Models;

/// <summary>
///     Representa um endereço postal simplificado.
/// </summary>
public class PostalAddress
{
    /// <summary>
    ///     CEP do endereço.
    /// </summary>
    [JsonPropertyName("postal_code")]
    public string PostalCode { get; set; } = string.Empty;
}

