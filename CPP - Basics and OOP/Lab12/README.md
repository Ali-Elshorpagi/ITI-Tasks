# Lab12

## Overview

A C++ lab focused on **operator overloading** for a template-based `Stack<type>` class. Implements the **assignment operator (`=`)**, **addition operator (`+`)**, and a **friend `<<` operator** for output streaming.

---

## Files

```
Lab12/
├── bin/
├── obj/
├── Lab12.cbp
├── Lab12.depend
├── Lab12.layout
└── main.cpp
```

---

## Template Class — `Stack<type>`

A generic dynamic stack with overloaded operators.

```cpp
template <class type>
class Stack {
    int _size;
    int top;
    type *arr;
};
```

### Core Methods

| Member              | Description                                  |
|---------------------|----------------------------------------------|
| `Stack()`           | Default constructor — capacity 5             |
| `Stack(int size)`   | Parameterized constructor                    |
| `Stack(const Stack&)` | **Deep copy** constructor                  |
| `push(type)`        | Push a value; no-op if full                  |
| `pop(type&)`        | Pop top value; returns `true`/`false`        |
| `isEmpty()` / `isFull()` | Standard checks                         |
| `print()`           | Print all elements top-to-bottom             |
| `~Stack()`          | Frees heap memory with `delete[]`            |

---

## Overloaded Operators

### `operator=` (Assignment)

```cpp
Stack<type>& operator=(const Stack<type>& other);
```

- **Self-assignment guard**: `if (this == &other) return *this;`
- **Same size**: just copies the elements and `top`.
- **Different size**: allocates new array, copies elements, frees old array, updates `_size` and `top`.
- Returns `*this` for chaining (`a = b = c`).

---

### `operator+` (Concatenation)

```cpp
Stack<type> operator+(const Stack<type>& other) const;
```

- Creates a **new stack** with capacity `_size + other._size`.
- Copies all elements from `this` first, then from `other`.
- Returns the combined stack (elements from both stacks in order).

---

### `operator<<` (Stream Output — Friend Function)

```cpp
friend ostream& operator<<(ostream& os, const Stack<type>& st);
```

- Prints elements from top to bottom, separated by spaces.
- Allows natural usage: `cout << myStack`.

---

## Main — Test Flow

```cpp
Stack<int> s1(5);  // push 1, 2, 3
Stack<int> s2(5);  // push 10, 20

s3 = s1 + s2;     // concatenation → [1, 2, 3, 10, 20]
s4 = s3;           // assignment operator
s5 = s4;           // copy constructor
s5.pop(val);       // pops 20 from s5
```

**Output**:
```
s1: 3 2 1
s2: 20 10
s3: 20 10 3 2 1
s4 (after = s3): 20 10 3 2 1
s5 (copy of s4): 20 10 3 2 1
popped from s5: 20
s5 now: 10 3 2 1
```

---

## Key Concepts Demonstrated

- **Operator overloading** — `=`, `+`, `<<`
- **Friend functions** — granting `operator<<` access to private members
- **Self-assignment guard** in `operator=`
- **Deep copy** in both copy constructor and assignment operator
- **Template class** — works with any type
- **Method chaining** — `operator=` returns `*this`

---
