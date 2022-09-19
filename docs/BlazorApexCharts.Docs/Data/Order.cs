using System;

namespace BlazorApexCharts.Docs
{
    public class Order
    {
        public Guid OrderId { get; set; } = Guid.NewGuid();
        public string CustomerName { get; set; }
        public string Country { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public OrderType OrderType { get; set; }
        public decimal GrossValue { get; set; }
        public decimal NetValue { get =>  GrossValue * (1 - (DiscountPercentage / 100)) ; }
        public decimal DiscountPercentage { get; set; }
    }

    public enum OrderType
    {
        Web, Contract, Mail, Phone
    }
}
