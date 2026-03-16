# Lab03

## Overview

This lab acts as a capstone for Responsive Web Design. It integrates all elements (Flexbox, Grid, Media Queries) to build a sophisticated, mobile-friendly **Single Page Portfolio Website**.

---

## Files

```text
Lab03/
├── assets/
├── index.html
└── styles.css
```

---

## Tasks / Features

### 1. Adaptive Navigation
- Uses standard inline links (`nav-menu`) pushed visually mapping wide dimensions.
- **Mobile Menu Interaction**: Employs `@media (max-width: 768px)` inside the stylesheet to entirely hide the standard list, replacing it programmatically with a JavaScript-clickable Hamburger Button to reveal a vertical navigation overlay.

### 2. Scalable Media Layouts
- **Hero Wrapper**: Flexes heavy header texts alongside a floating circular profile image, forcefully stacking them vertically across mobile breakpoints.
- **Case Study Grids**: Integrates alternating asymmetrical cards splitting images and texts 50/50 until screen width reduction forces them into a 100% stacked vertical display.

### 3. Wrapping Elements smoothly
- **Company Logos & Testimonials**: Formats rows of independent boxed assets gracefully colliding downwards into subsequent lines when space diminishes rather than enforcing ugly horizontal scrollbars.

---

## Key Concepts Demonstrated

- **Media Queries (`@media`)** — Implementing dynamic conditional CSS overrides at specific pixel boundaries.
- **DOM Manipulation tied to CSS** — Executing a JavaScript script to attach `active` classes allowing CSS states to transition a mobile menu on and off the screen.
- **Responsive Stacking** — Switching structural commands gracefully from horizontal `row` displays into vertical `column` displays to save screen real estate.
- **Fluid Typography and Paddings** — Adjusting `font-size` maps inherently upon screen collapse (e.g., resizing h1 variants from `48px` to `32px`).

---