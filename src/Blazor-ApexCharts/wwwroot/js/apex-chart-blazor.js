

window.blazor_apexchart = {

    charts: [],

    //testAlert: function (message) {
    //    return alert(message);
    //},

    //testChart(container) {

    //    var data = {
    //        chart: {
    //            type: 'line'
    //        },
    //        series: [{
    //            name: 'sales',
    //            data: [30, 40, 35, 50, 49, 60, 70, 91, 125]
    //        }],
    //        xaxis: {
    //            categories: ["1991", "1992", "1993", "1994", "1995", "1996", "1997", "1998", "1999"]
    //        }
    //    }

    //    var chart = new ApexCharts(container, data);
    //    chart.render();

    //    //alert(options);
    //},

    renderChart(dotNetObject, container, options) {
        console.log(options);
        var options = JSON.parse(options);

        if (options.seriesNonXAxis != undefined) {

            options.series = options.seriesNonXAxis;
        }

        options.chart.events =  {
            dataPointSelection: (event, chartContext, config) => {
                console.log('Datapoint Selected');
            
                console.log(config.selectedDataPoints);

             

                var selection = {
                    dataPointIndex: config.dataPointIndex,
                    seriesIndex: config.seriesIndex,
                    selectedDataPoints: config.selectedDataPoints
                    //value: config.w.config.series[config.seriesIndex].data[config.dataPointIndex],
                    //label: config.w.config.labels[config.dataPointIndex]
                }

                dotNetObject.invokeMethodAsync('DataPointSelected', selection);
               // alert('Click');
            }
        }

            //var myEvents = [{
            //    dataPointSelection: function(event, chartContext, config) {
            //        console.log(chartContext, config)
            //    }];


            //options.chartv
            //options.chart.events['dataPointSelection'] = function (event, chartContext, config) {
            //    alert('Kalle');
            //}

         //  console.log(myHonda);



            var chartFind = this.charts.filter(x => x.opts.chart.id === options.chart.id)
            var chart;

            if (chartFind.length > 0) {
                console.log('Chart Found');
                chart = chartFind[0];
                chart.updateOptions(options, true, true, true);
            }
            else {
                console.log('Chart Not Found');
                chart = new ApexCharts(container, options);
                this.charts.push(chart)
                chart.render();
            }




        }
    }