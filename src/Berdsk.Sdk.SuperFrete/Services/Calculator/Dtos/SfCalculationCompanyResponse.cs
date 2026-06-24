using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Calculator.Dtos
{
    /// <summary>
    ///     Dados da transportadora responsável pelo serviço de entrega calculado.
    /// </summary>
    public class SfCalculationCompanyResponse
    {
        /// <summary>
        ///     Código interno da transportadora na plataforma SuperFrete.
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        ///     Nome comercial da transportadora (ex: "Correios", "Jadlog").
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
