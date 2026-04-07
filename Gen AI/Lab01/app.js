"use strict";

const inputEl = document.getElementById("numbersInput");
const algorithmSelectEl = document.getElementById("algorithmSelect");
const parallelSortButtonEl = document.getElementById("parallelSortButton");
const parallelBenchmarkButtonEl = document.getElementById("parallelBenchmarkButton");
const testButtonEl = document.getElementById("testButton");
const messageEl = document.getElementById("message");
const resultEl = document.getElementById("result");
const benchmarkButtonEl = document.getElementById("benchmarkButton");
const runsInputEl = document.getElementById("runsInput");
const benchmarkMessageEl = document.getElementById("benchmarkMessage");
const benchmarkTableBodyEl = document.getElementById("benchmarkTableBody");
const benchmarkExplanationEl = document.getElementById("benchmarkExplanation");
const benchmarkChartEl = document.getElementById("benchmarkChart");
const parallelBenchmarkChartEl = document.getElementById("parallelBenchmarkChart");

const LARGE_ARRAY_THRESHOLD = 5000;
const MAX_ALLOWED_ITEMS = 200000;
const BENCHMARK_SIZES = [100, 1000, 10000];
const BUBBLE_SORT_SAFE_LIMIT = 20000;
const PARALLEL_MIN_SIZE = 10000;
const PARALLEL_MIN_CHUNK_SIZE = 4000;
const SORTER_BENCHMARK_SIZES = [10000, 50000, 100000];
const SORTER_BENCHMARK_RUNS = 3;

if (!window.SortingAlgorithms) {
    throw new Error("SortingAlgorithms module is not loaded. Check script order in index.html.");
}

const SORTERS = {
    quickSort: {
        label: "QuickSort",
        sort: window.SortingAlgorithms.quickSort,
    },
    mergeSort: {
        label: "MergeSort",
        sort: window.SortingAlgorithms.mergeSort,
    },
    bubbleSort: {
        label: "BubbleSort",
        sort: window.SortingAlgorithms.bubbleSort,
    },
    heapSort: {
        label: "HeapSort",
        sort: window.SortingAlgorithms.heapSort,
    },
};

/**
 * Parse comma-separated input into an array of numbers.
 * Throws an Error for empty or invalid values so the click handler
 * can display a user-friendly message.
 */
function parseInputToNumberArray(rawValue) {
    if (typeof rawValue !== "string" || rawValue.trim() === "") {
        throw new Error("Please enter at least one number.");
    }

    const parts = rawValue.split(",");
    const numbers = [];

    for (let i = 0; i < parts.length; i += 1) {
        const token = parts[i].trim();

        if (token === "") {
            throw new Error(`Value #${i + 1} is empty. Check extra commas.`);
        }

        const value = Number(token);
        if (!Number.isFinite(value)) {
            throw new Error(`Value #${i + 1} ("${parts[i]}") is not a valid number.`);
        }

        numbers.push(value);
    }

    if (numbers.length > MAX_ALLOWED_ITEMS) {
        throw new Error(
            `Too many values (${numbers.length}). Please use at most ${MAX_ALLOWED_ITEMS} numbers.`
        );
    }

    return numbers;
}

function getSelectedSorter() {
    const selectedKey = algorithmSelectEl.value;
    const selectedSorter = SORTERS[selectedKey];

    if (!selectedSorter) {
        throw new Error("Please select a valid sorting algorithm.");
    }

    return selectedSorter;
}

function setButtonState(button, state, doneTimeoutMs = 700) {
    if (!button) return;

    if (state === "running") {
        button.classList.remove("is-done");
        button.classList.add("is-running");
        return;
    }

    if (state === "done") {
        button.classList.remove("is-running");
        button.classList.add("is-done");
        setTimeout(() => {
            button.classList.remove("is-done");
        }, doneTimeoutMs);
        return;
    }

    button.classList.remove("is-running", "is-done");
}

window.SortingLabAPI = {
    parseInputToNumberArray,
    getSelectedSorter,
    showError,
    showInfo,
    showSuccess,
};

