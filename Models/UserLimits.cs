using Newtonsoft.Json;

namespace Berdsk.Sdk.SuperFrete.Models;

public class UserLimits
{
    [JsonProperty("shipments")] public int Shipments { get; set; }

    [JsonProperty("shipments_available")] public int ShipmentsAvailable { get; set; }
}
