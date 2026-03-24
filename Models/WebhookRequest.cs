using Newtonsoft.Json;

namespace Berdsk.Sdk.SuperFrete.Models;

/// <summary>
///     Requisição para criar ou atualizar um webhook.
/// </summary>
public class WebhookRequest
{
    /// <summary>
    ///     Nome do Webhook app.
    /// </summary>
    [JsonProperty("name")]
    public string? Name { get; set; }

    /// <summary>
    ///     URL que receberá as notificações.
    /// </summary>
    [JsonProperty("url")]
    public string? Url { get; set; }

    /// <summary>
    ///     Lista de eventos para receber as atualizações.
    /// </summary>
    [JsonProperty("events")]
    public List<string>? Events { get; set; }

    /// <summary>
    ///     Ativar ou desativar o webhook.
    /// </summary>
    [JsonProperty("is_active")]
    public bool? IsActive { get; set; }
}
