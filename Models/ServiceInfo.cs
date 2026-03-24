using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Models;

/// <summary>
///     Informações técnicas de um serviço de entrega.
/// </summary>
public class ServiceInfo
{
    [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;

    [JsonPropertyName("type")] public string Type { get; set; } = string.Empty;

    [JsonPropertyName("range")] public string Range { get; set; } = string.Empty;

    [JsonPropertyName("restrictions")] public ServiceRestrictions Restrictions { get; set; } = new();

    [JsonPropertyName("requirements")] public List<string> Requirements { get; set; } = new();

    [JsonPropertyName("optionals")] public List<string> Optionals { get; set; } = new();

    [JsonPropertyName("company")] public CompanyInfo Company { get; set; } = new();
}

