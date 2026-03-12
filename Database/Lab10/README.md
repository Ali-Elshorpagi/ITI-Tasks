# Lab10 — Cursors, Sequences, Database Snapshots & Stored Procedures (Recap)

## Lab Description

This lab covers **Cursors** for row-by-row processing, **Sequences** as identity alternatives, **Database Snapshots** for point-in-time recovery, and a **Stored Procedure recap** that converts all UDFs from Lab07 into equivalent stored procedures. It works across `ITI`, `Company_SD`, and `AdventureWorks2012` databases.

## Topics Covered

- **Cursors** — `DECLARE`, `OPEN`, `FETCH`, `WHILE @@FETCH_STATUS = 0`, `CLOSE`, `DEALLOCATE`
  - Conditional logic inside cursor loops (`IF/ELSE` for salary tiers)
  - String concatenation across rows using cursor
  - `PRINT` output inside cursor
- **Sequences** — `CREATE SEQUENCE` with `START WITH`, `INCREMENT BY`, `MINVALUE`, `MAXVALUE`, `NO CYCLE`
  - Using `NEXT VALUE FOR` in an `INSERT`
- **Database Snapshots** — `CREATE DATABASE ... AS SNAPSHOT OF`
  - Reading data from the snapshot as a read-only view of a past state
- **Stored Procedures (Labs recap)** — converting all Lab07 UDFs to `CREATE PROC` equivalents

## Lab Contents

| File | Description |
|------|-------------|
| `Lab.docx` | Full problem sheet — cursors, sequences, snapshots, SP recap |
| `Lab.sql` | SQL solutions — all 6 sections |
| `ITI_Students.xlsx` | Excel file with student data (supporting material) |

## Databases

- **`Company_SD`** — `Employee`
- **`ITI`** — `Student`, `Instructor`, `Department`
- **`AdventureWorks2012`** / **`AdventureWorksSnap`** — snapshot demo
- **`master`** — used to create the snapshot database

## SQL Highlights (`Lab.sql`)

### Cursors

| # | Cursor | Description |
|---|--------|-------------|
| 1 | `empCursor` | Iterates Employee rows — salary < 3000 → ×1.1 raise, else → ×1.2 raise |
| 2 | `deptCursor` | Iterates departments — `PRINT` dept name and manager name line by line |
| 3 | `studentCursor` | Concatenates all student first names into a single string with `@allNames` |

### Sequence

| # | Description |
|---|-------------|
| 4 | `EmpTestSeq` — sequence 1 to 10, no cycle; used with `NEXT VALUE FOR` on insert |

### Database Snapshot

| # | Description |
|---|-------------|
| 5 | `AdventureWorksSnap` — snapshot of `AdventureWorks2012`; read `Person.Address` from snapshot |

### Stored Procedures Recap (from Lab07 UDFs)

| # | Procedure | Description |
|---|-----------|-------------|
| 6.1 | `getMonthNameProc @d date` | `SELECT FORMAT(@d, 'MMMM')` |
| 6.2 | `getRangeProc @first, @last` | `WHILE` loop — `PRINT` each integer in range |
| 6.3 | `getStudentFullNameDepartmentNameProc @stdNo` | Student name + dept from join |
| 6.4 | `studentNamesStatusProc @stdNo` | Null-name status check — `SELECT @result` |
| 6.5 | `getManagerInfoProc @mgrId` | Dept name, manager name, hire date |
| 6.6 | `getStudentNameQueryBasedProc @input` | Conditional — first/last/full name from Student table |

## Key Differences: Functions vs Procedures

| Aspect | UDF (Function) | Stored Procedure |
|--------|---------------|-----------------|
| Return | `RETURNS` type / table | No return type (`SELECT` for output) |
| Call | `SELECT dbo.fn()` / `SELECT * FROM dbo.fn()` | `EXEC procName` or `procName params` |
| Use in query | Yes (inline) | No |
| Side effects | Not allowed | Allowed |
