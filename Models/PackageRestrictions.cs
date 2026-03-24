using Newtonsoft.Json;

namespace Berdsk.Sdk.SuperFrete.Models;

public class PackageRestrictions
{
    [JsonProperty("weight")] public MinMaxRange Weight { get; set; } = new();

    [JsonProperty("width")] public MinMaxRange Width { get; set; } = new();

    [JsonProperty("height")] public MinMaxRange Height { get; set; } = new();

    [JsonProperty("length")] public MinMaxRange Length { get; set; } = new();

    [JsonProperty("sum")] public double Sum { get; set; }
}
