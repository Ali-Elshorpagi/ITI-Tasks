# Lab01

## Overview

This lab focuses on the implementation and practical application of a **Linked List** data structure. It features a generic, templated **Doubly Linked List** and implements an Employee CRUD (Create, Read, Update, Delete) management system using this structure.

---

## Files

```text
Lab01/
├── DoublyLinkedList.cpp
├── EmpCRUD.cpp
├── Employee.cpp
├── Helper.cpp
└── main.cpp
```

---

## Tasks / Features

### 1. Templated Doubly Linked List (`DoublyLinkedList.cpp`)
- Implements a generic `Node` containing pointers to the next and previous nodes.
- Exposes methods to link/unlink nodes, append at the end (`insert_end`), and cleanly free memory blocks on destruction.
- Functions explicitly developed for tracking size, checking if the list is empty, mapping memory iteratively for console rendering (`print`), searching elements (`search`), and targeted deletions (`delete_node_with_key`, `delete_all_nodes_with_key`, `delete_all`).

### 2. Employee Entity (`Employee.cpp`)
- Basic `Employee` object model equipped with attributes like ID, Name, Salary, etc.
- Serves as the payload struct pushed into the Doubly Linked List.

### 3. Employee CRUD Utility (`EmpCRUD.cpp`)
- Exposes the business logic to manage employees dynamically.
- Interacts closely with the linked list to find, add, list, alter, or remove employees from the active session.

### 4. Helper API (`Helper.cpp`)
- Utilizes console-level utilities.
- Provides styling modifications like positioning using `gotoxy` and changing terminal colors using `textattr`.

---

## Key Concepts Demonstrated

- **Generic Programming** — Usage of C++ `template <class T>` to abstract the underlying linked list datatype.
- **Dynamic Memory Allocation** — Safely chaining dynamically populated elements (`new`/`delete`).
- **Memory Management** — Safely deallocating the linked structure layer by layer to prevent memory leaks in the class destructor.
- **Terminal UI (TUI)** — Modifying console buffer attributes to structure the display nicely for CLI interaction.

---
