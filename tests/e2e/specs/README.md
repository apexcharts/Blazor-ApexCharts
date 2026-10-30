# E2E specs

`tests/e2e/run.mjs` runs `smoke.mjs` and then every `*.mjs` file in this folder against the docs
demo (`docs/BlazorApexCharts.Docs.Wasm`) in headless Chromium. `npm test` and the E2E Smoke workflow
both go through `run.mjs`, so a spec added here runs in CI with no other change.

## The contract

One file per spec, named `issue-<number>-<slug>.mjs` (or `<feature>-<slug>.mjs` when there is no
issue). It default-exports an async function:

```js
import { gotoChart, watchErrors, assert, assertNoErrors } from '../lib/helpers.mjs';

export default async function (page, BASE) {
  const realErrors = watchErrors(page);
  await gotoChart(page, BASE, 'issues/123');
  assert(await page.locator('.apexcharts-bar-area').count() === 3, 'expected 3 bars');
  assertNoErrors(realErrors, 'issue-123');
}
```

- `page` is a fresh Playwright page in its own browser context (viewport 1400x1000).
- `BASE` is the demo origin without a trailing slash, for example `http://localhost:5184`.
- Throw to fail, return to pass. Each spec has a 120 s budget (`SPEC_TIMEOUT_MS`).
- Files starting with `_` are not run. `_example.mjs` is the template to copy.
- Never edit another spec or `smoke.mjs` to make yours pass; each spec stands alone.
- Wait on elements or on text that only the expected outcome produces
  (`locator.waitFor`, `page.waitForSelector`, `page.waitForFunction`), not on fixed delays.

Helpers in `../lib/helpers.mjs`: `gotoChart(page, BASE, route, selector?)`, `watchErrors(page)`,
`assert(cond, msg)`, `assertNoErrors(realErrors, label)`, `isIgnorableError(text)`.

## Blazor Server

The specs run against the WebAssembly demo by default. A spec whose outcome depends on the host
(anything that needs synchronous JS to .NET calls, such as the .NET label formatters) asks
`await blazorHost(page)`, which returns `'wasm'` or `'server'` from the Blazor script the page loaded,
and checks the documented behavior of that host. Set `E2E_HOST=wasm` or `E2E_HOST=server` to override
the detection. `recordSyncWarnings(page)` (before navigating) and `syncWarnings(page)` collect the
console warnings the library writes when a host cannot call .NET synchronously.

## A demo page for the spec

When no existing docs page reproduces the issue, add one as its own file at
`docs/BlazorApexCharts.Docs/Components/Issues/Issue<number>.razor` with
`@page "/issues/<number>"`. Pages there are routed without any menu entry, so parallel changes do
not touch a shared file. Give the elements the spec reads a stable `id`.

## Running locally

```sh
dotnet build docs/BlazorApexCharts.Docs.Wasm/BlazorApexCharts.Docs.Wasm.csproj -c Debug
dotnet run --project docs/BlazorApexCharts.Docs.Wasm/BlazorApexCharts.Docs.Wasm.csproj -c Debug --no-build --urls http://localhost:5184 &

cd tests/e2e
npm install && npx playwright install chromium   # once
BASE_URL=http://localhost:5184 node run.mjs                     # smoke + all specs
BASE_URL=http://localhost:5184 SKIP_SMOKE=1 node run.mjs 123    # only specs whose name contains "123"
```
