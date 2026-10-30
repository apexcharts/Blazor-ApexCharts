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

## Both hosting models

The suite runs against the WASM docs host and against the Blazor Server docs host
(`docs/BlazorApexCharts.Docs.Server`). `HOST` tells the run which one is behind `BASE_URL`: `wasm` (the default) or `server`. `run.mjs` reads the boot
script of the page it warms up and stops when it is the other model, so a spec never runs against
the wrong host.

- `blazorHost()` returns `'wasm'` or `'server'`; `isServerHost()` is the boolean form. Use them
  when the expected outcome differs by host (a Server circuit adds latency to every interop call,
  for example), and keep asserting on both.
- `skipOnHost(host, reason)` ends the spec as skipped on that host. Use it only for a spec that
  cannot mean anything there, and say why: `run.mjs` prints `SKIP` with the reason and lists every
  skip in its summary.
- `detectBlazorHost(page)` reads the model from a loaded page (`'server'`, `'wasm'` or `null`).

```js
import { skipOnHost } from '../lib/helpers.mjs';

export default async function (page, BASE) {
  skipOnHost('server', 'measures the WASM download, which a Server circuit does not have');
  // ...
}
```

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
npm install && npx playwright install chromium   # once (or symlink node_modules, see below)
BASE_URL=http://localhost:5184 node run.mjs                     # smoke + all specs
BASE_URL=http://localhost:5184 SKIP_SMOKE=1 node run.mjs 123    # only specs whose name contains "123"
```

Against the Blazor Server host:

```sh
dotnet run --project docs/BlazorApexCharts.Docs.Server/BlazorApexCharts.Docs.Server.csproj -c Debug --urls http://localhost:5185 &
cd tests/e2e
HOST=server BASE_URL=http://localhost:5185 node run.mjs
```

To reuse a Playwright install from another checkout or worktree, symlink it instead of running
`npm install`, and keep the link out of git through `.git/info/exclude` (the repository's
`.gitignore` files list `node_modules/`, which matches a directory but not a symlink):

```sh
ln -s /path/to/other/checkout/tests/e2e/node_modules tests/e2e/node_modules
echo 'tests/e2e/node_modules' >> "$(git rev-parse --git-common-dir)/info/exclude"
```
