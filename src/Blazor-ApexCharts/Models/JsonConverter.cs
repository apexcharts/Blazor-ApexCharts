using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ApexCharts.Models
{
   public class DataPointConverter : JsonConverter<IDataPoint>
{
    public override IDataPoint Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, IDataPoint value, JsonSerializerOptions options)
    {
        if (value is DataPoint)
            JsonSerializer.Serialize(writer, value as DataPoint, typeof(DataPoint), options);
        else if (value is BubblePoint)
            JsonSerializer.Serialize(writer, value as BubblePoint, typeof(BubblePoint), options);
        else
            throw new ArgumentOutOfRangeException(nameof(value), $"Unknown implementation of the interface {nameof(IDataPoint)} for the parameter {nameof(value)}. Unknown implementation: {value?.GetType().Name}");
    }
}
}
