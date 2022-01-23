window.blazor_apexchart = {
    charts: [],

    getYAxisLabel(value, index, w) {

        if (window.wasmBinaryFile === undefined) {
            console.warn("YAxis labels is only supported in Blazor WASM");
            return value;
        }

        if (w !== undefined) {
            return w.config.dotNetObject.invokeMethod('GetFormattedYAxisValue', value);
        };

        if (index !== undefined) {
            return index.w.config.dotNetObject.invokeMethod('GetFormattedYAxisValue', value);
        }

        return value;
    },

    findChart(chartId) {
        var chartFind = this.charts.filter(x => x.opts.chart.chartId === chartId)
        if (chartFind.length > 0) {
            return chartFind[0];
        }
        return undefined;
    },

    destroyChart(chartId) {
        var chart = this.findChart(chartId);
        if (chart !== undefined) {
            chart.destroy();
            this.charts = this.charts.filter(x => x.opts.chart.chartId !== chartId);
        }
    },

    updateSeries(chartId, series, animate) {
        var data = JSON.parse(series);
        var chart = this.findChart(chartId);
        if (chart !== undefined) {
            if (chart.options.debug === true) {
                console.log('Update series chartId: ' + chartId);
                console.log(data);
                console.log('------');
            }
            chart.updateSeries(data, animate);
        }
    },

    addPointAnnotation(chartId, annotation, pushToMemory) {
        var data = JSON.parse(annotation);
        var chart = this.findChart(chartId);
        if (chart !== undefined) {
            if (chart.options.debug === true) {
                console.log('Add point annotation series chartId: ' + chartId);
                console.log(data);
                console.log('------');
            }
            chart.addPointAnnotation(data, pushToMemory);
        }
    },

    addXaxisAnnotation(chartId, annotation, pushToMemory) {
        var data = JSON.parse(annotation);
        var chart = this.findChart(chartId);
        if (chart !== undefined) {
            if (chart.options.debug === true) {
                console.log('Add XAxis annotation chartId: ' + chartId);
                console.log(data);
                console.log('------');
            }
            chart.addXaxisAnnotation(data, pushToMemory);
        }
    },

    addYaxisAnnotation(chartId, annotation, pushToMemory) {
        var data = JSON.parse(annotation);
        var chart = this.findChart(chartId);
        if (chart !== undefined) {
            if (chart.options.debug === true) {
                console.log('Add YAxis annotation chartId: ' + chartId);
                console.log(data);
                console.log('------');
            }
            chart.addYaxisAnnotation(data, pushToMemory);
        }
    },

    clearAnnotations(chartId) {
        var chart = this.findChart(chartId);
        if (chart !== undefined) {
            if (chart.options.debug === true) {
                console.log('Clear annotations chartId: ' + chartId);
                console.log('------');
            }
            chart.clearAnnotations();
        }
    },

    removeAnnotation(chartId, id) {
        var chart = this.findChart(chartId);
        if (chart !== undefined) {
            if (chart.options.debug === true) {
                console.log('Remove annotation ' + id + ' chartId: ' + chartId);
                console.log('------');
            }
            chart.removeAnnotation(id);
        }
    },

    toggleSeries(chartId, seriesName) {
        var chart = this.findChart(chartId);
        if (chart !== undefined) {

            if (chart.options.debug === true) {
                console.log('Toogle series ' + seriesName + ' chartId: ' + chartId);
            }
            chart.toggleSeries(seriesName)
        }
    },

    showSeries(chartId, seriesName) {
        var chart = this.findChart(chartId);
        if (chart !== undefined) {

            if (chart.options.debug === true) {
                console.log('Show series ' + seriesName + ' chartId: ' + chartId);
            }
            chart.showSeries(seriesName)
        }
    },

    hideSeries(chartId, seriesName) {
        var chart = this.findChart(chartId);
        if (chart !== undefined) {

            if (chart.options.debug === true) {
                console.log('Hide series ' + seriesName + ' chartId: ' + chartId);
            }
            chart.hideSeries(seriesName)
        }
    },

    renderChart(dotNetObject, container, options) {
        if (options.debug == true) {
            console.log(options);
        }

        var options = JSON.parse(options, (key, value) =>
            (key === 'formatter' || key === 'custom') && value.length !== 0 ? eval("(" + value + ")") : value
        );

        if (options.debug == true) {
            console.log(options);
        }

        options.dotNetObject = dotNetObject;

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

        //Always destry chart if it exists
        this.destroyChart(options.chart.chartId);

        chart = new ApexCharts(container, options);
        this.charts.push(chart);
        chart.render();

        if (options.debug == true) {
            console.log('Chart ' + options.chart.chartId + ' rendered');
        }
    }
}