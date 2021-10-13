using Bogus;
using System;
using System.Collections.Generic;

namespace BlazorApexCharts.Docs
{
    public static class SampleData
    {

        public static List<Order> GetRandomOrders()
        {
            var rnd = new Random();
            var orders = new List<Order>();

            for (int i = 0; i < rnd.Next(5, 20); i++)
            {
                orders.Add(new Order { CustomerName = "Odio Corporation", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-12), GrossValue = rnd.Next(2000, 50000), DiscountPrecentage = rnd.Next(10, 50), OrderType = (OrderType)rnd.Next(0, 4) });
            }

            return orders;
        }

        public static List<Order> GetOrders()
        {
            var orders = new List<Order>();
            orders.Add(new Order { CustomerName = "Odio Corporation", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-12), GrossValue = 34531, DiscountPrecentage = 21, OrderType = OrderType.Contract });
            orders.Add(new Order { CustomerName = "Odio Corporation", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-100), GrossValue = 2800, DiscountPrecentage = 12, OrderType = OrderType.Mail });
            orders.Add(new Order { CustomerName = "Odio Corporation", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-128), GrossValue = 12532, DiscountPrecentage = 24, OrderType = OrderType.Contract });
            orders.Add(new Order { CustomerName = "Odio Corporation", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-232), GrossValue = 1400, DiscountPrecentage = 65, OrderType = OrderType.Mail });
            orders.Add(new Order { CustomerName = "Odio Corporation", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-321), GrossValue = 22000, DiscountPrecentage = 10, OrderType = OrderType.Contract });
            orders.Add(new Order { CustomerName = "Odio Corporation", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-400), GrossValue = 3000, DiscountPrecentage = 17, OrderType = OrderType.Web });

            orders.Add(new Order { CustomerName = "Nascetur AB", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-17), GrossValue = 2134, DiscountPrecentage = 10, OrderType = OrderType.Phone });
            orders.Add(new Order { CustomerName = "Nascetur AB", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-27), GrossValue = 11345, DiscountPrecentage = 12, OrderType = OrderType.Phone });
            orders.Add(new Order { CustomerName = "Nascetur AB", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-124), GrossValue = 63400, DiscountPrecentage = 79, OrderType = OrderType.Mail });
            orders.Add(new Order { CustomerName = "Nascetur AB", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-299), GrossValue = 1235, DiscountPrecentage = 12, OrderType = OrderType.Mail });
            orders.Add(new Order { CustomerName = "Nascetur AB", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-372), GrossValue = 44000, DiscountPrecentage = 11, OrderType = OrderType.Phone });
            orders.Add(new Order { CustomerName = "Nascetur AB", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-410), GrossValue = 17000, DiscountPrecentage = 5, OrderType = OrderType.Phone });

            orders.Add(new Order { CustomerName = "Justo Eu Institute", Country = "Spain", OrderDate = DateTimeOffset.Now.AddDays(-10), GrossValue = 12000, DiscountPrecentage = 23, OrderType = OrderType.Mail });
            orders.Add(new Order { CustomerName = "Justo Eu Institute", Country = "Spain", OrderDate = DateTimeOffset.Now.AddDays(-13), GrossValue = 92800, DiscountPrecentage = 48, OrderType = OrderType.Mail });
            orders.Add(new Order { CustomerName = "Justo Eu Institute", Country = "Spain", OrderDate = DateTimeOffset.Now.AddDays(-45), GrossValue = 12532, DiscountPrecentage = 24, OrderType = OrderType.Web });
            orders.Add(new Order { CustomerName = "Justo Eu Institute", Country = "Spain", OrderDate = DateTimeOffset.Now.AddDays(-60), GrossValue = 1400, DiscountPrecentage = 12, OrderType = OrderType.Mail });
            orders.Add(new Order { CustomerName = "Justo Eu Institute", Country = "Spain", OrderDate = DateTimeOffset.Now.AddDays(-150), GrossValue = 22000, DiscountPrecentage = 10, OrderType = OrderType.Web });
            orders.Add(new Order { CustomerName = "Justo Eu Institute", Country = "Spain", OrderDate = DateTimeOffset.Now.AddDays(-200), GrossValue = 3000, DiscountPrecentage = 17, OrderType = OrderType.Web });

            orders.Add(new Order { CustomerName = "Ani Vent", Country = "France", OrderDate = DateTimeOffset.Now.AddDays(-17), GrossValue = 2134, DiscountPrecentage = 34, OrderType = OrderType.Phone });
            orders.Add(new Order { CustomerName = "Ani Vent", Country = "France", OrderDate = DateTimeOffset.Now.AddDays(-27), GrossValue = 11345, DiscountPrecentage = 12, OrderType = OrderType.Phone });
            orders.Add(new Order { CustomerName = "Ani Vent", Country = "France", OrderDate = DateTimeOffset.Now.AddDays(-124), GrossValue = 17002, DiscountPrecentage = 32, OrderType = OrderType.Mail });

            orders.Add(new Order { CustomerName = "Cali Inc", Country = "France", OrderDate = DateTimeOffset.Now.AddDays(-10), GrossValue = 77000, DiscountPrecentage = 17, OrderType = OrderType.Web });
            orders.Add(new Order { CustomerName = "Cali Inc", Country = "France", OrderDate = DateTimeOffset.Now.AddDays(-110), GrossValue = 120000, DiscountPrecentage = 23, OrderType = OrderType.Web });
            orders.Add(new Order { CustomerName = "Cali Inc", Country = "France", OrderDate = DateTimeOffset.Now.AddDays(-243), GrossValue = 44000, DiscountPrecentage = 8, OrderType = OrderType.Web });

            return orders;
        }

        public static List<SupportIncident> GetSupportIncidents(int severityMin = 0, int severityMax = 100)
        {
            var fakeIncidents = new Faker<SupportIncident>()
            .RuleFor(o => o.Severity, f => f.Random.Number(severityMin, severityMax))
            .RuleFor(o => o.Source, f => f.PickRandom<IncidentSource>())
            .RuleFor(o => o.WeekNumber, f => f.Random.Number(1, 20));

            var test = fakeIncidents.Generate(300);
            return test;

        }

        public static List<SupportIncident> GetSupportIncidentsForRange()
        {
            return new List<SupportIncident>
           {
               new SupportIncident { Source = IncidentSource.Customer, Severity = 20},
               new SupportIncident { Source = IncidentSource.Customer, Severity = 35},
               new SupportIncident { Source = IncidentSource.Integration, Severity = 5},
               new SupportIncident { Source = IncidentSource.Integration, Severity = 70},
               new SupportIncident { Source = IncidentSource.Internal, Severity = 55},
               new SupportIncident { Source = IncidentSource.Internal, Severity = 83},
               new SupportIncident { Source = IncidentSource.ThridParty, Severity = 12},
               new SupportIncident { Source = IncidentSource.ThridParty, Severity = 24},
           };
        }

        public static List<StockPrice> GetStockPrices()
        {
            return new List<StockPrice>
            {
                new StockPrice { Company = "ApexInc", Date = DateTimeOffset.Now.AddDays(-10), Open = 10.1m, High = 13.4m, Low = 9.5m, Close = 12.6m, Volume = 13021 },
                new StockPrice { Company = "ApexInc", Date = DateTimeOffset.Now.AddDays(-9), Open = 12.6m, High = 13.1m, Low = 11.3m, Close = 13.2m, Volume = 8562 },
                new StockPrice { Company = "ApexInc", Date = DateTimeOffset.Now.AddDays(-8), Open = 13.2m, High = 13.9m, Low = 11.1m, Close = 11.8m, Volume = 5932 },
                new StockPrice { Company = "ApexInc", Date = DateTimeOffset.Now.AddDays(-7), Open = 11.8m, High = 12.4m, Low = 9.5m, Close = 12.6m, Volume = 13021 },
                new StockPrice { Company = "ApexInc", Date = DateTimeOffset.Now.AddDays(-6), Open = 12.6m, High = 13.1m, Low = 12.3m, Close = 13.2m, Volume = 8562 },
                new StockPrice { Company = "ApexInc", Date = DateTimeOffset.Now.AddDays(-5), Open = 13.2m, High = 12.9m, Low = 10.1m, Close = 11.8m, Volume = 5932 },
                new StockPrice { Company = "ApexInc", Date = DateTimeOffset.Now.AddDays(-4), Open = 10.1m, High = 13.4m, Low = 9.5m, Close = 12.6m, Volume = 13021 },
                new StockPrice { Company = "ApexInc", Date = DateTimeOffset.Now.AddDays(-3), Open = 12.6m, High = 13.1m, Low = 11.3m, Close = 13.2m, Volume = 8562 },
                new StockPrice { Company = "ApexInc", Date = DateTimeOffset.Now.AddDays(-2), Open = 13.2m, High = 13.9m, Low = 11.1m, Close = 11.8m, Volume = 5932 },
                new StockPrice { Company = "ApexInc", Date = DateTimeOffset.Now.AddDays(-1), Open = 11.8m, High = 12.4m, Low = 9.5m, Close = 12.6m, Volume = 13021 },
                new StockPrice { Company = "ApexInc", Date = DateTimeOffset.Now.AddDays(0), Open = 12.6m, High = 13.1m, Low = 12.3m, Close = 13.2m, Volume = 8562 },
            };


        }

    }
}
