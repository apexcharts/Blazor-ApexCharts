import ApexCharts from './apexcharts.esm.js?ver=6.5.0'

// export function for Blazor to point to the window.blazor_apexchart. To be compatible with the most JS Interop calls the window will be return.
export function get_apexcharts() {
    window.ApexCharts = ApexCharts
    return window;
}

window.blazor_apexchart = {

    getDotNetObjectReference(index, w) {
        var chartId = null;

        if (w !== undefined && w.config !== undefined) {
            chartId = w.config.chart.id;
        }

        if (w !== undefined && w.w !== undefined && w.w.config !== undefined) {
            chartId = w.w.config.chart.id;
        }

        if (index !== undefined && index.w !== undefined && index.w.config !== undefined) {
            chartId = index.w.config.chart.id;
        }

        if (index !== undefined && index.config !== undefined) {
            chartId = index.config.chart.id;
        }

        if (chartId != null) {
            return this.dotNetRefs.get(chartId);
        }
        return null;
    },

    getXAxisLabel(value, index, w) {

        if (window.wasmBinaryFile === undefined && window.WebAssembly === undefined) {
            console.warn("XAxis labels is only supported in Blazor WASM");
            return value;
        }

        var dotNetRef = this.getDotNetObjectReference(index, w);
        if (dotNetRef != null) {
            return dotNetRef.invokeMethod('JSGetFormattedXAxisValue', value);
        }

        return value;
    },

    getYAxisLabel(value, index, w) {

        if (window.wasmBinaryFile === undefined && window.WebAssembly === undefined) {
            console.warn("YAxis labels is only supported in Blazor WASM");
            return value;
        }

        var dotNetRef = this.getDotNetObjectReference(index, w);
        if (dotNetRef != null) {
            return dotNetRef.invokeMethod('JSGetFormattedYAxisValue', value);
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

        this.dotNetRefs.delete(id);

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

    setGlobalOptions(options) {
        var opt = this.parseOptions(options);

        if (opt.debug === true) {
            console.log('------');
            console.log('Method: setGlobalOptions');
            console.log(opt);
            console.log('------');
        }

        opt._chartInstances = Apex._chartInstances;
        Apex = opt;
    },

    setLicense(licenseKey) {
        try {
            if (ApexCharts && typeof ApexCharts.setLicense === 'function') {
                ApexCharts.setLicense(licenseKey);
                return true;
            }
            console.error('ApexCharts.setLicense is not available');
            return false;
        } catch (error) {
            console.error('failed to set apexcharts license:', error);
            return false;
        }
    },

    // ===== v6 crossfilter engine (link filter mode) =====

    registerCrossfilter(id, records) {
        try {
            if (ApexCharts && typeof ApexCharts.crossfilter === 'function') {
                ApexCharts.crossfilter({ id: id, records: records });
                return true;
            }
            console.error('ApexCharts.crossfilter is not available');
            return false;
        } catch (error) {
            console.error('failed to register crossfilter:', error);
            return false;
        }
    },

    resetCrossfilter(id) {
        var cf = ApexCharts.getCrossfilter(id);
        if (cf && typeof cf.reset === 'function') { cf.reset(); }
    },

    onCrossfilterChange(id, dotNetRef) {
        var cf = ApexCharts.getCrossfilter(id);
        if (cf && typeof cf.on === 'function') {
            cf.on('change', function (state) {
                // Sanitize to plain, serializable data before crossing the interop boundary.
                dotNetRef.invokeMethodAsync('OnCrossfilterChange', {
                    total: state.total,
                    filteredCount: state.filteredCount,
                    filters: state.filters
                });
            });
        }
    },

    // Center a storyboard step inside its own scroll container WITHOUT scrolling the page: adjust
    // only the scroller's scrollTop. Used by manual beat controls (prev/next/dots) to keep the
    // story panel in sync with a goTo(). Mirrors the scrollytelling reference's scrollToBeat.
    scrollStoryToStep(scrollerSelector, stepSelector) {
        var scroller = document.querySelector(scrollerSelector);
        var step = document.querySelector(stepSelector);
        if (!scroller || !step) { return; }
        var sRect = step.getBoundingClientRect();
        var cRect = scroller.getBoundingClientRect();
        scroller.scrollTop += sRect.top - cRect.top - (scroller.clientHeight - step.clientHeight) / 2;
    },

    // ===== v6 premium feature methods =====

    undo(id) {
        var chart = this.findChart(id);
        if (chart !== undefined) { chart.history.undo(); }
    },

    redo(id) {
        var chart = this.findChart(id);
        if (chart !== undefined) { chart.history.redo(); }
    },

    jumpHistory(id, historyId) {
        var chart = this.findChart(id);
        if (chart !== undefined) { chart.history.jump(historyId); }
    },

    startMeasure(id) {
        var chart = this.findChart(id);
        if (chart !== undefined) { chart.startMeasure(); }
    },

    stopMeasure(id) {
        var chart = this.findChart(id);
        if (chart !== undefined) { chart.stopMeasure(); }
    },

    clearMeasures(id) {
        var chart = this.findChart(id);
        if (chart !== undefined) { chart.clearMeasures(); }
    },

    refreshTokens(id) {
        var chart = this.findChart(id);
        if (chart !== undefined) { chart.refreshTokens(); }
    },

    getActiveRenderer(id) {
        var chart = this.findChart(id);
        return chart !== undefined ? chart.getActiveRenderer() : null;
    },

    capturePerspective(id) {
        var chart = this.findChart(id);
        return chart !== undefined ? JSON.stringify(chart.perspectives.capture()) : null;
    },

    applyPerspective(id, token, animate) {
        var chart = this.findChart(id);
        if (chart !== undefined) { chart.perspectives.apply(JSON.parse(token), { animate: animate }); }
    },

    perspectiveToUrl(id) {
        var chart = this.findChart(id);
        return chart !== undefined ? chart.perspectives.toURL() : null;
    },

    savePerspective(id, name) {
        var chart = this.findChart(id);
        if (chart !== undefined) { chart.perspectives.save(name); }
    },

    listPerspectives(id) {
        var chart = this.findChart(id);
        return chart !== undefined ? JSON.stringify(chart.perspectives.list()) : null;
    },

    // Restore a perspective encoded in the current page URL (#apex=<token>). Returns true if a
    // token was present and applied, so a shared link reopens the exact captured view.
    applyPerspectiveFromUrl(id) {
        var chart = this.findChart(id);
        if (chart === undefined) { return false; }
        var token = ApexCharts.perspectives.fromURL(window.location.href);
        if (!token) { return false; }
        chart.perspectives.apply(token);
        return true;
    },

    // Copy text to the clipboard (from a user gesture, on a secure context / localhost).
    copyText(text) {
        try {
            if (navigator.clipboard && navigator.clipboard.writeText) {
                navigator.clipboard.writeText(text);
                return true;
            }
        } catch (e) { /* clipboard blocked */ }
        return false;
    },

    // Reflect a URL in the address bar without navigating/reloading (so it can be copied there too).
    setBrowserUrl(url) {
        try { history.replaceState(null, '', url); } catch (e) { /* ignore */ }
    },

    storyboardBind(id, options) {
        var chart = this.findChart(id);
        return chart !== undefined ? chart.storyboard.bind(this.parseOptions(options)) : null;
    },

    storyboardGoTo(id, beat, animate) {
        var chart = this.findChart(id);
        if (chart !== undefined) { chart.storyboard.goTo(beat, { animate: animate }); }
    },

    storyboardCurrent(id) {
        var chart = this.findChart(id);
        return chart !== undefined ? JSON.stringify(chart.storyboard.current()) : null;
    },

    storyboardUnbind(id) {
        var chart = this.findChart(id);
        if (chart !== undefined) { chart.storyboard.unbind(); }
    },

    updateOptions(id, options, redrawPaths, animate, updateSyncedCharts, zoom) {
        var options = this.parseOptions(options);
        var chart = this.findChart(id);
        if (chart !== undefined) {
            this.LogMethodCall(chart, "updateOptions", options);
            chart.updateOptions(options, redrawPaths, animate, updateSyncedCharts);

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

    setLocale(id, name) {
        var chart = this.findChart(id);
        if (chart !== undefined) {
            this.LogMethodCall(chart, 'setLocale ' + name);
            chart.setLocale(name);
            chart.update();
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

   async getSvgStringAsync(id) {
        var chart = this.findChart(id);
        if (chart !== undefined) {
            this.LogMethodCall(chart, 'getSvgString');
            const svgString = await chart.getSvgString();
            return svgString;
        }
        return '';
    },

    appendSeries(id, series, animate, overwriteInitialSeries) {
        var data = JSON.parse(series);
        var chart = this.findChart(id);
        if (chart !== undefined) {
            this.LogMethodCall(chart, 'appendSeries', series);
            chart.appendSeries(data, animate, overwriteInitialSeries);
        }
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

    highlightSeries(id, seriesName) {
        var chart = this.findChart(id);
        if (chart !== undefined) {
            this.LogMethodCall(chart, 'highlightSeries', seriesName);
            chart.highlightSeries(seriesName)
        }
    },

    copyTooltipContent(chartId) {

        var sourceId = "tooltip_source_" + chartId;
        var targetId = "tooltip_target_" + chartId;

        var sourceElement = document.getElementById(sourceId);
        var targetElement = document.getElementById(targetId);

        if (sourceElement && targetElement) {
            targetElement.innerHTML = sourceElement.innerHTML;
        }

    },

    dotNetRefs: new Map(),

    renderChart(dotNetObject, container, options, events) {
        var options = this.parseOptions(options);

        if (options.debug == true) {
            console.log(options);
        }

        options.chart.events = {};

        if (options.tooltip != undefined && options.tooltip.customTooltip == true) {
            options.tooltip.custom = function ({ series, seriesIndex, dataPointIndex, w }) {

                if (dataPointIndex == null) {
                    dataPointIndex = seriesIndex;
                    seriesIndex = 0
                }


                var selection = {
                    dataPointIndex: dataPointIndex || 0,
                    seriesIndex: seriesIndex
                };

                var targetId = "tooltip_target_" + w.globals.chartID;
                var el = document.getElementById(targetId);

                if (el === null) {
                    el = document.createElement("DIV");
                    el.id = targetId;
                }

                dotNetObject.invokeMethodAsync('RazorTooltip', selection);

                return el;


            };
        }

        if (events.hasDataPointLeave === true) {
            options.chart.events.dataPointMouseLeave = function (event, chartContext, config) {
                var selection = {
                    dataPointIndex: config.dataPointIndex,
                    seriesIndex: config.seriesIndex
                };

                dotNetObject.invokeMethodAsync('JSDataPointLeave', selection);
            }
        };

        if (events.hasDataPointEnter === true) {
            options.chart.events.dataPointMouseEnter = function (event, chartContext, config) {
                var selection = {
                    dataPointIndex: config.dataPointIndex,
                    seriesIndex: config.seriesIndex
                };

                dotNetObject.invokeMethodAsync('JSDataPointEnter', selection);
            }
        };

        if (events.hasDataPointSelection === true) {
            options.chart.events.dataPointSelection = function (event, chartContext, config) {
                var selection = {
                    dataPointIndex: config.dataPointIndex,
                    seriesIndex: config.seriesIndex,
                    selectedDataPoints: config.selectedDataPoints
                };

                dotNetObject.invokeMethodAsync('JSDataPointSelected', selection);
            }
        };

        if (events.hasMarkerClick === true) {
            options.chart.events.markerClick = function (event, chartContext, config) {
                var selection = {
                    dataPointIndex: config.dataPointIndex,
                    seriesIndex: config.seriesIndex,
                    selectedDataPoints: config.selectedDataPoints
                };

                dotNetObject.invokeMethodAsync('JSMarkerClick', selection);
            }
        };

        if (events.hasXAxisLabelClick === true) {
            options.chart.events.xAxisLabelClick = function (event, chartContext, config) {
                var data = {
                    labelIndex: config.labelIndex,
                    caption: event.target.innerHTML
                };

                dotNetObject.invokeMethodAsync('JSXAxisLabelClick', data);
            }
        };

        if (events.hasLegendClick === true) {
            options.chart.events.legendClick = function (chartContext, seriesIndex, config) {
                var legendClick = {
                    seriesIndex: seriesIndex,
                    collapsed: config.globals.collapsedSeriesIndices.indexOf(seriesIndex) !== -1
                };

                dotNetObject.invokeMethodAsync('JSLegendClicked', legendClick);
            }
        };

        if (events.hasSelection === true) {
            options.chart.events.selection = function (chartContext, config) {
                dotNetObject.invokeMethodAsync('JSSelected', config);
            };
        };

        if (events.hasBrushScrolled === true) {
            options.chart.events.brushScrolled = function (chartContext, config) {
                dotNetObject.invokeMethodAsync('JSBrushScrolled', config);
            };
        };

        if (events.hasZoomed === true) {
            options.chart.events.zoomed = function (chartContext, config) {
                dotNetObject.invokeMethodAsync('JSZoomed', config);
            };
        };

        if (events.hasAnimationEnd === true) {
            options.chart.events.animationEnd = function (chartContext, options) {
                dotNetObject.invokeMethodAsync('JSAnimationEnd');
            };
        };

        if (events.hasBeforeMount === true) {
            options.chart.events.beforeMount = function (chartContext, config) {
                dotNetObject.invokeMethodAsync('JSBeforeMount');
            };
        };

        if (events.hasMounted === true) {
            options.chart.events.mounted = function (chartContext, config) {
                dotNetObject.invokeMethodAsync('JSMounted');
            };
        };

        if (events.hasUpdated === true) {
            options.chart.events.updated = function (chartContext, config) {
                dotNetObject.invokeMethodAsync('JSUpdated');
            };
        };

        if (events.hasMouseMove === true) {
            options.chart.events.mouseMove = function (event, chartContext, config) {
                var selection = {
                    dataPointIndex: -1, // Documentation notes that these details are available in cartesian charts, this will prevent null reference in .NET callback
                    seriesIndex: -1
                };

                if (config.dataPointIndex != null && config.dataPointIndex >= 0)
                    selection.dataPointIndex = Number(config.dataPointIndex);

                if (config.dataPointIndex != null && config.seriesIndex >= 0)
                    selection.seriesIndex = Number(config.seriesIndex);

                dotNetObject.invokeMethodAsync('JSMouseMove', selection);
            };
        };

        if (events.hasMouseLeave === true) {
            options.chart.events.mouseLeave = function (event, chartContext, config) {
                dotNetObject.invokeMethodAsync('JSMouseLeave');
            };
        };

        if (events.hasClick === true) {
            options.chart.events.click = function (event, chartContext, config) {
                var selection = {
                    dataPointIndex: -1,
                    seriesIndex: -1
                };

                if (config.dataPointIndex >= 0 && config.dataPointIndex !== null)
                    selection.dataPointIndex = Number(config.dataPointIndex);

                if (config.seriesIndex >= 0 && config.seriesIndex !== null)
                    selection.seriesIndex = Number(config.seriesIndex);

                dotNetObject.invokeMethodAsync('JSClick', selection);
            };
        };

        if (events.hasBeforeZoom === true) {
            options.chart.events.beforeZoom = function (chartContext, config) {
                if (config.yaxis !== undefined || Array.isArray(config.yaxis))
                    config.yaxis = undefined;

                var data = dotNetObject.invokeMethod('JSBeforeZoom', config);

                return {
                    xaxis: {
                        min: data.min,
                        max: data.max
                    }
                };
            };
        };

        if (events.hasBeforeResetZoom === true) {
            options.chart.events.beforeResetZoom = function (chartContext, opts) {
                var data = dotNetObject.invokeMethod('JSBeforeResetZoom');

                return {
                    xaxis: {
                        min: data.min,
                        max: data.max
                    }
                };
            };
        };

        if (events.hasScrolled === true) {
            options.chart.events.scrolled = function (chartContext, config) {
                dotNetObject.invokeMethodAsync('JSScrolled', config);
            };
        };

        // v6 premium feature events (ink / measure / storyboard)
        if (events.hasAnnotationCreated === true) {
            options.chart.events.annotationCreated = function (ctx, args) {
                dotNetObject.invokeMethodAsync('JSAnnotationCreated', args);
            };
        };

        if (events.hasAnnotationDragged === true) {
            options.chart.events.annotationDragged = function (ctx, args) {
                dotNetObject.invokeMethodAsync('JSAnnotationDragged', args);
            };
        };

        if (events.hasAnnotationEdited === true) {
            options.chart.events.annotationEdited = function (ctx, args) {
                dotNetObject.invokeMethodAsync('JSAnnotationEdited', args);
            };
        };

        if (events.hasAnnotationStyled === true) {
            options.chart.events.annotationStyled = function (ctx, args) {
                dotNetObject.invokeMethodAsync('JSAnnotationStyled', args);
            };
        };

        if (events.hasAnnotationDeleted === true) {
            options.chart.events.annotationDeleted = function (ctx, args) {
                dotNetObject.invokeMethodAsync('JSAnnotationDeleted', args);
            };
        };

        if (events.hasMeasured === true) {
            options.chart.events.measured = function (ctx, payload) {
                dotNetObject.invokeMethodAsync('JSMeasured', payload);
            };
        };

        if (events.hasBeatChange === true) {
            options.chart.events.beatChange = function (ctx, info) {
                // info may carry a DOM element (info.el); forward only serializable fields
                dotNetObject.invokeMethodAsync('JSBeatChange', {
                    index: info != null ? info.index : null,
                    key: info != null ? info.key : null,
                    direction: info != null ? info.direction : null
                });
            };
        };

        //Always destroy chart if it exists
        this.destroyChart(options.chart.id);
        this.dotNetRefs.set(options.chart.id, dotNetObject)

        var chart = new ApexCharts(container, options);
        chart.render();

        if (options.debug == true) {
            console.log('Chart ' + options.chart.id + ' rendered');
        }
    },

    parseOptions(options) {
        return JSON.parse(options, (key, value) => {
            if (value && typeof value === 'object' && '@eval' in value) {
                value = value['@eval'];
                if (Array.isArray(value))
                    return value.map(item => eval?.("'use strict'; (" + item + ")"));
                else
                    return eval?.("'use strict'; (" + value + ")");
            }
            else {
                return value;
            }
        });
    }
}
