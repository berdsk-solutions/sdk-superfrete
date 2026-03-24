using Newtonsoft.Json;

namespace Berdsk.Sdk.SuperFrete.Models;

public class OrderTag
{
    [JsonProperty("tag")] public string Tag { get; set; } = string.Empty;

    [JsonProperty("url")] public string? Url { get; set; }
}
