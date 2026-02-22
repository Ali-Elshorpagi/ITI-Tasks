# Lab10

## Overview

A C++ lab combining **templates**, **inheritance**, and a full **CRUD (Create, Read, Update, Delete)** system for employee records stored in a **template-based Stack**.

---

## Files

```
Lab10/
├── bin/
├── obj/
├── Lab10.cbp
├── Lab10.depend
├── Lab10.layout
└── main.cpp
```

---

## Template Function

### `Swap<type>()`

A generic swap function using templates:

```cpp
template <class type>
void Swap(type &first, type &second);
```

Tested with `int`, `float`, and `double` types.

---

## Template Class — `Stack<type>`

A generic, dynamic stack that works with **any data type**.

```cpp
template <class type>
class Stack {
    int _size;
    int top;
    type *arr;
};
```

| Member              | Description                                        |
|---------------------|----------------------------------------------------|
| `Stack()`           | Default constructor — capacity 5                   |
| `Stack(int size)`   | Parameterized constructor                          |
| `Stack(Stack&)`     | **Deep copy** constructor                          |
| `push(type)`        | Pushes a value; no-op if full                      |
| `pop(type&)`        | Pops top value; returns `true`/`false`             |
| `isEmpty()` / `isFull()` | Standard checks                               |
| `print()`           | Prints all elements top-to-bottom                  |
| `~Stack()`          | Frees heap memory with `delete[]`                  |

---

## Inheritance Hierarchy

```
Person
├── Employee
│   └── Instructor
└── Student
```

### Person (Base Class)

| Field   | Type       | Validation        |
|---------|------------|-------------------|
| `_id`   | `int`      | Must be > 0       |
| `_age`  | `int`      | Must be > 0       |
| `_name` | `char[50]` | Must be non-empty |

### Employee (inherits Person)

| Field     | Type     | Validation         |
|-----------|----------|--------------------|
| `_salary` | `double` | Must be >= 0       |

- **Overrides** `setAge()` — additional constraint: `age > 30` (calls `Person::setAge()` only if satisfied).
- **Overrides** `print()` — calls `Person::print()` then appends salary.

### Student (inherits Person)

| Field    | Type   | Validation     |
|----------|--------|----------------|
| `_grade` | `char` | Must not be `' '` |

### Instructor (inherits Employee)

| Field        | Type          | Description               |
|--------------|---------------|---------------------------|
| `_subjects`  | `char[5][20]` | Array of up to 5 subjects |

- **Overrides** `print()` — calls `Employee::print()` then appends subjects.

---

## CRUD System — `TestCRUD()`

A menu-driven employee management system using `Stack<Employee>`:

| # | Operation | Description |
|---|-----------|-------------|
| 1 | **Insert** | Reads `Id`, `Name`, `Age`, `Salary` and pushes a new `Employee` onto the stack |
| 2 | **Update** | Finds an employee by `Id` (pops all into a temp stack, matches, then restores). Shows a sub-menu to update individual fields (Id, Name, Age, Salary) |
| 3 | **Delete** | Finds and removes an employee by `Id` (skips it during the temp stack restore) |
| 4 | **Search** | Finds an employee by `Id` and prints their details |
| 5 | **Sort**   | Pops all employees into an array, sorts by `Id` using **Bubble Sort** + `Swap<Employee>()`, then pushes back |
| 6 | **Exit**   | Exits the application |

### Stack-Based CRUD Pattern

Since a stack only exposes `push`/`pop`, all operations (update, delete, search, sort) follow the same pattern:
1. Pop all elements into a **temporary stack** (or array for sort).
2. Perform the operation on the matching element.
3. Push everything back into the original stack.

---

## Key Concepts Demonstrated

- **Templates** — generic `Stack<type>` and `Swap<type>()`
- **Inheritance** — 4-level class hierarchy with `Person → Employee → Instructor`
- **Method overriding** — `setAge()` and `print()` overridden in subclasses
- **CRUD operations** — full Insert/Update/Delete/Search/Sort on a stack-based store
- **Bubble Sort** with a generic swap

---
