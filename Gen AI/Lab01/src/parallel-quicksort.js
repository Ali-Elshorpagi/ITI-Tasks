"use strict";

const os = require("os");
const {
    Worker,
    isMainThread,
    parentPort,
    workerData,
} = require("worker_threads");
const { quickSortIterative } = require("./quicksort");

if (!isMainThread) {
    try {
        // Each worker receives an isolated copy of a chunk, so no shared mutable state exists.
        const sorted = quickSortIterative(workerData.chunk, {
            pivotStrategy: workerData.sortOptions.pivotStrategy,
            smallPartitionThreshold: workerData.sortOptions.smallPartitionThreshold,
            inPlace: false,
        });
        parentPort.postMessage({ sorted });
    } catch (error) {
        parentPort.postMessage({ error: error.message || "Worker sort failed." });
    }
}

function validateParallelInput(array, options) {
    if (!Array.isArray(array)) {
        throw new TypeError("Input must be an array.");
    }

    if (options == null || typeof options !== "object") {
        throw new TypeError("Options must be an object.");
    }

    if (!Number.isInteger(options.maxWorkers) || options.maxWorkers < 1) {
        throw new TypeError("maxWorkers must be an integer >= 1.");
    }

    if (!Number.isInteger(options.minParallelSize) || options.minParallelSize < 2) {
        throw new TypeError("minParallelSize must be an integer >= 2.");
    }

    if (!Number.isInteger(options.minChunkSize) || options.minChunkSize < 2) {
        throw new TypeError("minChunkSize must be an integer >= 2.");
    }

    // Functions cannot be posted to workers via structured clone.
    if (typeof options.sortOptions.compareFn === "function") {
        throw new TypeError(
            "parallelQuickSort does not support custom compareFn because worker data must be serializable."
        );
    }
}

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

function runWorkerSort(chunk, sortOptions) {
    return new Promise((resolve, reject) => {
        const worker = new Worker(__filename, {
            workerData: { chunk, sortOptions },
        });

        worker.on("message", (message) => {
            if (message && message.error) {
                reject(new Error(message.error));
                return;
            }
            resolve(message.sorted);
        });

        worker.on("error", (error) => {
            reject(error);
        });

        worker.on("exit", (code) => {
            if (code !== 0) {
                reject(new Error(`Worker stopped with exit code ${code}.`));
            }
        });
    });
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
    if (chunks.length === 0) return [];
    if (chunks.length === 1) return chunks[0];

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

/**
 * Parallel QuickSort:
 * 1) Split input into disjoint chunks
 * 2) Sort each chunk concurrently in worker threads
 * 3) Merge sorted chunks into one sorted array
 */
async function parallelQuickSort(array, userOptions = {}) {
    const cpuCount = Math.max(1, (os.cpus() || []).length);

    const options = {
        maxWorkers: Math.max(1, cpuCount - 1),
        minParallelSize: 20000,
        minChunkSize: 5000,
        sortOptions: {
            pivotStrategy: "median3",
            smallPartitionThreshold: 16,
        },
        ...userOptions,
    };

    options.sortOptions = {
        pivotStrategy: "median3",
        smallPartitionThreshold: 16,
        ...(userOptions.sortOptions || {}),
    };

    validateParallelInput(array, options);

    if (array.length <= 1) return array.slice();

    // For small arrays, worker startup overhead is usually slower than direct sorting.
    if (array.length < options.minParallelSize || options.maxWorkers === 1) {
        return quickSortIterative(array, {
            ...options.sortOptions,
            inPlace: false,
        });
    }

    const candidateWorkers = Math.ceil(array.length / options.minChunkSize);
    const workerCount = Math.min(options.maxWorkers, candidateWorkers, array.length);

    if (workerCount <= 1) {
        return quickSortIterative(array, {
            ...options.sortOptions,
            inPlace: false,
        });
    }

    const chunks = splitIntoChunks(array, workerCount);
    const sortedChunks = await Promise.all(
        chunks.map((chunk) => runWorkerSort(chunk, options.sortOptions))
    );

    return mergeSortedChunks(sortedChunks);
}

if (isMainThread) {
    module.exports = {
        parallelQuickSort,
    };
}