function splitIntoChunks(array, chunkCount) {
    const chunks = [];
    const baseSize = Math.floor(array.length / chunkCount);
    const remainder = array.length % chunkCount;
    let start = 0;

    for (let i = 0; i < chunkCount; i += 1) {
        const size = baseSize + (i < remainder ? 1 : 0);
        const end = start + size;
        chunks.push(array.slice(start, end));
        start = end;
    }

    return chunks;
}

function mergeTwoSortedArrays(left, right) {
    const merged = new Array(left.length + right.length);
    let i = 0;
    let j = 0;
    let k = 0;

    while (i < left.length && j < right.length) {
        if (left[i] <= right[j]) {
            merged[k] = left[i];
            i += 1;
        } else {
            merged[k] = right[j];
            j += 1;
        }
        k += 1;
    }

    while (i < left.length) {
        merged[k] = left[i];
        i += 1;
        k += 1;
    }

    while (j < right.length) {
        merged[k] = right[j];
        j += 1;
        k += 1;
    }

    return merged;
}

function mergeSortedChunks(chunks) {
    if (chunks.length <= 1) return chunks[0] || [];

    let current = chunks;
    while (current.length > 1) {
        const next = [];
        for (let i = 0; i < current.length; i += 2) {
            if (i + 1 < current.length) {
                next.push(mergeTwoSortedArrays(current[i], current[i + 1]));
            } else {
                next.push(current[i]);
            }
        }
        current = next;
    }

    return current[0];
}

function sortChunkWithWorker(chunk) {
    return new Promise((resolve, reject) => {
        const worker = new Worker("browser-sort-worker.js");

        worker.onmessage = (event) => {
            const payload = event.data;
            worker.terminate();

            if (payload && payload.error) {
                reject(new Error(payload.error));
                return;
            }

            resolve(payload.sorted || []);
        };

        worker.onerror = (error) => {
            worker.terminate();
            reject(error);
        };

        worker.postMessage({ chunk });
    });
}

/**
 * Parallel QuickSort in browser:
 * - Splits input into chunks
 * - Sorts chunks concurrently in web workers
 * - Merges sorted chunks in the main thread
 */
async function parallelQuickSortBrowser(input) {
    const arr = input.slice();
    if (arr.length <= 1) return arr;

    if (arr.length < PARALLEL_MIN_SIZE || typeof Worker === "undefined") {
        return window.SortingAlgorithms.quickSort(arr);
    }

    const hardware = Math.max(2, Number(navigator.hardwareConcurrency) || 2);
    const maxWorkers = Math.max(2, hardware - 1);
    const suggestedWorkers = Math.ceil(arr.length / PARALLEL_MIN_CHUNK_SIZE);
    const workerCount = Math.max(2, Math.min(maxWorkers, suggestedWorkers));

    const chunks = splitIntoChunks(arr, workerCount);
    const sortedChunks = await Promise.all(chunks.map((chunk) => sortChunkWithWorker(chunk)));
    return mergeSortedChunks(sortedChunks);
}

async function runParallelVsStandardBenchmark() {
    const rows = [];

    for (const size of SORTER_BENCHMARK_SIZES) {
        let standardTotal = 0;
        let parallelTotal = 0;

        for (let run = 1; run <= SORTER_BENCHMARK_RUNS; run += 1) {
            const sample = generateRandomArray(size);

            const standardMs = measureTimeMs(() => window.SortingAlgorithms.quickSort(sample));

            const startParallel = performance.now();
            const parallelSorted = await parallelQuickSortBrowser(sample);
            const parallelMs = performance.now() - startParallel;

            const standardSorted = window.SortingAlgorithms.quickSort(sample);
            assertSameArray(
                standardSorted,
                parallelSorted,
                `Parallel benchmark mismatch (size=${size}, run=${run})`
            );

            standardTotal += standardMs;
            parallelTotal += parallelMs;
        }

        const standardAvg = standardTotal / SORTER_BENCHMARK_RUNS;
        const parallelAvg = parallelTotal / SORTER_BENCHMARK_RUNS;
        const faster = parallelAvg < standardAvg ? "Parallel QuickSort" : "Standard QuickSort";
        const relative =
            parallelAvg < standardAvg
                ? standardAvg / parallelAvg
                : parallelAvg / standardAvg;

        rows.push({
            size,
            runs: SORTER_BENCHMARK_RUNS,
            standardAvg,
            parallelAvg,
            faster,
            relative,
        });
    }

    return rows;
}

