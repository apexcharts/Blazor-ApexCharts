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
                AddRandom(date);
                date = date.AddDays(1);
            }

        }

        private void SetRange(int points)
        {
            Range = DateTimeOffset.Now.ToUnixTimeMilliseconds() - DateTimeOffset.Now.AddDays(-points).ToUnixTimeMilliseconds();
        }

      

        public void Update()
        {
            var first = TimeSeries.First();
            TimeSeries.Remove(first);

            var maxDate = TimeSeries.Max(e => e.Date);
            maxDate = maxDate.AddDays(1);
            AddRandom(maxDate);

        }

        private void AddRandom(DateTimeOffset date)
        {
            var rnd = new Random();
            var value = rnd.Next(1, 100);
            TimeSeries.Add(new TimeSeries { Date = date, Value = value });
        }

    }

    public class TimeSeries
    {
        public DateTimeOffset Date { get; set; }
        public decimal Value { get; set; }
    }
}
