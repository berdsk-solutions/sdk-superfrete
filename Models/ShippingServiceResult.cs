using Newtonsoft.Json;

namespace Berdsk.Sdk.SuperFrete.Models;

/// <summary>
///     Resultado de um serviço de frete calculado.
/// </summary>
public class ShippingServiceResult
{
    /// <summary>
    ///     ID do serviço.
    /// </summary>
    [JsonProperty("id")]
    public int Id { get; set; }

    /// <summary>
    ///     Nome do serviço (ex: PAC, SEDEX).
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     Preço do frete.
    /// </summary>
    [JsonProperty("price")]
    public decimal Price { get; set; }

    /// <summary>
    ///     Prazo de entrega em dias.
    /// </summary>
    [JsonProperty("delivery_time")]
    public int DeliveryTime { get; set; }

    /// <summary>
    ///     Indica se houve erro no cálculo deste serviço.
    /// </summary>
    [JsonProperty("error")]
    public string? Error { get; set; }
}
