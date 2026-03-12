# Lab07 — User-Defined Functions (UDFs)

## Lab Description

This lab covers all three types of **SQL Server User-Defined Functions (UDFs)**: scalar functions, inline table-valued functions (TVFs), and multi-statement table-valued functions. It works against the `ITI` database and includes bonus exercises on `hierarchyid` and bulk `INSERT` with a `WHILE` loop.

## Topics Covered

- **Scalar Functions** — return a single value; called with `dbo.FunctionName()`
- **Inline Table-Valued Functions** — single `RETURN SELECT` statement, act like parameterized views
- **Multi-Statement Table-Valued Functions** — use a declared `@table` variable, support `IF`/`WHILE` logic inside
- `FORMAT()` for date formatting
- `ISNULL` / `CONCAT` for safe name building
- Conditional logic inside functions (`IF / ELSE IF`)
- `SUBSTRING` + `LEN` for string manipulation
- `DELETE` with multi-table join
- Bonus: `hierarchyid` data type for hierarchical data
- Bonus: `WHILE` loop for bulk inserts

## Lab Contents

| File | Description |
|------|-------------|
| `Lab.docx` | Problem sheet with 8 questions + 2 bonus tasks |
| `Lab.sql` | SQL solutions — all 8 UDFs + bonus |

## Database

**`ITI`** — Tables: `Student`, `Instructor`, `Department`, `Stud_Course`

## SQL Highlights (`Lab.sql`)

| # | Function | Type | Description |
|---|----------|------|-------------|
| 1 | `getMonthName(@d date)` | Scalar | Returns the full month name using `FORMAT(@d, 'MMMM')` |
| 2 | `getRange(@first, @last)` | Multi-Statement TVF | Returns a table of integers between `@first` and `@last` using `WHILE` |
| 3 | `getStudentFullNameDepartmentName(@stdNo)` | Inline TVF | Returns student full name and department name for a given student ID |
| 4 | `studentNamesStatus(@stdNo)` | Scalar | Returns a status message based on whether `Fname`/`Lname` are null |
| 5 | `getManagerInfo(@mgrId)` | Inline TVF | Returns dept name, manager name, and hire date for a given manager ID |
| 6 | `getStudentNameQueryBased(@input)` | Multi-Statement TVF | Returns student first, last, or full name depending on the input string |
| 7 | — | Plain Query | Returns `St_Id` + first name without last character (`SUBSTRING + LEN`) |
| 8 | — | `DELETE` | Removes all grades for students in the SD department (multi-table join) |

## Bonus

| # | Description |
|---|-------------|
| B1 | `hierarchyid` table — models an employee hierarchy with `/1/`, `/1/1/`, `/1/2/` paths |
| B2 | Bulk insert 3001 rows into `tmpStudent` using a `WHILE` loop (IDs 3000–6000) |
