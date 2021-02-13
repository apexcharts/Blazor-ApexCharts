window.blazor_apexchart = {
    charts: [],

    destroyChart(chartId) {
        var chartFind = this.charts.filter(x => x.opts.chart.chartId === chartId)
        if (chartFind.length > 0) {
            var chart = chartFind[0];
            chart.destroy();
            this.charts = this.charts.filter(x => x.opts.chart.chartId !== chartId);
        }
    },

    renderChart(dotNetObject, container, options) {
        var options = JSON.parse(options);

        if (options.debug == true) {
            console.log(options);
        }

        if (options.seriesNonXAxis != undefined) {

            options.series = options.seriesNonXAxis;
        }

        options.chart.events = {
            dataPointSelection: (event, chartContext, config) => {

                var selection = {
                    dataPointIndex: config.dataPointIndex,
                    seriesIndex: config.seriesIndex,
                    selectedDataPoints: config.selectedDataPoints
                }

                dotNetObject.invokeMethodAsync('DataPointSelected', selection);
            }
        }

        //Always destry chart
        this.destroyChart(options.chart.chartId);

        chart = new ApexCharts(container, options);
        this.charts.push(chart)
        chart.render();
        if (options.debug == true) {
            console.log('Chart ' + options.chart.chartId + ' rendered');
        }
    }
}