function renderSorterBenchmarkResult(rows) {
    const lines = rows.map(
        (row) =>
            `Size ${row.size} | Standard: ${row.standardAvg.toFixed(2)} ms | ` +
            `Parallel: ${row.parallelAvg.toFixed(2)} ms | Faster: ${row.faster} (${row.relative.toFixed(2)}x)`
    );

    resultEl.innerHTML = lines.join("<br>");

    console.log("Sorter tab benchmark: Parallel QuickSort vs Standard QuickSort");
    console.table(
        rows.map((row) => ({
            Size: row.size,
            Runs: row.runs,
            "Standard Avg (ms)": row.standardAvg.toFixed(2),
            "Parallel Avg (ms)": row.parallelAvg.toFixed(2),
            Faster: row.faster,
            "Relative Speed": `${row.relative.toFixed(2)}x`,
        }))
    );

    renderGroupedBenchmarkChart(parallelBenchmarkChartEl, rows, {
        leftKey: "standardAvg",
        leftLabel: "Standard",
        leftClass: "standard",
        rightKey: "parallelAvg",
        rightLabel: "Parallel",
        rightClass: "parallel",
    });
}

function renderGroupedBenchmarkChart(containerEl, rows, options) {
    if (!containerEl) return;

    if (!rows || rows.length === 0) {
        containerEl.innerHTML = '<p class="placeholder">No chart data available.</p>';
        return;
    }

    const maxValue = Math.max(
        1,
        ...rows.map((row) => Math.max(Number(row[options.leftKey]) || 0, Number(row[options.rightKey]) || 0))
    );

    const scaleValue = (value) => {
        const normalized = Math.max(0, value) / maxValue;
        // Sqrt scaling keeps large values dominant while preserving visibility for small bars.
        return Math.max(10, Math.sqrt(normalized) * 100);
    };

    const groups = rows
        .map((row) => {
            const left = Number(row[options.leftKey]) || 0;
            const right = Number(row[options.rightKey]) || 0;
            const leftHeight = scaleValue(left);
            const rightHeight = scaleValue(right);

            return `
                <div class="chart-group">
                    <div class="chart-size-label">n=${row.size}</div>
                    <div class="chart-bars">
                        <div class="chart-bar ${options.leftClass}" style="height:${leftHeight}%" title="${options.leftLabel}: ${left.toFixed(2)} ms">
                            <span class="chart-value">${left.toFixed(2)}</span>
                        </div>
                        <div class="chart-bar ${options.rightClass}" style="height:${rightHeight}%" title="${options.rightLabel}: ${right.toFixed(2)} ms">
                            <span class="chart-value">${right.toFixed(2)}</span>
                        </div>
                    </div>
                </div>
            `;
        })
        .join("");

    const maxLabel = `${maxValue.toFixed(2)} ms`;
    const midLabel = `${(maxValue / 2).toFixed(2)} ms`;

    containerEl.innerHTML = `
        <div class="chart-meta">Scale: sqrt relative to max (${maxLabel})</div>
        <div class="chart-layout">
            <div class="chart-axis">
                <span>${maxLabel}</span>
                <span>${midLabel}</span>
                <span>0 ms</span>
            </div>
            <div class="chart-plot">
                <div class="chart-grid"></div>
                <div class="chart-groups">${groups}</div>
            </div>
        </div>
        <div class="chart-legend">
            <span><span class="legend-dot ${options.leftClass}"></span>${options.leftLabel}</span>
            <span><span class="legend-dot ${options.rightClass}"></span>${options.rightLabel}</span>
        </div>
    `;
}

