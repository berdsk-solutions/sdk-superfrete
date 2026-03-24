using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Models;

public class CompanyInfo
{
    [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;

    [JsonPropertyName("picture")] public string Picture { get; set; } = string.Empty;
}

