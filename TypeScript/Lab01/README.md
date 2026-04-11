# Lab01

## Lab Description
This lab focuses on modeling student data with TypeScript interfaces and building a typed reporting flow to evaluate performance.

## Exercise Summary
The implementation defines a `Student` interface and several helper functions to:
- add students into a typed array
- calculate average grades using `reduce`
- classify performance level from the computed average
- print a structured report to the console

## Core Concepts Practiced
- Interface design with required and optional properties
- Typed arrays (`Student[]`) for structured collections
- Function return types (`void`, `number`, `string`)
- Conditional logic for classification tiers
- Array processing (`reduce`, `forEach`) with static typing

## Files
| File | Role |
|------|------|
| `Lab.ts` | Main TypeScript implementation of interface, helper functions, sample data, and output |
| `Lab.js` | JavaScript compiled from `Lab.ts` |
| `lap tasks.ts` | Original task requirements and expected output format |
| `index.html` | Loads compiled script in browser context |
| `tsconfig.json` | Compiler settings including strict type-checking options |

## Logic Breakdown
1. Define a `Student` interface with `id`, `name`, optional `email`, `isActive`, and `grades`.
2. Store records in a shared `students` collection.
3. Use `calcAvgGrades()` to compute per-student mean score.
4. Use `getStudentStatus()` to map average to performance labels.
5. Print a report for each student through `printStudentReport()`.

## Typical Workflow
1. Edit student structure or logic in `Lab.ts`.
2. Compile TypeScript to JavaScript.
3. Run with browser console or Node-compatible environment.
4. Verify printed average and status values.

## Expected Outcome
The script outputs each student report with grades, calculated average, and a readable performance category.