function runSelfTests() {
    const testCases = [
        { name: "empty", data: [] },
        { name: "single", data: [7] },
        { name: "simple", data: [3, 1, 2] },
        { name: "duplicates", data: [5, 5, 2, 2, 9, 1] },
        { name: "mixed", data: [10, -4, 0, 7, -1, 3] },
    ];

    const sortersToTest = [
        ["QuickSort", window.SortingAlgorithms.quickSort],
        ["MergeSort", window.SortingAlgorithms.mergeSort],
        ["BubbleSort", window.SortingAlgorithms.bubbleSort],
        ["HeapSort", window.SortingAlgorithms.heapSort],
    ];

    const report = [];
    const started = performance.now();

    for (const [name, sorter] of sortersToTest) {
        const cases = [];
        for (const testCase of testCases) {
            const expected = testCase.data.slice().sort((a, b) => a - b);
            const actual = sorter(testCase.data);
            assertSameArray(actual, expected, `${name} self-test failed (${testCase.name})`);
            cases.push({
                caseName: testCase.name,
                input: JSON.stringify(testCase.data),
                output: JSON.stringify(actual),
                status: "PASS",
            });
        }

        report.push({ algorithm: name, cases });
    }

    return {
        elapsedMs: performance.now() - started,
        report,
        totalAlgorithms: sortersToTest.length,
        totalCases: sortersToTest.length * testCases.length,
    };
}

function renderSelfTestOutput(testResult) {
    const lines = [];
    lines.push(`Self Test Summary: PASS (${testResult.totalAlgorithms} algorithms, ${testResult.totalCases} checks)`);
    lines.push(`Elapsed: ${testResult.elapsedMs.toFixed(2)} ms`);
    lines.push("");

    for (const group of testResult.report) {
        lines.push(`${group.algorithm}:`);
        for (const testCase of group.cases) {
            lines.push(`- ${testCase.status} [${testCase.caseName}] input=${testCase.input} => output=${testCase.output}`);
        }
        lines.push("");
    }

    resultEl.textContent = lines.join("\n");
}

/**
 * Render helpers for success/error feedback.
 */
function showError(message) {
    messageEl.textContent = message;
    messageEl.className = "message error";
    resultEl.textContent = "";
}

function showInfo(message) {
    messageEl.textContent = message;
    messageEl.className = "message info";
}

function showSuccess(sorted, durationMs, algorithmLabel) {
    messageEl.textContent = `${algorithmLabel} sorted ${sorted.length} number(s) in ${durationMs.toFixed(2)} ms.`;
    messageEl.className = "message success";
    resultEl.textContent = sorted.join(", ");
}

/**
 * Create random integer array in [min, max].
 */
function generateRandomArray(size, min = -100000, max = 100000) {
    const output = new Array(size);
    for (let i = 0; i < size; i += 1) {
        output[i] = Math.floor(Math.random() * (max - min + 1)) + min;
    }
    return output;
}

/**
 * Measure function runtime in milliseconds.
 */
function measureTimeMs(fn) {
    const start = performance.now();
    fn();
    return performance.now() - start;
}

/**
 * Ensure benchmark outputs are logically equivalent.
 */
function assertSameArray(a, b, context) {
    if (a.length !== b.length) {
        throw new Error(`${context}: output lengths differ.`);
    }

    for (let i = 0; i < a.length; i += 1) {
        if (a[i] !== b[i]) {
            throw new Error(`${context}: mismatch at index ${i}.`);
        }
    }
}

function showBenchmarkError(message) {
    benchmarkMessageEl.textContent = message;
    benchmarkMessageEl.className = "message error";
}

function showBenchmarkInfo(message) {
    benchmarkMessageEl.textContent = message;
    benchmarkMessageEl.className = "message info";
}

function showBenchmarkSuccess(message) {
    benchmarkMessageEl.textContent = message;
    benchmarkMessageEl.className = "message success";
}

