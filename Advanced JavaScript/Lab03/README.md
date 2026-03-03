# Lab03

## Overview

This lab emphasizes **Asynchronous JavaScript**, focusing heavily on using the `Fetch API` and Promises. It features a sequence of dependent API requests rendering related views, alongside exploring a comprehensive modular approach to a DOM-heavy CRUD dashboard.

---

## Files

```text
Lab03/
├── Part01/
│   ├── index.html
│   └── script.js
└── Part02/
    ├── data/
    ├── index.html
    ├── scripts/
    │   ├── add.js
    │   ├── app.js
    │   ├── delete.js
    │   ├── edit.js
    │   ├── pagination.js
    │   ├── search.js
    │   ├── sort.js
    │   └── table.js
    └── styles.css
```

---

## Tasks / Features

### Part01: Asynchronous Data Fetching (REST Countries)
- **Primary Request**: Fetches the payload for a country entered into a search box using `https://restcountries.com/v2/name/{country}`.
- **Dependent Request**: Once the country resolves, reads its `borders` property and sequentially triggers another `fetch` to load the neighboring country using `https://restcountries.com/v2/alpha/{neighbor}`.
- **DOM Rendering**: Maps JSON endpoints to DOM elements (flags, names, regions, populations, currencies).
- **Error Handling**: Implements `.catch()` routines in the Promise chain to gracefully log network anomalies.

### Part02: Modular Application (CRUD Operations)
- **Application Structure**: Rather than maintaining a global state in single monolithic file, behaviors are decoupled into isolated modules managing specific functional boundaries:
  - `add.js` / `delete.js` / `edit.js`: The CRUD mutation modules.
  - `table.js`: Handling the core layout generation of records.
  - `search.js` & `sort.js`: Data querying algorithms connected to the DOM.
  - `pagination.js`: Limits DOM nodes visually by implementing pages.
  - `app.js`: High-level orchestrator.
- **ES6 Modules**: Demonstrates the use of ECMAScript modules (import/export) on the web.

---

## Key Concepts Demonstrated

- **Fetch API & Promises** — Chaining promises, `then()`, handling subsequent requests dynamically based on preceding responses.
- **DOM Manipulation** — Creating interactive applications through dynamic HTML generation.
- **Scalable Architecture** — Dividing UI tasks into manageable structural JS files.
- **Error Propagation** — Correctly dealing with rejected Promises and displaying graceful fallbacks.

---
