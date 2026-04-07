"use strict";

(function exposeSortingAlgorithms(globalScope) {
    const SMALL_PARTITION_THRESHOLD = 16;

    /**
     * Insertion sort for tiny ranges. Used as an optimization inside QuickSort.
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

    /**
     * Chooses a pivot using median-of-three to reduce bad partitions.
     */
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

    /**
     * Lomuto partitioning. Moves values <= pivot left of final pivot position.
     */
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

    /**
     * QuickSort (iterative):
     * - Avoids deep recursion by using an explicit stack
     * - Uses insertion sort for tiny partitions
     */
    function quickSort(input) {
        const arr = input.slice();
        if (arr.length <= 1) return arr;

        const stack = [[0, arr.length - 1]];

        while (stack.length > 0) {
            const [left, right] = stack.pop();
            if (left >= right) continue;

            if (right - left + 1 <= SMALL_PARTITION_THRESHOLD) {
                insertionSortRange(arr, left, right);
                continue;
            }

            const pivotIndex = partition(arr, left, right);

            const leftStart = left;
            const leftEnd = pivotIndex - 1;
            const rightStart = pivotIndex + 1;
            const rightEnd = right;

            const leftSize = leftEnd >= leftStart ? leftEnd - leftStart + 1 : 0;
            const rightSize = rightEnd >= rightStart ? rightEnd - rightStart + 1 : 0;

            // Push larger side first so smaller side is processed earlier.
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

    /**
     * MergeSort (recursive):
     * - Divide array into halves
     * - Sort each half
     * - Merge sorted halves
     */
    function mergeSort(input) {
        const arr = input.slice();
        if (arr.length <= 1) return arr;

        function merge(left, right) {
            const merged = [];
            let i = 0;
            let j = 0;

            while (i < left.length && j < right.length) {
                if (left[i] <= right[j]) {
                    merged.push(left[i]);
                    i += 1;
                } else {
                    merged.push(right[j]);
                    j += 1;
                }
            }

            while (i < left.length) {
                merged.push(left[i]);
                i += 1;
            }

            while (j < right.length) {
                merged.push(right[j]);
                j += 1;
            }

            return merged;
        }

        function mergeSortRecursive(values) {
            if (values.length <= 1) return values;
            const mid = Math.floor(values.length / 2);
            const left = mergeSortRecursive(values.slice(0, mid));
            const right = mergeSortRecursive(values.slice(mid));
            return merge(left, right);
        }

        return mergeSortRecursive(arr);
    }

    /**
     * BubbleSort:
     * - Repeatedly swaps adjacent out-of-order elements
     * - Uses swapped-flag optimization to stop early on sorted input
     */
    function bubbleSort(input) {
        const arr = input.slice();
        const n = arr.length;

        for (let i = 0; i < n - 1; i += 1) {
            let swapped = false;

            for (let j = 0; j < n - 1 - i; j += 1) {
                if (arr[j] > arr[j + 1]) {
                    [arr[j], arr[j + 1]] = [arr[j + 1], arr[j]];
                    swapped = true;
                }
            }

            if (!swapped) break;
        }

        return arr;
    }

    /**
     * HeapSort:
     * - Builds a max-heap
     * - Repeatedly moves max element to the end
     */
    function heapSort(input) {
        const arr = input.slice();
        const n = arr.length;

        function heapify(heapSize, root) {
            let largest = root;
            const left = 2 * root + 1;
            const right = 2 * root + 2;

            if (left < heapSize && arr[left] > arr[largest]) {
                largest = left;
            }

            if (right < heapSize && arr[right] > arr[largest]) {
                largest = right;
            }

            if (largest !== root) {
                [arr[root], arr[largest]] = [arr[largest], arr[root]];
                heapify(heapSize, largest);
            }
        }

        for (let i = Math.floor(n / 2) - 1; i >= 0; i -= 1) {
            heapify(n, i);
        }

        for (let i = n - 1; i > 0; i -= 1) {
            [arr[0], arr[i]] = [arr[i], arr[0]];
            heapify(i, 0);
        }

        return arr;
    }

    globalScope.SortingAlgorithms = {
        quickSort,
        mergeSort,
        bubbleSort,
        heapSort,
    };
})(window);
