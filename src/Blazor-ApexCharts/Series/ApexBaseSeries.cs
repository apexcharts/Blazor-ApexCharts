using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
        public async Task Toggle()
        {
            await Chart?.ToggleSeries(Name);
        }

        public async Task Show()
        {
            await Chart?.ShowSeries(Name);
        }

        public async Task Hide()
        {
            await Chart?.HideSeries(Name);
        }

    }
}