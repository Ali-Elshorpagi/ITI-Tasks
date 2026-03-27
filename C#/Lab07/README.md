# Lab07 — Multithreading and Asynchronous Programming

## Lab Description

This lab focuses on scaling application performance and maintaining standard UI thread responsiveness through asynchronous `.NET` tasks via `async` and `await`.

## Topics Covered

- Offloading exhaustive CPU-blocking processes implicitly allocating tasks natively.
- Using `async / await` alongside `Task.Run()` contexts routing blocking math computations natively onto distinct thread pool partitions preserving primary UI event cycles dynamically.
- Handling Windows Forms application latency states (like cursor switches or toggleable controls) appropriately natively.

## Projects

| Project | Description |
|---------|-------------|
| **Task01** | Operates a responsive Windows Forms (WinForms) GUI containing an exhaustive loop calculating a sum of Prime numbers dynamically across massive thresholds concurrently. Shows offloading computation visually avoiding immediate framework hangs securely. |
