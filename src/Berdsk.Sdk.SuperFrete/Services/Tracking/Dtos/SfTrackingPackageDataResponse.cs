using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Services.Tracking.Dtos
{
    /// <summary>
    ///     Dimensões e peso do pacote.
    ///     A API retorna estes valores ora como número, ora como string — a leitura aceita ambos.
    /// </summary>
    [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)]
    public class SfTrackingPackageDataResponse
    {
        /// <summary>
        ///     Largura do pacote, em centímetros.
        /// </summary>
        [JsonPropertyName("width")]
        public decimal? Width { get; set; }

        /// <summary>
        ///     Altura do pacote, em centímetros.
        /// </summary>
        [JsonPropertyName("height")]
        public decimal? Height { get; set; }

        /// <summary>
        ///     Comprimento do pacote, em centímetros.
        /// </summary>
        [JsonPropertyName("depth")]
        public decimal? Depth { get; set; }

        /// <summary>
        ///     Diâmetro do pacote, em centímetros (para formato rolo/cilindro).
        /// </summary>
        [JsonPropertyName("diameter")]
        public decimal? Diameter { get; set; }

        /// <summary>
        ///     Peso do pacote, em quilogramas.
        /// </summary>
        [JsonPropertyName("weight")]
        public decimal? Weight { get; set; }

        /// <summary>
        ///     Valor declarado do conteúdo.
        /// </summary>
        [JsonPropertyName("declared_value")]
        public decimal? DeclaredValue { get; set; }

        /// <summary>
        ///     Indica se a opção de valor declarado foi contratada.
        /// </summary>
        [JsonPropertyName("declared_value_option")]
        public bool? DeclaredValueOption { get; set; }
    }
}
