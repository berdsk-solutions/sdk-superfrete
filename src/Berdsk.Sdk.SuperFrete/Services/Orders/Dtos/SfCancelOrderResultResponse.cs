using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Orders.Dtos
{
    /// <summary>
    ///     Resultado do cancelamento de uma etiqueta específica.
    ///     A API retorna um dicionário <c>Dictionary&lt;string, SfCancelOrderResultResponse&gt;</c>
    ///     onde a chave é o ID da etiqueta cancelada e este DTO é o valor correspondente.
    /// </summary>
    public class SfCancelOrderResultResponse
    {
        /// <summary>
        ///     Indica se a etiqueta foi cancelada com sucesso.
        /// </summary>
        [JsonPropertyName("canceled")]
        public bool Canceled { get; set; }
    }
}
