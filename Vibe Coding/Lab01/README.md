# Lab01 - QuickSort Project

## Lab Description
This lab focuses on implementing and evaluating a production-style **QuickSort** solution using JavaScript. It combines algorithm design, UI integration, testing, performance benchmarking, and API exposure to practice end-to-end development.

## Main Objectives
- Build a robust QuickSort implementation with iterative optimization.
- Validate and process user input safely in a browser interface.
- Compare custom algorithm performance against native sorting.
- Add test coverage and benchmark tooling.
- Expose sorting functionality through a simple REST API.

## 1. Project Overview

This project demonstrates a production-style QuickSort implementation in JavaScript with:

- A browser-based UI for sorting comma-separated numeric input
- Input validation and user-friendly error feedback
- An optimized iterative QuickSort approach for large arrays
- Automated unit tests with Jest
- A benchmark tool to compare QuickSort against JavaScript's built-in `Array.sort()`

The goal is to combine algorithm fundamentals with practical engineering practices such as validation, maintainability, debugging workflow, and performance measurement.

## 2. How GitHub Copilot Assisted

GitHub Copilot supported the project across key engineering tasks:

### Writing code

- Helped scaffold modular QuickSort logic and UI interaction flow
- Generated clear helper functions for parsing, partitioning, and rendering results
- Assisted in creating consistent project structure across source, tests, docs, and scripts

### Debugging

- Introduced and analyzed a controlled bug in partition logic to validate understanding
- Helped trace incorrect output step-by-step and identify root cause in loop bounds
- Guided patching with a corrected partition implementation and verification pass

### Optimization

- Suggested iterative QuickSort to reduce recursion depth risk
- Applied median-of-three pivot selection to reduce poor partition patterns
- Added insertion sort for small partitions to improve practical runtime
- Added benchmark tooling and in-page benchmark tab for performance visibility

### Testing

- Generated Jest tests for edge cases and standard cases
- Added validation coverage for invalid input and pivot strategy configuration
- Included large random dataset tests to verify correctness under load

## 3. QuickSort Implementation Explanation

The implementation uses divide-and-conquer:

1. Select a pivot element (median-of-three in optimized flow)
2. Partition the array around the pivot
3. Sort left and right partitions until subarrays are size 0 or 1

Core implementation choices:

- **Partition strategy:** Lomuto partitioning
- **Pivot strategy:** median-of-three by default for better split quality
- **Large input safety:** iterative QuickSort with explicit stack
- **Small partition optimization:** insertion sort threshold for lower constant overhead
- **Validation:** strict parsing and finite-number checks before sorting

Complexity summary:

- Best case: `O(n log n)`
- Average case: `O(n log n)`
- Worst case: `O(n^2)`
- Space (iterative typical): `O(log n)` average stack usage, `O(n)` worst case

## 4. Performance Comparison with Built-in Sorting

A benchmark script compares:

- Custom QuickSort implementation
- Native `Array.sort((a, b) => a - b)`

Benchmark behavior:

- Generates random arrays of multiple sizes (`100`, `1000`, `10000`)
- Runs multiple iterations per size (default: `20`)
- Measures timing with `performance.now()`
- Reports average execution times in a table

Typical observed trend:

- `Array.sort()` often outperforms custom QuickSort in modern engines because:
  - Native/runtime-level optimizations are highly tuned
  - Custom JavaScript QuickSort has extra overhead from function calls and swaps

## 5. Challenges Faced and Solutions

### Challenge: Incorrect QuickSort output after partition change

- **Problem:** A loop boundary bug (`i <= right`) included the pivot in partition scanning
- **Impact:** Wrong pivot placement and incorrect sorted results
- **Solution:** Corrected loop bound to `i < right` and revalidated behavior

### Challenge: Input quality and malformed user data

- **Problem:** Empty values, non-numeric tokens, and trailing commas
- **Impact:** Potential NaN propagation or unclear failures
- **Solution:** Added strict token-level validation with precise error messages

### Challenge: Large input handling in browser UI

- **Problem:** Potential recursion depth and UI responsiveness concerns
- **Impact:** Risk of stack issues and poor user experience
- **Solution:** Switched to iterative QuickSort and added status feedback for heavy runs

## 6. Key Learnings

- Algorithm correctness depends heavily on partition invariants and boundary handling
- Optimization is not only about big-O; constant factors matter in real applications
- Native platform APIs can outperform custom implementations in many practical scenarios
- Robust input validation significantly improves reliability and user trust
- Combining unit tests and benchmarks gives better confidence than either one alone

## 7. Instructions to Run the Project

## Prerequisites

- Node.js (LTS recommended)
- npm

## Install dependencies

```bash
npm install
```

## Run tests

```bash
npm test
```

## Run performance benchmark (CLI)

```bash
npm run bench
```

## Run parallel performance benchmark (CLI)

```bash
npm run bench:parallel
```

## Run REST API

```bash
npm run start:api
```

Base URL:

- `http://localhost:3000`

### Health check endpoint

- Method: `GET`
- Path: `/api/health`

### QuickSort endpoint

- Method: `POST`
- Path: `/api/sort/quicksort`
- Body fields:
  - `array` (required): numeric array
  - `mode` (optional): `iterative` or `recursive`
  - `pivotStrategy` (optional): `first`, `last`, `median3`, `random`
  - `smallPartitionThreshold` (optional): integer >= 0

Example request body:

```json
{
  "array": [5, 3, 1, 4, 2],
  "mode": "iterative",
  "pivotStrategy": "median3",
  "smallPartitionThreshold": 16
}
```

### Test API using curl

```bash
curl -X POST http://localhost:3000/api/sort/quicksort \
  -H "Content-Type: application/json" \
  -d '{"array":[5,3,1,4,2],"mode":"iterative"}'
```

### Test API using Postman

1. Open Postman and create a new request.
2. Set method to `POST`.
3. Set URL to `http://localhost:3000/api/sort/quicksort`.
4. In the Headers tab, add:
   - Key: `Content-Type`
   - Value: `application/json`
5. In the Body tab, choose `raw` and select `JSON`, then paste:

```json
{
  "array": [5, 3, 1, 4, 2],
  "mode": "iterative"
}
```

6. Click **Send**.

## Run the web app

Open `index.html` in your browser.

Inside the web app:

- **Sorter tab:** enter comma-separated numbers and click **Sort**
- **Benchmark tab:** run in-browser benchmark and view results table

## Project structure

```text
.
├── app.js
├── index.html
├── styles.css
├── visualizer.html
├── visualizer.css
├── visualizer.js
├── browser-sort-worker.js
├── sorting-algorithms.js
├── src/
│   ├── quicksort.js
│   └── parallel-quicksort.js
├── tests/
│   └── quicksort.test.js
├── scripts/
│   ├── performance-test.js
│   └── parallel-performance-test.js
├── api/
│   └── server.js
├── docs/
│   └── quicksort-explanation.md
└── package.json
```
