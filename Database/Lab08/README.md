# Lab08 — Views & Indexes

## Lab Description

This lab covers **SQL Server Views** and **Indexes** in depth. It is split into two parts: Part 1 uses the `ITI` and `Company_SD` databases to build views with various options (`WITH ENCRYPTION`, `WITH SCHEMABINDING`, `WITH CHECK OPTION`), indexes, temporary tables, and the `MERGE` statement. Part 2 uses the `SD32-Company` database to practice creating, modifying, deleting, querying through, and inserting into views.

## Topics Covered

**Views:**
- `CREATE VIEW` — basic, encrypted (`WITH ENCRYPTION`), schema-bound (`WITH SCHEMABINDING`)
- `WITH CHECK OPTION` — prevent DML through a view that violates the view's `WHERE` clause
- Querying, inserting, and updating data through views
- `ALTER VIEW` — modifying an existing view
- `DROP VIEW` — removing one or multiple views
- `sp_helptext` — view the definition of a non-encrypted view
- Nested views — building a view on top of another view
- Grouped views with `COUNT`

**Indexes:**
- Clustered vs non-clustered indexes
- `CREATE CLUSTERED INDEX` on a non-PK column (requires removing clustered PK first)
- `CREATE UNIQUE INDEX` and the expected failure on non-unique data
- `sp_helpindex` — list indexes on a table

**Other:**
- `CREATE TABLE #tempTable` — session-scoped temporary table
- `MERGE` statement — upsert: `WHEN MATCHED → UPDATE`, `WHEN NOT MATCHED → INSERT`

## Lab Contents

| File | Description |
|------|-------------|
| `Lab.docx` | Full problem sheet (both parts) |
| `Lab.sql` | Complete SQL solutions — views, indexes, temp tables, MERGE |

## Databases

- **`ITI`** — `Student`, `Instructor`, `Department`, `Ins_Course`, `Course`, `Topic`, `Stud_Course`
- **`Company_SD`** — `Employee`, `Project`, `Works_for`
- **`SD32-Company`** — `Works_on`, `Company.Project`, `Company.Department`, `[Human Resources].Employee`

## SQL Highlights (`Lab.sql`)

### Part 1 — ITI & Company_SD

| # | Object | Description |
|---|--------|-------------|
| 1 | `stdGrades` view | Students with grade > 50 — shows full name, course name, grade |
| 2 | `managerTopics` view | Encrypted view — manager names and topics they teach |
| 3 | `instructorDepartment` view | Schema-bound view — instructors in SD or Java departments |
| 4 | `v1` view | Students in Alex or Cairo — with `WITH CHECK OPTION` to block out-of-range updates |
| 5 | `ix_dept_hiredate` | Clustered index on `manager_hiredate` (requires moving PK to non-clustered) |
| 6 | `ix_student_age` | Unique index on `st_age` — expected to fail (non-unique data) |
| 7 | `#todaytasks` | Session-scoped temp table for employee daily tasks |
| 8 | `Emp_Count` view | Grouped view — project name + count of employees (Company_SD) |
| 9 | `MERGE` | Upsert between `lastt` and `Dailyt` tables using `WHEN MATCHED / NOT MATCHED` |

### Part 2 — SD32-Company

| # | Object | Description |
|---|--------|-------------|
| 1 | `v_clerk` | Employees on 'Clerk' jobs — EmpNo, ProjectNo, Enter_Date |
| 2 | `v_without_budget` | All projects without the budget column |
| 3 | `v_count` | Project name + count of jobs (`GROUP BY`) |
| 4 | `v_project_p2` | Nested view on `v_clerk` — only project 2 records |
| 5 | `ALTER VIEW v_without_budget` | Modified to show all data for projects 1 and 2 |
| 6 | `DROP VIEW v_clerk, v_count` | Deleting two views at once |
| 7 | `Emp_Data` | Employees in dept 2 — EmpNo + last name |
| 8 | Query through `Emp_Data` | Filter by last name containing letter 'J' |
| 9 | `v_dept` | Department number and name |
| 10 | Insert via `v_dept` | Insert a new development department through the view |
| 11 | `v_2006_check` | Work entries from 2006 only — enforced with `WITH CHECK OPTION` |
