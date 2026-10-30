using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ApexCharts.Internal;

/// <summary>
/// Writes a size that apexcharts.js parses as a CSS length string ("50%" or "200px").
/// A number is written as a px string, because apexcharts.js calls string methods on the value
/// and throws when it receives a number. A string is written as it is.
/// </summary>
internal class CssLengthConverter : JsonConverter<object>
{
    public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
    {
        switch (value)
        {
            case null:
                writer.WriteNullValue();
                return;
            case string text:
                writer.WriteStringValue(text);
                return;
            case double or float or decimal or int or long or short or byte or sbyte or uint or ulong or ushort:
                writer.WriteStringValue(Convert.ToString(value, CultureInfo.InvariantCulture) + "px");
                return;
            default:
                JsonSerializer.Serialize(writer, value, value.GetType(), options);
                return;
        }
    }
}
