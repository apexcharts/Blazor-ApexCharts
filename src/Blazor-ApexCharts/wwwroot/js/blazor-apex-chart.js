

window.blazor_apexchart = {

    charts: [],

    destroyChart(chartId) {
        var chartFind = this.charts.filter(x => x.opts.chart.chartId === chartId)
        if (chartFind.length > 0) {
            var chart = chartFind[0];
            chart.destroy();
            this.charts = this.charts.filter(x => x.opts.chart.chartId !== chartId);
            console.log('Chart ' + chartId + ' destroyed');
        }
    },

    renderChart(dotNetObject, container, options) {
        console.log(options);
        var options = JSON.parse(options);

        if (options.seriesNonXAxis != undefined) {

            options.series = options.seriesNonXAxis;
        }

        options.chart.events = {
            dataPointSelection: (event, chartContext, config) => {
                // console.log('Datapoint Selected');
                //console.log(config.selectedDataPoints);

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

        //var chartFind = this.charts.filter(x => x.opts.chart.id === options.chart.id)
        //var chart;

        //if (chartFind.length > 0) {
        //    console.log('Chart Found');
        //    chart = chartFind[0];
        //    chart.destroy();
        //    this.charts = this.charts.filter(x => x.opts.chart.id !== options.chart.id);
        //    //chart.updateOptions(options, false, true, true);
        //}
        //   else {
        //console.log('Chart Not Found');
        chart = new ApexCharts(container, options);
        this.charts.push(chart)
        chart.render();
        console.log('Chart ' + options.chart.chartId + ' rendered');
        // }




    }
}