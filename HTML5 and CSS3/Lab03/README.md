# Lab03

## Overview

This lab serves as an introduction to advanced 2D architectures using **CSS Grid**. It revolves around building a complete storefront dashboard template encompassing sidebars, product grids, and header/footer rails.

---

## Files

- **`Lab03.pdf`**: The official lab problem sheet containing all instructions and requirements.

```text
Lab03/
├── Lab03.pdf
├── assets/
├── index.html
└── styles.css
```

---

## Tasks / Features

### 1. Dashboard Macro Architecture
- Outlines the layout skeleton utilizing primary semantic wrappers: `<header>`, `<aside>`, `<main>`, and `<footer>` natively.
- Evaluates allocating rigid spatial columns to the sidebar, leaving the `main` layout completely unrestricted to stretch efficiently mapping standard Admin Panel aesthetics.

### 2. Product Micro-Grid (`<section class="grid">`)
- Establishes a modular internal bounding grid populating product `<article class="card">` items.
- Defines repeatable columns tracking maximum/minimum dimensions natively avoiding rigid manual width configurations.
- Focuses on wrapping product thumbnails alongside textual tags and active buttons within independent context cards.

---

## Key Concepts Demonstrated

- **Grid Skeletons** — Establishing `display: grid;`.
- **Template Columns** — Passing specific geometric constraints splitting screen real estate (`grid-template-columns`, `grid-template-rows`).
- **Semantic HTML5 Architectures** — Ensuring optimal screen reader compatibility prioritizing `aside` vs `main` hierarchies.
- **Nested Grids** — Nesting secondary grid environments (Product sections) independently decoupled from the macro Page grids boundaries.

---
