using System;
using System.Collections.Concurrent;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ApexCharts.Internal
{
    /// <summary>
    /// Contains methods to assist with serializing chart data
    /// </summary>
    internal static class ChartSerializer
    {
        private static readonly ConcurrentDictionary<Type, JsonSerializerOptions> _serializerOptions = new();

        private static JsonSerializerOptions GenerateOptions<TItem>() where TItem : class
        {
            var serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            };

            serializerOptions.Converters.Add(new DataPointConverter<TItem>());
            serializerOptions.Converters.Add(new SeriesConverter<TItem>());
            serializerOptions.Converters.Add(new CustomJsonStringEnumConverter());
            serializerOptions.Converters.Add(new ValueOrListConverter<string>());
            serializerOptions.Converters.Add(new ValueOrListConverter<double>());
            serializerOptions.Converters.Add(new ValueOrListConverter<Curve>());
            serializerOptions.Converters.Add(new ValueOrListConverter<FillPatternStyle>());
            serializerOptions.Converters.Add(new ValueOrListConverter<FillType>());
            
            return serializerOptions;
        }

        /// <summary>
        /// Returns the JSON serializer options for the provided data type
        /// </summary>
        /// <typeparam name="TItem">The data type to be serialized</typeparam>
        internal static JsonSerializerOptions GetOptions<TItem>() where TItem : class
        {
            var key = typeof(TItem);

            if (_serializerOptions.ContainsKey(key))
            {
                return _serializerOptions[key];
            }

            var newOptions = GenerateOptions<TItem>();
            _serializerOptions.TryAdd(key, newOptions);

            return newOptions;
        }
    }
}
