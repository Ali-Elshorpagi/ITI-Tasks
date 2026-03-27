# Lab05 — Delegates, Funcs, and Lambdas

## Lab Description

This lab addresses dynamic functional programming references in **C#** using Delegates. It progresses systematically from custom delegates through built-in standard delegates and finally down to modern inline lambda expressions.

## Topics Covered

- Understanding publisher/consumer concepts matching method signatures to `delegate` definitions.
- Passing logic natively as arguments to higher-order functions parameterizing sorting heuristics.
- Referencing built-in `Func<>` equivalents.
- Transitioning to pure inline anonymous `delegate(args) { return X; }` functions.
- Simplifying standard iterations leveraging modern Lambda/Arrow Expression `a => a.value` syntax.

## Projects

| Project | Description |
|---------|-------------|
| **Task01** | Processes an Array of `Book` entities across a scalable `LibraryEngine`. Illustrates seven distinct strategies routing varying retrieval patterns (e.g. Prices, Dates, Authors) via delegates up into pure Lambda evaluations dynamically. |
