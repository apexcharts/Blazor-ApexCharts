﻿using System;
using System.Collections.Generic;

namespace DemoCharts
{
    public static class SampleData
    {


        public static List<Order> GetRandomOrders()
        {
            var rnd = new Random();
            var orders = new List<Order>();

            for (int i = 0; i < rnd.Next(5,20); i++)
            {
                orders.Add(new Order { CustomerName = "Odio Corporation", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-12), GrossValue = rnd.Next(2000,50000), DiscountPrecentage = rnd.Next(10, 50), OrderType = (OrderType)rnd.Next(0,4) });
            }

            return orders;
        }

        public static List<Order> GetOrders()
        {
            var orders = new List<Order>();
            orders.Add(new Order { CustomerName = "Odio Corporation", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-12), GrossValue = 34531, DiscountPrecentage = 21, OrderType = OrderType.Contract });
            orders.Add(new Order { CustomerName = "Odio Corporation", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-100), GrossValue = 2800, DiscountPrecentage = 12, OrderType = OrderType.Mail });
            orders.Add(new Order { CustomerName = "Odio Corporation", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-128), GrossValue = 12532, DiscountPrecentage = 24, OrderType = OrderType.Contract });
            orders.Add(new Order { CustomerName = "Odio Corporation", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-232), GrossValue = 1400, DiscountPrecentage = 12, OrderType = OrderType.Mail });
            orders.Add(new Order { CustomerName = "Odio Corporation", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-321), GrossValue = 22000, DiscountPrecentage = 10, OrderType = OrderType.Contract });
            orders.Add(new Order { CustomerName = "Odio Corporation", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-400), GrossValue = 3000, DiscountPrecentage = 17, OrderType = OrderType.Web });

            orders.Add(new Order { CustomerName = "Nascetur AB", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-17), GrossValue = 2134, DiscountPrecentage = 10, OrderType = OrderType.Phone });
            orders.Add(new Order { CustomerName = "Nascetur AB", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-27), GrossValue = 11345, DiscountPrecentage = 12, OrderType = OrderType.Phone });
            orders.Add(new Order { CustomerName = "Nascetur AB", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-124), GrossValue = 63400, DiscountPrecentage = 79, OrderType = OrderType.Mail });
            orders.Add(new Order { CustomerName = "Nascetur AB", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-299), GrossValue = 1235, DiscountPrecentage = 12, OrderType = OrderType.Mail });
            orders.Add(new Order { CustomerName = "Nascetur AB", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-372), GrossValue = 44000, DiscountPrecentage = 11, OrderType = OrderType.Phone });
            orders.Add(new Order { CustomerName = "Nascetur AB", Country = "Sweden", OrderDate = DateTimeOffset.Now.AddDays(-410), GrossValue = 17000, DiscountPrecentage = 5, OrderType = OrderType.Phone });

            orders.Add(new Order { CustomerName = "Justo Eu Institute", Country = "Spain", OrderDate = DateTimeOffset.Now.AddDays(-13), GrossValue = 2800, DiscountPrecentage = 12, OrderType = OrderType.Mail });
            orders.Add(new Order { CustomerName = "Justo Eu Institute", Country = "Spain", OrderDate = DateTimeOffset.Now.AddDays(-45), GrossValue = 12532, DiscountPrecentage = 24, OrderType = OrderType.Web });
            orders.Add(new Order { CustomerName = "Justo Eu Institute", Country = "Spain", OrderDate = DateTimeOffset.Now.AddDays(-60), GrossValue = 1400, DiscountPrecentage = 12, OrderType = OrderType.Mail });
            orders.Add(new Order { CustomerName = "Justo Eu Institute", Country = "Spain", OrderDate = DateTimeOffset.Now.AddDays(-150), GrossValue = 22000, DiscountPrecentage = 10, OrderType = OrderType.Web });
            orders.Add(new Order { CustomerName = "Justo Eu Institute", Country = "Spain", OrderDate = DateTimeOffset.Now.AddDays(-200), GrossValue = 3000, DiscountPrecentage = 17, OrderType = OrderType.Web });

            orders.Add(new Order { CustomerName = "Ani Vent", Country = "France", OrderDate = DateTimeOffset.Now.AddDays(-17), GrossValue = 2134, DiscountPrecentage = 10, OrderType = OrderType.Phone });
            orders.Add(new Order { CustomerName = "Ani Vent", Country = "France", OrderDate = DateTimeOffset.Now.AddDays(-27), GrossValue = 11345, DiscountPrecentage = 12, OrderType = OrderType.Phone });
            orders.Add(new Order { CustomerName = "Ani Vent", Country = "France", OrderDate = DateTimeOffset.Now.AddDays(-124), GrossValue = 17002, DiscountPrecentage = 32, OrderType = OrderType.Mail });

            orders.Add(new Order { CustomerName = "Cali Inc", Country = "France", OrderDate = DateTimeOffset.Now.AddDays(-10), GrossValue = 77000, DiscountPrecentage = 17, OrderType = OrderType.Web });
            orders.Add(new Order { CustomerName = "Cali Inc", Country = "France", OrderDate = DateTimeOffset.Now.AddDays(-110), GrossValue = 120000, DiscountPrecentage = 23, OrderType = OrderType.Web });
            orders.Add(new Order { CustomerName = "Cali Inc", Country = "France", OrderDate = DateTimeOffset.Now.AddDays(-243), GrossValue = 44000, DiscountPrecentage = 8, OrderType = OrderType.Web });

            return orders;
        }
    }
}


