window.blazor_apexchart = {
 
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

    findChart(id) {
        if (Apex._chartInstances === undefined) {
            return undefined;
        }
        return ApexCharts.getChartByID(id)
 
    },

    destroyChart(id) {
        var chart = this.findChart(id);
        if (chart !== undefined) {
            chart.destroy();
        }
    },


    updateOptions(id, options, redrawPaths, animate, updateSyncedCharts) {
        var data = JSON.parse(options);
        var chart = this.findChart(id);
        if (chart !== undefined) {
            if (chart.options.debug === true) {
                console.log('Update options id: ' + id);
                console.log(data);
                console.log('------');
            }
            chart.updateOptions(newOptions, redrawPaths, animate, updateSyncedCharts);
        }
    },

    updateSeries(id, series, animate) {
        var data = JSON.parse(series);
        var chart = this.findChart(id);
        console.log(chart);
        if (chart !== undefined) {
            if (chart.options.debug === true) {
                console.log('Update series id: ' + id);
                console.log(data.length);
                console.log('------');
            }
            chart.updateSeries(data, animate);
        }
    },

    addPointAnnotation(id, annotation, pushToMemory) {
        var data = JSON.parse(annotation);
        var chart = this.findChart(id);
        if (chart !== undefined) {
            if (chart.options.debug === true) {
                console.log('Add point annotation series id: ' + id);
                console.log(data);
                console.log('------');
            }
            chart.addPointAnnotation(data, pushToMemory);
        }
    },

    addXaxisAnnotation(id, annotation, pushToMemory) {
        var data = JSON.parse(annotation);
        var chart = this.findChart(id);
        if (chart !== undefined) {
            if (chart.options.debug === true) {
                console.log('Add XAxis annotation id: ' + id);
                console.log(data);
                console.log('------');
            }
            chart.addXaxisAnnotation(data, pushToMemory);
        }
    },

    addYaxisAnnotation(id, annotation, pushToMemory) {
        var data = JSON.parse(annotation);
        var chart = this.findChart(id);
        if (chart !== undefined) {
            if (chart.options.debug === true) {
                console.log('Add YAxis annotation id: ' + id);
                console.log(data);
                console.log('------');
            }
            chart.addYaxisAnnotation(data, pushToMemory);
        }
    },

    clearAnnotations(id) {
        var chart = this.findChart(id);
        if (chart !== undefined) {
            if (chart.options.debug === true) {
                console.log('Clear annotations id: ' + id);
                console.log('------');
            }
            chart.clearAnnotations();
        }
    },

    removeAnnotation(chartid, id) {
        var chart = this.findChart(chartid);
        if (chart !== undefined) {
            if (chart.options.debug === true) {
                console.log('Remove annotation ' + id + ' Chartid: ' + chartid);
                console.log('------');
            }
            chart.removeAnnotation(id);
        }
    },

    toggleSeries(id, seriesName) {
        var chart = this.findChart(id);
        if (chart !== undefined) {

            if (chart.options.debug === true) {
                console.log('Toogle series ' + seriesName + ' id: ' + id);
            }
            chart.toggleSeries(seriesName)
        }
    },

    showSeries(id, seriesName) {
        var chart = this.findChart(id);
        if (chart !== undefined) {

            if (chart.options.debug === true) {
                console.log('Show series ' + seriesName + ' id: ' + id);
            }
            chart.showSeries(seriesName)
        }
    },

    hideSeries(id, seriesName) {
        var chart = this.findChart(id);
        if (chart !== undefined) {

            if (chart.options.debug === true) {
                console.log('Hide series ' + seriesName + ' id: ' + id);
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
        this.destroyChart(options.chart.id);

        chart = new ApexCharts(container, options);
        chart.render();

        if (options.debug == true) {
            console.log('Chart ' + options.chart.id + ' rendered');
        }
    }
}