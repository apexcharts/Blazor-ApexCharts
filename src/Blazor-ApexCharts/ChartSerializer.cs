using ApexCharts.Models;
using System.Collections.Generic;
using System.Text.Json;

namespace BlazorApexCharts
{
    public class ChartSerializer
    {
        private static Dictionary<string, JsonSerializerOptions> _serializerOptions = new Dictionary<string, JsonSerializerOptions>();

        private JsonSerializerOptions GenerateOptions<TItem>() where TItem : class
        {
            var serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                IgnoreNullValues = true,
            };
            serializerOptions.Converters.Add(new DataPointConverter<TItem>());
            serializerOptions.Converters.Add(new SeriesConverter<TItem>());
            serializerOptions.Converters.Add(new CustomJsonStringEnumConverter());

            return serializerOptions;
        }

        public JsonSerializerOptions GetOptions<TItem>() where TItem : class
        {
            string key = typeof(TItem).ToString();
            if (_serializerOptions.ContainsKey(key))
            {
                return _serializerOptions[key];
            }

            var newOptions = GenerateOptions<TItem>();
            _serializerOptions.Add(key, newOptions);

            return newOptions;
        }
    }
}
