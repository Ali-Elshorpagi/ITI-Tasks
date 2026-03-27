# Lab04 — Generics and Complex Types

## Lab Description

This lab covers writing reusable, type-safe structures in **C#** utilizing Generics. It dives into creating custom data structures from scratch mimicking built-in framework lists exactly and utilizing generic methods to reduce code duplication.

## Topics Covered

- Understanding `Generic` components — creating `Swap<Type>()`.
- Complex number parsing and operator overloading (`Complex` structure).
- Creating a generic dynamic list `MyList<T>` encompassing resizing methodologies internally without depending on the .NET framework library `List<T>`.

## Projects

| Project | Description |
|---------|-------------|
| **Task01** | Develops a strongly-typed generic `Swap<Type>(ref Type a, ref Type b)` avoiding boxing/unboxing overheads found in `object` casting. |
| **Task02** | Focuses on overloading arithmetic and comparison logic mapping mathematical complex numbers. Internally contains a generic custom dynamic array (`MyList<T>`) acting identically to C# built-in generic lists. |
