namespace Berdsk.Sdk.SuperFrete.Helpers
{
    /// <summary>
    ///     Constantes de eventos de webhook da SuperFrete.
    /// </summary>
    public static class SfWebhookEvent
    {
        /// <summary>
        ///     Disparado quando um pedido/etiqueta é criado na plataforma.
        /// </summary>
        public const string OrderCreated = "order.created";

        /// <summary>
        ///     Disparado quando uma etiqueta é paga e liberada para postagem.
        /// </summary>
        public const string OrderReleased = "order.released";

        /// <summary>
        ///     Disparado quando a etiqueta de frete é gerada.
        /// </summary>
        public const string OrderGenerated = "order.generated";

        /// <summary>
        ///     Disparado quando o objeto é postado na transportadora.
        /// </summary>
        public const string OrderPosted = "order.posted";

        /// <summary>
        ///     Disparado quando o objeto é entregue ao destinatário.
        /// </summary>
        public const string OrderDelivered = "order.delivered";

        /// <summary>
        ///     Disparado quando um pedido/etiqueta é cancelado.
        /// </summary>
        public const string OrderCancelled = "order.cancelled";
    }
}
