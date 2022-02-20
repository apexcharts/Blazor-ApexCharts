namespace ApexCharts
{
    public class LegendClicked<TItem> where TItem : class
    {
        public Series<TItem> Series { get; set; }
        public bool Collapsed { get; set; }
    }
}
