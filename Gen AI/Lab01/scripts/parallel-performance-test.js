"use strict";

const { performance } = require("perf_hooks");
const { quickSortIterative } = require("../src/quicksort");
const { parallelQuickSort } = require("../src/parallel-quicksort");

const SIZES = [10000, 50000, 100000];
const RUNS_PER_SIZE = 5;

function generateRandomArray(size, min = -1000000, max = 1000000) {
    const out = new Array(size);
    for (let i = 0; i < size; i += 1) {
        out[i] = Math.floor(Math.random() * (max - min + 1)) + min;
    }
    return out;
}

async function measureTimeMsAsync(fn) {
    const start = performance.now();
    await fn();
    return performance.now() - start;
}

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

async function benchmarkSize(size, runs) {
    let standardTotal = 0;
    let parallelTotal = 0;

    for (let run = 1; run <= runs; run += 1) {
        const input = generateRandomArray(size);

        let standardResult;
        const standardTime = await measureTimeMsAsync(async () => {
            standardResult = quickSortIterative(input, {
                pivotStrategy: "median3",
                smallPartitionThreshold: 16,
            });
        });

        let parallelResult;
        const parallelTime = await measureTimeMsAsync(async () => {
            parallelResult = await parallelQuickSort(input, {
                minParallelSize: 20000,
                minChunkSize: 5000,
            });
        });

        assertEqualArrays(standardResult, parallelResult, `size=${size}, run=${run}`);

        standardTotal += standardTime;
        parallelTotal += parallelTime;
    }

    const standardAvgMs = standardTotal / runs;
    const parallelAvgMs = parallelTotal / runs;

    return {
        size,
        runs,
        standardAvgMs,
        parallelAvgMs,
        faster: parallelAvgMs < standardAvgMs ? "Parallel QuickSort" : "Standard QuickSort",
        relativeSpeed:
            parallelAvgMs < standardAvgMs
                ? standardAvgMs / parallelAvgMs
                : parallelAvgMs / standardAvgMs,
    };
}

function printResults(results) {
    const table = results.map((result) => ({
        Size: result.size,
        Runs: result.runs,
        "Standard Avg (ms)": result.standardAvgMs.toFixed(2),
        "Parallel Avg (ms)": result.parallelAvgMs.toFixed(2),
        Faster: result.faster,
        "Relative Speed": `${result.relativeSpeed.toFixed(2)}x`,
    }));

    console.log("\nParallel QuickSort Benchmark");
    console.log("----------------------------");
    console.table(table);

    const parallelWins = results.filter(
        (result) => result.faster === "Parallel QuickSort"
    ).length;
    const standardWins = results.length - parallelWins;

    console.log("Explanation:");
    if (parallelWins > standardWins) {
        console.log(
            "- Parallel QuickSort performed better on more sizes because independent chunks were sorted concurrently across CPU cores."
        );
    } else {
        console.log(
            "- Standard QuickSort performed better on more sizes in this run, likely due to worker startup and merge overhead."
        );
    }

    console.log("- Parallelization helps most for large arrays where CPU work dominates thread management cost.");
    console.log("- Limitations: worker creation overhead, memory copying between threads, and merge-phase cost.");
}

async function main() {
    const results = [];
    for (const size of SIZES) {
        const result = await benchmarkSize(size, RUNS_PER_SIZE);
        results.push(result);
    }

    printResults(results);
}

main().catch((error) => {
    console.error("Benchmark failed:", error.message);
    process.exitCode = 1;
});
