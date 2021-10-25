using ApexCharts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorApexCharts.Services
{

  
    public class ChartService
    {
        private Dictionary<string, JsonSerializerOptions> _serializerOptions = new Dictionary<string, JsonSerializerOptions>();

        private JsonSerializerOptions GenerateOptions<TItem>()
        {
            var serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                IgnoreNullValues = true,
            };
            serializerOptions.Converters.Add(new DataPointConverter<TItem>());
            serializerOptions.Converters.Add(new CustomJsonStringEnumConverter());

            return serializerOptions;
        }

        public JsonSerializerOptions GetOptions<TItem>()
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
