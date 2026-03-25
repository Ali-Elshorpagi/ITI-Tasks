"use strict";

/**
 * Worker-side QuickSort for sorting one chunk.
 * The worker receives an isolated chunk and returns a sorted copy.
 */
function insertionSortRange(arr, left, right) {
    for (let i = left + 1; i <= right; i += 1) {
        const current = arr[i];
        let j = i - 1;

        while (j >= left && arr[j] > current) {
            arr[j + 1] = arr[j];
            j -= 1;
        }

        arr[j + 1] = current;
    }
}

function choosePivotIndex(arr, left, right) {
    const mid = left + ((right - left) >> 1);
    const a = arr[left];
    const b = arr[mid];
    const c = arr[right];

    if (a < b) {
        if (b < c) return mid;
        return a < c ? right : left;
    }

    if (a < c) return left;
    return b < c ? right : mid;
}

function partition(arr, left, right) {
    const pivotIndex = choosePivotIndex(arr, left, right);
    [arr[pivotIndex], arr[right]] = [arr[right], arr[pivotIndex]];

    const pivot = arr[right];
    let storeIndex = left;

    for (let i = left; i < right; i += 1) {
        if (arr[i] <= pivot) {
            [arr[i], arr[storeIndex]] = [arr[storeIndex], arr[i]];
            storeIndex += 1;
        }
    }

    [arr[storeIndex], arr[right]] = [arr[right], arr[storeIndex]];
    return storeIndex;
}

function quickSortChunk(input) {
    const arr = input.slice();
    if (arr.length <= 1) return arr;

    const threshold = 16;
    const stack = [[0, arr.length - 1]];

    while (stack.length > 0) {
        const [left, right] = stack.pop();
        if (left >= right) continue;

        if (right - left + 1 <= threshold) {
            insertionSortRange(arr, left, right);
            continue;
        }

        const pivotIndex = partition(arr, left, right);

        if (left < pivotIndex - 1) stack.push([left, pivotIndex - 1]);
        if (pivotIndex + 1 < right) stack.push([pivotIndex + 1, right]);
    }

    return arr;
}

self.onmessage = (event) => {
    try {
        const chunk = event.data.chunk;
        const sorted = quickSortChunk(chunk);
        self.postMessage({ sorted });
    } catch (error) {
        self.postMessage({ error: error.message || "Worker sort failed." });
    }
};
