using Newtonsoft.Json;

namespace Berdsk.Sdk.SuperFrete.Models;

public class ServiceFormats
{
    [JsonProperty("package")] public PackageRestrictions Package { get; set; } = new();
}
