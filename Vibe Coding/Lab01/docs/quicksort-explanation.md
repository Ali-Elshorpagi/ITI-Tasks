# QuickSort Overview

## 1. Divide-and-Conquer Approach

QuickSort follows divide-and-conquer:

1. Choose a pivot element.
2. Partition the array so values `<= pivot` are on the left and values `> pivot` are on the right.
3. Sort left and right partitions independently.

This keeps breaking the problem into smaller subproblems until each partition has 0 or 1 item.

## 2. Partitioning Logic (Lomuto)

In this project, partitioning works as follows:

1. Select pivot index by strategy (`first`, `last`, `median3`, `random`).
2. Move pivot to the end.
3. Scan from `left` to `right - 1`.
4. Keep a `storeIndex` where the next smaller/equal element should go.
5. Swap pivot into `storeIndex` at the end.

After partitioning, the pivot is in its final sorted position.

## 3. Recursive Flow and Base Cases

Recursive quicksort:

- Base case: if `left >= right`, the segment is already sorted.
- Recurrence: partition then sort left and right ranges.

Optimization used here:

- Recurse into the smaller partition first, then continue sorting the larger partition in a loop.
- This reduces stack growth and helps avoid deep recursion in many cases.

## 4. Iterative Version

The iterative version replaces recursion with an explicit stack of index ranges:

1. Push full range.
2. Pop a range.
3. Partition it.
4. Push resulting subranges.

This avoids call-stack overflow risk on large arrays.

## 5. Additional Optimizations

- Median-of-three pivot reduces chance of worst-case splits on partially sorted data.
- Random pivot can reduce adversarial worst-case patterns.
- Insertion sort for very small partitions improves practical performance due to low constant overhead.

## 6. Time and Space Complexity

QuickSort:

- Best time: O(n log n)
- Average time: O(n log n)
- Worst time: O(n^2)
- Space:
  - Recursive optimized: typically O(log n), worst O(n)
  - Iterative stack: typically O(log n), worst O(n)

## 7. Sorting Algorithm Comparison

| Algorithm | Best Time | Average Time | Worst Time | Extra Space | Stable | Typical Use Cases |
|---|---:|---:|---:|---:|---|---|
| QuickSort | O(n log n) | O(n log n) | O(n^2) | O(log n) typical | No | Fast in-memory general-purpose sorting |
| MergeSort | O(n log n) | O(n log n) | O(n log n) | O(n) | Yes | Stable sorting, linked lists, external sorting |
| HeapSort | O(n log n) | O(n log n) | O(n log n) | O(1) | No | Predictable worst-case with tight memory |
| JS built-in sort | Engine-dependent (usually O(n log n)) | Engine-dependent | Engine-dependent | Engine-dependent | Stable in modern JS engines | Most application-level sorting tasks |

Notes:

- Built-in JavaScript sort is usually highly optimized and should be preferred unless custom control is required.
- QuickSort is not stable by default.
