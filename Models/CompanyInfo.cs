using Newtonsoft.Json;

namespace Berdsk.Sdk.SuperFrete.Models;

public class CompanyInfo
{
    [JsonProperty("name")] public string Name { get; set; } = string.Empty;

    [JsonProperty("picture")] public string Picture { get; set; } = string.Empty;
}
