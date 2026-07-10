using System.Text.Json;
using System.Text.Json.Serialization;
using Berdsk.Sdk.SuperFrete.Converters;
using Berdsk.Sdk.SuperFrete.Services.Webhooks.Dtos;
using FluentAssertions;

namespace Berdsk.Sdk.SuperFrete.Tests.Unit.Converters
{
    public class SfWebhookTagsConverterTests
    {
        private class TagsHolder
        {
            [JsonPropertyName("tags")]
            [JsonConverter(typeof(SfWebhookTagsConverter))]
            public List<SfWebhookTag> Tags { get; set; } = new();
        }

        [Fact]
        public void Read_IndexedObject_ReturnsTagList()
        {
            const string json =
                """{ "tags": { "0": { "name": "order_id", "value": "order-1555" }, "1": { "name": "loja", "value": "abc" } } }""";

            var result = JsonSerializer.Deserialize<TagsHolder>(json);

            result!.Tags.Should().HaveCount(2);
            result.Tags[0].Name.Should().Be("order_id");
            result.Tags[0].Value.Should().Be("order-1555");
            result.Tags[1].Name.Should().Be("loja");
            result.Tags[1].Value.Should().Be("abc");
        }

        [Fact]
        public void Read_EmptyArray_ReturnsEmptyList()
        {
            const string json = """{ "tags": [] }""";

            var result = JsonSerializer.Deserialize<TagsHolder>(json);

            result!.Tags.Should().NotBeNull();
            result.Tags.Should().BeEmpty();
        }

        [Fact]
        public void Read_EmptyObject_ReturnsEmptyList()
        {
            const string json = """{ "tags": {} }""";

            var result = JsonSerializer.Deserialize<TagsHolder>(json);

            result!.Tags.Should().BeEmpty();
        }

        [Fact]
        public void Read_Null_ReturnsEmptyList()
        {
            const string json = """{ "tags": null }""";

            var result = JsonSerializer.Deserialize<TagsHolder>(json);

            result!.Tags.Should().NotBeNull();
            result.Tags.Should().BeEmpty();
        }

        [Fact]
        public void Read_ArrayOfObjects_ReturnsTagList()
        {
            const string json = """{ "tags": [ { "name": "order_id", "value": "order-1555" } ] }""";

            var result = JsonSerializer.Deserialize<TagsHolder>(json);

            result!.Tags.Should().ContainSingle();
            result.Tags[0].Name.Should().Be("order_id");
            result.Tags[0].Value.Should().Be("order-1555");
        }

        [Fact]
        public void Read_StringValues_ReturnsTagsWithName()
        {
            const string json = """{ "tags": { "0": "tagx", "1": "tagy" } }""";

            var result = JsonSerializer.Deserialize<TagsHolder>(json);

            result!.Tags.Should().HaveCount(2);
            result.Tags[0].Name.Should().Be("tagx");
            result.Tags[0].Value.Should().BeNull();
            result.Tags[1].Name.Should().Be("tagy");
        }

        [Fact]
        public void Read_ArrayOfStrings_ReturnsTagsWithName()
        {
            const string json = """{ "tags": [ "tagx", "tagy" ] }""";

            var result = JsonSerializer.Deserialize<TagsHolder>(json);

            result!.Tags.Should().HaveCount(2);
            result.Tags[0].Name.Should().Be("tagx");
            result.Tags[1].Name.Should().Be("tagy");
        }

        [Fact]
        public void Read_NullEntries_AreSkipped()
        {
            const string json = """{ "tags": { "0": null, "1": { "name": "order_id", "value": "x" } } }""";

            var result = JsonSerializer.Deserialize<TagsHolder>(json);

            result!.Tags.Should().ContainSingle();
            result.Tags[0].Name.Should().Be("order_id");
        }

        [Fact]
        public void Read_RealWebhookPayload_ParsesTags()
        {
            const string json =
                """{"id":"ClmHZOg0p9CWbpFwKsLm","tags":{"0":{"name":"order_id","value":"order-1555"}},"status":"released"}""";

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var result = JsonSerializer.Deserialize<SfWebhookPayloadData>(json, options);

            result!.Tags.Should().ContainSingle();
            result.Tags[0].Name.Should().Be("order_id");
            result.Tags[0].Value.Should().Be("order-1555");
        }

        [Fact]
        public void Write_TagList_WritesArrayOfObjects()
        {
            var holder = new TagsHolder
            {
                Tags = new List<SfWebhookTag> { new() { Name = "order_id", Value = "order-1555" } }
            };

            var json = JsonSerializer.Serialize(holder);

            json.Should().Be("""{"tags":[{"name":"order_id","value":"order-1555"}]}""");
        }

        [Fact]
        public void Write_EmptyList_WritesEmptyArray()
        {
            var json = JsonSerializer.Serialize(new TagsHolder());

            json.Should().Be("""{"tags":[]}""");
        }
    }
}
