# Lab03

## Overview

A set of **two JavaScript tasks** focusing on **asynchronous programming** in JavaScript: **Promise chains** for sequential data processing and **`async`/`await`** for simulated API calls with error handling.

---

## Files

```
Lab03/
├── index.html
├── Lab 3 ES.txt
├── Task01.js
└── Task02.js
```

---

## Task Details

### Task01 — Promise Chain (`Task01.js`)

Simulates a product ordering pipeline using a `products` array:

```
getProducts() → filter available → calculate total → apply discount
```

| Step | Operation | Technique |
|------|-----------|-----------|
| 1 | Get all products | `new Promise` + `setTimeout` (simulates API delay) |
| 2 | Filter available products (stock > 0) | `.then()` + `filter()` |
| 3 | Calculate total price | `.then()` + `reduce()` |
| 4 | Apply 10% discount (reject if total < 5000) | `.then()` + `new Promise(resolve, reject)` |
| — | Error handling | `.catch()` |

- Full **Promise chain** with chained `.then()` calls.
- Demonstrates `resolve` and `reject` for conditional processing.

---

### Task02 — Async/Await with API Simulation (`Task02.js`)

Simulates fetching user and post data from an API:

| Function | Purpose |
|----------|---------|
| `fetchData()` | Returns `Users` and `Posts` arrays after a simulated delay (`setTimeout` + `Promise`) |
| `searchUserById(data, userId)` | Finds a user by ID, rejects if not found |
| `searchPostById(data, userId, postId)` | Finds a post by ID belonging to a user, rejects if not found or doesn't belong |
| `showUserPost(userId, postId)` | **`async`** function orchestrating the full flow with `await` |

- Uses `async`/`await` to write asynchronous code in a synchronous style.
- Error handling via `reject()` in Promises and `.catch()` on the caller side.
- Validates both user existence and post ownership.

---

## Key Concepts Demonstrated

- **Promises** — `new Promise(resolve, reject)`, chained `.then()`, `.catch()`
- **Async/Await** — `async function`, `await` for sequential async operations
- **Simulated API calls** — `setTimeout` wrapped in Promises
- **Error handling** — `reject()` for validation errors, `.catch()` for error propagation
- **Data pipelines** — chained transformations (filter → reduce → validate)

---
