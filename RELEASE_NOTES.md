# Release notes

## v7.0.0 (proposed) — ApexCharts.js 6.5.0 upgrade + premium licensing

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

- **Canvas renderer** (`Chart.Renderer` = `Svg`/`Canvas`/`Auto`, `Chart.RendererThreshold`) — hybrid SVG/canvas for
  large datasets and heatmaps. `chart.GetActiveRendererAsync()` reports the active renderer.
- **Real-time streaming** (`Chart.Streaming` { `Enabled`, `MaxPoints` }).
- **Native mobile gestures** (`Chart.Zoom.Pinch`, new `Chart.Pan` { `Inertia`, `Friction` }).
- **OS-aware themes** (`Theme.Follow` = `Os`, `Theme.Name`).
- **Pluggable easing** — `Easing` enum extended with `EaseInSine`…`EaseInOutBack`; new `DynamicAnimation.Easing`.
- **Bar chart race** — `DataLabels.Animate` and `DataLabels.CountUp`.
- **Measure toolbar tool** — `Toolbar.AutoSelected = Measure`, `Toolbar.Tools.Measure`.

### ✨ New premium options (gated)

- `Chart.History` (undo/redo), `Chart.Perspectives`, `Chart.Link` (+ `Chart.Group`, `ChartLinkBins`), `Chart.Ink`,
  `Chart.Measure` (+ `MeasureColors`, `MeasureFormat`), `Chart.ContextMenu` (+ items/labels/line).

### ✨ New methods (on `ApexChart<TItem>`)

- History: `UndoAsync`, `RedoAsync`, `JumpHistoryAsync`.
- Measure: `StartMeasureAsync`, `StopMeasureAsync`, `ClearMeasuresAsync`.
- Perspectives: `CapturePerspectiveAsync`, `ApplyPerspectiveAsync`, `PerspectiveToUrlAsync`, `SavePerspectiveAsync`, `ListPerspectivesAsync`.
- Storyboard: `StoryboardBindAsync`, `StoryboardGoToAsync`, `StoryboardCurrentAsync`, `StoryboardUnbindAsync`.
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
