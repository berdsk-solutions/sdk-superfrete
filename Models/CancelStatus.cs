using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Models;

public class CancelStatus
{
    [JsonPropertyName("canceled")] public bool Canceled { get; set; }
}

