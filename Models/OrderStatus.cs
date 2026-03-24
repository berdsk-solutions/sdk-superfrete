namespace Berdsk.Sdk.SuperFrete.Models;

/// <summary>
///     Status possÃ­veis de uma etiqueta/pedido.
/// </summary>
public static class OrderStatus
{
    /// <summary>
    ///     Aguardando emissÃ£o (pagamento).
    /// </summary>
    public const string Pending = "pending";

    /// <summary>
    ///     Aguardando postagem.
    /// </summary>
    public const string Released = "released";

    /// <summary>
    ///     Postado.
    /// </summary>
    public const string Posted = "posted";

    /// <summary>
    ///     Entregue.
    /// </summary>
    public const string Delivered = "delivered";

    /// <summary>
    ///     Cancelado.
    /// </summary>
    public const string Canceled = "canceled";
}

