using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChartSamples
{
    public class Order
    {
        public Guid OrderId { get; set; } = Guid.NewGuid();
        public string CustomerName { get; set; }
        public string Country { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public OrderType OrderType { get; set; }
        public decimal GrossValue { get; set; }
        public decimal NetValue { get =>  GrossValue * (1 - (DiscountPrecentage / 100)) ; }
        public decimal DiscountPrecentage { get; set; }
    }

    public enum OrderType
    {
        Web, Contract, Mail, Phone
    }
}
