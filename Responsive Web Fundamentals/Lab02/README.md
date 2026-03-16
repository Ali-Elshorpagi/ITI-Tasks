# Lab02

## Overview

This lab moves away from abstract frameworks and emphasizes native CSS architectural capabilities. The objective explores building scalable independent components strictly via structural CSS grids and responsive units.

---

## Files

```text
Lab02/
├── Task01/
│   ├── index.html
│   ├── scripts.js
│   └── styles.css
├── Task02/
│   ├── index.html
│   └── styles.css
└── assets/
```

---

## Tasks / Features

### 1. Form Builder Layout (Task 01)
- Develops an interactive UI mapping a dual-panel layout (Toolbox vs Canvas).
- **Toolbox (Left)**: Creates structured pill-shaped draggable tiles aligning symbols (`radio`, `checkbox`) side-by-side perfectly via flex layouts.
- **Canvas (Right)**: Implements empty drop zones scaled properly using flexible native units (`%`, `vw/vh`) to occupy the remaining screen real estate dynamically.

### 2. Employee Directory Layout (Task 02)
- Focuses on wrapping a dataset into identically sized `<div class="employee-card">` containers.
- **Grid Bounding**: Scales identical bounding boxes utilizing CSS grid wraps natively resizing the amount of columns visible on the screen based on screen compression.
- **Header Structure**: Positions search bars, titles, and clustered action buttons (`Import`, `Export`, `Add`) seamlessly aligned via flex margins without rigid pixel positioning.

---

## Key Concepts Demonstrated

- **Grid/Flex Container Management** — Distributing items linearly and symmetrically.
- **Scalable Architecture** — Writing layouts without relying heavily on static dimensions (`px`); instead mapping boundaries relatively (`fr`, `rem`, `%`).
- **Responsive Component Structuring** — Formulating components to look flawless independent of whether they lie in a side-collapse window or a full-width container.

---
