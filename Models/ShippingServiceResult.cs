using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Models;

/// <summary>
///     Resultado de um serviço de frete calculado.
/// </summary>
public class ShippingServiceResult
{
    /// <summary>
    ///     ID do serviço.
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; set; }

    /// <summary>
    ///     Nome do serviço (ex: PAC, SEDEX).
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     Preço do frete.
    /// </summary>
    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    /// <summary>
    ///     Prazo de entrega em dias.
    /// </summary>
    [JsonPropertyName("delivery_time")]
    public int DeliveryTime { get; set; }

    /// <summary>
    ///     Indica se houve erro no cálculo deste serviço.
    /// </summary>
    [JsonPropertyName("error")]
    public string? Error { get; set; }
}

