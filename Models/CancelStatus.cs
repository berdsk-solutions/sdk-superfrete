using Newtonsoft.Json;

namespace Berdsk.Sdk.SuperFrete.Models;

public class CancelStatus
{
    [JsonProperty("canceled")] public bool Canceled { get; set; }
}
