using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Models;

public class ServiceRestrictions
{
    [JsonPropertyName("insurance_value")] public MinMaxRange InsuranceValue { get; set; } = new();

    [JsonPropertyName("formats")] public ServiceFormats Formats { get; set; } = new();
}