//private static void GetGeneratedData()
//{

//    var data = new[] {
//            new { CompanyName = "Ac LLC", OrderDate = DateTime.ParseExact("24/11/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "09E6E3DF-8835-8F3D-EAEE-DBA911516C5E", NetValue = "24941.17", Country = "Austria", Discount = 33, OrderType = "Web" },
//            new { CompanyName = "Urna Suscipit Nonummy LLP", OrderDate = DateTime.ParseExact("04/05/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "746F984F-DC49-7100-4B8E-92B838975385", NetValue = "98752.65", Country = "Austria", Discount = 12, OrderType = "Web" },
//            new { CompanyName = "Odio Corporation", OrderDate = DateTime.ParseExact("13/03/2020", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "308FCA11-38F8-BCA8-5A62-4AD161A473C9", NetValue = "16518.31", Country = "Belgium", Discount = 20, OrderType = "Web" },
//            new { CompanyName = "Rutrum Lorem Ac Associates", OrderDate = DateTime.ParseExact("02/05/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "D845CEA0-E838-2E89-92F7-7B72D155C5E4", NetValue = "95423.93", Country = "Austria", Discount = 35, OrderType = "Web" },
//            new { CompanyName = "Blandit LLP", OrderDate = DateTime.ParseExact("19/01/2020", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "B9BE0B88-A1A2-30FC-4BA9-89122215B0AD", NetValue = "83521.79", Country = "United Kingdom", Discount = 27, OrderType = "Web" },
//            new { CompanyName = "Fringilla Purus Corporation", OrderDate = DateTime.ParseExact("08/05/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "530D706D-254A-5F90-2E21-476EAA450435", NetValue = "35324.67", Country = "Belgium", Discount = 33, OrderType = "Web" },
//            new { CompanyName = "Non Massa Non LLC", OrderDate = DateTime.ParseExact("12/12/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "B92B1735-FFD8-DD61-52AA-00B4A7CFD320", NetValue = "67318.73", Country = "Sweden", Discount = 32, OrderType = "Web" },
//            new { CompanyName = "Molestie Dapibus Ligula PC", OrderDate = DateTime.ParseExact("15/11/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "3AD8669F-6056-F35E-47E1-AB6CDB04528E", NetValue = "63810.33", Country = "France", Discount = 39, OrderType = "Web" },
//            new { CompanyName = "Nostra Per LLC", OrderDate = DateTime.ParseExact("26/02/2020", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "A4FB0A9F-FE97-7669-99BB-399FC9847865", NetValue = "17138.02", Country = "Sweden", Discount = 17, OrderType = "Web" },
//            new { CompanyName = "Orci Adipiscing Non Company", OrderDate = DateTime.ParseExact("18/08/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "3B77D2D4-60AA-C0C8-E1C9-B9F1DC2D6417", NetValue = "61893.53", Country = "Sweden", Discount = 16, OrderType = "Web" },
//            new { CompanyName = "Quisque Nonummy Consulting", OrderDate = DateTime.ParseExact("17/01/2020", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "FC61D3DD-24C9-8545-DA04-AFD6611EF77A", NetValue = "93030.32", Country = "France", Discount = 35, OrderType = "Mail" },
//            new { CompanyName = "Nunc Lectus Pede PC", OrderDate = DateTime.ParseExact("18/07/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "AC2CCEB8-046B-DA57-6584-04DAC403E23B", NetValue = "31458.46", Country = "Belgium", Discount = 21, OrderType = "Mail" },
//            new { CompanyName = "Pellentesque Institute", OrderDate = DateTime.ParseExact("10/04/2020", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "A3017FBE-8191-918D-BFEC-776BEEB28AF3", NetValue = "7628.99", Country = "Sweden", Discount = 12, OrderType = "Mail" },
//            new { CompanyName = "Penatibus Et Magnis Inc.", OrderDate = DateTime.ParseExact("18/10/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "09653EFE-7FE3-3ACD-1881-A3E6C93F60FC", NetValue = "3559.45", Country = "France", Discount = 24, OrderType = "Mail" },
//            new { CompanyName = "Pharetra Felis Eget Institute", OrderDate = DateTime.ParseExact("07/12/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "A41DFE60-4714-9492-9F34-5F84A0EAC558", NetValue = "92558.37", Country = "Austria", Discount = 40, OrderType = "Mail" },
//            new { CompanyName = "Semper Erat In Associates", OrderDate = DateTime.ParseExact("06/11/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "A73EB35B-1987-8CCA-D39B-5BF542AF1CA6", NetValue = "18038.96", Country = "Sweden", Discount = 15, OrderType = "Mail" },
//            new { CompanyName = "Nunc Ac Incorporated", OrderDate = DateTime.ParseExact("13/02/2020", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "F861FDE2-1AFA-B6A3-A229-6357B442DA6B", NetValue = "32986.82", Country = "United Kingdom", Discount = 39, OrderType = "Mail" },
//            new { CompanyName = "Urna Suscipit Nonummy Industries", OrderDate = DateTime.ParseExact("24/11/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "47FF46E3-3147-F243-BB1A-44ACDB9EDC46", NetValue = "33898.24", Country = "France", Discount = 36, OrderType = "Mail" },
//            new { CompanyName = "Condimentum Eget Associates", OrderDate = DateTime.ParseExact("05/10/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "C0B884BF-FF67-2E1F-BE7F-1327279ACEF5", NetValue = "31052.89", Country = "Sweden", Discount = 18, OrderType = "Mail" },
//            new { CompanyName = "Ipsum Limited", OrderDate = DateTime.ParseExact("23/02/2020", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "74F2E296-726C-6EA4-7B6D-754113B6A029", NetValue = "87595.18", Country = "Austria", Discount = 25, OrderType = "Mail" },
//            new { CompanyName = "Pede LLC", OrderDate = DateTime.ParseExact("06/03/2020", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "5B6B7687-665D-2530-47BA-901485947F9C", NetValue = "3000.66", Country = "France", Discount = 29, OrderType = "Contract" },
//            new { CompanyName = "Et Industries", OrderDate = DateTime.ParseExact("03/01/2020", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "1A8CFFE6-12B6-EE8D-9726-CFB95D065104", NetValue = "8101.88", Country = "Austria", Discount = 32, OrderType = "Contract" },
//            new { CompanyName = "Fusce Feugiat LLP", OrderDate = DateTime.ParseExact("30/10/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "73E0E82A-3D86-0358-B7FE-67BE5A8E62C8", NetValue = "76843.66", Country = "Sweden", Discount = 18, OrderType = "Contract" },
//            new { CompanyName = "Quisque Imperdiet Erat LLP", OrderDate = DateTime.ParseExact("01/06/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "1CE4AC0C-F849-B9E7-B99A-9F49864660A0", NetValue = "5450.58", Country = "France", Discount = 24, OrderType = "Contract" },
//            new { CompanyName = "Semper Pretium Corporation", OrderDate = DateTime.ParseExact("12/07/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "2E02BADD-E479-F306-5F38-0519C8C4C6D4", NetValue = "71839.32", Country = "Sweden", Discount = 18, OrderType = "Contract" },
//            new { CompanyName = "Eleifend Vitae Company", OrderDate = DateTime.ParseExact("30/10/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "8F7CF788-E07C-EAE2-1389-A48B1300B079", NetValue = "51119.46", Country = "Belgium", Discount = 28, OrderType = "Contract" },
//            new { CompanyName = "A Sollicitudin Orci LLP", OrderDate = DateTime.ParseExact("12/04/2020", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "BBB82CCE-C3E5-1ECD-FD7C-8EC910348D65", NetValue = "32082.86", Country = "United Kingdom", Discount = 11, OrderType = "Contract" },
//            new { CompanyName = "Semper Auctor LLC", OrderDate = DateTime.ParseExact("14/04/2020", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "0080D496-986D-7B55-AF27-FB57D2200A19", NetValue = "2038.47", Country = "France", Discount = 11, OrderType = "Contract" },
//            new { CompanyName = "Risus Nunc Associates", OrderDate = DateTime.ParseExact("08/04/2020", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "E46E8231-3036-C565-9FB9-A27F34FD11CE", NetValue = "84143.69", Country = "United Kingdom", Discount = 30, OrderType = "Contract" },
//            new { CompanyName = "Amet Consectetuer Adipiscing LLC", OrderDate = DateTime.ParseExact("11/11/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "FD76C474-0B7A-75ED-2CE8-B80A3BA45DB4", NetValue = "14530.45", Country = "France", Discount = 32, OrderType = "Contract" },
//            new { CompanyName = "Fusce Associates", OrderDate = DateTime.ParseExact("08/02/2020", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "6066F4FE-81C5-0E32-A5DA-61D32A9E405A", NetValue = "93209.06", Country = "United Kingdom", Discount = 16, OrderType = "Phone" },
//            new { CompanyName = "Convallis Institute", OrderDate = DateTime.ParseExact("28/12/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "562B8B0F-8462-8F39-CA5C-853712D80AE1", NetValue = "16034.63", Country = "United Kingdom", Discount = 11, OrderType = "Phone" },
//            new { CompanyName = "Pharetra Nibh PC", OrderDate = DateTime.ParseExact("12/09/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "C175A3FD-4FBF-3EB0-E449-77DC2FBD47B3", NetValue = "99596.00", Country = "United Kingdom", Discount = 18, OrderType = "Phone" },
//            new { CompanyName = "Suspendisse Dui Limited", OrderDate = DateTime.ParseExact("21/01/2020", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "CAE8A753-5AFB-142B-8463-531C1E949074", NetValue = "8784.28", Country = "Austria", Discount = 21, OrderType = "Phone" },
//            new { CompanyName = "Vitae Purus Gravida Ltd", OrderDate = DateTime.ParseExact("25/03/2020", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "68B813B9-D519-8654-F3F9-7C7AE1237E91", NetValue = "4306.06", Country = "Austria", Discount = 21, OrderType = "Phone" },
//            new { CompanyName = "Ut Ipsum Corp.", OrderDate = DateTime.ParseExact("11/12/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "4C1A1CCF-D2DB-4AED-A9FD-2A210E5B6A74", NetValue = "15748.40", Country = "France", Discount = 14, OrderType = "Phone" },
//            new { CompanyName = "Neque Sed Corporation", OrderDate = DateTime.ParseExact("15/05/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "A4A0C3DE-E2AC-CBDF-B351-3AFD8C7965B2", NetValue = "25273.87", Country = "Austria", Discount = 37, OrderType = "Phone" },
//            new { CompanyName = "Urna Institute", OrderDate = DateTime.ParseExact("26/10/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "9C40784C-1175-6E7E-B160-34BBD637CDFB", NetValue = "61951.90", Country = "United Kingdom", Discount = 35, OrderType = "Phone" },
//            new { CompanyName = "Arcu LLP", OrderDate = DateTime.ParseExact("02/09/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "62EB0D07-6634-EE11-E516-2CFEA69C95BC", NetValue = "20520.01", Country = "Austria", Discount = 11, OrderType = "Phone" },
//            new { CompanyName = "Eget Odio Institute", OrderDate = DateTime.ParseExact("24/01/2020", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "B38FAB0F-D10E-62AE-0D68-6A61C395E06E", NetValue = "90754.99", Country = "Austria", Discount = 26, OrderType = "Phone" },
//            new { CompanyName = "Velit In Corp.", OrderDate = DateTime.ParseExact("01/01/2020", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "96053518-F8A9-F42C-F95E-E22E9DCF7A66", NetValue = "45814.50", Country = "Sweden", Discount = 13, OrderType = "Web" },
//            new { CompanyName = "Malesuada Malesuada LLC", OrderDate = DateTime.ParseExact("11/04/2020", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "A2F88E8A-3FC1-F127-EB85-934FF7DA6DC2", NetValue = "44003.03", Country = "Sweden", Discount = 13, OrderType = "Web" },
//            new { CompanyName = "Nisl Corporation", OrderDate = DateTime.ParseExact("05/11/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "C4E600F9-C740-3C1E-2B13-9078F9D38635", NetValue = "55500.80", Country = "United Kingdom", Discount = 27, OrderType = "Web" },
//            new { CompanyName = "Euismod In Dolor Company", OrderDate = DateTime.ParseExact("29/08/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "C0730B0D-F646-968E-6629-C65B06F064ED", NetValue = "26664.11", Country = "United Kingdom", Discount = 31, OrderType = "Web" },
//            new { CompanyName = "Hendrerit Id LLC", OrderDate = DateTime.ParseExact("17/11/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "52D04524-FEB0-4DD8-3449-A3FD4D1EB161", NetValue = "3305.74", Country = "Austria", Discount = 28, OrderType = "Web" },
//            new { CompanyName = "Dui Semper Institute", OrderDate = DateTime.ParseExact("15/09/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "5677641F-CFFF-0564-F37F-239C95D318AA", NetValue = "19527.71", Country = "Sweden", Discount = 24, OrderType = "Web" },
//            new { CompanyName = "Vitae Limited", OrderDate = DateTime.ParseExact("10/10/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "379201B7-82F8-A152-5B76-4CA7E9490799", NetValue = "3966.06", Country = "United Kingdom", Discount = 30, OrderType = "Web" },
//            new { CompanyName = "Ipsum Associates", OrderDate = DateTime.ParseExact("02/10/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "FEE35D08-6BDF-B99A-18D1-37D51989A981", NetValue = "89105.67", Country = "United Kingdom", Discount = 35, OrderType = "Web" },
//            new { CompanyName = "Vel Inc.", OrderDate = DateTime.ParseExact("01/01/2020", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "5DEDAAAB-F188-5E12-AE49-6AC3DDDC03C4", NetValue = "6412.79", Country = "Austria", Discount = 24, OrderType = "Web" },
//            new { CompanyName = "Enim Mi Corp.", OrderDate = DateTime.ParseExact("03/10/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "CE3E71E5-85DF-AB6E-D56C-485356DDF42F", NetValue = "20480.67", Country = "Sweden", Discount = 35, OrderType = "Web" },
//            new { CompanyName = "Turpis Non Corporation", OrderDate = DateTime.ParseExact("21/02/2020", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "F2AFB2CE-935F-93C0-6BE1-FED6DF02BFA5", NetValue = "4478.27", Country = "Sweden", Discount = 18, OrderType = "Mail" },
//            new { CompanyName = "Aliquet Corp.", OrderDate = DateTime.ParseExact("04/11/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "59B6370E-59F1-5CE5-B8D2-2F2B98936446", NetValue = "85935.25", Country = "Austria", Discount = 32, OrderType = "Mail" },
//            new { CompanyName = "Faucibus Ut Inc.", OrderDate = DateTime.ParseExact("27/12/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "1A62B11A-F1A4-9413-EDBB-F546882AFDF7", NetValue = "75659.94", Country = "Austria", Discount = 13, OrderType = "Mail" },
//            new { CompanyName = "Et Malesuada Limited", OrderDate = DateTime.ParseExact("17/03/2020", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "15354F23-2864-6522-06B4-B87AE4998C2D", NetValue = "51054.86", Country = "Belgium", Discount = 31, OrderType = "Mail" },
//            new { CompanyName = "Sagittis Felis Donec Industries", OrderDate = DateTime.ParseExact("28/12/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "3802A371-EBEB-7941-DD42-4EE4EC6F143A", NetValue = "95868.12", Country = "Belgium", Discount = 25, OrderType = "Mail" },
//            new { CompanyName = "Eu Odio Inc.", OrderDate = DateTime.ParseExact("29/02/2020", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "ADB52352-19BE-1F0E-6331-7A0DF47DE5B5", NetValue = "62291.61", Country = "Belgium", Discount = 15, OrderType = "Mail" },
//            new { CompanyName = "Nibh Dolor Ltd", OrderDate = DateTime.ParseExact("28/12/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "3514AFEB-7C26-2966-6037-0F24B6EB9240", NetValue = "77785.29", Country = "Belgium", Discount = 26, OrderType = "Mail" },
//            new { CompanyName = "Aliquet Ltd", OrderDate = DateTime.ParseExact("21/02/2020", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "53B34467-FE0E-012D-D649-1440E8FE1C25", NetValue = "22808.41", Country = "United Kingdom", Discount = 14, OrderType = "Mail" },
//            new { CompanyName = "Ornare Facilisis Eget Limited", OrderDate = DateTime.ParseExact("24/09/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "906CABB4-D045-26C5-6D85-CA7C88C77317", NetValue = "53637.99", Country = "France", Discount = 26, OrderType = "Mail" },
//            new { CompanyName = "Quam Foundation", OrderDate = DateTime.ParseExact("27/11/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "1FEAF1AC-230A-0881-06B9-566F8F6C5F2C", NetValue = "54905.74", Country = "Sweden", Discount = 19, OrderType = "Mail" },
//            new { CompanyName = "Pellentesque Ltd", OrderDate = DateTime.ParseExact("31/10/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "110C7A7C-D8A8-2698-1469-99E4D83A50BF", NetValue = "60825.40", Country = "Belgium", Discount = 36, OrderType = "Contract" },
//            new { CompanyName = "Lacus Ltd", OrderDate = DateTime.ParseExact("14/03/2020", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "31FD6173-ABB6-B6CB-FB47-1A2F987E2599", NetValue = "13082.56", Country = "France", Discount = 35, OrderType = "Contract" },
//            new { CompanyName = "Tincidunt Tempus Risus LLP", OrderDate = DateTime.ParseExact("20/10/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "71A76CE8-B8E4-EC2C-222E-C97F54505A9E", NetValue = "53405.04", Country = "Sweden", Discount = 16, OrderType = "Contract" },
//            new { CompanyName = "Risus Nunc Ac Corp.", OrderDate = DateTime.ParseExact("15/07/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "9780AEF6-1ED5-37F5-C192-F31DE91C705B", NetValue = "60852.77", Country = "United Kingdom", Discount = 36, OrderType = "Contract" },
//            new { CompanyName = "Cum Sociis Limited", OrderDate = DateTime.ParseExact("18/12/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "F171482A-0428-F754-946F-320BF9D9880E", NetValue = "58332.33", Country = "Sweden", Discount = 37, OrderType = "Contract" },
//            new { CompanyName = "Posuere Cubilia Curae; Inc.", OrderDate = DateTime.ParseExact("15/03/2020", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "D1C7535F-F742-1F03-E679-CE31BB01A605", NetValue = "64703.96", Country = "Sweden", Discount = 27, OrderType = "Contract" },
//            new { CompanyName = "Phasellus LLP", OrderDate = DateTime.ParseExact("05/11/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "2DFA32C1-7D1F-78ED-F89E-15D288853382", NetValue = "63562.64", Country = "Austria", Discount = 16, OrderType = "Contract" },
//            new { CompanyName = "A Corporation", OrderDate = DateTime.ParseExact("25/03/2020", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "4B5875B7-A3E7-CFF7-C131-5688F3A251C4", NetValue = "8462.06", Country = "Austria", Discount = 24, OrderType = "Contract" },
//            new { CompanyName = "Sagittis Semper Nam Incorporated", OrderDate = DateTime.ParseExact("22/10/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "A5C77976-8290-F39D-1190-3955099FC3E9", NetValue = "27456.04", Country = "Austria", Discount = 10, OrderType = "Contract" },
//            new { CompanyName = "Luctus Limited", OrderDate = DateTime.ParseExact("17/04/2020", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "FEE71EDD-E5ED-D148-DA71-23C857A9E4AD", NetValue = "67368.39", Country = "Belgium", Discount = 11, OrderType = "Contract" },
//            new { CompanyName = "Blandit At Nisi Foundation", OrderDate = DateTime.ParseExact("12/04/2020", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "A39D4CF1-14EB-5DD9-5791-2578F86C6A9F", NetValue = "53722.95", Country = "Belgium", Discount = 40, OrderType = "Phone" },
//            new { CompanyName = "Ullamcorper Associates", OrderDate = DateTime.ParseExact("11/03/2020", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "D07C0B46-5CDA-F82A-ED0B-7AB3C925B324", NetValue = "73532.95", Country = "Belgium", Discount = 35, OrderType = "Phone" },
//            new { CompanyName = "Donec Inc.", OrderDate = DateTime.ParseExact("19/08/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "E40A09EF-F3C7-4D68-C775-BB2FA6735122", NetValue = "68928.54", Country = "Sweden", Discount = 31, OrderType = "Phone" },
//            new { CompanyName = "Rutrum Urna Associates", OrderDate = DateTime.ParseExact("05/04/2020", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "9C67AFC8-2C08-19A7-5EAA-5CEB578B8C7C", NetValue = "13008.07", Country = "Austria", Discount = 11, OrderType = "Phone" },
//            new { CompanyName = "Euismod Et Corporation", OrderDate = DateTime.ParseExact("10/03/2020", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "1A18989B-3C9E-2298-084B-23A5BECC1092", NetValue = "95690.34", Country = "United Kingdom", Discount = 11, OrderType = "Phone" },
//            new { CompanyName = "Tempor Augue Ac Industries", OrderDate = DateTime.ParseExact("06/01/2020", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "CF027B5C-3316-CADD-28F6-F909537B57A5", NetValue = "6874.00", Country = "Sweden", Discount = 27, OrderType = "Phone" },
//            new { CompanyName = "Sed Eget Industries", OrderDate = DateTime.ParseExact("02/11/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "5A66E3CF-9540-A341-6B9F-002769308A13", NetValue = "49457.71", Country = "Sweden", Discount = 36, OrderType = "Phone" },
//            new { CompanyName = "Vel Faucibus Corporation", OrderDate = DateTime.ParseExact("01/05/2020", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "450B8590-7EB8-3E14-43E3-458CF625D1FC", NetValue = "2406.68", Country = "France", Discount = 11, OrderType = "Phone" },
//            new { CompanyName = "Pede PC", OrderDate = DateTime.ParseExact("22/05/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "EDD894F1-C518-850F-729A-003549FA383B", NetValue = "93886.76", Country = "Sweden", Discount = 12, OrderType = "Phone" },
//            new { CompanyName = "Felis Donec Tempor LLP", OrderDate = DateTime.ParseExact("25/08/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "815943B0-39DE-0793-9A09-4618806E7460", NetValue = "13841.38", Country = "United Kingdom", Discount = 15, OrderType = "Phone" },
//            new { CompanyName = "Non Ltd", OrderDate = DateTime.ParseExact("03/05/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "18971131-F2F2-05B2-A6B7-1AB40A0D3B74", NetValue = "36495.64", Country = "Austria", Discount = 26, OrderType = "Web" },
//            new { CompanyName = "Ut Tincidunt Orci Inc.", OrderDate = DateTime.ParseExact("15/06/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "66FBB0D3-FA77-0F44-90C3-76BF8AD0C7C3", NetValue = "57144.21", Country = "France", Discount = 34, OrderType = "Web" },
//            new { CompanyName = "Nunc Corporation", OrderDate = DateTime.ParseExact("20/07/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "5416B647-C0F3-0712-8A42-891E0A159431", NetValue = "3809.25", Country = "Sweden", Discount = 37, OrderType = "Web" },
//            new { CompanyName = "Ipsum Cursus Vestibulum LLC", OrderDate = DateTime.ParseExact("26/09/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "06213C3F-F74F-485B-AFA2-03FAFB7772D4", NetValue = "9778.08", Country = "United Kingdom", Discount = 17, OrderType = "Web" },
//            new { CompanyName = "Justo Eu Institute", OrderDate = DateTime.ParseExact("14/09/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "28F77653-CB9A-BC8A-521D-646994368D2C", NetValue = "79137.01", Country = "France", Discount = 23, OrderType = "Web" },
//            new { CompanyName = "Nascetur PC", OrderDate = DateTime.ParseExact("05/07/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "D33B96D2-6E14-07D1-ED74-D59B6A13B592", NetValue = "22765.74", Country = "United Kingdom", Discount = 18, OrderType = "Web" },
//            new { CompanyName = "Sed LLP", OrderDate = DateTime.ParseExact("25/07/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "0D4BB2DA-A6AD-880B-D2F9-91A52D2E4C74", NetValue = "95665.24", Country = "Sweden", Discount = 27, OrderType = "Web" },
//            new { CompanyName = "Sociis Natoque LLC", OrderDate = DateTime.ParseExact("11/10/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "717C9B30-46B7-9E6A-C092-2DD12F95D4EB", NetValue = "14470.32", Country = "United Kingdom", Discount = 14, OrderType = "Web" },
//            new { CompanyName = "Sed Sapien Nunc Incorporated", OrderDate = DateTime.ParseExact("03/09/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "B3BE24DE-642B-E98A-998B-3204CED3137D", NetValue = "24046.40", Country = "Sweden", Discount = 21, OrderType = "Web" },
//            new { CompanyName = "Tristique Ac Eleifend LLC", OrderDate = DateTime.ParseExact("07/03/2020", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "66A2644E-9ACE-10E2-6D5A-691CA01C6888", NetValue = "46440.36", Country = "France", Discount = 10, OrderType = "Web" },
//            new { CompanyName = "Mollis Nec Foundation", OrderDate = DateTime.ParseExact("10/09/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "97F71F5F-C0AB-1798-53CD-5C4DE2C38207", NetValue = "24078.38", Country = "United Kingdom", Discount = 24, OrderType = "Mail" },
//            new { CompanyName = "Cras Consulting", OrderDate = DateTime.ParseExact("03/01/2020", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "C24DECEB-AE0D-796E-9007-55318F763615", NetValue = "44117.83", Country = "Sweden", Discount = 19, OrderType = "Mail" },
//            new { CompanyName = "Vehicula Incorporated", OrderDate = DateTime.ParseExact("22/05/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "65F5D2E6-59AC-32AD-7C7C-B535917419A1", NetValue = "71861.03", Country = "Austria", Discount = 38, OrderType = "Mail" },
//            new { CompanyName = "Vestibulum Associates", OrderDate = DateTime.ParseExact("09/10/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "973BB6B1-FC72-4281-B03C-D9FABD75571C", NetValue = "48431.27", Country = "Sweden", Discount = 32, OrderType = "Mail" },
//            new { CompanyName = "Leo In Corporation", OrderDate = DateTime.ParseExact("24/09/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "66A1AEDB-EFE4-6DAE-5CDD-6D56B10F7F71", NetValue = "19586.27", Country = "Austria", Discount = 39, OrderType = "Mail" },
//            new { CompanyName = "Nunc Ac Mattis LLP", OrderDate = DateTime.ParseExact("03/06/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "28521D0B-6642-5D56-D5D4-95081046834A", NetValue = "2928.26", Country = "Austria", Discount = 25, OrderType = "Mail" },
//            new { CompanyName = "Ut Nulla Company", OrderDate = DateTime.ParseExact("06/12/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "EB0002C1-CA06-F32C-6AF4-4E00FB16C2D3", NetValue = "5110.21", Country = "France", Discount = 37, OrderType = "Mail" },
//            new { CompanyName = "Semper Pretium Corporation", OrderDate = DateTime.ParseExact("25/12/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "122FE846-A94F-4B06-DA22-E30592A156A6", NetValue = "33417.06", Country = "Sweden", Discount = 39, OrderType = "Mail" },
//            new { CompanyName = "Orci Limited", OrderDate = DateTime.ParseExact("27/08/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "D1783F65-B1B5-1396-13EC-63481D59E0C3", NetValue = "32476.47", Country = "United Kingdom", Discount = 12, OrderType = "Mail" },
//            new { CompanyName = "Tincidunt Vehicula Risus Ltd", OrderDate = DateTime.ParseExact("31/12/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture), OrderId = "A9AC48EF-16C6-CF73-6654-86EEBEE9C401", NetValue = "70922.83", Country = "France", Discount = 31, OrderType = "Mail" }
//            };
//}