# Lab01

## Overview

An introductory **JavaScript lab** demonstrating the basics of JS: **variable declaration**, **number formatting** (`toFixed`), **user input** via `prompt`, **input validation** (`NaN`, `null`, empty string checks), **type conversion** (`parseFloat`), and **external vs internal script** inclusion.

---

## Files

```
Lab01/
├── Task/
│   ├── Core JS.pdf
│   ├── Lab 1.pdf
│   └── lecture1 Notes.docx
├── External.js
└── index.html
```

---

## Exercise Details

### External Script (`External.js`)

- A simple external script file demonstrating `<script src="...">` inclusion.

---

### Internal Script (`index.html`)

The HTML file contains an internal `<script>` block with three exercises:

#### 1. Number Formatting

- Declares `num1 = 10`, `num2 = 22`.
- Uses `toFixed(6)` to display numbers with 6 decimal places.
- Demonstrates `NaN` result from multiplying a number by `undefined`.

#### 2. Two-Number Addition with Validation

- Prompts the user for two values via `prompt()`.
- Validates input:
  - `null` → user canceled
  - `''` → empty input
  - `isNaN()` → non-numeric input
- Converts and sums using `parseFloat()`.

#### 3. Loop with Input & Formatting

- Uses a `for` loop to prompt for 5 values.
- Each value is validated (cancel, empty, non-numeric).
- Valid entries are displayed with `parseFloat().toFixed(5)`.

---

## Key Concepts Demonstrated

- **Script inclusion** — external (`<script src>`) vs internal (`<script>`) JS
- **Variable declaration** — `var` keyword
- **Number methods** — `toFixed()` for decimal formatting
- **User input** — `prompt()` for interactive input
- **Type conversion** — `parseFloat()`, `parseInt()`
- **Input validation** — `null`, empty string, `isNaN()` checks
- **Loops** — `for` loop with validation per iteration

---