function renderBenchmarkTable(results) {
    const rowsHtml = results
        .map(
            (result) =>
                `<tr>
                    <td>${result.size}</td>
                    <td>${result.runs}</td>
                    <td>${result.quickAvgMs.toFixed(3)}</td>
                    <td>${result.nativeAvgMs.toFixed(3)}</td>
                    <td>${result.faster}</td>
                    <td>${result.relativeSpeed.toFixed(2)}x</td>
                </tr>`
        )
        .join("");

    benchmarkTableBodyEl.innerHTML = rowsHtml;
}

function renderBenchmarkExplanation(results) {
    const quickWins = results.filter((result) => result.faster === "QuickSort").length;
    const nativeWins = results.length - quickWins;

    let winner;
    if (quickWins > nativeWins) {
        winner = "QuickSort performed better in more tested sizes.";
    } else if (nativeWins > quickWins) {
        winner = "Array.sort() performed better in more tested sizes.";
    } else {
        winner = "Both methods performed similarly across tested sizes.";
    }

    benchmarkExplanationEl.textContent =
        `${winner} In most browsers, Array.sort() often wins because JavaScript engines ` +
        "apply low-level optimizations in native code, while custom QuickSort runs fully " +
        "in JavaScript with additional function and swap overhead.";

    renderGroupedBenchmarkChart(benchmarkChartEl, results, {
        leftKey: "quickAvgMs",
        leftLabel: "QuickSort",
        leftClass: "quick",
        rightKey: "nativeAvgMs",
        rightLabel: "Array.sort",
        rightClass: "native",
    });
}

function getRunCount() {
    const parsed = Number(runsInputEl.value);
    if (!Number.isInteger(parsed) || parsed < 1 || parsed > 100) {
        throw new Error("Runs per size must be an integer between 1 and 100.");
    }
    return parsed;
}

function runBenchmarkForSize(size, runs) {
    let quickTotal = 0;
    let nativeTotal = 0;

    for (let run = 1; run <= runs; run += 1) {
        const base = generateRandomArray(size);
        const nativeCopy = base.slice();

        quickTotal += measureTimeMs(() => {
            window.SortingAlgorithms.quickSort(base);
        });

        nativeTotal += measureTimeMs(() => {
            nativeCopy.sort((a, b) => a - b);
        });

        const quickSorted = window.SortingAlgorithms.quickSort(base);
        assertSameArray(quickSorted, nativeCopy, `size=${size}, run=${run}`);
    }

    const quickAvgMs = quickTotal / runs;
    const nativeAvgMs = nativeTotal / runs;

    return {
        size,
        runs,
        quickAvgMs,
        nativeAvgMs,
        faster: quickAvgMs < nativeAvgMs ? "QuickSort" : "Array.sort",
        relativeSpeed:
            quickAvgMs < nativeAvgMs ? nativeAvgMs / quickAvgMs : quickAvgMs / nativeAvgMs,
    };
}

async function runBenchmark() {
    try {
        benchmarkButtonEl.disabled = true;
        const runs = getRunCount();

        showBenchmarkInfo("Running benchmark... this may take a few seconds.");
        benchmarkExplanationEl.textContent = "";
        benchmarkTableBodyEl.innerHTML =
            '<tr><td colspan="6" class="placeholder">Running benchmark...</td></tr>';

        // Let the browser paint status text before heavy computations.
        await new Promise((resolve) => requestAnimationFrame(resolve));

        const results = BENCHMARK_SIZES.map((size) => runBenchmarkForSize(size, runs));

        renderBenchmarkTable(results);
        renderBenchmarkExplanation(results);
        showBenchmarkSuccess("Benchmark completed successfully.");

        // Keep a readable benchmark output in the console as well.
        console.log("QuickSort vs Array.sort benchmark results:");
        console.table(
            results.map((result) => ({
                Size: result.size,
                Runs: result.runs,
                "QuickSort Avg (ms)": result.quickAvgMs.toFixed(3),
                "Array.sort Avg (ms)": result.nativeAvgMs.toFixed(3),
                Faster: result.faster,
                "Relative Speed": `${result.relativeSpeed.toFixed(2)}x`,
            }))
        );
    } catch (error) {
        showBenchmarkError(error.message || "Unexpected benchmark error.");
        benchmarkTableBodyEl.innerHTML =
            '<tr><td colspan="6" class="placeholder">No benchmark results available.</td></tr>';
        benchmarkExplanationEl.textContent = "";
        renderGroupedBenchmarkChart(benchmarkChartEl, [], {
            leftKey: "quickAvgMs",
            leftLabel: "QuickSort",
            leftClass: "quick",
            rightKey: "nativeAvgMs",
            rightLabel: "Array.sort",
            rightClass: "native",
        });
    } finally {
        benchmarkButtonEl.disabled = false;
    }
}

