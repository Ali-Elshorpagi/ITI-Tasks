# Lab03

## Overview

A conceptual lab that explores **Searching Algorithms**, focusing primarily on minimizing target identification cycles inside systematically sorted array vectors through logical splitting strategies.

---

## Files

```text
Lab03/
├── headers/
├── sources/
├── binary_search.cpp
└── main.cpp
```

---

## Tasks / Features

### Binary Search Array Techniques (`binary_search.cpp`)
- Evaluates locating specific `int` values bounded in `std::vector<int>`.
- **Iterative Implementation**: Traces bounds manipulating `left` and `right` indices continuously utilizing a steady `while` loop until bounding failure or midmatch resolution.
- **Recursive Implementation**: Achieves matching results via algorithmic function callbacks, narrowing indices down logically through stack accumulation.
- Calculates pivot pointers cleanly utilizing bitwise operations (`left + ((right - left) >> 1)`) replacing standard summation division logic to avoid `int` memory overflow.

---

## Key Concepts Demonstrated

- **Algorithm Performance Metrics** — Noting Best-Case `O(1)` against Worst-Case temporal complexities `O(log(N))`.
- **Space Management** — Evaluating recursion depth stacking space over pure looping references.
- **Bitwise Shifting** — Replacing structural division `/ 2` cleanly invoking shift right operator `>> 1` representing base-2 shifts.
- **Divide and Conquer** — Discarding effectively half the iteration space progressively inside ordered sequences.

---
