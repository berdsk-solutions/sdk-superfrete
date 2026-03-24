using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Models;

/// <summary>
///     InformaÃ§Ãµes do usuÃ¡rio autenticado.
/// </summary>
public class UserInfo
{
    [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;

    [JsonPropertyName("firstname")] public string Firstname { get; set; } = string.Empty;

    [JsonPropertyName("lastname")] public string Lastname { get; set; } = string.Empty;

    [JsonPropertyName("phone")] public string Phone { get; set; } = string.Empty;

    [JsonPropertyName("email")] public string Email { get; set; } = string.Empty;

    [JsonPropertyName("document")] public string Document { get; set; } = string.Empty;

    [JsonPropertyName("limits")] public UserLimits Limits { get; set; } = new();

    [JsonPropertyName("balance")] public decimal Balance { get; set; }
}

