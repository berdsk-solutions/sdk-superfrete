using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Models;

public class PrintInfo
{
    [JsonPropertyName("url")] public string Url { get; set; } = string.Empty;
}