parallelSortButtonEl.addEventListener("click", async () => {
    try {
        parallelSortButtonEl.disabled = true;
        setButtonState(parallelSortButtonEl, "running");

        const numbers = parseInputToNumberArray(inputEl.value);
        showInfo("Running parallel QuickSort with web workers...");
        await new Promise((resolve) => requestAnimationFrame(resolve));

        const start = performance.now();
        const sorted = await parallelQuickSortBrowser(numbers);
        const duration = performance.now() - start;

        if (window.SortingVisualizerAPI && typeof window.SortingVisualizerAPI.animateToValues === "function") {
            await window.SortingVisualizerAPI.animateToValues(sorted, "Parallel QuickSort");
        }

        showSuccess(sorted, duration, "Parallel QuickSort");
        setButtonState(parallelSortButtonEl, "done");
    } catch (error) {
        showError(error.message || "Unexpected parallel sort error.");
        setButtonState(parallelSortButtonEl, "idle");
    } finally {
        parallelSortButtonEl.disabled = false;
    }
});

testButtonEl.addEventListener("click", () => {
    try {
        testButtonEl.disabled = true;
        setButtonState(testButtonEl, "running");
        const testResult = runSelfTests();
        renderSelfTestOutput(testResult);
        messageEl.textContent = "Self tests passed for QuickSort, MergeSort, BubbleSort, and HeapSort.";
        messageEl.className = "message success";
        if (window.SortingVisualizerAPI && typeof window.SortingVisualizerAPI.flashSuccess === "function") {
            window.SortingVisualizerAPI.flashSuccess();
        }
        setButtonState(testButtonEl, "done");
    } catch (error) {
        showError(error.message || "Self tests failed.");
        setButtonState(testButtonEl, "idle");
    } finally {
        testButtonEl.disabled = false;
    }
});

parallelBenchmarkButtonEl.addEventListener("click", async () => {
    try {
        parallelBenchmarkButtonEl.disabled = true;
        setButtonState(parallelBenchmarkButtonEl, "running");
        showInfo("Running sorter benchmark: Parallel QuickSort vs Standard QuickSort...");
        resultEl.textContent = "Preparing benchmark runs...";

        await new Promise((resolve) => requestAnimationFrame(resolve));
        const rows = await runParallelVsStandardBenchmark();

        renderSorterBenchmarkResult(rows);
        messageEl.textContent =
            `Benchmark completed (${SORTER_BENCHMARK_RUNS} runs per size). See details below and in console.`;
        messageEl.className = "message success";
        if (window.SortingVisualizerAPI && typeof window.SortingVisualizerAPI.setOperation === "function") {
            window.SortingVisualizerAPI.setOperation("Parallel benchmark completed");
        }
        setButtonState(parallelBenchmarkButtonEl, "done");
    } catch (error) {
        showError(error.message || "Sorter benchmark failed.");
        setButtonState(parallelBenchmarkButtonEl, "idle");
    } finally {
        parallelBenchmarkButtonEl.disabled = false;
    }
});

benchmarkButtonEl.addEventListener("click", runBenchmark);
