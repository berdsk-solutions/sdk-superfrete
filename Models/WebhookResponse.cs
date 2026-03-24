using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Models;

/// <summary>
///     Informações de um webhook cadastrado.
/// </summary>
public class WebhookResponse
{
    [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;

    [JsonPropertyName("url")] public string Url { get; set; } = string.Empty;

    [JsonPropertyName("secret_token")] public string? SecretToken { get; set; }

    [JsonPropertyName("events")] public List<string> Events { get; set; } = new();

    [JsonPropertyName("is_active")] public bool IsActive { get; set; }

    [JsonPropertyName("created_at")] public string CreatedAt { get; set; } = string.Empty;

    [JsonPropertyName("updated_at")] public string? UpdatedAt { get; set; }
}

