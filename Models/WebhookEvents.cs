namespace Berdsk.Sdk.SuperFrete.Models;

/// <summary>
///     Eventos disponíveis para webhooks.
/// </summary>
public static class WebhookEvents
{
    public const string OrderCreated = "order.created";
    public const string OrderReleased = "order.released";
    public const string OrderGenerated = "order.generated";
    public const string OrderPosted = "order.posted";
    public const string OrderDelivered = "order.delivered";
    public const string OrderCancelled = "order.cancelled";
}

