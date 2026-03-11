# Lab04 — Aggregate Functions, Subqueries & Advanced DML

## Lab Description

This lab dives into **aggregate functions**, **GROUP BY / HAVING**, **subqueries** (`EXISTS`, `NOT EXISTS`, `IN`, correlated), **UNION**, **TOP N**, and more advanced **DML** operations (`INSERT`, `UPDATE`, `DELETE` with joins). It works against the `Company_SD` database.

## Topics Covered

- Aggregate functions: `SUM`, `COUNT`, `AVG`, `MIN`, `MAX`
- `GROUP BY` and `HAVING` for grouped filtering
- `UNION ALL` for combining result sets
- Subqueries: correlated, `EXISTS`, `NOT EXISTS`, `IN`, `NOT IN`
- `TOP N` queries — two approaches (declarative and correlated subquery)
- Multi-table `UPDATE` and `DELETE` using JOINs
- Referential integrity — cascading effects when deleting referenced rows
- `INSERT INTO ... VALUES` for new departments

## Lab Contents

| File | Description |
|------|-------------|
| `Lab.docx` | Problem sheet with all 14 questions |
| `Lab.sql` | SQL solutions (14 queries + DML statements) |

## Database

**`Company_SD`** — Tables: `Employee`, `Departments`, `Project`, `Works_for`, `Dependent`

## SQL Highlights (`Lab.sql`)

| Query # | Description |
|---------|-------------|
| 1 | `UNION ALL` — Female dependents of female employees + male dependents of male employees |
| 2 | Total hours worked per project (`SUM + GROUP BY + LEFT JOIN`) |
| 3 | Department of the employee with the minimum SSN (subquery) |
| 4 | Max, Min, Avg salary per department |
| 5.1 | Managers with no dependents — using `NOT EXISTS` |
| 5.2 | Managers with no dependents — using `NOT IN` |
| 6 | Departments where avg salary < overall avg salary (`HAVING + subquery`) |
| 7.1 | Employees + projects ordered by dept then name (`INNER JOIN` + `ORDER BY`) |
| 7.2 | Same query using implicit join syntax |
| 8.1 | Top 2 salaries using `TOP 2 ... ORDER BY DESC` |
| 8.2 | Top 2 salaries using correlated subquery with `COUNT` |
| 9 | Employees sharing their name with their dependent (`LIKE + DISTINCT`) |
| 10 | Employees who have dependents (`EXISTS`) |
| 11 | `INSERT` new department 'DEPT IT' |
| 12a–c | `UPDATE` — reassign managers and supervisor relationships |
| 13 | `DELETE` employee — cascade: update dependents, departments, works_for first |
| 14 | `UPDATE` salary +30% for employees on project 'Al Rabwah' (multi-table update) |
