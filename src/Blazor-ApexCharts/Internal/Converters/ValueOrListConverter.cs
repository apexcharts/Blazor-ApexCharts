using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ApexCharts.Internal
{
    /// <summary>
    /// Facilitates serialization of <see cref="ValueOrList{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class ValueOrListConverter<T> : JsonConverter<ValueOrList<T>>
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
            if (value == null || value.Count == 0)
                JsonSerializer.Serialize(writer, null, options);
            else if (value.Count == 1)
                JsonSerializer.Serialize(writer, value[0], options);
            else
                JsonSerializer.Serialize<List<T>>(writer, value, options);
        }
    }
}
