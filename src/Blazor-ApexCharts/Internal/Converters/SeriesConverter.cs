using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ApexCharts.Internal
{
    /// <summary>
    /// Facilitates serialization of <see cref="Series{TItem}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class SeriesConverter<T> : JsonConverter<List<Series<T>>> where T : class
    {
        /// <inheritdoc/>
        /// <exception cref="NotImplementedException"></exception>
        public override List<Series<T>> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override void Write(Utf8JsonWriter writer, List<Series<T>> value, JsonSerializerOptions options)
        {
            if (value == null || !value.Any())
            {
                JsonSerializer.Serialize(writer, (IDataPoint<T>)null, options);
            }
            else
            {
                var series = value.First();

                if (series.ApexSeries.Chart.IsNoAxisChart)
                {
                    var data = series.Data.Select(e => (DataPoint<T>)e).Where(e => e.Y != null).Select(e => (decimal)e.Y);
                    JsonSerializer.Serialize(writer, data, typeof(IEnumerable<decimal>), options);
                }
                else
                {
                    JsonSerializer.Serialize(writer, value, typeof(IEnumerable<Series<T>>), options);
                }
            }
        }
    }
}
