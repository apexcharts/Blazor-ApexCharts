using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ApexCharts.Internal
{
    internal class NullToDefaultIntConverter : JsonConverter<int>
    {
        public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return -1; // Default value for null
            }

            if (reader.TokenType == JsonTokenType.Number && reader.TryGetInt32(out int value))
            {
                return value; // Handle numeric values
            }

            if (reader.TokenType == JsonTokenType.String)
            {
                string stringValue = reader.GetString();
                if (int.TryParse(stringValue, out int parsedValue))
                {
                    return parsedValue; // Handle string values that can be parsed as int
                }
                return -1; // Default value for invalid strings
            }

            throw new JsonException($"Cannot convert {reader.GetString() ?? "null"} to int.");
        }

        public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value);
        }
    }
}
