using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Berdsk.Sdk.SuperFrete.Services.Webhooks.Dtos;

namespace Berdsk.Sdk.SuperFrete.Converters
{
    /// <summary>
    ///     Conversor JSON que normaliza o campo <c>tags</c> dos webhooks da SuperFrete para uma lista de
    ///     <see cref="SfWebhookTag" />, detectando automaticamente o formato recebido:
    ///     <list type="bullet">
    ///         <item>Objeto indexado por posição (ex: <c>{ "0": { "name": "order_id", "value": "order-1555" } }</c>)</item>
    ///         <item>Array vazio (ex: <c>[]</c>), enviado pela API quando o pedido não possui tags</item>
    ///         <item>Array de objetos (ex: <c>[{ "name": "order_id", "value": "order-1555" }]</c>)</item>
    ///         <item>Strings simples como itens ou valores, convertidas para <see cref="SfWebhookTag.Name" /></item>
    ///     </list>
    ///     Na serialização, escreve sempre um array de objetos. Nunca retorna <c>null</c>: valores nulos ou
    ///     vazios resultam em lista vazia.
    /// </summary>
    public class SfWebhookTagsConverter : JsonConverter<List<SfWebhookTag>>
    {
        /// <summary>
        ///     Indica que o conversor também deve ser invocado para tokens <c>null</c>.
        /// </summary>
        public override bool HandleNull => true;

        /// <summary>
        ///     Lê o campo <c>tags</c> em qualquer um dos formatos suportados e converte para lista de
        ///     <see cref="SfWebhookTag" />.
        /// </summary>
        /// <param name="reader">Leitor JSON posicionado no valor a ser lido.</param>
        /// <param name="typeToConvert">Tipo alvo da conversão.</param>
        /// <param name="options">Opções de serialização.</param>
        /// <returns>Lista de tags do pedido. Lista vazia quando o valor for nulo, array vazio ou objeto vazio.</returns>
        /// <exception cref="JsonException">Lançada quando o token JSON não representa um conjunto de tags reconhecível.</exception>
        public override List<SfWebhookTag> Read(ref Utf8JsonReader reader, Type typeToConvert,
            JsonSerializerOptions options)
        {
            var tags = new List<SfWebhookTag>();

            switch (reader.TokenType)
            {
                case JsonTokenType.Null:
                    return tags;

                case JsonTokenType.StartArray:
                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                    {
                        var tag = ReadTag(ref reader, options);
                        if (tag != null)
                            tags.Add(tag);
                    }

                    return tags;

                case JsonTokenType.StartObject:
                    while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
                    {
                        if (reader.TokenType != JsonTokenType.PropertyName)
                            continue;

                        reader.Read();
                        var tag = ReadTag(ref reader, options);
                        if (tag != null)
                            tags.Add(tag);
                    }

                    return tags;

                default:
                    throw new JsonException(
                        $"Token JSON inesperado '{reader.TokenType}' ao desserializar as tags de um webhook da SuperFrete.");
            }
        }

        /// <summary>
        ///     Escreve as tags como array de objetos, ou array vazio quando a lista for nula ou vazia.
        /// </summary>
        /// <param name="writer">Escritor JSON para o qual o valor será escrito.</param>
        /// <param name="value">Lista de tags a serializar.</param>
        /// <param name="options">Opções de serialização.</param>
        public override void Write(Utf8JsonWriter writer, List<SfWebhookTag> value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();

            if (value != null)
                foreach (var tag in value)
                    if (tag != null)
                        JsonSerializer.Serialize(writer, tag, options);

            writer.WriteEndArray();
        }

        private static SfWebhookTag? ReadTag(ref Utf8JsonReader reader, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.Null:
                    return null;

                case JsonTokenType.String:
                    var name = reader.GetString();
                    return string.IsNullOrWhiteSpace(name) ? null : new SfWebhookTag { Name = name };

                case JsonTokenType.StartObject:
                    return JsonSerializer.Deserialize<SfWebhookTag>(ref reader, options);

                default:
                    reader.Skip();
                    return null;
            }
        }
    }
}
