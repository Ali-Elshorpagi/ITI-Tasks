# Lab02

## Overview

A set of **three HTML/JS tasks** focusing on **array manipulation** and **string processing**: removing duplicates, sorting, filtering, finding max/min, computing sum and product (including `eval()`), capitalizing first letters, and finding the longest word in a sentence.

---

## Files

```
Lab02/
├── Task01.html
├── Task02.html
└── Task03.html
```

---

## Task Details

### Task01 — Array Operations (`Task01.html`)

Works on an array of numbers with multiple exercises:

| Exercise | Description | Technique |
|----------|-------------|-----------|
| Remove duplicates | Filters unique values from an array | `forEach` + `includes()` |
| Sort ascending | Sorts the unique array numerically | `sort()` with comparator `a - b` |
| Filter > 50 | Keeps only numbers greater than 50 | Custom function + `filter()` |
| Max & Min | Finds largest and smallest values | Manual loop, `sort`, and `Math.max/min(...arr)` |
| Sum & Product | Computes sum and product of `[1..10]` | Loop-based `sumAll()` / `mulAll()` |
| Eval sum | Builds an expression string and evaluates it | `join("+")` + `eval()` |

- Input validation applied to each array element (null, empty, NaN checks).
- Multiple approaches demonstrated for each operation.

---

### Task02 — Capitalize First Letters (`Task02.html`)

- Prompts user for a string.
- Splits by spaces, capitalizes the first character of each word using `toUpperCase()` + `slice(1)`.
- Joins back with spaces.
- Example: `"hello world"` → `"Hello World"`.

---

### Task03 — Find Longest Word (`Task03.html`)

- Prompts user for a string.
- Splits by spaces, iterates to find the word with the maximum length.
- Returns the longest word from the input.

---

## Key Concepts Demonstrated

- **Array deduplication** — `includes()` check before pushing
- **Sorting** — numeric comparator function with `sort()`
- **Filtering** — custom filter vs built-in `filter()`
- **Max/Min** — manual loop, sort-based, and `Math.max(...spread)`
- **Sum/Product** — iterative accumulation with validation
- **`eval()`** — evaluating dynamically built expression strings
- **String manipulation** — `split()`, `toUpperCase()`, `slice()`, `join()`

---
