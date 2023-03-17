window.blazor_apexchart = {

    getYAxisLabel(value, index, w) {

        if (window.wasmBinaryFile === undefined && window.WebAssembly === undefined) {
            console.warn("YAxis labels is only supported in Blazor WASM");
            return value;
        }

        if (w !== undefined) {
            return w.config.dotNetObject.invokeMethod('JSGetFormattedYAxisValue', value);
        };

        if (index !== undefined) {
            return index.w.config.dotNetObject.invokeMethod('JSGetFormattedYAxisValue', value);
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

    LogMethodCall(chart, method, data) {
        if (chart !== undefined) {
            if (chart.opts.debug === true) {
                console.log('------');
                console.log('Method:' + method);
                console.log("Chart Id: " + chart.opts.chart.id)
                if (data !== undefined) {
                    console.log(data);
                }
                console.log('------');
            }
        }
    },

    updateOptions(id, options, redrawPaths, animate, updateSyncedCharts, zoom) {
        var data = JSON.parse(options, (key, value) =>
            (key === 'formatter' || key === 'dateFormatter' || key === 'custom') && value.length !== 0 ? eval("(" + value + ")") : value
        );
        var chart = this.findChart(id);
        if (chart !== undefined) {
            this.LogMethodCall(chart, "updateOptions", options);
            chart.updateOptions(data, redrawPaths, animate, updateSyncedCharts);

            if (zoom !== null) {
                chart.zoomX(zoom.start, zoom.end);
            }

        }
    },

    appendData(id, data) {
        var newData = JSON.parse(data);
        var chart = this.findChart(id);
        if (chart !== undefined) {
            this.LogMethodCall(chart, "appendDate", newData);
            return chart.appendData(newData);
        }
    },

    toggleDataPointSelection(id, seriesIndex, dataPointIndex) {
        var chart = this.findChart(id);
        if (chart !== undefined) {
            this.LogMethodCall(chart, "toggleDataPointSelection [" + seriesIndex + '] [' + dataPointIndex + ']');
            var pointIndex;
            if (dataPointIndex !== null) {
                pointIndex = dataPointIndex;
            }

            return chart.toggleDataPointSelection(seriesIndex, pointIndex);
        }
    },

    zoomX(id, start, end) {
        var chart = this.findChart(id);
        if (chart !== undefined) {
            this.LogMethodCall(chart, 'zoomX ' + start + ", " + end);
            return chart.zoomX(start, end);
        }
    },

    resetSeries(id, shouldUpdateChart, shouldResetZoom) {
        var chart = this.findChart(id);
        if (chart !== undefined) {
            this.LogMethodCall(chart, 'resetSeries ' + shouldUpdateChart + ", " + shouldResetZoom);
            return chart.resetSeries(shouldUpdateChart, shouldResetZoom);
        }
    },

    dataUri(id, options) {
        var opt = JSON.parse(options);
        var chart = this.findChart(id);
        if (chart !== undefined) {
            this.LogMethodCall(chart, 'dataUri', options);
            return chart.dataURI(opt);
        }

        return '';
    },

    updateSeries(id, series, animate) {
        var data = JSON.parse(series);
        var chart = this.findChart(id);
        if (chart !== undefined) {
            this.LogMethodCall(chart, 'updateSeries', series);
            chart.updateSeries(data, animate);
        }
    },

    addPointAnnotation(id, annotation, pushToMemory) {
        var data = JSON.parse(annotation);
        var chart = this.findChart(id);
        if (chart !== undefined) {
            this.LogMethodCall(chart, 'addPointAnnotation', annotation);
            chart.addPointAnnotation(data, pushToMemory);
        }
    },

    addXaxisAnnotation(id, annotation, pushToMemory) {
        var data = JSON.parse(annotation);
        var chart = this.findChart(id);
        if (chart !== undefined) {
            this.LogMethodCall(chart, 'addXaxisAnnotation', annotation);
            chart.addXaxisAnnotation(data, pushToMemory);
        }
    },

    addYaxisAnnotation(id, annotation, pushToMemory) {
        var data = JSON.parse(annotation);
        var chart = this.findChart(id);
        if (chart !== undefined) {
            this.LogMethodCall(chart, 'addYaxisAnnotation', annotation);
            chart.addYaxisAnnotation(data, pushToMemory);
        }
    },

    clearAnnotations(id) {
        var chart = this.findChart(id);
        if (chart !== undefined) {
            this.LogMethodCall(chart, 'clearAnnotations');
            chart.clearAnnotations();
        }
    },

    removeAnnotation(chartid, id) {
        var chart = this.findChart(chartid);
        if (chart !== undefined) {
            this.LogMethodCall(chart, 'removeAnnotation', id);
            chart.removeAnnotation(id);
        }
    },

    toggleSeries(id, seriesName) {
        var chart = this.findChart(id);
        if (chart !== undefined) {
            this.LogMethodCall(chart, 'toggleSeries', seriesName);
            chart.toggleSeries(seriesName)
        }
    },

    showSeries(id, seriesName) {
        var chart = this.findChart(id);
        if (chart !== undefined) {
            this.LogMethodCall(chart, 'showSeries', seriesName);
            chart.showSeries(seriesName)
        }
    },

    hideSeries(id, seriesName) {
        var chart = this.findChart(id);
        if (chart !== undefined) {
            this.LogMethodCall(chart, 'hideSeries', seriesName);
            chart.hideSeries(seriesName)
        }
    },

    renderChart(dotNetObject, container, options) {
        if (options.debug == true) {
            console.log(options);
        }

        var options = JSON.parse(options, (key, value) =>
            (key === 'formatter' || key === 'tooltipHoverFormatter' || key === 'dateFormatter' || key === 'custom') && value.length !== 0 ? eval("(" + value + ")") : value
        );

        if (options.debug == true) {
            console.log(options);
        }

        options.dotNetObject = dotNetObject;


        options.chart.events = {};

        if (options.hasDataPointLeave === true) {
            options.chart.events.dataPointMouseLeave = function (event, chartContext, config) {
                var selection = {
                    dataPointIndex: config.dataPointIndex,
                    seriesIndex: config.seriesIndex
                }
                dotNetObject.invokeMethodAsync('JSDataPointLeave', selection);
            }
        };

        if (options.hasDataPointEnter === true) {
            options.chart.events.dataPointMouseEnter = function (event, chartContext, config) {
                var selection = {
                    dataPointIndex: config.dataPointIndex,
                    seriesIndex: config.seriesIndex
                }
                dotNetObject.invokeMethodAsync('JSDataPointEnter', selection);
            }
        };


        if (options.hasDataPointSelection === true) {
            options.chart.events.dataPointSelection = function (event, chartContext, config) {
                var selection = {
                    dataPointIndex: config.dataPointIndex,
                    seriesIndex: config.seriesIndex,
                    selectedDataPoints: config.selectedDataPoints
                }
                dotNetObject.invokeMethodAsync('JSDataPointSelected', selection);
            }
        };

        if (options.hasMarkerClick === true) {
            options.chart.events.markerClick = function (event, chartContext, config) {
                var selection = {
                    dataPointIndex: config.dataPointIndex,
                    seriesIndex: config.seriesIndex,
                    selectedDataPoints: config.selectedDataPoints
                }
                dotNetObject.invokeMethodAsync('JSMarkerClick', selection);
            }
        };

        if (options.hasXAxisLabelClick === true) {
            options.chart.events.xAxisLabelClick = function (event, chartContext, config) {
                var data = {
                    labelIndex: config.labelIndex,
                    caption: event.target.innerHTML
                };
                dotNetObject.invokeMethodAsync('JSXAxisLabelClick', data);
            }
        };

       
        if (options.hasLegendClick === true) {
            options.chart.events.legendClick = function (chartContext, seriesIndex, config) {
                var legendClick = {
                    seriesIndex: seriesIndex,
                    collapsed: config.globals.collapsedSeriesIndices.indexOf(seriesIndex) !== -1
                }

                dotNetObject.invokeMethodAsync('JSLegendClicked', legendClick);
            }
        };

        if (options.hasSelection === true) {
            options.chart.events.selection = function (chartContext, config) {
                dotNetObject.invokeMethodAsync('JSSelected', config);
            };
        };

        if (options.hasBrushScrolled === true) {
            options.chart.events.brushScrolled = function (chartContext, config) {
                dotNetObject.invokeMethodAsync('JSBrushScrolled', config);
            };
        };

        if (options.hasZoomed === true) {
            options.chart.events.zoomed = function (chartContext, config) {
                dotNetObject.invokeMethodAsync('JSZoomed', config);
            };
        };

        //Always destry chart if it exists
        this.destroyChart(options.chart.id);

        chart = new ApexCharts(container, options);
        chart.render();

        if (options.debug == true) {
            console.log('Chart ' + options.chart.id + ' rendered');
        }
    }
}
