# Release notes

## v8.0.0

Bundles ApexCharts.js 8.8.1 (was 6.5.0). The core moves up two major versions, some of its defaults change what a
click or a scroll does, and the bundled files change shape, so this is a major release. Two properties changed type
(see C# changes below): code that assigns them still compiles, code that reads them back as `double?` does not.

### C# changes

- `AnnotationsPoint.Y` changed from `double?` to `object`, so a point annotation can name a category row on a
  horizontal bar or rangeBar chart. Assigning a number still compiles; reading it back needs a conversion, for example
  `point.Y is string ? 0 : Convert.ToDouble(point.Y)`.
- `AnnotationsYAxis.Width` changed from `double?` to `object` and is sent as a CSS length such as `"50%"` or
  `"200px"` (a number broke the chart, #611). Code that reads it as `double?` needs the same change.
- A library compiled against 7.x needs a rebuild.

### ⚠️ Behaviour changes to check when upgrading

- A legend click on a pie, donut or polar area chart hides or shows that slice instead of selecting it.
  `OnLegendClicked` still runs; `OnDataPointSelection` does not, so `DataPointSelectionSource.Legend` is no longer
  reported. To select from the legend, call `ToggleDataPointSelectionAsync` in `OnLegendClicked`.
- The mouse wheel zooms only with Ctrl or Cmd held (or a trackpad pinch); a plain wheel scrolls the page and shows a
  hint. `Zoom.AllowMouseWheelZoom = true` brings back the old wheel.
- A series hidden from the legend stays hidden when the chart updates.
- Data labels that collide across series are moved apart, and bar data labels slide to their new place on an update
  (`DataLabels.Animate.Enabled = false` to snap).
- `--apx-font-family` on the page sets the chart font unless `Chart.FontFamily` is set.
- A `Chart.ForeColor`, grid colour or axis colour you set now wins over the `--apx-fore` and `--apx-grid` tokens,
  even when it equals the default.
- If you set `JavascriptPath`, copy `js/apexcharts.full.min.js` next to your script as well as `js/apexcharts.esm.js`,
  which is now a small module that loads it, plus `js/features/ignite-ui.js` for Ignite UI.

### ✨ New

- Ignite UI theming, opt-in: `AddApexCharts(o => o.IgniteUI = true)` (also `AddApexChartsMaui`) loads the add-on, and
  charts take their palette, text, gridlines, tooltip, font and light or dark variant from the page's Ignite UI theme.
  No extra license key. `Theme.DesignSystem = false` turns it off for one chart. See the README.
- `--apx-*` tokens: `--apx-font-family` sets the chart font and `--apx-radius-factor` scales the built-in corner radii.
  `Theme.Tokens = false` stops one chart reading any token (`--apx-*` and `--ig-*`).
- Basque (`eu`) locale; the bundled locales gain the newer toolbar and accessibility strings.

## v7.1.0

### ⚠️ Property type changes

Code that assigns these properties keeps compiling. Code that reads them back as their old type needs a change.

- `PlotOptionsBar.DumbbellColors`: `List<string>` → `DumbbellColorList`. Each entry is a single color for both dots
  or a start/end pair, e.g. `DumbbellColors = [["#008FFB", "#00E396"]]`. Assigning a `List<string>` or
  `List<List<string>>` still compiles; reading the value as `List<string>` does not (#676).
- `LegendMarkers.StrokeWidth`: `double?` → `Size`, so it accepts one width per series. Assigning a number still
  compiles; reading the value as `double?` does not (#676).

### ✨ New

- `ChartType.Funnel` and `ChartType.Pyramid`, drawn from bar series (#663, #676).
- `PlotOptions.Funnel` { `Shape` = `Rectangle`/`Trapezoid`, `LastShape` = `Flat`/`Taper` } (#663, #676).
- `Animations.RespectReducedMotion`: set `false` to keep animations when the OS requests reduced motion (#674,
  fixes #672).
- Azerbaijani (`az`) locale, listed in `IApexChartService.LocaleResources` (#671, #675).
- Docs: an Examples gallery at `/blazor-chart-demos` and new demos for most chart types (#663, thanks @PeerConradi).

### 🐛 Fixes

- Chart events and `ApexPointTooltip` no longer throw after the host passes a new `Options` instance (#673, fixes
  #487).

## v7.0.0 (proposed): ApexCharts.js 6.5.0 upgrade + premium licensing

> The package version is set from the git tag at release time (`dotnet pack -p:Version=<tag>`). `7.0.0` is the
> recommended tag for this release (major: the vendored core jumps from 5.16.0 to 6.5.0, several ApexCharts 6.0
> behaviors are on by default, and a new licensing/watermark model is introduced). Change the tag if you prefer.

Upgrades the vendored ApexCharts core from **5.16.0 to 6.5.0** and surfaces the new v6 capabilities through the
strongly-typed Blazor API. The seven premium modules are gated by the same offline license/watermark mechanism used
across the ApexCharts family (apexgantt, apextree, apexsankey).

### ✨ New: licensing

- Premium modules (history, perspectives, link/crossfilter, ink, measure, contextMenu, storyboard) work fully in
  **trial mode** but show an `APEXCHARTS` watermark until a valid key is applied. All chart types and every other
  option are never gated.
- Apply a key application-wide: `services.AddApexCharts(o => o.LicenseKey = "APEX-...")` (also `AddApexChartsMaui`).
- Or per-chart: `Chart.License`.
- Or at runtime: `IApexChartService.SetLicenseAsync("APEX-...")`.
- Keys are validated offline (no network call); the format is shared across the ApexCharts family.

### ✨ New free options (no license required)

- **Canvas renderer** (`Chart.Renderer` = `Svg`/`Canvas`/`Auto`, `Chart.RendererThreshold`): hybrid SVG/canvas for
  large datasets and heatmaps. `chart.GetActiveRendererAsync()` reports the active renderer.
- **Real-time streaming** (`Chart.Streaming` { `Enabled`, `MaxPoints` }).
- **Native mobile gestures** (`Chart.Zoom.Pinch`, new `Chart.Pan` { `Inertia`, `Friction` }).
- **OS-aware themes** (`Theme.Follow` = `Os`, `Theme.Name`).
- **Pluggable easing**: `Easing` enum extended with `EaseInSine`…`EaseInOutBack`; new `DynamicAnimation.Easing`.
- **Bar chart race**: `DataLabels.Animate` and `DataLabels.CountUp`.
- **Measure toolbar tool**: `Toolbar.AutoSelected = Measure`, `Toolbar.Tools.Measure`.

### ✨ New premium options (gated)

- `Chart.History` (undo/redo), `Chart.Perspectives`, `Chart.Link` (+ `Chart.Group`, `ChartLinkBins`), `Chart.Ink`,
  `Chart.Measure` (+ `MeasureColors`, `MeasureFormat`), `Chart.ContextMenu` (+ items/labels/line).

### ✨ New methods (on `ApexChart<TItem>`)

- History: `UndoAsync`, `RedoAsync`, `JumpHistoryAsync`.
- Measure: `StartMeasureAsync`, `StopMeasureAsync`, `ClearMeasuresAsync`.
- Perspectives: `CapturePerspectiveAsync`, `ApplyPerspectiveAsync`, `PerspectiveToUrlAsync`, `ApplyPerspectiveFromUrlAsync` (restore a shared `#apex=` link on render), `SavePerspectiveAsync`, `ListPerspectivesAsync`.
- Storyboard: `StoryboardBindAsync` (with `StoryboardOptions.Scroller`/`Offset`/`Animate` and per-beat `View`/`Options`/`Announce` for scroll-driven scrollytelling), `StoryboardGoToAsync`, `StoryboardCurrentAsync`, `StoryboardUnbindAsync`.
- Renderer/theme: `GetActiveRendererAsync`, `RefreshTokensAsync`.

### ✨ New crossfilter engine (on `IApexChartService`)

- `RegisterCrossfilterAsync(id, records)` registers a shared record set (`ApexCharts.crossfilter`); charts that
  declare a matching `Chart.Link.Id` + `Chart.Link.Dimension` aggregate over it, and clicking a mark filters every
  linked chart. `ResetCrossfilterAsync(id)` clears filters; `SubscribeCrossfilterAsync(id, dotNetRef)` delivers
  `change` events (as `CrossfilterState`) to a `[JSInvokable] OnCrossfilterChange` handler.

### ✨ New events

- Ink: `OnAnnotationCreated`, `OnAnnotationDragged`, `OnAnnotationEdited`, `OnAnnotationStyled`, `OnAnnotationDeleted`.
- Measure: `OnMeasured`. Storyboard: `OnBeatChange`.

### 🔧 Behavior changes (from ApexCharts 6.0/6.4, on by default)

- Data updates that add/remove points animate coherently; two-finger pinch-zoom and pan are on for touch (opt out via
  `Chart.Animations.DynamicAnimation.Enabled`, `Chart.Zoom.Pinch`, `Chart.Pan.Inertia`).
- Heatmap defaults changed (tooltip anchored above the cell, zoom off, y-label thinning).
- Mouse-wheel zoom is now smooth and cursor-anchored.

### 📦 Internal

- Vendored `wwwroot/js/apexcharts.esm.js`, `wwwroot/css/apexcharts.css`, and `wwwroot/locales/*.json` refreshed to
  6.5.0 (adds `bg`, `gl`, `ro` locales and the `measure` toolbar label). Asset cache-busters (`?ver=`) set to `6.5.0`
  (the vendored core version).
- Changes mirrored in the `Blazor-ApexCharts-MAUI` service.
- E2E smoke test extended to assert the trial-vs-licensed watermark and the new demo routes render.

### Notes

- Function-valued options (e.g. `Measure.Format`, `Link.Dimension/Reduce`, custom `ContextMenu` item `OnClick`,
  `Storyboard` beats) are provided as JS function strings via the existing `@eval` convention.
- The JS-authoring statics (`registerPlugin`/Weave, `registerSeriesType`/Marks, `registerTheme`, `registerEasing`)
  are intentionally out of scope for the declarative Blazor wrapper. The `crossfilter()` engine is exposed via
  `IApexChartService` (see above).
