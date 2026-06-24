namespace Berdsk.Sdk.SuperFrete.Helpers
{
    /// <summary>
    ///     Constantes de status de pedido/etiqueta da SuperFrete.
    /// </summary>
    public static class SfOrderStatus
    {
        /// <summary>
        ///     Aguardando emissão (pagamento).
        /// </summary>
        public const string Pending = "pending";

        /// <summary>
        ///     Aguardando postagem (etiqueta paga e liberada).
        /// </summary>
        public const string Released = "released";

        /// <summary>
        ///     Postado na transportadora.
        /// </summary>
        public const string Posted = "posted";

        /// <summary>
        ///     Entregue ao destinatário.
        /// </summary>
        public const string Delivered = "delivered";

        /// <summary>
        ///     Cancelado.
        /// </summary>
        public const string Canceled = "canceled";
    }
}
