using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApexCharts.Docs
{
    public static class Icons
    {
        public static string Home { get => @"<path stroke='none' d='M0 0h24v24H0z' fill='none' /><polyline points='5 12 3 12 12 3 21 12 19 12' /><path d='M5 12v7a2 2 0 0 0 2 2h10a2 2 0 0 0 2 -2v-7' /><path d='M9 21v-6a2 2 0 0 1 2 -2h2a2 2 0 0 1 2 2v6' />"; }
        public static string Chart_donut { get => @"<path stroke='none' d='M0 0h24v24H0z' fill='none' /><path d='M10 3.2a9 9 0 1 0 10.8 10.8a1 1 0 0 0 -1 -1h-3.8a4.1 4.1 0 1 1 -5 -5v-4a0.9 .9 0 0 0 -1 -.8' /><path d='M15 3.5a9 9 0 0 1 5.5 5.5h-4.5a9 9 0 0 0 -1 -1v-4.5' />"; }
        public static string Chart_line { get => @"<path stroke='none' d='M0 0h24v24H0z' fill='none' /><line x1='4' y1='19' x2='20' y2='19' /><polyline points='4 15 8 9 12 11 16 6 20 10' />"; }
        public static string Chart_infographic { get => @"<path stroke='none' d='M0 0h24v24H0z' fill='none' /><circle cx='7' cy='7' r='4' /><path d='M7 3v4h4' /><line x1='9' y1='17' x2='9' y2='21' /><line x1='17' y1='14' x2='17' y2='21' /><line x1='13' y1='13' x2='13' y2='21' /><line x1='21' y1='12' x2='21' y2='21' />"; }
        public static string Brand_github { get => @"<path stroke='none' d='M0 0h24v24H0z' fill='none' /><path d='M9 19c-4.3 1.4 -4.3 -2.5 -6 -3m12 5v-3.5c0 -1 .1 -1.4 -.5 -2c2.8 -.3 5.5 -1.4 5.5 -6a4.6 4.6 0 0 0 -1.3 -3.2a4.2 4.2 0 0 0 -.1 -3.2s-1.1 -.3 -3.5 1.3a12.3 12.3 0 0 0 -6.2 0c-2.4 -1.6 -3.5 -1.3 -3.5 -1.3a4.2 4.2 0 0 0 -.1 3.2a4.6 4.6 0 0 0 -1.3 3.2c0 4.6 2.7 5.7 5.5 6c-.6 .6 -.6 1.2 -.5 2v3.5' />"; }
        public static string Chart_bar { get => @"<path stroke='none' d='M0 0h24v24H0z' fill='none' /><rect x='3' y='12' width='6' height='8' rx='1' /><rect x='9' y='8' width='6' height='12' rx='1' /><rect x='15' y='4' width='6' height='16' rx='1' /><line x1='4' y1='20' x2='18' y2='20' />"; }
        public static string Chart_dots { get => @"<path stroke='none' d='M0 0h24v24H0z' fill='none' /><path d='M3 3v18h18' /><circle cx='9' cy='9' r='2' /><circle cx='19' cy='7' r='2' /><circle cx='14' cy='15' r='2' /><line x1='10.16' y1='10.62' x2='12.5' y2='13.5' /><path d='M15.088 13.328l2.837 -4.586' />"; }
        public static string Chart_scatter { get => @"<path stroke='none' d='M0 0h24v24H0z' fill='none' /><path d='M3 3v18h18' /><circle cx='9' cy='9' r='2' /><circle cx='19' cy='7' r='2' /><circle cx='14' cy='15' r='2' />"; }
        public static string Chart_arcs_3 { get => @"<path stroke='none' d='M0 0h24v24H0z' fill='none' /><circle cx='12' cy='12' r='1' /><path d='M7 12a5 5 0 1 0 5 -5' /><path d='M6.29 18.957a9 9 0 1 0 5.71 -15.957' />"; }
        public static string Chart_arcs { get => @"<path stroke='none' d='M0 0h24v24H0z' fill='none' /><circle cx='12' cy='12' r='1' /><path d='M16.924 11.132a5 5 0 1 0 -4.056 5.792' /><path d='M3 12a9 9 0 1 0 9 -9' />"; }
        public static string Chart_area_line { get => @"<path stroke='none' d='M0 0h24v24H0z' fill='none' /><polyline points='4 19 8 13 12 15 16 10 20 14 20 19 4 19' /><polyline points='4 12 7 8 11 10 16 4 20 8' />"; }
        public static string Chart_area { get => @"<path stroke='none' d='M0 0h24v24H0z' fill='none' /><line x1='4' y1='19' x2='20' y2='19' /><polyline points='4 15 8 9 12 11 16 6 20 10 20 15 4 15' />"; }
        public static string Chart_arrows_vertical { get => @"<path stroke='none' d='M0 0h24v24H0z' fill='none' /><path d='M18 21v-14' /><path d='M9 15l3 -3l3 3' /><path d='M15 10l3 -3l3 3' /><line x1='3' y1='21' x2='21' y2='21' /><line x1='12' y1='21' x2='12' y2='12' /><path d='M3 6l3 -3l3 3' /><path d='M6 21v-18' />"; }
        public static string Chart_arrows { get => @"<path stroke='none' d='M0 0h24v24H0z' fill='none' /><line x1='3' y1='18' x2='17' y2='18' /><path d='M9 9l3 3l-3 3' /><path d='M14 15l3 3l-3 3' /><line x1='3' y1='3' x2='3' y2='21' /><line x1='3' y1='12' x2='12' y2='12' /><path d='M18 3l3 3l-3 3' /><line x1='3' y1='6' x2='21' y2='6' />"; }
        public static string Chart_bubble { get => @"<path stroke='none' d='M0 0h24v24H0z' fill='none' /><circle cx='6' cy='16' r='3' /><circle cx='16' cy='19' r='2' /><circle cx='14.5' cy='7.5' r='4.5' />"; }
        public static string Chart_donut_2 { get => @"<path stroke='none' d='M0 0h24v24H0z' fill='none' /><path d='M12 3v5m4 4h5' /><circle cx='12' cy='12' r='4' /><circle cx='12' cy='12' r='9' />"; }
        public static string Chart_donut_3 { get => @"<path stroke='none' d='M0 0h24v24H0z' fill='none' /><path d='M12 3v5m4 4h5' /><path d='M8.929 14.582l-3.429 2.918' /><circle cx='12' cy='12' r='4' /><circle cx='12' cy='12' r='9' />"; }
        public static string Chart_donut_4 { get => @"<path stroke='none' d='M0 0h24v24H0z' fill='none' /><path d='M8.848 14.667l-3.348 2.833' /><path d='M12 3v5m4 4h5' /><circle cx='12' cy='12' r='9' /><path d='M14.219 15.328l2.781 4.172' /><circle cx='12' cy='12' r='4' />"; }
        public static string Chart_pie_2 { get => @"<path stroke='none' d='M0 0h24v24H0z' fill='none' /><path d='M12 3v9h9' /><circle cx='12' cy='12' r='9' />"; }
        public static string Chart_pie_3 { get => @"<path stroke='none' d='M0 0h24v24H0z' fill='none' /><path d='M12 12l-6.5 5.5' /><path d='M12 3v9h9' /><circle cx='12' cy='12' r='9' />"; }
        public static string Chart_pie_4 { get => @"<path stroke='none' d='M0 0h24v24H0z' fill='none' /><path d='M12 12l-6.5 5.5' /><path d='M12 3v9h9' /><circle cx='12' cy='12' r='9' /><path d='M12 12l5 7.5' />"; }
        public static string Chart_pie { get => @"<path stroke='none' d='M0 0h24v24H0z' fill='none' /><path d='M10 3.2a9 9 0 1 0 10.8 10.8a1 1 0 0 0 -1 -1h-6.8a2 2 0 0 1 -2 -2v-7a0.9 .9 0 0 0 -1 -.8' /><path d='M15 3.5a9 9 0 0 1 5.5 5.5h-4.5a1 1 0 0 1 -1 -1v-4.5' />"; }
        public static string Chart_radar { get => @"<path stroke='none' d='M0 0h24v24H0z' fill='none' /><path d='M12 3l9.5 7l-3.5 11h-12l-3.5 -11z' /><path d='M12 7.5l5.5 4l-2.5 5.5h-6.5l-2 -5.5z' /><path d='M2.5 10l9.5 3l9.5 -3' /><path d='M12 3v10l6 8' /><path d='M6 21l6 -8' />"; }

    }
}
