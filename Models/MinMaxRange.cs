using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Models;

public class MinMaxRange
{
    [JsonPropertyName("min")] public double Min { get; set; }

    [JsonPropertyName("max")] public double Max { get; set; }
}

