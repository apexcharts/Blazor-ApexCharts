using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApexCharts
{
    public interface IApexSeries<TItem> : IDisposable where TItem : class
    {
        ApexChart<TItem> Chart { get; set; }
        string Name { get; set; }
        Func<TItem, object> XValue { get; set; }
        IEnumerable<TItem> Items { get; set; }

        string Color { get; set; }

        bool ShowDataLabels { get; set; }
        SeriesStroke Stroke { get; set; }

        public IEnumerable<IDataPoint<TItem>> GetData();
        public ChartType GetChartType();

        public Task Toggle();
        public Task Show();
        public Task Hide();

    }
}
