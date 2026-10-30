// Runs the e2e suite against the docs demo: smoke.mjs first, then every spec in specs/.
//
// Usage (the demo must already be running, see specs/README.md):
//   BASE_URL=http://localhost:5184 node run.mjs              # smoke + all specs
//   BASE_URL=http://localhost:5184 node run.mjs issue-123    # smoke + specs whose file name contains "issue-123"
//   SKIP_SMOKE=1 node run.mjs issue-123                      # only the matching specs
//   PW_CHANNEL=chrome node run.mjs                           # drive an installed Chrome
//
// Exits non-zero if smoke.mjs fails or any spec throws.
import { spawnSync } from 'node:child_process';
import { readdirSync } from 'node:fs';
import { dirname, join } from 'node:path';
import { fileURLToPath, pathToFileURL } from 'node:url';

const here = dirname(fileURLToPath(import.meta.url));
const specsDir = join(here, 'specs');
const BASE = process.env.BASE_URL || 'http://localhost:5184';
const channel = process.env.PW_CHANNEL;
const SPEC_TIMEOUT_MS = Number(process.env.SPEC_TIMEOUT_MS || 120000);
const filters = process.argv.slice(2);

const failures = [];

if (process.env.SKIP_SMOKE !== '1') {
  console.log('=== smoke.mjs ===');
  const smoke = spawnSync(process.execPath, [join(here, 'smoke.mjs')], { stdio: 'inherit', env: process.env });
  if (smoke.status !== 0) failures.push(`smoke.mjs exited with ${smoke.status ?? smoke.signal}`);
}

// Spec files: *.mjs directly in specs/, run in name order. A leading underscore marks a file that is
// not a spec (a shared fixture or a disabled spec).
const specs = readdirSync(specsDir)
  .filter(f => f.endsWith('.mjs') && !f.startsWith('_'))
  .filter(f => filters.length === 0 || filters.some(x => f.includes(x)))
  .sort();

if (filters.length && specs.length === 0) failures.push(`no spec matched ${JSON.stringify(filters)}`);

if (specs.length) {
  let chromium;
  try {
    ({ chromium } = await import('playwright'));
  } catch {
    ({ chromium } = await import('playwright-core'));
  }

  const browser = await chromium.launch(channel ? { channel, headless: true } : { headless: true });

  // Boot once so the WASM runtime is cached before the first spec measures anything.
  {
    const warm = await browser.newPage();
    await warm.goto(BASE + '/', { waitUntil: 'domcontentloaded' }).catch(() => {});
    await warm.waitForSelector('.apexcharts-canvas svg', { timeout: 60000 }).catch(() => {});
    await warm.close();
  }

  for (const file of specs) {
    console.log(`\n=== specs/${file} ===`);
    // Each spec gets its own context, so cookies, storage and listeners never leak between specs.
    const context = await browser.newContext({ viewport: { width: 1400, height: 1000 } });
    const page = await context.newPage();
    const started = Date.now();
    let timer;
    try {
      const mod = await import(pathToFileURL(join(specsDir, file)).href);
      if (typeof mod.default !== 'function') throw new Error('spec has no default export function');
      const timeout = new Promise((_, reject) => {
        timer = setTimeout(() => reject(new Error(`timed out after ${SPEC_TIMEOUT_MS} ms`)), SPEC_TIMEOUT_MS);
      });
      await Promise.race([mod.default(page, BASE), timeout]);
      console.log(`PASS specs/${file} (${Date.now() - started} ms)`);
    } catch (e) {
      console.error(`FAIL specs/${file}: ${e?.stack || e}`);
      failures.push(`specs/${file}: ${e?.message || e}`);
    } finally {
      clearTimeout(timer);
      await context.close().catch(() => {});
    }
  }

  await browser.close();
}

if (failures.length) {
  console.error('\nE2E FAILED:\n' + failures.map(f => '  - ' + f).join('\n'));
  process.exit(1);
}
console.log(`\nE2E passed: smoke ${process.env.SKIP_SMOKE === '1' ? 'skipped' : 'ok'}, ${specs.length} spec(s) ok.`);
