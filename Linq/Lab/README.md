# LINQ Lab

## Overview

This lab is a complete hands-on practice set for **LINQ operators** in C#. It uses realistic sample data (Products, Customers, Orders, and a dictionary word list) to train both query writing and result shaping.

The implementation is centered in `Program.cs`, where each operator family is grouped into focused sections.

## Files

```text
Lab/
├── Lab.slnx
└── Task01/
    ├── Customers.xml
    ├── dictionary_english.txt
    ├── ListGenerators.cs
    ├── Program.cs
    └── Task01.csproj
```

## Tasks / Features

### Querying In-Memory Objects (`Program.cs`)
The codebase demonstrates the following LINQ categories:

- **Restriction operators**: filtering records with `.Where()` (for example, out-of-stock products).
- **Element operators**: selecting single elements with `.First()` and `.FirstOrDefault()`.
- **Set operators**: `.Distinct()`, `.Union()`, `.Intersect()`, `.Except()`, and `.Concat()` across sequences.
- **Aggregate operators**: `.Count()`, `.Sum()`, `.Min()`, `.Max()`, and `.Average()` over numbers and grouped data.
- **Ordering operators**: `.OrderBy()`, `.OrderByDescending()`, `.ThenBy()`, and case-insensitive sorting.
- **Partitioning operators**: `.Take()`, `.Skip()`, `.TakeWhile()`, and `.SkipWhile()` for sequence slicing.
- **Projection operators**: `.Select()` and `.SelectMany()` to transform and flatten results.
- **Quantifiers**: `.Any()` and `.All()` for conditional existence checks.
- **Grouping operators**: `.GroupBy()` with keys such as category, modulo remainder, and first letter.

### Data Generators (`ListGenerators.cs`)
- Defines base models: `Customer`, `Order`, and `Product`.
- Loads customer and order data from `Customers.xml` using `XDocument.Load()`.
- Builds a static product list used in most query exercises.


## Learning Objectives

- Build confidence with the main LINQ operator families.
- Compare query syntax and method syntax in practical scenarios.
- Practice shaping output using anonymous types and flattened projections.
- Understand how grouping and aggregation work together for summaries.
- Learn safe querying patterns when data may be missing.

## Key Concepts Demonstrated

- **LINQ to Objects** — Querying generic `IEnumerable` & `IQueryable` variables entirely localized in memory.
- **Anonymous Types** — Passing un-declared data shapes instantly utilizing `new { p.ProductName, p.Category, Price = p.UnitPrice }`.
- **Deferred Execution** — Writing query rules isolated from loop manifestations ensuring optimal data runtime performance.
- **Query vs. Method Syntax** — Alternating fluently across C#'s SQL-like query structures and its standard Fluent extension methods.

## Typical Workflow

1. Prepare source collections from `ListGenerators`.
2. Write a focused LINQ query for one operator category.
3. Execute and inspect the result set.
4. Refactor query style (method/query syntax) when useful.
5. Repeat with grouped and aggregated scenarios to validate understanding.
