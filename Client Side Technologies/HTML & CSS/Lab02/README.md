# Lab02

## Overview

A collection of **five HTML tasks** plus a server page, focusing on **HTML tables** (simple, complex, and nested) and **HTML forms**. Covers hyperlink types (`sms:`, `tel:`, `http://wa.me`), table attributes (`colspan`, `rowspan`, `bgcolor`), nested tables, and a registration form with various input types.

---

## Files

```
Lab02/
├── CSS.pdf
├── server.html
├── task01.html
├── task02.html
├── task03.html
├── task04.html
└── task05.html
```

---

## Task Details

### 1. Hyperlink Types Table (`task01.html`)

- A centered table with four rows demonstrating different link protocols:

| Row | Action | Link Type |
|-----|--------|-----------|
| 1 | Send Message | `sms:` protocol |
| 2 | Make a Call | `tel:` protocol |
| 3 | Send on WhatsApp | `http://wa.me` link |
| 4 | CSS Cheatsheet | `download` attribute for file download |

- Uses `<th>` for headers and `<td>` with centered `<a>` tags.

---

### 2. Complex Table with Colspan & Rowspan (`task02.html`)

- Two side-by-side **"Favorites"** tables displaying data for Bob and Alice.
- Demonstrates:
  - `colspan="2"` and `rowspan="2"` for merged cells.
  - `bgcolor` for table background color (`#FFFFE0` — light yellow).
  - Favorite/Least Favorite categories for Color and Flavor.

---

### 3. Nested Tables (`task03.html`)

- An outer device table with columns: **Device**, **Brand**, **Specifications**.
- The Smartphone row contains a **nested inner table** listing Apple models:
  - iPhone 12 Pro — 256 GB
  - iPhone SE — 128 GB
- Uses colored header rows (`bgcolor="#33cccc"`, `#fddb5d"`).

---

### 4. Timetable Layout (`task04.html`)

- A weekly school **timetable** using advanced table attributes:
  - `colspan="6"` for the title row.
  - `rowspan="6"` for the "Hours" header spanning all time slots.
  - `colspan="5"` for a "Lunch" break row.
  - `rowspan="2" colspan="2"` for a merged "Project" cell (Thu–Fri, last two hours).
- Subjects include Math, Science, and Arts.

---

### 5. Registration Form (`task05.html`)

- A centered **registration form** using a table layout with fields:
  - Full Name, User Name (`<input type="text">`)
  - Email (`<input type="email">`)
  - Phone Number (`<input type="phone">`)
  - Password, Confirm Password (`<input type="password">`)
  - Gender selection (`<input type="radio">` — Male, Female, Prefer not to say)
  - Submit button with highlighted background (`bgcolor="#faac7c"`)
- Form uses `method="post"` and submits to `server.html`.

---

### Server Page (`server.html`)

- A simple placeholder page displaying **"Server Page"** in red text with `teko` font face.
- Used as the `action` target for the registration form.

---

## Key Concepts Demonstrated

- **Hyperlink protocols** — `sms:`, `tel:`, WhatsApp deep links, `download` attribute
- **Table attributes** — `colspan`, `rowspan`, `cellspacing`, `border`, `bgcolor`, `align`
- **Nested tables** — table inside a `<td>` for detailed sub-data
- **Form elements** — `text`, `email`, `phone`, `password`, `radio`, `submit` input types
- **Form submission** — `method="post"`, `action` attribute, `placeholder` text
- **Table-based layout** — using tables for page structure (pre-CSS approach)

---
