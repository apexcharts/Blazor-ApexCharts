using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ApexCharts.Internal;

/// <summary>
/// Converter for lists of strings that detects entries containing a JavaScript function definition
/// (case-insensitive search for the word "function") and serializes those as an object with the "@eval" key
/// so that ApexCharts interprets them as executable functions instead of plain strings.
///
/// Output behavior:
/// - Normal strings are written as JSON string elements inside the array.
/// - Strings representing functions are written as:
///   { "@eval": "function(x) { return x; }" }
///
/// Example:
/// C# input:
/// <code>
/// new List&lt;string&gt;
/// {
///     "Label 1",
///     "function(w) { return w; }",
///     "Another label"
/// };
/// </code>
///
/// JSON output:
/// <code>
/// [
///   "Label 1",
///   { "@eval": "function(w) { return w; }" },
///   "Another label"
/// ]
/// </code>
///
/// Note: Deserialization (Read) is not implemented because this converter is intended only for outgoing
/// serialization to the client.
/// </summary>
internal class ListStringOrFunctionConverter : JsonConverter<List<string>>
{
    public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(List<string>);

    public override List<string> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, List<string> value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();

        foreach (var item in value)
        {
            if (ChartUtilities.IsJavaScriptFunction(item))
            {
                writer.WriteStartObject();
                writer.WritePropertyName("@eval");
                JsonSerializer.Serialize(writer, item, options);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteStringValue(item);
            }
        }

        writer.WriteEndArray();
    }
}