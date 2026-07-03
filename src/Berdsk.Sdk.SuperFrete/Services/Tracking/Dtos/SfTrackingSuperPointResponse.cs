using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Berdsk.Sdk.SuperFrete.Converters;

namespace Berdsk.Sdk.SuperFrete.Services.Tracking.Dtos
{
    /// <summary>
    ///     Ponto de postagem/retirada (Superpoint / PUDO) utilizado no envio.
    /// </summary>
    public class SfTrackingSuperPointResponse
    {
        /// <summary>
        ///     Indica se o ponto está ativo.
        /// </summary>
        [JsonPropertyName("active")]
        public bool? Active { get; set; }

        /// <summary>
        ///     Logradouro do ponto.
        /// </summary>
        [JsonPropertyName("address")]
        public string? Address { get; set; }

        /// <summary>
        ///     Transportadoras aceitas pelo ponto, indexadas por posição (ex: <c>{"0": "correios", "1": "jadlog"}</c>).
        /// </summary>
        [JsonPropertyName("carriers")]
        public Dictionary<string, string>? Carriers { get; set; }

        /// <summary>
        ///     Cidade do ponto.
        /// </summary>
        [JsonPropertyName("city")]
        public string? City { get; set; }

        /// <summary>
        ///     Razão social/nome fantasia do estabelecimento.
        /// </summary>
        [JsonPropertyName("company_name")]
        public string? CompanyName { get; set; }

        /// <summary>
        ///     Data de cadastro do ponto (UTC).
        /// </summary>
        [JsonPropertyName("created_at")]
        [JsonConverter(typeof(SfDateTimeConverter))]
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        ///     Data de exclusão do ponto (UTC), quando aplicável.
        /// </summary>
        [JsonPropertyName("deleted_at")]
        [JsonConverter(typeof(SfDateTimeConverter))]
        public DateTime? DeletedAt { get; set; }

        /// <summary>
        ///     Distância em quilômetros entre o remetente e o ponto.
        /// </summary>
        [JsonPropertyName("distance")]
        public double? Distance { get; set; }

        /// <summary>
        ///     Indica se o ponto está marcado como favorito pelo remetente.
        /// </summary>
        [JsonPropertyName("favorite")]
        public bool? Favorite { get; set; }

        /// <summary>
        ///     Coordenadas geográficas do ponto.
        /// </summary>
        [JsonPropertyName("geolocation")]
        public SfTrackingGeolocationResponse? Geolocation { get; set; }

        /// <summary>
        ///     Identificador do ponto (ex: <c>PROVIDER|pegaki|EID|6431</c>).
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        /// <summary>
        ///     Identificador do ponto no banco de dados legado (MongoDB).
        /// </summary>
        [JsonPropertyName("idMongo")]
        public string? MongoId { get; set; }

        /// <summary>
        ///     Código MCU do ponto.
        /// </summary>
        [JsonPropertyName("mcu")]
        public string? Mcu { get; set; }

        /// <summary>
        ///     Bairro do ponto.
        /// </summary>
        [JsonPropertyName("neighborhood")]
        public string? Neighborhood { get; set; }

        /// <summary>
        ///     Número do endereço do ponto.
        /// </summary>
        [JsonPropertyName("number")]
        public string? Number { get; set; }

        /// <summary>
        ///     Horário de funcionamento do ponto.
        /// </summary>
        [JsonPropertyName("opening_hours")]
        public string? OpeningHours { get; set; }

        /// <summary>
        ///     CEP do ponto.
        /// </summary>
        [JsonPropertyName("postcode")]
        public string? Postcode { get; set; }

        /// <summary>
        ///     Provedor da rede de pontos (ex: <c>pegaki</c>).
        /// </summary>
        [JsonPropertyName("provider")]
        public string? Provider { get; set; }

        /// <summary>
        ///     Unidade federativa (UF) do ponto.
        /// </summary>
        [JsonPropertyName("region")]
        public string? Region { get; set; }

        /// <summary>
        ///     Tipo do ponto (ex: <c>public</c>).
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        /// <summary>
        ///     Nome da unidade do ponto.
        /// </summary>
        [JsonPropertyName("unit")]
        public string? Unit { get; set; }

        /// <summary>
        ///     Data da última atualização do cadastro do ponto (UTC).
        /// </summary>
        [JsonPropertyName("updated_at")]
        [JsonConverter(typeof(SfDateTimeConverter))]
        public DateTime? UpdatedAt { get; set; }
    }
}
