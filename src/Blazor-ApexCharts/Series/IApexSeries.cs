using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ApexCharts
{
    public interface IApexSeries<TItem>
    {
        string Name { get; set; }
        Expression<Func<TItem, object>> XValue { get; set; }
        IEnumerable<TItem> Items { get; set; }
    }
}
