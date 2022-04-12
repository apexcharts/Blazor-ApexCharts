using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApexCharts
{

    public abstract class ApexBaseSeries<TItem> : ComponentBase where TItem : class
    {
        [CascadingParameter(Name = "Chart")] public ApexChart<TItem> Chart { get; set; }
        [Parameter] public string Name { get; set; }
        [Parameter] public Func<TItem, object> XValue { get; set; }
        [Parameter] public bool ShowDataLabels { get; set; }
        [Parameter] public IEnumerable<TItem> Items { get; set; }
        [Parameter] public SeriesStroke Stroke { get; set; }
        [Parameter] public string Color { get; set; }
        [Parameter] public Func<TItem, string> PointColor { get; set; }
        public async Task Toggle()
        {
            await Chart?.ToggleSeriesAsync(Name);
        }

        public async Task Show()
        {
            await Chart?.ShowSeriesAsync(Name);
        }

        public async Task Hide()
        {
            await Chart?.HideSeriesAsync(Name);
        }

        public string GetPointColor(TItem item)
        {
            if (PointColor == null || item == null) { return null; }
            return PointColor.Invoke(item);
        }

        public string GetPointColor(IEnumerable<TItem> items)
        {
            if (PointColor == null || items == null || !items.Any()) { return null; }
            return PointColor.Invoke(items.First());
        }

        public List<T> UpdateDataPoints<T>(IEnumerable<T> items, Action<T> updateMethod)
        {
            var data = items.ToList();
            if (updateMethod == null)
            {
                return data;
            }

            foreach (T item in data)
            {
                updateMethod(item);
            }
            return data;
        }
    }
}