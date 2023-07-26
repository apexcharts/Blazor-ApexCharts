using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ApexCharts.Models
{
    /// <summary>
    /// Facilitates serialization of <see cref="ValueOrList{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ValueOrListConverter<T> : JsonConverter<ValueOrList<T>>
    {
        /// <inheritdoc/>
        /// <exception cref="NotImplementedException"></exception>
        public override ValueOrList<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override bool CanConvert(Type typeToConvert) => typeof(ValueOrList<T>).IsAssignableFrom(typeToConvert);

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, ValueOrList<T> value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                JsonSerializer.Serialize(writer, null, options);
            }
            else
            {
                if (value.IsList)
                {
                    JsonSerializer.Serialize(writer, value.GetList, typeof(IEnumerable<T>), options);
                }
                else
                {
                    JsonSerializer.Serialize(writer, value.GetValue, typeof(T), options);
                }
            }
        }
    }
}

