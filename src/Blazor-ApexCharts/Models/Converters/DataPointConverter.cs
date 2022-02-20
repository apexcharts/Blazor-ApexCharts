using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ApexCharts.Models
{
    public class DataPointConverter<T> : JsonConverter<IDataPoint<T>>
    {
        public override IDataPoint<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, IDataPoint<T> value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                JsonSerializer.Serialize(writer, (IDataPoint<T>)null, options);
            }
            else
            {
                var type = value.GetType();
                JsonSerializer.Serialize(writer, value, type, options);
            }

        }
    }
}

