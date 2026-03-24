using Newtonsoft.Json;

namespace Berdsk.Sdk.SuperFrete.Models;

public class ServiceRestrictions
{
    [JsonProperty("insurance_value")] public MinMaxRange InsuranceValue { get; set; } = new();

    [JsonProperty("formats")] public ServiceFormats Formats { get; set; } = new();
}
