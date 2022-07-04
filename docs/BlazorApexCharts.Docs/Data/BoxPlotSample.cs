using System;

namespace BlazorApexCharts.Docs
{
    public class BoxPlotSample
    {
        public string Name { get; set; }
        public DateTimeOffset EventDate { get; set; }

        public decimal Min { get; set; }
        public decimal Q1 { get; set; }
        public decimal Median { get; set; }
        public decimal Q3 { get; set; }
        public decimal Max { get; set; }
    }
}
