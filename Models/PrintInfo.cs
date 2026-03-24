using Newtonsoft.Json;

namespace Berdsk.Sdk.SuperFrete.Models;

public class PrintInfo
{
    [JsonProperty("url")] public string Url { get; set; } = string.Empty;
}
