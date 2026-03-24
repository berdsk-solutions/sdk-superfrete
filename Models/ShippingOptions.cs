using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Models;

/// <summary>
///     OpÃ§Ãµes adicionais para o cÃ¡lculo do frete.
/// </summary>
public class ShippingOptions
{
    /// <summary>
    ///     Indica se o serviÃ§o de MÃ£o PrÃ³pria deve ser considerado.
    /// </summary>
    [JsonPropertyName("own_hand")]
    public bool OwnHand { get; set; }

    /// <summary>
    ///     Indica se o serviÃ§o de Aviso de Recebimento deve ser considerado.
    /// </summary>
    [JsonPropertyName("receipt")]
    public bool Receipt { get; set; }

    /// <summary>
    ///     Valor declarado da encomenda utilizado para cÃ¡lculo do seguro.
    /// </summary>
    [JsonPropertyName("insurance_value")]
    public decimal? InsuranceValue { get; set; }

    /// <summary>
    ///     Indica se o cÃ¡lculo deve incluir o seguro.
    /// </summary>
    [JsonPropertyName("use_insurance_value")]
    public bool UseInsuranceValue { get; set; }
}

