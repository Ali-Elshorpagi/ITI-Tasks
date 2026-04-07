"use strict";

const { performance } = require("perf_hooks");
const { quickSortIterative } = require("../src/quicksort");

const SIZES = [100, 1000, 10000];
const RUNS_PER_SIZE = 20;

/**
 * Generate an array of random integers in [min, max].
 */
function generateRandomArray(size, min = -100000, max = 100000) {
    const out = new Array(size);
    for (let i = 0; i < size; i += 1) {
        out[i] = Math.floor(Math.random() * (max - min + 1)) + min;
    }
    return out;
}

/**
 * Measures execution time (ms) for a function call.
 */
function measureTimeMs(fn) {
    const start = performance.now();
    fn();
    return performance.now() - start;
}

/**
 * Verify both sort outputs are identical.
 */
function assertEqualArrays(a, b, context) {
    if (a.length !== b.length) {
        throw new Error(`${context}: output lengths differ.`);
    }

    for (let i = 0; i < a.length; i += 1) {
        if (a[i] !== b[i]) {
            throw new Error(`${context}: mismatch at index ${i}.`);
        }
    }
}

/**
 * Run benchmark for one array size, averaging over multiple runs.
 */
function benchmarkSize(size, runs) {
    let quickSortTotal = 0;
    let nativeSortTotal = 0;

    for (let run = 1; run <= runs; run += 1) {
        const base = generateRandomArray(size);

        const quickTime = measureTimeMs(() => {
            // quickSortIterative returns a sorted copy by default.
            const quickSorted = quickSortIterative(base, {
                pivotStrategy: "median3",
                smallPartitionThreshold: 16,
            });

            // Native sort mutates; use a copy to keep fairness.
            const nativeSorted = base.slice().sort((a, b) => a - b);
            assertEqualArrays(quickSorted, nativeSorted, `size=${size}, run=${run}`);
        });

        const nativeTime = measureTimeMs(() => {
            base.slice().sort((a, b) => a - b);
        });

        quickSortTotal += quickTime;
        nativeSortTotal += nativeTime;
    }

    const quickAvg = quickSortTotal / runs;
    const nativeAvg = nativeSortTotal / runs;

    return {
        size,
        runs,
        quickAvgMs: quickAvg,
        nativeAvgMs: nativeAvg,
        faster: quickAvg < nativeAvg ? "QuickSort" : "Array.sort",
        speedup:
            quickAvg < nativeAvg
                ? nativeAvg / quickAvg
                : quickAvg / nativeAvg,
    };
}

function printResults(results) {
    const table = results.map((r) => ({
        Size: r.size,
        Runs: r.runs,
        "QuickSort Avg (ms)": r.quickAvgMs.toFixed(3),
        "Array.sort Avg (ms)": r.nativeAvgMs.toFixed(3),
        Faster: r.faster,
        "Relative Speed": `${r.speedup.toFixed(2)}x`,
    }));

    console.log("\nQuickSort vs Array.sort Performance Benchmark");
    console.log("------------------------------------------------");
    console.table(table);

    const quickWins = results.filter((r) => r.faster === "QuickSort").length;
    const nativeWins = results.filter((r) => r.faster === "Array.sort").length;

    console.log("Summary:");
    if (quickWins > nativeWins) {
        console.log("- QuickSort won in more sizes for this run.");
    } else if (nativeWins > quickWins) {
        console.log("- Array.sort won in more sizes for this run.");
    } else {
        console.log("- Tie: each method won in an equal number of sizes.");
    }

    console.log("Why this usually happens:");
    console.log("- Array.sort is engine-optimized (native/runtime-level optimizations).");
    console.log("- Custom QuickSort has JavaScript-level overhead (calls, swaps, bounds checks).");
    console.log("- On some datasets/sizes, custom QuickSort can still be competitive.");
}

function main() {
    const results = SIZES.map((size) => benchmarkSize(size, RUNS_PER_SIZE));
    printResults(results);
}

main();
