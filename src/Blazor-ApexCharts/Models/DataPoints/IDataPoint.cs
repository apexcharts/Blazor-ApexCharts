using System.Collections.Generic;

namespace ApexCharts
{
    public interface IDataPoint<TItem>
    {
        IEnumerable<TItem> Items { get; set; }
        object X { get; set; }
        string FillColor { get; set; }
 
        object Extra { get; set; }
    }
}
