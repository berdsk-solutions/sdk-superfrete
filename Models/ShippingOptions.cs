using Newtonsoft.Json;

namespace Berdsk.Sdk.SuperFrete.Models;

/// <summary>
///     Opções adicionais para o cálculo do frete.
/// </summary>
public class ShippingOptions
{
    /// <summary>
    ///     Indica se o serviço de Mão Própria deve ser considerado.
    /// </summary>
    [JsonProperty("own_hand")]
    public bool OwnHand { get; set; }

    /// <summary>
    ///     Indica se o serviço de Aviso de Recebimento deve ser considerado.
    /// </summary>
    [JsonProperty("receipt")]
    public bool Receipt { get; set; }

    /// <summary>
    ///     Valor declarado da encomenda utilizado para cálculo do seguro.
    /// </summary>
    [JsonProperty("insurance_value")]
    public decimal? InsuranceValue { get; set; }

    /// <summary>
    ///     Indica se o cálculo deve incluir o seguro.
    /// </summary>
    [JsonProperty("use_insurance_value")]
    public bool UseInsuranceValue { get; set; }
}
