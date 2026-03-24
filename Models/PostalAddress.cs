using Newtonsoft.Json;

namespace Berdsk.Sdk.SuperFrete.Models;

/// <summary>
///     Representa um endereço postal simplificado.
/// </summary>
public class PostalAddress
{
    /// <summary>
    ///     CEP do endereço.
    /// </summary>
    [JsonProperty("postal_code")]
    public string PostalCode { get; set; } = string.Empty;
}
