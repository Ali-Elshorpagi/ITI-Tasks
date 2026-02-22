# Lab13

## Overview

A C++ lab building a fully-featured **`Fraction` class** that demonstrates **operator overloading**, **static members**, **friend functions**, and **type conversion operators**. Includes automatic **simplification** via GCD.

---

## Files

```
Lab13/
├── bin/
├── obj/
├── Lab13.cbp
├── Lab13.depend
├── Lab13.layout
└── main.cpp
```

---

## Helper Function

### `_gcd_iterative(int A, int B)`

Computes the **Greatest Common Divisor** using the Euclidean algorithm (iterative):
- Handles negative numbers by taking absolute values.
- Ensures `A >= B` before starting.
- Used internally by `Fraction::simplification()` for auto-reduction.

---

## Fraction Class

```cpp
class Fraction {
    int _numerator, _denominator;
    static int counter;
};
```

### Constructors & Destructor

| Member                          | Description                                       |
|---------------------------------|---------------------------------------------------|
| `Fraction()`                    | Default `1/1`, increments `counter`               |
| `Fraction(int num, int den)`    | Parameterized; guards `den == 0` → sets to 1; auto-simplifies |
| `Fraction(const Fraction&)`     | Copy constructor, increments `counter`            |
| `~Fraction()`                   | Decrements `counter`                              |

### Static Member — `counter`

Tracks the **total number of living `Fraction` objects** at any point:
- Incremented in every constructor.
- Decremented in the destructor.
- Accessed via `Fraction::get_counter()`.

### Auto-Simplification

Every fraction is automatically reduced to lowest terms via `simplification()`:
- Normalizes sign (negative denominator → flip both signs).
- Divides both numerator and denominator by their GCD.

---

## Overloaded Operators

### Arithmetic

| Operator | Signature | Formula |
|----------|-----------|---------|
| `Fraction + Fraction` | `operator+(const Fraction&) const` | `(a*d + c*b) / (b*d)` |
| `Fraction + int` | `operator+(int) const` | `(a + val*b) / b` |
| `int + Fraction` | `friend operator+(int, const Fraction&)` | Delegates to `Fraction + int` |

### Comparison

| Operator | Implementation | Description |
|----------|---------------|-------------|
| `==` | `num == other.num && den == other.den` | Works because fractions are always simplified |
| `!=` | `!(*this == other)` | Delegates to `==` |
| `<`  | `a*d < c*b` | Cross-multiplication comparison |
| `>`  | `a*d > c*b` | Cross-multiplication comparison |

### Increment

| Operator | Type | Description |
|----------|------|-------------|
| `++f` (prefix) | `Fraction& operator++()` | Adds 1 (`num += den`), simplifies, returns `*this` |
| `f++` (postfix) | `Fraction operator++(int)` | Saves old value, increments, returns the old copy |

### Stream Output (Friend)

```cpp
friend ostream& operator<<(ostream& os, const Fraction& f);
// Output format: "numerator/denominator"
```

### Assignment

```cpp
Fraction& operator=(const Fraction& other);
// Self-assignment guard included
```

### Type Conversion

```cpp
operator float() const {
    return (float)_numerator / (float)_denominator;
}
```

Allows implicit and explicit conversion:
```cpp
float af = (float)a;  // explicit cast
float bf = a;          // implicit conversion
```

---

## Main — Test Flow

```cpp
Fraction a(1, 2);  // 1/2
Fraction b(3, 4);  // 3/4

a + b  → 5/4
a == b → 0 (false)
a < b  → 1 (true)
b++    → returns 3/4 (old), b becomes 7/4
a + 2  → 5/2
2 + a  → 5/2
++a    → 3/2
a++    → returns 3/2 (old), a becomes 5/2
float(a) → 2.5
```

---

## Key Concepts Demonstrated

- **Operator overloading** — arithmetic (`+`), comparison (`==`, `!=`, `<`, `>`), increment (`++`), assignment (`=`), stream (`<<`), type conversion (`operator float`)
- **Friend functions** — `operator<<` and `int + Fraction`
- **Static members** — object instance counter
- **Auto-simplification** — GCD-based reduction on every construction/operation
- **Prefix vs Postfix** increment semantics
- **Implicit type conversion** — `Fraction` → `float`

---
