# Lab02

## Overview

A set of **three tasks** demonstrating **ES6 classes**, **inheritance**, **polymorphism**, **private fields** with getters/setters, and **ES6 modules** (`export`/`import`). The lab progresses from class hierarchies to modular code organization.

---

## Files

```
Lab02/
├── indices/
│   ├── Task01.html
│   ├── Task02.html
│   └── Task03.html
└── scripts/
    ├── Task01.js
    ├── Task02.js
    ├── Task03Mod01.js
    ├── Task03Mod02.js
    └── Mod01Mod02.js
```

---

## Task Details

### Task01 — Inheritance & Method Overriding (`Task01.js`)

- **`Person`** base class with private `#name`, `#age` and validated getters/setters.
- **`Teacher`** extends `Person` — adds `#subject`, `teach()` method, overrides `introduce()`.
- **`Student`** extends `Person` — adds `#major`, `study()` method, overrides `introduce()`.
- Both subclasses call `super.introduce()` and append their own context.
- Input validation: name/subject/major ≥ 3 characters, age between 5–100.

---

### Task02 — Polymorphism with Shapes (`Task02.js`)

- **`Shape`** base class with empty `calcArea()`.
- **`Rectangle`** extends `Shape` — private `#width`, `#height`, overrides `calcArea()` → `width × height`.
- **`Circle`** extends `Shape` — private `#radius`, overrides `calcArea()` → `π × r²`.
- Creates a mixed array of shapes and iterates using `forEach()` to demonstrate **polymorphic behavior**.
- Validation: width, height, radius must be ≥ 0.

---

### Task03 — ES6 Modules (`Task03Mod01.js`, `Task03Mod02.js`, `Mod01Mod02.js`)

A modular Employee management system split across three files:

| Module | File | Exports |
|--------|------|---------|
| **Module 1** | `Task03Mod01.js` | `Employee` class (private fields, validated setters, `getFullName()`), `departments` array |
| **Module 2** | `Task03Mod02.js` | `addEmployee()`, `findEmployeeByName()`, `increaseSalary()` functions |
| **Main** | `Mod01Mod02.js` | Creates employees, adds them, renders info to the DOM using `document.writeln()` |

- Uses `export` / `import` syntax with `type="module"` in the HTML script tag.
- Validation: firstName/lastName ≥ 3 chars, age ≥ 18, salary ≥ 0.

---

## Key Concepts Demonstrated

- **ES6 classes** — `class`, `constructor`, `extends`, `super`
- **Private fields** — `#field` syntax for encapsulation
- **Getters/Setters** — `get`/`set` with input validation
- **Polymorphism** — method overriding with `calcArea()` and `introduce()`
- **ES6 Modules** — `export`, `import`, `type="module"` in HTML
- **Modular architecture** — separating data models, operations, and entry point

---
