using BlazorApexCharts.Docs.Data;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApexCharts.Docs
{
    public static class SampleData
    {
        private static readonly string[] Summaries = new[]
{
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        public static Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
        {
            return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = startDate.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToArray());
        }

        public static List<BoxPlotSample> GetBoxPlotData()
        {

            var now = DateTimeOffset.Now;
            var result = new List<BoxPlotSample>();

            result.Add(new BoxPlotSample { Name = "Eva", EventDate = now.AddDays(-10), Min = 40, Q1 = 52, Median = 56.59m, Q3 = 60, Max = 63.85m });
            result.Add(new BoxPlotSample { Name = "Jonas", EventDate = now.AddDays(-9), Min = 43.66m, Q1 = 44.99m, Median = 51.35m, Q3 = 52.95m, Max = 59.42m });
            result.Add(new BoxPlotSample { Name = "Bart", EventDate = now.AddDays(-8), Min = 52.76m, Q1 = 57.35m, Median = 59.15m, Q3 = 63.03m, Max = 67.98m });
            result.Add(new BoxPlotSample { Name = "Cecilia", EventDate = now.AddDays(-7), Min = 48m, Q1 = 49m, Median = 52m, Q3 = 62m, Max = 68m });
            result.Add(new BoxPlotSample { Name = "Ann", EventDate = now.AddDays(-6), Min = 38m, Q1 = 41m, Median = 45m, Q3 = 52m, Max = 55m });
            return result;
        }


        public static List<SimpleValue> GetSimpleValues()
        {
            var result = new List<SimpleValue>();

            result.Add(new SimpleValue { Name = "Blue", Value = 40 });
            result.Add(new SimpleValue { Name = "Green", Value = 130 });
            result.Add(new SimpleValue { Name = "Black", Value = null });
            result.Add(new SimpleValue { Name = "Red", Value = 45 });
            result.Add(new SimpleValue { Name = "White", Value = 11 });
            return result;
        }

        public static List<Order> GetRandomOrders()
        {
            var rnd = new Random();
            var orders = new List<Order>();

            for (int i = 0; i < rnd.Next(5, 20); i++)
            {
                orders.Add(new Order { CustomerName = "Odio Corporation", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-1 * i), GrossValue = rnd.Next(2000, 50000), DiscountPercentage = rnd.Next(10, 50), OrderType = (OrderType)rnd.Next(0, 4) });
            }

            return orders;
        }

        public static List<Order> GetOrdersForGroup()
        {
            var orders = GetOrders();

            orders.Add(new Order { CustomerName = "Expansion Inc", Country = "Andorra", OrderDate = DateTimeOffset.Now.AddDays(-12), GrossValue = 1234, DiscountPercentage = 21, OrderType = OrderType.Contract });
            orders.Add(new Order { CustomerName = "Expansion Inc", Country = "Andorra", OrderDate = DateTimeOffset.Now.AddDays(-2), GrossValue = 12, DiscountPercentage = 14, OrderType = OrderType.Mail });

            orders.Add(new Order { CustomerName = "Trick Corp.", Country = "San Marino", OrderDate = DateTimeOffset.Now.AddDays(-10), GrossValue = 3543, DiscountPercentage = 11, OrderType = OrderType.Web });
            orders.Add(new Order { CustomerName = "Trick Corp.", Country = "San Marino", OrderDate = DateTimeOffset.Now.AddDays(-4), GrossValue = 126, DiscountPercentage = 17, OrderType = OrderType.Contract });

            orders.Add(new Order { CustomerName = "Restless Group", Country = "Monaco", OrderDate = DateTimeOffset.Now.AddDays(-14), GrossValue = 1266, DiscountPercentage = 13, OrderType = OrderType.Web });



            return orders;

        }

        public static List<Order> GetOrders()
        {
            var orders = new List<Order>();
            orders.Add(new Order { CustomerName = "Odio Corporation", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-12), GrossValue = 34531, DiscountPercentage = 21, OrderType = OrderType.Contract });
            orders.Add(new Order { CustomerName = "Odio Corporation", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-100), GrossValue = 2800, DiscountPercentage = 12, OrderType = OrderType.Mail });
            orders.Add(new Order { CustomerName = "Odio Corporation", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-128), GrossValue = 12532, DiscountPercentage = 24, OrderType = OrderType.Contract });
            orders.Add(new Order { CustomerName = "Odio Corporation", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-232), GrossValue = 1400, DiscountPercentage = 65, OrderType = OrderType.Mail });
            orders.Add(new Order { CustomerName = "Odio Corporation", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-321), GrossValue = 22000, DiscountPercentage = 10, OrderType = OrderType.Contract });
            orders.Add(new Order { CustomerName = "Odio Corporation", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-400), GrossValue = 3000, DiscountPercentage = 17, OrderType = OrderType.Web });

            orders.Add(new Order { CustomerName = "Nascetur AB", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-17), GrossValue = 2134, DiscountPercentage = 10, OrderType = OrderType.Phone });
            orders.Add(new Order { CustomerName = "Nascetur AB", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-27), GrossValue = 11345, DiscountPercentage = 12, OrderType = OrderType.Phone });
            orders.Add(new Order { CustomerName = "Nascetur AB", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-124), GrossValue = 63400, DiscountPercentage = 79, OrderType = OrderType.Mail });
            orders.Add(new Order { CustomerName = "Nascetur AB", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-299), GrossValue = 1235, DiscountPercentage = 12, OrderType = OrderType.Mail });
            orders.Add(new Order { CustomerName = "Nascetur AB", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-372), GrossValue = 44000, DiscountPercentage = 11, OrderType = OrderType.Phone });
            orders.Add(new Order { CustomerName = "Nascetur AB", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-410), GrossValue = 17000, DiscountPercentage = 5, OrderType = OrderType.Phone });

            orders.Add(new Order { CustomerName = "Justo Eu Institute", Country = "Spain", OrderDate = DateTimeOffset.Now.AddDays(-10), GrossValue = 12000, DiscountPercentage = 23, OrderType = OrderType.Mail });
            orders.Add(new Order { CustomerName = "Justo Eu Institute", Country = "Spain", OrderDate = DateTimeOffset.Now.AddDays(-13), GrossValue = 92800, DiscountPercentage = 48, OrderType = OrderType.Mail });
            orders.Add(new Order { CustomerName = "Justo Eu Institute", Country = "Spain", OrderDate = DateTimeOffset.Now.AddDays(-45), GrossValue = 12532, DiscountPercentage = 24, OrderType = OrderType.Web });
            orders.Add(new Order { CustomerName = "Justo Eu Institute", Country = "Spain", OrderDate = DateTimeOffset.Now.AddDays(-60), GrossValue = 1400, DiscountPercentage = 12, OrderType = OrderType.Mail });
            orders.Add(new Order { CustomerName = "Justo Eu Institute", Country = "Spain", OrderDate = DateTimeOffset.Now.AddDays(-150), GrossValue = 22000, DiscountPercentage = 10, OrderType = OrderType.Web });
            orders.Add(new Order { CustomerName = "Justo Eu Institute", Country = "Spain", OrderDate = DateTimeOffset.Now.AddDays(-200), GrossValue = 3000, DiscountPercentage = 17, OrderType = OrderType.Web });

            orders.Add(new Order { CustomerName = "Ani Vent", Country = "France", OrderDate = DateTimeOffset.Now.AddDays(-17), GrossValue = 2134, DiscountPercentage = 34, OrderType = OrderType.Phone });
            orders.Add(new Order { CustomerName = "Ani Vent", Country = "France", OrderDate = DateTimeOffset.Now.AddDays(-27), GrossValue = 11345, DiscountPercentage = 12, OrderType = OrderType.Phone });
            orders.Add(new Order { CustomerName = "Ani Vent", Country = "France", OrderDate = DateTimeOffset.Now.AddDays(-124), GrossValue = 17002, DiscountPercentage = 32, OrderType = OrderType.Mail });
            orders.Add(new Order { CustomerName = "Cali Inc", Country = "France", OrderDate = DateTimeOffset.Now.AddDays(-10), GrossValue = 77000, DiscountPercentage = 17, OrderType = OrderType.Web });
            orders.Add(new Order { CustomerName = "Cali Inc", Country = "France", OrderDate = DateTimeOffset.Now.AddDays(-110), GrossValue = 120000, DiscountPercentage = 23, OrderType = OrderType.Web });
            orders.Add(new Order { CustomerName = "Cali Inc", Country = "France", OrderDate = DateTimeOffset.Now.AddDays(-243), GrossValue = 44000, DiscountPercentage = 8, OrderType = OrderType.Web });


            orders.Add(new Order { CustomerName = "Chart Inc", Country = "Brazil", OrderDate = DateTimeOffset.Now.AddDays(-11), GrossValue = 2345, DiscountPercentage = 11, OrderType = OrderType.Phone });
            orders.Add(new Order { CustomerName = "Chart Inc", Country = "Brazil", OrderDate = DateTimeOffset.Now.AddDays(-14), GrossValue = 34567, DiscountPercentage = 22, OrderType = OrderType.Phone });
            orders.Add(new Order { CustomerName = "Chart Inc", Country = "Brazil", OrderDate = DateTimeOffset.Now.AddDays(-121), GrossValue = 45662, DiscountPercentage = 23, OrderType = OrderType.Mail });
            orders.Add(new Order { CustomerName = "Chart Inc", Country = "Brazil", OrderDate = DateTimeOffset.Now.AddDays(-11), GrossValue = 66000, DiscountPercentage = 11, OrderType = OrderType.Web });
            orders.Add(new Order { CustomerName = "Chart Inc", Country = "Brazil", OrderDate = DateTimeOffset.Now.AddDays(-90), GrossValue = 10000, DiscountPercentage = 8, OrderType = OrderType.Web });
            orders.Add(new Order { CustomerName = "Chart Inc", Country = "Brazil", OrderDate = DateTimeOffset.Now.AddDays(-123), GrossValue = 69000, DiscountPercentage = 25, OrderType = OrderType.Web });


            return orders;
        }

        public static List<Project> GetProjects()
        {
            var result = new List<Project>();

            result.Add(new Project { Name = "Design", StartDate = DateTime.Now.AddDays(-30), EndDate = DateTime.Now.AddDays(-10), Score = 20 });
            result.Add(new Project { Name = "Construct", StartDate = DateTime.Now.AddDays(-20), EndDate = DateTime.Now.AddDays(-5), Score = -12 });
            result.Add(new Project { Name = "Install", StartDate = DateTime.Now.AddDays(-14), EndDate = DateTime.Now.AddDays(0), Score = -4 });
            result.Add(new Project { Name = "Train", StartDate = DateTime.Now.AddDays(-18), EndDate = DateTime.Now.AddDays(5), Score = 26 });
            return result;

        }

        public static List<Activity> GetActivites()
        {
            var result = new List<Activity>();

            result.Add(new Activity { Name = "Design", ActivityDate = DateTimeOffset.Now.AddDays(-30) });
            result.Add(new Activity { Name = "Design", ActivityDate = DateTimeOffset.Now.AddDays(-20) });

            result.Add(new Activity { Name = "Develop", ActivityDate = DateTimeOffset.Now.AddDays(-22) });
            result.Add(new Activity { Name = "Develop", ActivityDate = DateTimeOffset.Now.AddDays(-10) });

            result.Add(new Activity { Name = "Test", ActivityDate = DateTimeOffset.Now.AddDays(-14) });
            result.Add(new Activity { Name = "Test", ActivityDate = DateTimeOffset.Now.AddDays(-8) });


            return result;

        }

        public static List<SupportIncident> GetSupportIncidents(int severityMin = 0, int severityMax = 100)
        {
            var fakeIncidents = new Faker<SupportIncident>()
            .RuleFor(o => o.Severity, f => f.Random.Number(severityMin, severityMax))
            .RuleFor(o => o.Source, f => f.PickRandom<IncidentSource>())
             .RuleFor(o => o.LeadTime, f => f.Random.Number(1, 10))
            .RuleFor(o => o.WeekNumber, f => f.Random.Number(1, 20))
            .RuleFor(o => o.PointColor, f => f.Internet.Color());

            var test = fakeIncidents.Generate(300);
            return test;

        }

        public static List<SupportIncident> GetSupportIncidentsForRange()
        {
            return new List<SupportIncident>
           {
               new SupportIncident { Source = IncidentSource.Customer, Severity = 20, PointColor="#e3001b"},
               new SupportIncident { Source = IncidentSource.Customer, Severity = 35,  PointColor="#e3001b"},
               new SupportIncident { Source = IncidentSource.Integration, Severity = 5, PointColor="#005ba3"},
               new SupportIncident { Source = IncidentSource.Integration, Severity = 70,PointColor="#005ba3"},
               new SupportIncident { Source = IncidentSource.Internal, Severity = 55, PointColor="#ffd500"},
               new SupportIncident { Source = IncidentSource.Internal, Severity = 83, PointColor="#ffd500"},
               new SupportIncident { Source = IncidentSource.ThirdParty, Severity = 12, PointColor="#00783c"},
               new SupportIncident { Source = IncidentSource.ThirdParty, Severity = 24, PointColor="#00783c"},
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

        public static List<MeteoSample> GetMeteoSample()
        {
            return new List<MeteoSample>
           {
               new MeteoSample { Month = "Jan", CurrentTemperature = 6, LowestTemperature = -3, HighestTemperature = 13},
               new MeteoSample { Month = "Feb", CurrentTemperature = 8, LowestTemperature = -2, HighestTemperature = 15},
               new MeteoSample { Month = "Mar", CurrentTemperature = 10, LowestTemperature = 1, HighestTemperature = 18},
               new MeteoSample { Month = "Apr", CurrentTemperature = 14, LowestTemperature = 4, HighestTemperature = 23},
               new MeteoSample { Month = "May", CurrentTemperature = 20, LowestTemperature = 9, HighestTemperature = 25},
               new MeteoSample { Month = "Jun", CurrentTemperature = 23, LowestTemperature = 12, HighestTemperature = 28},
               new MeteoSample { Month = "Jul", CurrentTemperature = 27, LowestTemperature = 16, HighestTemperature = 33},
           };
        }

    }
}
