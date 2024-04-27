using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ApexCharts.Internal;

/// <summary>
/// Ensures that JS function arrays are serialized with the key '@eval' so they can be appropriately evaluated on the client side
/// Example:
/// <code>
/// myFunction: {
///     "@eval": [ "function(value) { return value; }" ]
/// }
/// </code>
/// </summary>
internal class FunctionValueOrListConverterConverter : JsonConverter<CustomFunction>
{
    public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(CustomFunction);

    public override CustomFunction Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, CustomFunction value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("@eval");
        if (value == null || value.Count == 0)
            JsonSerializer.Serialize(writer, null, options);
        else if (value.Count == 1)
            JsonSerializer.Serialize(writer, value[0], options);
        else
            JsonSerializer.Serialize<List<string>>(writer, value, options);
        writer.WriteEndObject();
    }
}