using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Models;

public class ServiceFormats
{
    [JsonPropertyName("package")] public PackageRestrictions Package { get; set; } = new();
}

