using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.ShippingServices.Dtos
{
    /// <summary>
    ///     Formatos de embalagem aceitos por um serviço de entrega,
    ///     cada um com suas respectivas restrições de dimensões e peso.
    /// </summary>
    public class SfServiceFormatsResponse
    {
        /// <summary>
        ///     Restrições para o formato de caixa (<c>box</c>),
        ///     com limites de peso, largura, altura e comprimento.
        /// </summary>
        [JsonPropertyName("package")]
        public SfPackageRestrictionsResponse? Package { get; set; }
    }
}
