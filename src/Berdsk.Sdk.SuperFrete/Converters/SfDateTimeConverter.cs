using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Berdsk.Sdk.SuperFrete.Converters
{
    /// <summary>
    ///     Conversor JSON que desserializa datas da API SuperFrete para <see cref="DateTime" />,
    ///     detectando automaticamente o formato recebido:
    ///     <list type="bullet">
    ///         <item>String ISO 8601 (ex: <c>"2026-06-24T22:41:58.325Z"</c>)</item>
    ///         <item>String em formato brasileiro dia/mês/ano (ex: <c>"02/07/2026 21:53:27"</c>)</item>
    ///         <item>Objeto Firestore Timestamp (ex: <c>{ "_seconds": 1696156800, "_nanoseconds": 0 }</c>)</item>
    ///         <item>Número unix epoch em segundos ou milissegundos</item>
    ///     </list>
    ///     Na serialização, escreve sempre string ISO 8601.
    /// </summary>
    public class SfDateTimeConverter : JsonConverter<DateTime?>
    {
        /// <summary>
        ///     Limite a partir do qual um valor numérico unix epoch é interpretado como milissegundos.
        ///     Valores em segundos abaixo deste limite cobrem datas até o ano 2286.
        /// </summary>
        private const long UnixMillisecondsThreshold = 10_000_000_000;

        /// <summary>
        ///     Quantidade de nanossegundos em um tick de <see cref="DateTime" /> (100ns por tick).
        /// </summary>
        private const long NanosecondsPerTick = 100;

        /// <summary>
        ///     Formatos brasileiros dia/mês/ano usados pela API de rastreamento (ex: <c>"02/07/2026 21:53:27"</c>).
        ///     Precisam de <see cref="DateTime.TryParseExact(string, string, IFormatProvider, DateTimeStyles, out DateTime)" />
        ///     pois o parse invariante interpretaria o primeiro componente como mês.
        /// </summary>
        private static readonly string[] BrazilianDateFormats =
        {
            "dd/MM/yyyy HH:mm:ss",
            "dd/MM/yyyy"
        };

        /// <summary>
        ///     Indica que o conversor também deve ser invocado para tokens <c>null</c>.
        /// </summary>
        public override bool HandleNull => true;

        /// <summary>
        ///     Lê uma data em qualquer um dos formatos suportados e converte para <see cref="DateTime" /> em UTC.
        /// </summary>
        /// <param name="reader">Leitor JSON posicionado no valor a ser lido.</param>
        /// <param name="typeToConvert">Tipo alvo da conversão.</param>
        /// <param name="options">Opções de serialização.</param>
        /// <returns>Data convertida em UTC, ou <c>null</c> se o valor for nulo ou string vazia.</returns>
        /// <exception cref="JsonException">Lançada quando o token JSON não representa uma data reconhecível.</exception>
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.Null:
                    return null;

                case JsonTokenType.String:
                    var value = reader.GetString();
                    if (string.IsNullOrWhiteSpace(value))
                        return null;
                    if (DateTime.TryParseExact(value, BrazilianDateFormats, CultureInfo.InvariantCulture,
                            DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out var brazilianDate))
                        return brazilianDate;
                    return DateTime.Parse(value, CultureInfo.InvariantCulture,
                        DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal);

                case JsonTokenType.Number:
                    return FromUnixEpoch(reader.GetInt64());

                case JsonTokenType.StartObject:
                    return ReadFirestoreTimestamp(ref reader);

                default:
                    throw new JsonException(
                        $"Token JSON inesperado '{reader.TokenType}' ao desserializar uma data da SuperFrete.");
            }
        }

        /// <summary>
        ///     Escreve a data como string ISO 8601 (formato round-trip), ou <c>null</c>.
        /// </summary>
        /// <param name="writer">Escritor JSON para o qual o valor será escrito.</param>
        /// <param name="value">Data a serializar.</param>
        /// <param name="options">Opções de serialização.</param>
        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNullValue();
                return;
            }

            writer.WriteStringValue(value.Value);
        }

        private static DateTime ReadFirestoreTimestamp(ref Utf8JsonReader reader)
        {
            long seconds = 0;
            long nanoseconds = 0;

            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if (reader.TokenType != JsonTokenType.PropertyName)
                    continue;

                var propertyName = reader.GetString();
                reader.Read();

                switch (propertyName)
                {
                    case "_seconds":
                    case "seconds":
                        seconds = reader.GetInt64();
                        break;
                    case "_nanoseconds":
                    case "nanoseconds":
                        nanoseconds = reader.GetInt64();
                        break;
                    default:
                        reader.Skip();
                        break;
                }
            }

            return DateTimeOffset.FromUnixTimeSeconds(seconds).UtcDateTime
                .AddTicks(nanoseconds / NanosecondsPerTick);
        }

        private static DateTime FromUnixEpoch(long epoch)
        {
            return epoch >= UnixMillisecondsThreshold
                ? DateTimeOffset.FromUnixTimeMilliseconds(epoch).UtcDateTime
                : DateTimeOffset.FromUnixTimeSeconds(epoch).UtcDateTime;
        }
    }
}
