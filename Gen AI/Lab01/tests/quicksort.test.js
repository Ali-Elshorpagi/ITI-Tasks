"use strict";

const {
    quickSortRecursive,
    quickSortIterative,
} = require("../src/quicksort");

function numericSortedCopy(arr) {
    return arr.slice().sort((a, b) => a - b);
}

function buildRandomArray(size, min = -100000, max = 100000) {
    const out = [];
    for (let i = 0; i < size; i += 1) {
        out.push(Math.floor(Math.random() * (max - min + 1)) + min);
    }
    return out;
}

describe("QuickSort input validation", () => {
    test("throws for null/undefined/non-array", () => {
        expect(() => quickSortRecursive(null)).toThrow(TypeError);
        expect(() => quickSortRecursive(undefined)).toThrow(TypeError);
        expect(() => quickSortRecursive("bad")).toThrow(TypeError);

        expect(() => quickSortIterative(null)).toThrow(TypeError);
        expect(() => quickSortIterative(undefined)).toThrow(TypeError);
        expect(() => quickSortIterative({})).toThrow(TypeError);
    });

    test("throws for invalid pivot strategy", () => {
        expect(() =>
            quickSortRecursive([3, 2, 1], { pivotStrategy: "invalid" })
        ).toThrow(TypeError);
    });
});

describe("QuickSort core behavior", () => {
    const implementations = [
        ["recursive", quickSortRecursive],
        ["iterative", quickSortIterative],
    ];

    test.each(implementations)("%s handles empty array", (_, sortFn) => {
        expect(sortFn([])).toEqual([]);
    });

    test.each(implementations)("%s handles single element", (_, sortFn) => {
        expect(sortFn([42])).toEqual([42]);
    });

    test.each(implementations)("%s handles already sorted array", (_, sortFn) => {
        const input = [1, 2, 3, 4, 5, 6];
        expect(sortFn(input)).toEqual([1, 2, 3, 4, 5, 6]);
    });

    test.each(implementations)("%s handles reverse sorted array", (_, sortFn) => {
        const input = [6, 5, 4, 3, 2, 1];
        expect(sortFn(input)).toEqual([1, 2, 3, 4, 5, 6]);
    });

    test.each(implementations)("%s handles duplicates", (_, sortFn) => {
        const input = [5, 3, 8, 3, 9, 1, 5, 0, 3, 8, 8, 2];
        expect(sortFn(input)).toEqual(numericSortedCopy(input));
    });

    test.each(implementations)("%s is non-mutating by default", (_, sortFn) => {
        const input = [3, 2, 1];
        const original = input.slice();
        const sorted = sortFn(input);

        expect(input).toEqual(original);
        expect(sorted).toEqual([1, 2, 3]);
    });

    test.each(implementations)("%s mutates in inPlace mode", (_, sortFn) => {
        const input = [3, 2, 1];
        const out = sortFn(input, { inPlace: true });

        expect(input).toEqual([1, 2, 3]);
        expect(out).toBe(input);
    });
});

describe("QuickSort pivot strategies", () => {
    const strategies = ["first", "last", "median3", "random"];

    test.each(strategies)("recursive strategy %s works", (strategy) => {
        const input = [9, 4, 7, 1, 3, 6, 2, 8, 5];
        const result = quickSortRecursive(input, { pivotStrategy: strategy });
        expect(result).toEqual(numericSortedCopy(input));
    });

    test.each(strategies)("iterative strategy %s works", (strategy) => {
        const input = [9, 4, 7, 1, 3, 6, 2, 8, 5];
        const result = quickSortIterative(input, { pivotStrategy: strategy });
        expect(result).toEqual(numericSortedCopy(input));
    });
});

describe("QuickSort large random datasets", () => {
    test("recursive sorts large random dataset", () => {
        const input = buildRandomArray(10000);
        const expected = numericSortedCopy(input);
        const actual = quickSortRecursive(input, { pivotStrategy: "median3" });
        expect(actual).toEqual(expected);
    });

    test("iterative sorts large random dataset", () => {
        const input = buildRandomArray(10000);
        const expected = numericSortedCopy(input);
        const actual = quickSortIterative(input, { pivotStrategy: "median3" });
        expect(actual).toEqual(expected);
    });
});
