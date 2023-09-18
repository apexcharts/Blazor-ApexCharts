using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ApexCharts.Internal
{
    /// <summary>
    /// Facilitates serialization of <see cref="IDataPoint{TItem}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class DataPointConverter<T> : JsonConverter<IDataPoint<T>>
    {
        /// <inheritdoc/>
        /// <exception cref="NotImplementedException"></exception>
        public override IDataPoint<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
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
