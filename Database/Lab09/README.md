# Lab09 — Stored Procedures & Triggers

## Lab Description

This lab covers **Stored Procedures** and **Triggers** in SQL Server — two of the core programmability objects. Part 1 uses `ITI` and `Company_SD` databases to build procedures with/without parameters, `TRY/CATCH` error handling, and `AFTER`/`INSTEAD OF` triggers with audit logging. Part 2 (`.docx`) provides additional exercises.

## Topics Covered

**Stored Procedures:**
- `CREATE PROCEDURE` / `ALTER PROCEDURE` — without and with parameters
- `DECLARE` variables, `IF/ELSE`, `PRINT`, `SELECT` output
- `TRY/CATCH` blocks — catching and displaying errors with `ERROR_MESSAGE()`
- Calling procedures (`EXEC` / shorthand name)

**Triggers:**
- `AFTER INSERT` / `AFTER UPDATE` — DML triggers on table events
- `INSTEAD OF INSERT` / `INSTEAD OF DELETE` — intercept and override default behavior
- `inserted` and `deleted` virtual tables — accessing new/old row data
- `UPDATE()` function — check if a specific column was modified
- `ROLLBACK` inside a trigger — cancel the DML operation
- `SUSER_NAME()` / `USER_NAME()` / `GETDATE()` — audit metadata
- Audit table pattern — log all inserts/deletes with user and timestamp

## Lab Contents

| File | Description |
|------|-------------|
| `Lab.doc` | Part 1 problem sheet — 8 procedures & trigger tasks |
| `LabP01.sql` | SQL solutions for Part 1 |
| `LabP02.docx` | Part 2 problem sheet — additional exercises |

## Databases

- **`ITI`** — `Student`, `Department`, `Instructor`, `Stud_Course`
- **`Company_SD`** — `Employee`, `Works_for`, `Project`, `ProjectAudit`

## SQL Highlights (`LabP01.sql`)

### Stored Procedures

| # | Procedure | Description |
|---|-----------|-------------|
| 1 | `sp_StudentCountPerDept` | No params — count of students per department name (ITI) |
| 2 | `sp_CheckNoEmployeesInP1` | No params — if ≥ 3 employees in project: print message, else show employee list |
| 3 | `sp_ReplaceEmployee(@oldEmpNo, @newEmpNo, @projectNo)` | Replace employee on a project — wrapped in `TRY/CATCH` |

### Triggers

| # | Trigger | Table | Type | Description |
|---|---------|-------|------|-------------|
| 4 | `trg_ProjectBudgetAudit` | `Project` | `AFTER UPDATE` | Logs old/new budget to `ProjectAudit` when budget column changes |
| 5 | `trg_NoInsertIntoDepartment` | `Department` | `INSTEAD OF INSERT` | Blocks all inserts and prints a warning message |
| 6 | `trg_NoInsertIntoEmployeeInMarch` | `Employee` | `AFTER INSERT` | Rolls back inserts if current month is March |
| 7a | `StudentAudit` table | — | — | Audit table: ServerUserName, AuditDate, Note |
| 7b | `trg_StudentAuditAfterInsert` | `Student` | `AFTER INSERT` | Logs insert event with user + key to StudentAudit |
| 8 | `trg_StudentAuditInsteadOfDelete` | `Student` | `INSTEAD OF DELETE` | Logs delete attempt instead of actually deleting |

### Supporting DDL (inside lab)

| Object | Description |
|--------|-------------|
| `Project.budget` | `ALTER TABLE` to add a `money` column; initialized to 100000 |
| `ProjectAudit` | Audit table — `Pno`, `userName`, `modifiedDate`, `budgetOld`, `budgetNew` |
| `StudentAudit` | Audit table — `ServerUserName`, `AuditDate`, `Note` |
