# Lab08

## Overview

A C++ lab implementing two versions of a **Stack** data structure — a **StaticStack** (fixed-size array on the stack) and a **DynamicStack** (heap-allocated array) — and comparing the behavior of **pass-by-value**, **pass-by-pointer**, and **pass-by-reference** with each.

---

## Files

```
Lab08/
├── bin/
├── obj/
├── Lab08.cbp
├── Lab08.depend
├── Lab08.layout
└── main.cpp
```

---

## Classes

### StaticStack

A stack backed by a **fixed-size array** (`int arr[100]`) allocated on the stack.

```cpp
class StaticStack {
    int top;
    int arr[100];
    int size;
};
```

| Member         | Description                                          |
|----------------|------------------------------------------------------|
| `StaticStack()` | Default constructor — sets `top = -1`, `size = 100` |
| `push(int)`    | Pushes a value; no-op if full                        |
| `pop(int&)`    | Pops the top value into `ret` by reference; returns `1` on success, `0` if empty |
| `isEmpty()`    | Returns `true` if `top == -1`                        |
| `isFull()`     | Returns `true` if `top == size - 1`                  |
| `print()`      | Prints all elements from top to bottom               |
| `~StaticStack()` | Destructor with info message                       |

---

### DynamicStack

A stack backed by a **heap-allocated array** (`int* arr = new int[_size]`).

```cpp
class DynamicStack {
    int top;
    int* arr;
    int _size;
};
```

| Member                   | Description                                              |
|--------------------------|----------------------------------------------------------|
| `DynamicStack()`         | Default constructor — allocates 100 elements             |
| `DynamicStack(int size)` | Parameterized constructor — allocates `size` elements    |
| `DynamicStack(DynamicStack&)` | **Copy constructor** — performs a **deep copy** of `arr` |
| `push(int)`              | Pushes a value; no-op if full                            |
| `pop(int&)`              | Pops the top value; returns `1` on success               |
| `isEmpty()` / `isFull()` | Standard checks                                         |
| `print()`                | Prints all elements top to bottom                        |
| `~DynamicStack()`        | Frees `arr` with `delete[]`                              |

### Deep Copy Constructor

The `DynamicStack` copy constructor allocates **new memory** and copies elements one by one, avoiding the **double-free** bug that occurs with a shallow copy:

```cpp
DynamicStack(DynamicStack& st) {
    _size = st._size;
    top = st.top;
    arr = new int[_size];
    for (int i = 0; i <= top; ++i)
        arr[i] = st.arr[i];
}
```

---

## Pass-By Comparison Tests

Both `StaticStack` and `DynamicStack` are tested with three passing mechanisms:

| Function Variant | StaticStack Behavior | DynamicStack Behavior |
|---|---|---|
| **By Value** | Pops from a **copy** — original unchanged | Uses the **deep copy constructor** — original unchanged |
| **By Pointer** | Pops from the **original** via `st->pop()` | Pops from the **original** — changes persist |
| **By Reference** | Pops from the **original** via `st.pop()` | Pops from the **original** — changes persist |

### Observation
- After **by-value** call: the original stack still has all 3 elements.
- After **by-pointer** call: 2 elements are popped from the original.
- After **by-reference** call: 2 more elements are popped from the original.

---

## Key Concepts Demonstrated

- **Static vs dynamic memory** — stack-allocated array vs `new[]`/`delete[]`
- **Copy constructor** — deep copy for heap-allocated resources
- **Pass-by-value/pointer/reference** — and how each affects the original object
- **Destructor** and resource cleanup
- **LIFO** data structure implementation

---
