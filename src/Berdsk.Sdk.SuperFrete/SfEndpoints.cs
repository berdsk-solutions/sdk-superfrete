namespace Berdsk.Sdk.SuperFrete
{
    /// <summary>
    ///     Constantes de endpoints da API SuperFrete, organizadas por domínio de negócio.
    /// </summary>
    public static class SfEndpoints
    {
        /// <summary>
        ///     Endpoints do serviço de cotação de fretes.
        /// </summary>
        public static class Calculator
        {
            /// <summary>
            ///     Endpoint para calcular cotações de frete.
            ///     Método: POST.
            /// </summary>
            public const string Calculate = "api/v0/calculator";
        }

        /// <summary>
        ///     Endpoints do serviço de informações técnicas dos serviços de entrega.
        /// </summary>
        public static class ShippingServices
        {
            /// <summary>
            ///     Endpoint para obter informações técnicas (dimensões, pesos, restrições) de cada serviço.
            ///     Método: GET.
            /// </summary>
            public const string Info = "api/v0/services/info";
        }

        /// <summary>
        ///     Endpoints do serviço de carrinho de fretes.
        /// </summary>
        public static class Cart
        {
            /// <summary>
            ///     Endpoint para adicionar um frete ao carrinho e gerar uma etiqueta com status <c>pending</c>.
            ///     Método: POST.
            /// </summary>
            public const string Add = "api/v0/cart";
        }

        /// <summary>
        ///     Endpoints do serviço de checkout.
        /// </summary>
        public static class Checkout
        {
            /// <summary>
            ///     Endpoint para realizar o checkout (pagamento de etiquetas com saldo em conta).
            ///     Método: POST.
            /// </summary>
            public const string Finalize = "api/v0/checkout";
        }

        /// <summary>
        ///     Endpoints do serviço de pedidos e etiquetas.
        /// </summary>
        public static class Orders
        {
            /// <summary>
            ///     Endpoint para obter informações detalhadas de um pedido pelo ID.
            ///     Método: GET. Use <see cref="string.Format(string, object)" /> para substituir <c>{0}</c> pelo ID do pedido.
            /// </summary>
            public const string Info = "api/v0/order/info/{0}";

            /// <summary>
            ///     Endpoint para cancelar um pedido/etiqueta.
            ///     Método: POST.
            /// </summary>
            public const string Cancel = "api/v0/order/cancel";

            /// <summary>
            ///     Endpoint para obter o link de impressão (PDF) de etiquetas.
            ///     Método: POST.
            /// </summary>
            public const string Print = "api/v0/tag/print";

            /// <summary>
            ///     Endpoint para listar pedidos/etiquetas do usuário autenticado.
            ///     Método: GET.
            /// </summary>
            public const string List = "api/v0/me/orders";
        }

        /// <summary>
        ///     Endpoints do serviço de webhooks.
        /// </summary>
        public static class Webhooks
        {
            /// <summary>
            ///     Endpoint base de webhooks — usado para criação (POST) e listagem (GET).
            /// </summary>
            public const string Base = "api/v0/webhook";

            /// <summary>
            ///     Endpoint de webhook por ID — usado para atualização (PUT) e exclusão (DELETE).
            ///     Use <see cref="string.Format(string, object)" /> para substituir <c>{0}</c> pelo ID do webhook.
            /// </summary>
            public const string ById = "api/v0/webhook/{0}";
        }

        /// <summary>
        ///     Endpoints do serviço de usuário.
        /// </summary>
        public static class Users
        {
            /// <summary>
            ///     Endpoint para obter informações do usuário autenticado (saldo, limites etc.).
            ///     Método: GET.
            /// </summary>
            public const string Info = "api/v0/user";

            /// <summary>
            ///     Endpoint para listar os endereços cadastrados do usuário autenticado.
            ///     Método: GET.
            /// </summary>
            public const string Addresses = "api/v0/user/addresses";
        }
    }
}
