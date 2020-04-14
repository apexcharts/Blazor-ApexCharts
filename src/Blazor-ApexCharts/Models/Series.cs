using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApexCharts
{

    /// <summary> 
    /// Chart Series options.
    /// Use ApexNonAxisChartSeries for Pie and Donut charts.
    /// See https://apexcharts.com/docs/options/series/
    ///
    /// According to the documentation at
    /// https://apexcharts.com/docs/series/
    /// Section 1: data can be a list of single numbers
    /// Sections 2.1 and 3.1: data can be a list of tuples of two numbers
    /// Sections 2.2 and 3.2: data can be a list of objects where x is a string
    /// and y is a number
    /// And according to the demos, data can contain null.
    /// https://apexcharts.com/javascript-chart-demos/line-charts/null-values/
    /// </summary>
    public class Series
    {
        public List<DataPoint> Data { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        [JsonIgnore]
        public bool ShowDataLabels { get; set; }
    }

    public class DataPoint
    {

        public DataPoint() { }

        public DataPoint(object x)
        {
            X = x;
        }

        public DataPoint(object x, decimal? y)
        {
            X = x;
            Y = y;
        }

        public object X { get; set; }
        public object Y{ get; set; }

        [JsonIgnore]
        public List<object> Items { get; set; }

    }
}
