using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ApexCharts.Models
{

    //public class CategoryJsonConverter<T> : JsonConverter<Category<T>>
    //{
    //    public override Category<T> Read(ref Utf8JsonReader reader,
    //                                     Type typeToConvert,
    //                                     JsonSerializerOptions options)
    //    {
    //        var converter = GetKeyConverter(options);
    //        var key = converter.Read(ref reader, typeToConvert, options);

    //        return new Category<T>(key);
    //    }

    //    public override void Write(Utf8JsonWriter writer,
    //                               Category<T> value,
    //                               JsonSerializerOptions options)
    //    {
    //        var converter = GetKeyConverter(options);
    //        converter.Write(writer, value.Key, options);
    //    }

    //    private static JsonConverter<T> GetKeyConverter(JsonSerializerOptions options)
    //    {
    //        var converter = options.GetConverter(typeof(T)) as JsonConverter<T>;

    //        if (converter is null)
    //            throw new JsonException("...");

    //        return converter;
    //    }
    //}


    public class DataPointConverter<T> : JsonConverter<IDataPoint<T>>
    {
        public override IDataPoint<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, IDataPoint<T> value, JsonSerializerOptions options)
        {
            if (value is DataPoint<T>)
                JsonSerializer.Serialize(writer, value as DataPoint<T>, typeof(DataPoint<T>), options);
            else if (value is BubblePoint<T>)
                JsonSerializer.Serialize(writer, value as BubblePoint<T>, typeof(BubblePoint<T>), options);
            else
                throw new ArgumentOutOfRangeException(nameof(value), $"Unknown implementation of the interface {nameof(IDataPoint<T>)} for the parameter {nameof(value)}. Unknown implementation: {value?.GetType().Name}");
        }
    }
}
