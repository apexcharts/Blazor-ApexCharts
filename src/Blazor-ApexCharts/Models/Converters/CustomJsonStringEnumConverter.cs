using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ApexCharts.Models
{

    //https://stackoverflow.com/questions/59059989/system-text-json-how-do-i-specify-a-custom-name-for-an-enum-value
    public class CustomJsonStringEnumConverter : JsonConverterFactory
    {
        private readonly JsonNamingPolicy namingPolicy;
        private readonly bool allowIntegerValues;
        private readonly JsonStringEnumConverter baseConverter;

        public CustomJsonStringEnumConverter() : this(null, true) { }

        public CustomJsonStringEnumConverter(JsonNamingPolicy namingPolicy = null, bool allowIntegerValues = true)
        {
            this.namingPolicy = namingPolicy;
            this.allowIntegerValues = allowIntegerValues;
            this.baseConverter = new JsonStringEnumConverter(namingPolicy, allowIntegerValues);
        }

        public override bool CanConvert(Type typeToConvert) => baseConverter.CanConvert(typeToConvert);

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            var query = from field in typeToConvert.GetFields(BindingFlags.Public | BindingFlags.Static)
                        let attr = field.GetCustomAttribute<EnumMemberAttribute>()
                        where attr != null
                        select (field.Name, attr.Value);
            var dictionary = query.ToDictionary(p => p.Item1, p => p.Item2);
            if (dictionary.Count > 0)
            {
                return new JsonStringEnumConverter(new DictionaryLookupNamingPolicy(dictionary, namingPolicy), allowIntegerValues).CreateConverter(typeToConvert, options);
            }
            else
            {
                return new JsonStringEnumConverter(JsonNamingPolicy.CamelCase).CreateConverter(typeToConvert, options);
            }
        }
    }

    public class JsonNamingPolicyDecorator : JsonNamingPolicy
    {
        readonly JsonNamingPolicy underlyingNamingPolicy;

        public JsonNamingPolicyDecorator(JsonNamingPolicy underlyingNamingPolicy) => this.underlyingNamingPolicy = underlyingNamingPolicy;

        public override string ConvertName(string name) => underlyingNamingPolicy == null ? name : underlyingNamingPolicy.ConvertName(name);
    }

    internal class DictionaryLookupNamingPolicy : JsonNamingPolicyDecorator
    {
        readonly Dictionary<string, string> dictionary;

        public DictionaryLookupNamingPolicy(Dictionary<string, string> dictionary, JsonNamingPolicy underlyingNamingPolicy) : base(underlyingNamingPolicy) => this.dictionary = dictionary ?? throw new ArgumentNullException();

        public override string ConvertName(string name) => dictionary.TryGetValue(name, out var value) ? value : base.ConvertName(name);
    }
}
