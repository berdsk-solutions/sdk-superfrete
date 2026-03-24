using Newtonsoft.Json;

namespace Berdsk.Sdk.SuperFrete.Models;

/// <summary>
///     Informações do usuário autenticado.
/// </summary>
public class UserInfo
{
    [JsonProperty("id")] public string Id { get; set; } = string.Empty;

    [JsonProperty("firstname")] public string Firstname { get; set; } = string.Empty;

    [JsonProperty("lastname")] public string Lastname { get; set; } = string.Empty;

    [JsonProperty("phone")] public string Phone { get; set; } = string.Empty;

    [JsonProperty("email")] public string Email { get; set; } = string.Empty;

    [JsonProperty("document")] public string Document { get; set; } = string.Empty;

    [JsonProperty("limits")] public UserLimits Limits { get; set; } = new();

    [JsonProperty("balance")] public decimal Balance { get; set; }
}
