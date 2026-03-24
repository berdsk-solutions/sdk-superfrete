using Newtonsoft.Json;

namespace Berdsk.Sdk.SuperFrete.Models;

public class CancelOrderInfo
{
    [JsonProperty("id")] public string Id { get; set; } = string.Empty;

    [JsonProperty("description")] public string Description { get; set; } = "Cancelado pelo usuário";
}
