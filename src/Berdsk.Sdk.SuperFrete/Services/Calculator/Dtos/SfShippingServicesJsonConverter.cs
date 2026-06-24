using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Berdsk.Sdk.SuperFrete.Helpers;

namespace Berdsk.Sdk.SuperFrete.Services.Calculator.Dtos
{
    /// <summary>
    ///     Conversor JSON para serializar e desserializar um array de <see cref="SfShippingServiceType" />
    ///     no formato de string separada por vírgulas exigido pela API da SuperFrete (ex: "1,2,17").
    /// </summary>
    public class SfShippingServicesJsonConverter : JsonConverter<SfShippingServiceType[]?>
    {
        /// <summary>
        ///     Lê uma string separada por vírgulas da API e converte para um array de <see cref="SfShippingServiceType" />.
        /// </summary>
        /// <param name="reader">Leitor JSON posicionado no valor a ser lido.</param>
        /// <param name="typeToConvert">Tipo alvo da conversão.</param>
        /// <param name="options">Opções de serialização.</param>
        /// <returns>
        ///     Array de <see cref="SfShippingServiceType" /> representando os serviços encontrados na string,
        ///     ou <c>null</c> se o valor for vazio.
        /// </returns>
        public override SfShippingServiceType[]? Read(ref Utf8JsonReader reader, Type typeToConvert,
            JsonSerializerOptions options)
        {
            var value = reader.GetString();
            if (string.IsNullOrEmpty(value))
                return null;

            return value
                .Split(',')
                .Select(s => (SfShippingServiceType)int.Parse(s.Trim()))
                .ToArray();
        }

        /// <summary>
        ///     Converte um array de <see cref="SfShippingServiceType" /> para a string separada por vírgulas
        ///     esperada pela API da SuperFrete (ex: <c>[Pac, Sedex]</c> → <c>"1,2"</c>).
        /// </summary>
        /// <param name="writer">Escritor JSON para o qual o valor será escrito.</param>
        /// <param name="value">Array de serviços a serializar.</param>
        /// <param name="options">Opções de serialização.</param>
        public override void Write(Utf8JsonWriter writer, SfShippingServiceType[]? value, JsonSerializerOptions options)
        {
            if (value == null || value.Length == 0)
            {
                writer.WriteNullValue();
                return;
            }

            writer.WriteStringValue(string.Join(",", value.Select(s => (int)s)));
        }
    }
}
