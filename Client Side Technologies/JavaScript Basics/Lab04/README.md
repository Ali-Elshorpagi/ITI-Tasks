# Lab04

## Overview

A two-part lab focusing on **DOM manipulation** and **event handling**. **Part 1** covers DOM selectors, element traversal, attribute modification, an image slideshow, a student dropdown, and dynamic element creation. **Part 2** builds an interactive student table with live search and sort functionality.

---

## Files

```
Lab04/
├── Part01/
│   ├── Images/
│   ├── scripts/
│   │   ├── Task01.js
│   │   ├── Task02.js
│   │   ├── Task03.js
│   │   ├── Task04.js
│   │   └── Task05.js
│   ├── demo.html
│   ├── Task03.html
│   ├── Task04.html
│   └── Task05.html
└── Part02/
    ├── index.html
    └── script.js
```

---

## Part 01 — DOM Selectors & Events

### Demo Page (`demo.html`)

A reference HTML page containing various elements (lists, divs, paragraphs, images, tables, forms) used as the target for Task01 and Task02 DOM exercises.

---

### Task01 — DOM Selection (`Task01.js`)

| Exercise | Description | Selector Used |
|----------|-------------|---------------|
| a | Select all images (two ways) | `getElementsByTagName('img')` / `querySelectorAll('img')` |
| b | Select all option elements | `getElementsByTagName('option')` |
| c | Get rows of the second table | `querySelectorAll('table')` → `querySelectorAll('tr')` |
| d | Get elements with class `fontBlue` | `getElementsByClassName('fontBlue')` |

---

### Task02 — DOM Modification (`Task02.js`)

| Exercise | Description | Technique |
|----------|-------------|-----------|
| a | Change first anchor's href and text | `querySelector('a')` → modify `.href`, `.textContent` |
| b | Add pink border to last image | `querySelectorAll('img')` → `.style.border` |
| c | Find all checked checkboxes and alert values | `querySelectorAll('input[type="checkbox"]:checked')` |
| d | Change background of `#example` to pink | `getElementById('example')` → `.style.backgroundColor` |

---

### Task03 — Image Slideshow (`Task03.html` + `Task03.js`)

- Displays an image with **Prev**, **Next**, **Slide Show**, and **Stop** buttons.
- Uses `addEventListener('click', ...)` on each button.
- **Auto slideshow**: `setInterval` cycles through images every 500ms.
- **Stop**: `clearInterval` halts the slideshow.
- Images wrap cyclically (1–7).

---

### Task04 — Student Dropdown (`Task04.html` + `Task04.js`)

- Dynamically populates a `<select>` dropdown with student names using `createElement('option')`.
- On `change` event, displays selected student's info (ID, Name, Age, Course) in a styled `<div>`.
- Uses `filter()` to find the student by ID.

---

### Task05 — Dynamic Number Grid (`Task05.html` + `Task05.js`)

- Generates numbers 1–100 using `setInterval` (one number every 500ms).
- Creates `<p>` elements dynamically with `createElement`.
- Alternating styles: even → red dashed border with rounded corners, odd → blue solid border.

---

## Part 02 — Interactive Student Table

### Search & Sort Table (`index.html` + `script.js`)

- Displays a student table rendered via `innerHTML`.
- **Live search** (`keyup` event) — searches by name first, then by course.
- **Sort dropdown** (`change` event) — sort by Age or Name.
- Functions: `display()`, `searchByName()`, `searchByCrs()`, `sortByAge()`, `sortByName()`.
- Sorting uses `localeCompare()` for alphabetical order.

---

## Key Concepts Demonstrated

- **DOM selectors** — `getElementById`, `getElementsByTagName`, `getElementsByClassName`, `querySelector`, `querySelectorAll`
- **DOM manipulation** — `createElement`, `appendChild`, `innerHTML`, `textContent`, `style`
- **Event listeners** — `click`, `change`, `keyup`, `load` events
- **Timers** — `setInterval` / `clearInterval` for slideshow and animations
- **Dynamic HTML** — building tables, dropdowns, and elements via JavaScript
- **Live search** — real-time filtering with `keyup` event
- **Sorting** — numeric comparator and `localeCompare` for strings

---
