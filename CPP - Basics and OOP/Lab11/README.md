# Lab11

## Overview

A C++ lab demonstrating **polymorphism** and **pure virtual functions** through a shape area calculator. An abstract `Shape` base class defines a virtual `getArea()` method, and concrete subclasses (`Rectangle`, `Square`, `Triangle`, `Circle`) override it. A menu-driven program lets the user create shapes and compute the **sum of all areas** polymorphically.

---

## Files

```
Lab11/
├── bin/
├── obj/
├── Lab11.cbp
├── Lab11.depend
├── Lab11.layout
├── main.cpp
├── Point.cpp
└── Shapes.cpp
```

- **`main.cpp`** — Entry point; calls `shapesTest()`.
- **`Shapes.cpp`** — Contains the `Shape` hierarchy and the interactive test.
- **`Point.cpp`** — Contains a commented-out alternative implementation using `Geoshape` with `Point`-based composition (Rectangle, Triangle, Circle with `Point` vertices).

---

## Class Hierarchy

```
Shape (abstract)
├── Rectangle
│   └── Square
├── Triangle
└── Circle
```

### Shape (Abstract Base Class)

```cpp
class Shape {
protected:
    int _dim1;
    int _dim2;
public:
    virtual double getArea() = 0;  // pure virtual
    virtual ~Shape() {}            // virtual destructor
};
```

| Member           | Description                                     |
|------------------|-------------------------------------------------|
| `_dim1`, `_dim2` | Two generic dimensions (protected for subclass access) |
| `getArea()`      | **Pure virtual** — must be overridden            |
| `~Shape()`       | **Virtual destructor** — ensures proper cleanup  |

---

### Rectangle

```cpp
double getArea() { return _dim1 * _dim2; }  // width × height
```

### Square (inherits Rectangle)

```cpp
Square(int _dim) : Rectangle(_dim, _dim) {}
double getArea() { return _dim1 * _dim1; }  // side²
```

### Triangle

```cpp
double getArea() override { return 0.5 * _dim1 * _dim2; }  // ½ × base × height
```

### Circle

```cpp
const double pi{3.14};
double getArea() { return pi * _dim1 * _dim2; }  // π × r²
```

---

## Area Formulas Summary

| Shape     | Formula                | Input               |
|-----------|------------------------|---------------------|
| Rectangle | `width × height`       | Width, Height       |
| Square    | `side²`                | Side length         |
| Triangle  | `½ × base × height`   | Base, Height        |
| Circle    | `π × r²`              | Radius              |

---

## Menu-Driven Test — `shapesTest()`

Creates up to **10 shapes** (`MAX_SHAPES = 10`) via a polymorphic `Shape*` array:

| # | Option                | Description                                        |
|---|-----------------------|----------------------------------------------------|
| 1 | **Area of Rectangle** | Prompts for width & height, creates `new Rectangle` |
| 2 | **Area of Square**    | Prompts for side, creates `new Square`              |
| 3 | **Area of Triangle**  | Prompts for base & height, creates `new Triangle`   |
| 4 | **Area of Circle**    | Prompts for radius, creates `new Circle`            |
| 5 | **Sum Area of Shapes**| Iterates through all stored shapes and sums `getArea()` polymorphically |
| 6 | **Exit**              | Exits the application                              |

### Polymorphism in Action

The `myFun(Shape* s)` function calls `s->getArea()` — the correct overridden method is called at **runtime** based on the actual object type, not the pointer type. This is classic **runtime polymorphism** via vtable dispatch.

---

## Commented-Out Code — `Point.cpp`

Contains an alternative `Geoshape`-based hierarchy using `Point` composition:
- `Geoshape` (abstract) → `Rectangle`, `Triangle`, `Circle`
- Each shape owns `Point` objects for its vertices/center.
- Demonstrates combining **polymorphism + composition**.

---

## Key Concepts Demonstrated

- **Abstract class** with pure virtual function (`= 0`)
- **Virtual destructor** for safe polymorphic deletion
- **Runtime polymorphism** — calling `getArea()` through a base pointer
- **Inheritance hierarchy** — multi-level (`Shape → Rectangle → Square`)
- **Polymorphic array** — `Shape* s[10]` storing mixed types

---
