"use strict";

/**
 * Default comparator for numbers/strings.
 * Returns negative if a < b, 0 if equal, positive if a > b.
 */
function defaultCompare(a, b) {
    if (a === b) return 0;
    return a < b ? -1 : 1;
}

/**
 * Validates public API input/options.
 */
function validateInput(array, options) {
    if (!Array.isArray(array)) {
        throw new TypeError("Input must be an array.");
    }

    if (options == null || typeof options !== "object") {
        throw new TypeError("Options must be an object.");
    }

    const allowedStrategies = new Set(["first", "last", "median3", "random"]);

    if (!allowedStrategies.has(options.pivotStrategy)) {
        throw new TypeError(
            `pivotStrategy must be one of: ${Array.from(allowedStrategies).join(", ")}`
        );
    }

    if (typeof options.compareFn !== "function") {
        throw new TypeError("compareFn must be a function.");
    }

    if (
        !Number.isInteger(options.smallPartitionThreshold) ||
        options.smallPartitionThreshold < 0
    ) {
        throw new TypeError("smallPartitionThreshold must be a non-negative integer.");
    }
}

/**
 * Pick a pivot index using the requested strategy.
 */
function choosePivotIndex(arr, left, right, strategy, compareFn) {
    switch (strategy) {
        case "first":
            return left;
        case "last":
            return right;
        case "random":
            return left + Math.floor(Math.random() * (right - left + 1));
        case "median3": {
            const mid = left + ((right - left) >> 1);
            const a = arr[left];
            const b = arr[mid];
            const c = arr[right];

            // Return index of median value among left, mid, right.
            if (compareFn(a, b) < 0) {
                if (compareFn(b, c) < 0) return mid;
                return compareFn(a, c) < 0 ? right : left;
            }

            if (compareFn(a, c) < 0) return left;
            return compareFn(b, c) < 0 ? right : mid;
        }
        default:
            return right;
    }
}

/**
 * Insertion sort for tiny ranges; usually faster than QuickSort there.
 */
function insertionSortRange(arr, left, right, compareFn) {
    for (let i = left + 1; i <= right; i += 1) {
        const current = arr[i];
        let j = i - 1;

        while (j >= left && compareFn(arr[j], current) > 0) {
            arr[j + 1] = arr[j];
            j -= 1;
        }

        arr[j + 1] = current;
    }
}

/**
 * Lomuto partition around selected pivot.
 */
function partition(arr, left, right, strategy, compareFn) {
    const pivotIndex = choosePivotIndex(arr, left, right, strategy, compareFn);
    [arr[pivotIndex], arr[right]] = [arr[right], arr[pivotIndex]];

    const pivotValue = arr[right];
    let storeIndex = left;

    for (let i = left; i < right; i += 1) {
        if (compareFn(arr[i], pivotValue) <= 0) {
            [arr[i], arr[storeIndex]] = [arr[storeIndex], arr[i]];
            storeIndex += 1;
        }
    }

    [arr[storeIndex], arr[right]] = [arr[right], arr[storeIndex]];
    return storeIndex;
}

/**
 * Recursive quicksort with reduced stack depth:
 * recurse into smaller partition, loop on larger partition.
 */
function quickSortRecursiveRange(
    arr,
    left,
    right,
    strategy,
    compareFn,
    smallPartitionThreshold
) {
    while (left < right) {
        if (right - left + 1 <= smallPartitionThreshold) {
            insertionSortRange(arr, left, right, compareFn);
            return;
        }

        const pivot = partition(arr, left, right, strategy, compareFn);

        if (pivot - left < right - pivot) {
            quickSortRecursiveRange(
                arr,
                left,
                pivot - 1,
                strategy,
                compareFn,
                smallPartitionThreshold
            );
            left = pivot + 1;
        } else {
            quickSortRecursiveRange(
                arr,
                pivot + 1,
                right,
                strategy,
                compareFn,
                smallPartitionThreshold
            );
            right = pivot - 1;
        }
    }
}

/**
 * Public recursive API.
 * By default returns a sorted copy; set inPlace=true to mutate input.
 */
function quickSortRecursive(array, userOptions = {}) {
    const options = {
        pivotStrategy: "median3",
        compareFn: defaultCompare,
        smallPartitionThreshold: 16,
        inPlace: false,
        ...userOptions,
    };

    validateInput(array, options);

    const arr = options.inPlace ? array : array.slice();
    if (arr.length <= 1) return arr;

    quickSortRecursiveRange(
        arr,
        0,
        arr.length - 1,
        options.pivotStrategy,
        options.compareFn,
        options.smallPartitionThreshold
    );

    return arr;
}

/**
 * Public iterative API using explicit stack.
 * Avoids recursion and potential deep call stacks.
 */
function quickSortIterative(array, userOptions = {}) {
    const options = {
        pivotStrategy: "median3",
        compareFn: defaultCompare,
        smallPartitionThreshold: 16,
        inPlace: false,
        ...userOptions,
    };

    validateInput(array, options);

    const arr = options.inPlace ? array : array.slice();
    if (arr.length <= 1) return arr;

    const stack = [[0, arr.length - 1]];

    while (stack.length > 0) {
        const [left, right] = stack.pop();
        if (left >= right) continue;

        if (right - left + 1 <= options.smallPartitionThreshold) {
            insertionSortRange(arr, left, right, options.compareFn);
            continue;
        }

        const pivot = partition(
            arr,
            left,
            right,
            options.pivotStrategy,
            options.compareFn
        );

        const leftStart = left;
        const leftEnd = pivot - 1;
        const rightStart = pivot + 1;
        const rightEnd = right;

        const leftSize = leftEnd >= leftStart ? leftEnd - leftStart + 1 : 0;
        const rightSize = rightEnd >= rightStart ? rightEnd - rightStart + 1 : 0;

        // Push larger segment first so smaller one is processed next (keeps stack smaller).
        if (leftSize > rightSize) {
            if (leftStart < leftEnd) stack.push([leftStart, leftEnd]);
            if (rightStart < rightEnd) stack.push([rightStart, rightEnd]);
        } else {
            if (rightStart < rightEnd) stack.push([rightStart, rightEnd]);
            if (leftStart < leftEnd) stack.push([leftStart, leftEnd]);
        }
    }

    return arr;
}

module.exports = {
    quickSortRecursive,
    quickSortIterative,
    defaultCompare,
};
