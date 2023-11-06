using Bogus.Bson;
using Bogus.DataSets;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorApexCharts.Docs
{

    public class TimeSeriesGenerator
    {
        public List<TimeSeries> TimeSeries { get; set; } = new();

        public long Range { get; private set; }

        public TimeSeriesGenerator(int points)
        {
            SetRange(points);

            var date = DateTimeOffset.Now.AddDays(-100);

            for (int i = 0; i < points; i++)
            {
                TimeSeries.Add(GenerateNewPoint(date));
                date = date.AddDays(1);
            }

        }

        private void SetRange(int points)
        {
            Range = DateTimeOffset.Now.ToUnixTimeMilliseconds() - DateTimeOffset.Now.AddDays(-points).ToUnixTimeMilliseconds();
        }


        public TimeSeries GenerateNewPoint(DateTimeOffset newDate)
        {
            var rnd = new Random();
            var value = rnd.Next(10, 90);
            return new TimeSeries { Date = newDate, Value = value, Quantity = rnd.Next(1, 20) };
        }


    }

    public class TimeSeries
    {
        public long DateMilliseconds => Date.ToUnixTimeMilliseconds();
        public DateTimeOffset Date { get; set; }
        public decimal Value { get; set; }
        public int Quantity { get; set; }
    }
}
