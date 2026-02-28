# Lab01

## Overview

A set of **two JavaScript tasks** demonstrating modern ES6+ features: **array higher-order methods** (`map`, `filter`, `find`, `reduce`, `sort`, `forEach`), **spread operator** for copying/merging/updating arrays, and **destructuring with rest** syntax — all working on an array of student objects.

---

## Files

```
Lab01/
├── index.html
├── Task01.js
└── Task02.js
```

---

## Task Details

### Task01 — Array Methods & Spread Operator (`Task01.js`)

Works on a `students` array of objects with `id`, `name`, `grade`, and `city` properties:

| # | Exercise | Method/Technique |
|---|----------|------------------|
| 1 | Extract all student names | `map()` |
| 2 | Filter students with grade ≥ 85 | `filter()` |
| 3 | Find student named "Sara" | `find()` |
| 4 | Calculate average grade | `reduce()` |
| 5 | Sort students by grade (descending) | `sort()` |
| 6 | Print all students | `forEach()` |
| 7 | Deep copy using spread | `for...of` + `{...item}` |
| 8 | Add new student using spread | `[...arr, newItem]` |
| 9 | Merge two arrays using spread | `[...arr1, ...arr2]` |
| 10 | Update Omar's grade without mutation | `map()` + spread `{...st, grade: 95}` |
| 11 | Remove student by id using filter + spread | `[...arr.filter()]` |
| 12 | Destructure first student, keep rest | `[a, ...rest] = arr` |
| 13 | Extract first, rest into another array | `[b, ...rest] = arr` |
| 14 | Skip first two, store third | `[, , c] = arr` |

---

### Task02 — Rest Parameter Function (`Task02.js`)

- Implements `printNames(...students)` using the **rest parameter** (`...`).
- Accepts any number of student objects and prints each `name` using `for...of`.

---

## Key Concepts Demonstrated

- **Higher-order array methods** — `map`, `filter`, `find`, `reduce`, `sort`, `forEach`
- **Spread operator** — array copying, merging, immutable updates
- **Rest parameters** — `...args` in function signatures
- **Destructuring** — array destructuring with skip and rest syntax
- **Immutable patterns** — updating objects without mutating the original array

---
