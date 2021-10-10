using System.Collections.Generic;

namespace ApexCharts
{
    public interface IDataPoint<TItem>
    {
        IList<TItem> Items { get; set; }
        object X { get; set; }
        object YObject { get;}
       
    }
}
