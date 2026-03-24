using Newtonsoft.Json;

namespace Berdsk.Sdk.SuperFrete.Models;

/// <summary>
///     Informações de um webhook cadastrado.
/// </summary>
public class WebhookResponse
{
    [JsonProperty("id")] public string Id { get; set; } = string.Empty;

    [JsonProperty("name")] public string Name { get; set; } = string.Empty;

    [JsonProperty("url")] public string Url { get; set; } = string.Empty;

    [JsonProperty("secret_token")] public string? SecretToken { get; set; }

    [JsonProperty("events")] public List<string> Events { get; set; } = new();

    [JsonProperty("is_active")] public bool IsActive { get; set; }

    [JsonProperty("created_at")] public string CreatedAt { get; set; } = string.Empty;

    [JsonProperty("updated_at")] public string? UpdatedAt { get; set; }
}
