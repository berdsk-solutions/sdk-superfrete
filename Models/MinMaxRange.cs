using Newtonsoft.Json;

namespace Berdsk.Sdk.SuperFrete.Models;

public class MinMaxRange
{
    [JsonProperty("min")] public double Min { get; set; }

    [JsonProperty("max")] public double Max { get; set; }
}
