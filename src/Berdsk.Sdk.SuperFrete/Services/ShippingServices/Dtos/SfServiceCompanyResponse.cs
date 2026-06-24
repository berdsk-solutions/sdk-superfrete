using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.ShippingServices.Dtos
{
    /// <summary>
    ///     Dados da transportadora associada a um serviço de entrega da SuperFrete.
    /// </summary>
    public class SfServiceCompanyResponse
    {
        /// <summary>
        ///     Nome da transportadora (ex: "Correios", "Jadlog").
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        ///     URL do logotipo da transportadora.
        /// </summary>
        [JsonPropertyName("picture")]
        public string? Picture { get; set; }
    }
}
