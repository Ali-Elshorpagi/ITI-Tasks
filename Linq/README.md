# Language Integrated Query (LINQ)

## Course Description

This directory contains labs and tasks focused on **LINQ (Language Integrated Query)** in C#. The content explores querying, filtering, ordering, and transforming data sets natively within C# using both Query Syntax and Method Syntax.

The lab is structured to help you practice LINQ progressively, starting with basic filtering and projection, then moving to grouping, aggregation, set operations, and sequence partitioning.

## Labs

| Lab | Topic | Description |
|-----|-------|-------------|
| [Lab](Lab/) | **LINQ Operators and Query Patterns** | Hands-on implementation of core LINQ operator families on in-memory collections and XML-loaded data, including Restriction, Projection, Ordering, Set, Aggregate, Quantifier, Partitioning, and Grouping operators |

## Learning Outcomes

After completing this folder, you should be able to:

1. Write LINQ queries using both method syntax and query syntax.
2. Choose the correct operator for filtering, shaping, and combining sequences.
3. Apply aggregation and grouping to produce summarized results.
4. Understand deferred execution and when queries are evaluated.
5. Work with data from different sources (hard-coded lists and XML files) using a unified query style.

## Practice Focus

- **Readability**: express data logic in concise, maintainable queries.
- **Correctness**: avoid common mistakes with null/empty sequences and first-element operators.
- **Performance awareness**: understand where repeated enumeration or unnecessary materialization may occur.
