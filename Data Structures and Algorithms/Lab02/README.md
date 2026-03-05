# Lab02

## Overview

This lab explores implementing logical **LIFO** (Last-In-First-Out) and **FIFO** (First-In-First-Out) linear constraints via **Stacks** and **Queues**. It demonstrates utilizing arrays (Circular Queues) and pointers (Linked-List based Structures) to enforce these rules.

---

## Files

```text
Lab02/
├── circular_queue.cpp
├── expression.cpp
├── main.cpp
├── queue_linkedListBased.cpp
└── stack_linkedListBased.cpp
```

---

## Tasks / Features

### 1. Linked-List Based Data Structures
- **Stack (`stack_linkedListBased.cpp`)**: Wraps a linked list constraint so data is logically pushed and securely popped from the top of the chain.
- **Queue (`queue_linkedListBased.cpp`)**: A linked node implementation where nodes enqueue at the tail and dequeue logically from the front.

### 2. Circular Queue (`circular_queue.cpp`)
- A templated array implementation of a Queue ensuring circular mapping of positions (`front`, `rear`).
- Avoids resizing or sliding elements continuously by computing logical offsets (`(pos + 1) % size`).
- Implements injection capabilities like `insert_after` or `insert_before` bounded within the active capacity loop (`added_elements`).

### 3. Expression Evaluator (`expression.cpp`)
- Illustrates practical constraints applied by algorithmic stacks (or queues).
- Designed for processing string expressions (parsing mathematical notations such as postfix/infix evaluations, or parentheses balancing checking).

---

## Key Concepts Demonstrated

- **FIFO vs LIFO logic** — Adhering data retrieval rules strictly based on container design.
- **Memory Utilization** — Transforming 1D arrays logically into endless rings (Circular format) via modulus arithmetic operator (`%`).
- **Linked Linear Bounds** — Developing structure methods restricted entirely by Head vs Tail modifications.
- **Templating** — Allowing independent reusability of structs holding disparate types within C++.

---
