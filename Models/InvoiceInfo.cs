using Newtonsoft.Json;

namespace Berdsk.Sdk.SuperFrete.Models;

/// <summary>
///     Informações da nota fiscal.
/// </summary>
public class InvoiceInfo
{
    /// <summary>
    ///     Número da nota fiscal (44 dígitos).
    /// </summary>
    [JsonProperty("number")]
    public string Number { get; set; } = string.Empty;

    /// <summary>
    ///     Identificador da nota fiscal.
    /// </summary>
    [JsonProperty("key")]
    public string? Key { get; set; }
}
