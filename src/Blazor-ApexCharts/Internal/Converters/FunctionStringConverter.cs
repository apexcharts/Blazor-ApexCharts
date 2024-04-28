using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ApexCharts.Internal;

/// <summary>
/// Ensures that JS functions are serialized with the key '@eval' so they can be appropriately evaluated on the client side
/// Example:
/// <code>
/// myFunction: {
///     "@eval": "function(value) { return value; }"
/// }
/// </code>
/// </summary>
internal class FunctionStringConverter : JsonConverter<string>
{
    public override bool CanConvert(Type typeToConvert) => true;

    public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("@eval");
        writer.WriteStringValue(value);
        writer.WriteEndObject();
    }
}