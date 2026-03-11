# Lab05 — Advanced Queries: Window Functions, Built-in Functions & AdventureWorks

## Lab Description

This lab is split into two parts. **Part 1** uses the `ITI` database and covers advanced SELECT queries including window functions (`ROW_NUMBER`, `RANK`), `ISNULL`/`COALESCE`, self-joins, and `TOP`. **Part 2** switches to the `AdventureWorks2012` database and focuses on date filtering, string functions, `GROUP BY`, `SELECT INTO`, `UNION`, and `FORMAT`.

## Topics Covered

**Part 1 (`LabP01.sql` — ITI DB):**
- `COUNT`, `DISTINCT`, `ISNULL`, `COALESCE`, `CONCAT_WS`
- Multi-table `INNER JOIN` and `LEFT JOIN`
- `GROUP BY` with `COUNT` per category (courses per topic)
- `MIN`, `MAX`, `AVG` on salary
- Subquery in `WHERE` (salary < avg)
- `TOP` N values
- Self-join (students with their supervisors)
- `ROW_NUMBER() OVER (PARTITION BY ... ORDER BY ...)` — top 2 salaries per department
- `RANK() OVER (PARTITION BY ... ORDER BY NEWID())` — random 1 student per department

**Part 2 (`LabP02.sql` — AdventureWorks2012):**
- Date range filtering with `BETWEEN` on `OrderDate`
- `NULL` filtering with `IS NULL`
- `IN`, `LIKE` for string filtering
- `UPDATE` + wildcard `LIKE` with `_` escape character
- `SUM + GROUP BY` on sales totals by date
- `DISTINCT` on date columns
- `AVG(DISTINCT ...)` for unique prices
- `CONCAT` for string building with `BETWEEN` filter
- `SELECT INTO` — copy table structure with/without data (schema-only copy using false condition)
- `FORMAT(GETDATE(), ...)` with `UNION` for multiple date format outputs

## Lab Contents

| File | Description |
|------|-------------|
| `Lab.docx` | Combined problem sheet for both parts |
| `LabP01.sql` | SQL solutions — Part 1 (ITI database, 15 queries) |
| `LabP02.sql` | SQL solutions — Part 2 (AdventureWorks2012, 12 queries) |

## Databases

- **`ITI`** — Tables: `Student`, `Instructor`, `Department`, `Course`, `Topic`, `Stud_Course`
- **`AdventureWorks2012`** — `Sales.SalesOrderHeader`, `Production.Product`, `Production.ProductDescription`, `HumanResources.Employee`, `Sales.Store`

## SQL Highlights

### Part 1 — ITI DB

| Query # | Description |
|---------|-------------|
| 1 | Count students with a non-null age |
| 2 | Distinct instructor names |
| 3 | Student ID, full name (`ISNULL`), department name (JOIN) |
| 4 | Instructor name + department (`LEFT JOIN`) |
| 5 | Students with a non-null grade in a course |
| 6 | Number of courses per topic (`GROUP BY`) |
| 7 | Min and Max instructor salary |
| 8 | Instructors earning below average salary (subquery) |
| 9 | Department with the lowest-paid instructor (subquery) |
| 10 | Top 2 instructor salaries |
| 11 | Instructor salary with `COALESCE` for NULL display |
| 12 | Average instructor salary |
| 13 | Students with their supervisors (self-join) |
| 14 | Top 2 salaries per department using `ROW_NUMBER()` |
| 15 | 1 random student per department using `RANK() + NEWID()` |

### Part 2 — AdventureWorks2012

| Query # | Description |
|---------|-------------|
| 1 | Orders between two dates |
| 2 | Products with `StandardCost < 110` |
| 3 | Products with null weight |
| 4 | Products with color in a set |
| 5 | Products with name starting with 'B' |
| 6 | Update + search with escaped `_` wildcard |
| 7 | Sum of `TotalDue` grouped by `OrderDate` |
| 8 | Distinct hire dates |
| 9 | `AVG(DISTINCT ListPrice)` |
| 10 | `CONCAT` product description with price range |
| 11a | `SELECT INTO` to copy store table (with data) |
| 11b | Schema-only copy using `WHERE 1 > 2` |
| 12 | Multiple `FORMAT(GETDATE(), ...)` patterns with `UNION` |
