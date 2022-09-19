namespace ApexCharts
{
    public class GaugeValue
    {
        public string Label { get; set; }
        public decimal Percentage { get; set; }
        [System.Obsolete("This property is obsolete. Use Percentage instead.", false)]
        public decimal Precentage { get => Percentage; set => Percentage = value; }
    }
}

