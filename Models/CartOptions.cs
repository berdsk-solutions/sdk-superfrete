using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Models;

/// <summary>
///     Opções adicionais para o envio de frete.
/// </summary>
public class CartOptions : ShippingOptions
{
    /// <summary>
    ///     Indica se deve usar declaração de conteúdo (true) ou nota fiscal (false).
    /// </summary>
    [JsonPropertyName("non_commercial")]
    public bool? NonCommercial { get; set; }

    /// <summary>
    ///     Dados da nota fiscal.
    /// </summary>
    [JsonPropertyName("invoice")]
    public InvoiceInfo? Invoice { get; set; }
}

