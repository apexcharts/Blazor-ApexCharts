using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApexCharts.Docs
{
    public class StockPrice
    {
        public string  Company { get; set; }
        public DateTimeOffset Date { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
              

        public int Volume { get; set; }
    }



}
