using Newtonsoft.Json;

namespace Berdsk.Sdk.SuperFrete.Models;

/// <summary>
///     Informações técnicas de um serviço de entrega.
/// </summary>
public class ServiceInfo
{
    [JsonProperty("name")] public string Name { get; set; } = string.Empty;

    [JsonProperty("type")] public string Type { get; set; } = string.Empty;

    [JsonProperty("range")] public string Range { get; set; } = string.Empty;

    [JsonProperty("restrictions")] public ServiceRestrictions Restrictions { get; set; } = new();

    [JsonProperty("requirements")] public List<string> Requirements { get; set; } = new();

    [JsonProperty("optionals")] public List<string> Optionals { get; set; } = new();

    [JsonProperty("company")] public CompanyInfo Company { get; set; } = new();
}
