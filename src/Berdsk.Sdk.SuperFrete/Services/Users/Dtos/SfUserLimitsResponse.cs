using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Users.Dtos
{
    /// <summary>
    ///     Limites de uso da conta do usuário na plataforma SuperFrete.
    /// </summary>
    public class SfUserLimitsResponse
    {
        /// <summary>
        ///     Quantidade de etiquetas aguardando postagem (já pagas, mas ainda não postadas).
        /// </summary>
        [JsonPropertyName("shipments")]
        public int Shipments { get; set; }

        /// <summary>
        ///     Quantidade restante de etiquetas que podem ser colocadas em espera simultaneamente.
        /// </summary>
        [JsonPropertyName("shipments_available")]
        public int ShipmentsAvailable { get; set; }
    }
}
