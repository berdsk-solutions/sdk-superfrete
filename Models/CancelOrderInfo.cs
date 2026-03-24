using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Models;

public class CancelOrderInfo
{
    [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;

    [JsonPropertyName("description")] public string Description { get; set; } = "Cancelado pelo usuário";
}

