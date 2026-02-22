# Lab09

## Overview

A C++ lab demonstrating **Composition vs Aggregation** relationships in OOP. The `Point` class is used as a building block to construct `Rectangle` and `Triangle` shapes using both ownership models.

---

## Files

```
Lab09/
├── bin/
├── obj/
├── Lab09.cbp
├── Lab09.depend
├── Lab09.layout
├── main.cpp
└── _main.cpp
```

---

## Classes

### Point

A simple 2D point with `(x, y)` coordinates.

```cpp
class Point {
    int _x;
    int _y;
};
```

| Member              | Description                                    |
|---------------------|------------------------------------------------|
| `Point(int x, int y)` | Parameterized constructor                   |
| `setX()` / `getX()` | Setter validates `x > 0`                      |
| `setY()` / `getY()` | Setter validates `y > 0`                      |
| `setXY(int, int)`   | Sets both coordinates                          |
| `~Point()`          | Destructor with info message                   |

---

### RectangleComposition (Composition)

Owns two `Point` objects **by value** — the points are **embedded** inside the rectangle.

```cpp
class RectangleComposition {
    Point _ul;  // upper-left (owned)
    Point _lr;  // lower-right (owned)
};
```

- Constructor uses **initializer list**: `RectangleComposition(x1, y1, x2, y2) : _ul(x1, y1), _lr(x2, y2)`
- When the rectangle is destroyed, the points are **automatically destroyed** with it.
- This is a **"has-a" strong ownership** relationship.

---

### RectangleAggregation (Aggregation)

Holds two `Point` objects **by pointer** — the points exist **independently** and are passed in from outside.

```cpp
class RectangleAggregation {
    Point *_ul;  // upper-left (external)
    Point *_lr;  // lower-right (external)
};
```

- Constructor receives pointers: `RectangleAggregation(Point* ul, Point* lr)`
- When the rectangle is destroyed, the **points are NOT destroyed** — they are managed elsewhere.
- This is a **"has-a" weak reference** relationship.

---

### TriangleComposition (Composition)

Owns three `Point` objects by value.

```cpp
class TriangleComposition {
    Point _ul, _lr, _ut;  // three vertices (owned)
};
```

- Initializer list: `TriangleComposition(x1,y1, x2,y2, x3,y3) : _ul(x1,y1), _lr(x2,y2), _ut(x3,y3)`
- Points are destroyed with the triangle — **strong ownership**.

---

### TriangleAggregation (Aggregation)

Holds three `Point` objects by pointer.

```cpp
class TriangleAggregation {
    Point *_ul, *_lr, *_ut;  // three vertices (external)
};
```

- Points live independently — **weak reference**.

---

## Composition vs Aggregation Summary

| Aspect              | Composition                    | Aggregation                         |
|---------------------|--------------------------------|-------------------------------------|
| Member storage      | By value (embedded)            | By pointer (external reference)     |
| Lifetime coupling   | Points die with the shape      | Points survive shape destruction    |
| Ownership           | **Strong** ("owns" the parts)  | **Weak** ("uses" the parts)         |
| Constructor style   | Initializer list with values   | Receives pointers from outside      |
| Destructor behavior | Cascades to contained objects  | Does NOT delete pointed objects     |

---

## Test Functions

| Function                        | Description                                           |
|---------------------------------|-------------------------------------------------------|
| `testRectangleComposition()`    | Creates a rectangle by composition, prints, updates   |
| `testRectangleAggregation()`    | Creates a rectangle by aggregation, prints, updates   |
| `testTriangleComposition()`     | Creates a triangle by composition, prints, updates    |
| `testTriangleAggregation()`     | Creates a triangle by aggregation, prints, updates    |

Each test creates the shape, prints its initial state, then updates the corner points and prints again.

---

## Key Concepts Demonstrated

- **Composition** — embedding objects by value (strong ownership)
- **Aggregation** — referencing objects by pointer (weak reference)
- **Initializer lists** — required for composing objects without default constructors
- **Constructor/Destructor order** — observe the creation/destruction cascade

---
