using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Models;

public class PackageRestrictions
{
    [JsonPropertyName("weight")] public MinMaxRange Weight { get; set; } = new();

    [JsonPropertyName("width")] public MinMaxRange Width { get; set; } = new();

    [JsonPropertyName("height")] public MinMaxRange Height { get; set; } = new();

    [JsonPropertyName("length")] public MinMaxRange Length { get; set; } = new();

    [JsonPropertyName("sum")] public double Sum { get; set; }
}

