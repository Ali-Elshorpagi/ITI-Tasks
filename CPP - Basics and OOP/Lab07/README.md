# Lab07

## Overview

A C++ lab introducing core **OOP concepts**: classes with **constructors**, **destructors**, **encapsulation**, and demonstrating **pass-by-value vs pass-by-pointer vs pass-by-reference** behavior using two classes: `Complex` and `Employee`.

---

## Files

```
Lab07/
├── bin/
├── obj/
├── Lab07.cbp
├── Lab07.depend
├── Lab07.layout
├── main.cpp
└── __main.cpp
```

---

## Classes

### Complex

Represents a complex number with real and imaginary parts.

```cpp
class Complex {
    int _real;
    int _img;
};
```

| Member        | Description                                                     |
|---------------|-----------------------------------------------------------------|
| `Complex(int, int)` | Parameterized constructor using **initializer list**      |
| `set_real()` / `get_real()` | Setter/getter for the real part                |
| `set_img()` / `get_img()`   | Setter/getter for the imaginary part           |
| `print()`    | Prints the complex number (e.g., `34`, `5+3j`, `5-3j`)         |
| `~Complex()` | Destructor — prints a message when the object is destroyed      |

---

### Employee

Represents an employee with id, name, and salary.

```cpp
class Employee {
    int _id;
    char _name[50];
    double _salary{0.00};
};
```

| Member        | Description                                                     |
|---------------|-----------------------------------------------------------------|
| `Employee(int, char[], double)` | Full constructor (calls setters internally)   |
| `Employee(int, char[])`         | Constructor without salary (default `0.00`)   |
| `Employee(int)`                 | Constructor with id only                      |
| `set_id()` / `get_id()`         | Setter validates `id > 0`                    |
| `set_name()` / `get_name()`     | Setter uses `strcpy()`, validates non-empty  |
| `set_salary()` / `get_salary()` | Setter validates `salary > 0`                |
| `print()`    | Prints `Id, Name, Salary`                                       |
| `~Employee()` | Destructor — prints a message on destruction                    |

### Constructor Overloading

The `Employee` class has **3 constructors** to demonstrate overloading:
1. `Employee(id, name, salary)` — full initialization
2. `Employee(id, name)` — without salary
3. `Employee(id)` — id only

---

## Test Functions

### `TestComplex()`
- Creates a `Complex(34, 0)` object and prints it.
- Demonstrates constructor/destructor lifecycle.

### `TestEmployee()`
Demonstrates **three different ways to pass an object** to a function:

| Function               | Parameter Type    | Effect on Original                     |
|------------------------|-------------------|----------------------------------------|
| `test_value(Employee)` | By value (copy)   | ❌ No effect — works on a copy         |
| `test_pointer(Employee*)` | By pointer     | ✅ Can modify the original via `emp->` |
| `test_reference(Employee&)` | By reference | ✅ Can modify the original directly    |

- **By value**: a copy is made → destructor is called for the copy when the function ends.
- **By pointer/reference**: no copy is made → the original object is used directly.

---

## Key Concepts Demonstrated

- Class definition with private members and public interface
- **Constructor overloading** (3 different constructors for `Employee`)
- **Initializer list** (used in `Complex`)
- **Destructor** lifecycle — observe when destructors fire for copies vs originals
- **Pass-by-value vs pass-by-pointer vs pass-by-reference**
- **Encapsulation** via getters/setters with validation

---
