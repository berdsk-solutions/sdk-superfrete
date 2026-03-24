using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Models;

public class UserLimits
{
    [JsonPropertyName("shipments")] public int Shipments { get; set; }

    [JsonPropertyName("shipments_available")] public int ShipmentsAvailable { get; set; }
}

