using Newtonsoft.Json;

namespace Berdsk.Sdk.SuperFrete.Models;

/// <summary>
///     Opções adicionais para o envio de frete.
/// </summary>
public class CartOptions : ShippingOptions
{
    /// <summary>
    ///     Indica se deve usar declaração de conteúdo (true) ou nota fiscal (false).
    /// </summary>
    [JsonProperty("non_commercial")]
    public bool? NonCommercial { get; set; }

    /// <summary>
    ///     Dados da nota fiscal.
    /// </summary>
    [JsonProperty("invoice")]
    public InvoiceInfo? Invoice { get; set; }
}
