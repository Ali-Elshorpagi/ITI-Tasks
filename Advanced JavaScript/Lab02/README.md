# Lab02

## Overview

A practical lab focused on implementing Object-Oriented Programming (OOP) in JavaScript through **Constructor Functions** and **Prototype-based Inheritance**. It demonstrates building a base object `Car` and a derived child object `ElectricCar`.

---

## Files

```text
Lab02/
├── Part01/
│   ├── index.html
│   └── script.js
├── Part02/
│   ├── index.html
│   ├── script.js
│   └── style.css
└── spreadTest/
```

---

## Tasks / Features

### Part01: Function Constructors & Prototypes
- **Constructor Function**: Uses a constructor to implement a `Car` with `name` and `speed` properties.
- **Prototype Methods**: Implements `accelerate` (+10 km/h) and `brake` (-5 km/h) on `Car.prototype` to avoid duplicating functions per instance.
- **Testing**: Creates instances (like 'BMW' and 'Mercedes') and manipulates their speed.

### Part02: Prototype-Based Inheritance
- **Child Constructor**: Creates an `ElectricCar` (EV) that inherits from `Car`.
- **Inheriting Properties**: Uses `Car.call(this, _name, _speed)` inside the `ElectricCar` constructor to inherit properties, while introducing a new `charge` state.
- **Linking Prototypes**: Achieves inheritance through `Object.create(Car.prototype)`.
- **Method Overriding**: Overrides the base `accelerate` method to increase speed by 20 km/h and reduce the battery charge by 1%.
- **New Methods**: Adds `chargeBattery` to set specific battery charge values.
- **Testing**: Instantiates a 'Tesla' and runs through cycles of accelerating, braking, and recharging.

---

## Key Concepts Demonstrated

- **Constructor Functions** — Object instantiation blueprint.
- **Prototype-based Inheritance** — Linking `Child.prototype` to `Parent.prototype`.
- **Method Overriding** — Modifying inherited behaviors on a child prototype.
- **Constructor Call** — Executing the parent constructor within the child scope via `.call(this)`.

---
