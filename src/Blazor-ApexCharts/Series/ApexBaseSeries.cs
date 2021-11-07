using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ApexCharts
{

    public abstract class ApexBaseSeries<TItem> : ComponentBase where TItem : class
    {
        [CascadingParameter(Name = "Chart")] public ApexChart<TItem> Chart { get; set; }
        [Parameter] public string Name { get; set; }
        [Parameter] public Expression<Func<TItem, object>> XValue { get; set; }
        [Parameter] public bool ShowDataLabels { get; set; }
        [Parameter] public IEnumerable<TItem> Items { get; set; }
        [Parameter] public SeriesStroke Stroke { get; set; }
    }
}