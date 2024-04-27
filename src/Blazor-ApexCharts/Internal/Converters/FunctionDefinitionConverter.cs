using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ApexCharts.Internal;

/// <summary>
/// Ensures that JS functions are prefixed with '@eval:' so they can be appropriately evaluated on the client side 
/// </summary>
internal class FunctionDefinitionConverter : JsonConverter<string>
{
    public override bool CanConvert(Type typeToConvert) => true;

    public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotSupportedException();
    }

    public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
    {
        writer.WriteStringValue("@eval:" + value);
    }
}