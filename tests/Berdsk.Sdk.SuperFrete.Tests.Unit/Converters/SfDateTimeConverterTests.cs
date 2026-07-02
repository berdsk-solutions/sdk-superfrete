using System.Text.Json;
using System.Text.Json.Serialization;
using Berdsk.Sdk.SuperFrete.Converters;
using FluentAssertions;

namespace Berdsk.Sdk.SuperFrete.Tests.Unit.Converters
{
    public class SfDateTimeConverterTests
    {
        private class DateHolder
        {
            [JsonPropertyName("date")]
            [JsonConverter(typeof(SfDateTimeConverter))]
            public DateTime? Date { get; set; }
        }

        [Fact]
        public void Read_Iso8601String_ReturnsUtcDateTime()
        {
            const string json = """{ "date": "2026-06-24T22:41:58.325Z" }""";

            var result = JsonSerializer.Deserialize<DateHolder>(json);

            result!.Date.Should().Be(new DateTime(2026, 6, 24, 22, 41, 58, 325, DateTimeKind.Utc));
            result.Date!.Value.Kind.Should().Be(DateTimeKind.Utc);
        }

        [Fact]
        public void Read_FirestoreTimestampObject_ReturnsUtcDateTime()
        {
            const string json = """{ "date": { "_seconds": 1696156800, "_nanoseconds": 500000000 } }""";

            var result = JsonSerializer.Deserialize<DateHolder>(json);

            result!.Date.Should().Be(new DateTime(2023, 10, 1, 10, 40, 0, 500, DateTimeKind.Utc));
            result.Date!.Value.Kind.Should().Be(DateTimeKind.Utc);
        }

        [Fact]
        public void Read_FirestoreTimestampWithoutUnderscorePrefix_ReturnsUtcDateTime()
        {
            const string json = """{ "date": { "seconds": 1696156800, "nanoseconds": 0 } }""";

            var result = JsonSerializer.Deserialize<DateHolder>(json);

            result!.Date.Should().Be(new DateTime(2023, 10, 1, 10, 40, 0, DateTimeKind.Utc));
        }

        [Fact]
        public void Read_UnixEpochSeconds_ReturnsUtcDateTime()
        {
            const string json = """{ "date": 1696156800 }""";

            var result = JsonSerializer.Deserialize<DateHolder>(json);

            result!.Date.Should().Be(new DateTime(2023, 10, 1, 10, 40, 0, DateTimeKind.Utc));
        }

        [Fact]
        public void Read_UnixEpochMilliseconds_ReturnsUtcDateTime()
        {
            const string json = """{ "date": 1696156800500 }""";

            var result = JsonSerializer.Deserialize<DateHolder>(json);

            result!.Date.Should().Be(new DateTime(2023, 10, 1, 10, 40, 0, 500, DateTimeKind.Utc));
        }

        [Fact]
        public void Read_NullValue_ReturnsNull()
        {
            const string json = """{ "date": null }""";

            var result = JsonSerializer.Deserialize<DateHolder>(json);

            result!.Date.Should().BeNull();
        }

        [Fact]
        public void Read_EmptyString_ReturnsNull()
        {
            const string json = """{ "date": "" }""";

            var result = JsonSerializer.Deserialize<DateHolder>(json);

            result!.Date.Should().BeNull();
        }

        [Fact]
        public void Read_UnexpectedToken_ThrowsJsonException()
        {
            const string json = """{ "date": true }""";

            var act = () => JsonSerializer.Deserialize<DateHolder>(json);

            act.Should().Throw<JsonException>();
        }

        [Fact]
        public void Write_DateTime_WritesIso8601String()
        {
            var holder = new DateHolder { Date = new DateTime(2026, 6, 24, 22, 41, 58, DateTimeKind.Utc) };

            var json = JsonSerializer.Serialize(holder);

            json.Should().Contain("\"2026-06-24T22:41:58Z\"");
        }

        [Fact]
        public void Write_Null_WritesNull()
        {
            var holder = new DateHolder { Date = null };

            var json = JsonSerializer.Serialize(holder);

            json.Should().Contain("\"date\":null");
        }
    }
}
