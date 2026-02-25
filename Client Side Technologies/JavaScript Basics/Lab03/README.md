# Lab03

## Overview

A set of **four HTML/JS tasks** covering the **Browser Object Model (BOM)**: `window.open()` for child windows, `setInterval`/`clearInterval` for timed operations, **URL query parameters** with `URLSearchParams`, and a comprehensive **student management** system using arrays, objects, and `document.write()`.

---

## Files

```
Lab03/
├── assets/
│   └── (icons: pass.png, passs.png, ...)
├── styles/
│   └── login_style.css
├── Data.html
├── DataTask03.html
├── error.html
├── Task01.html
├── Task02.html
├── Task03.html
└── Task04.html
```

---

## Task Details

### Task01 — Child Window (`Task01.html` + `Data.html`)

- Opens a **child window** using `window.open()` with specified dimensions (`500×300`).
- `Data.html`: Uses `setInterval` to **type out "Welcome to JS"** character by character (1 character per second), then stops with `clearInterval`.

---

### Task02 — Counter with setInterval (`Task02.html`)

- Starts a **live counter** using `setInterval(fn, 1000)`.
- Each second: increments the counter, updates `document.title`, and appends a styled `<h3>` element with a red border.

---

### Task03 — Login Validation with URL Params (`Task03.html`)

- A **login form** (username + password) submitting via `GET` to itself.
- On page load, reads query parameters using `URLSearchParams`:
  - If `username === 'ali'` and `pass === '123'` → opens **success** child window (`DataTask03.html` — displays "Welcome Ya Ali").
  - Otherwise → opens **error** child window (`error.html` — displays error message).
- Styled with external CSS (`login_style.css`).

---

### Task04 — Student Management System (`Task04.html`)

A comprehensive set of **14 operations** on a `students` array rendered directly to the page via `document.write()`:

| # | Operation | Description |
|---|-----------|-------------|
| 1 | Display All | Renders students as an HTML table |
| 2 | Search by Name | `prompt` + linear search |
| 3 | Count Students | `students.length` |
| 4 | CS Students | `filter()` by course |
| 5 | Youngest Student | Manual min-age search |
| 6 | Sort by Age | `sort()` with comparator |
| 7 | Student Names | Extracts name array |
| 8 | All Above 18? | `every()` check |
| 9 | Same Course | Nested loop for shared courses |
| 10 | Average Age | Sum / length |
| 11 | Top 3 Oldest | `sort()` descending + `slice(0, 3)` |
| 12 | Duplicate Names | Nested loop for name matches |
| 13 | Reverse Array | Manual reverse into new array |
| 14 | Add Email | Appends `name@iti.com` to each object |

---

## Key Concepts Demonstrated

- **BOM** — `window.open()`, `document.title`, `window.location.search`
- **Timers** — `setInterval`, `clearInterval` for animations and counters
- **URL Query Parameters** — `URLSearchParams` for reading GET data
- **Child windows** — opening popups with custom dimensions
- **Array of objects** — search, filter, sort, count, reverse, every, slice
- **DOM output** — `document.write()` / `document.writeln()` for dynamic HTML
- **Input validation** — prompt-based with null/empty/NaN guards

---
