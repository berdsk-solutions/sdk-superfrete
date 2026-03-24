using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Models;

public class OrderTag
{
    [JsonPropertyName("tag")] public string Tag { get; set; } = string.Empty;

    [JsonPropertyName("url")] public string? Url { get; set; }
}

