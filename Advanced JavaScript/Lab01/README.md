# Lab01

## Overview

This lab focuses on fundamental concepts of Advanced JavaScript, specifically **Closures** and **Higher-Order array methods** (`map`, `filter`, `reduce`). The code demonstrates how to keep private states, create function factories, and apply functional programming principles to an array of `Employee` objects.

---

## Files

```text
Lab01/
├── Index.html
├── Task01.js
└── Task02.js
```

---

## Tasks / Features

### Task01.js (Closures)
Explores the use of Closures for encapsulating state and generating functions dynamically.
- **Function Factories**: Returns a function that gets an employee's name.
- **Counters**: A basic closure counter that increases with every call.
- **DOM Interaction**: Tracks button clicks and changes the background color using an internal `getRandomColor` method.
- **State Trackers**: Closures to add fixed numbers and track the number of employees added to the system.
- **Currying & Factories**: Calculating bonuses using closures and returning custom greetings for specific departments.

### Task02.js (Higher-Order Functions)
Focuses on pure functional methods to manipulate arrays of objects immutably.
- **Map & Filter**: Extracting employee names and filtering employees by salary criteria (e.g., salary > 4500).
- **Reduce**: Accumulating the total salaries of all employees.
- **Pure Functions**: Creating copies of objects (`{...employee}`) before updating values, such as increasing salary without modifying the original source.
- **Currying**: Higher-order functions to apply bonuses, taxes, and filter employees by department recursively.
- **Immutable Operations**: Adding new objects and updating properties on an array using `.map()` to preserve the original dataset.

---

## Key Concepts Demonstrated

- **Closures** — Keeping private state, function factories, event listener states.
- **Pure Functions** — Returning new objects instead of mutating existing state.
- **Higher-Order Data Processing** — Custom execution over sets via `map`, `filter`, `reduce`.
- **Currying Functions** — Composing specific behaviors like `applyBonus` or `filterByDepartment`.

---
