# Lab06 — DDL: Constraints, Schemas, Rules, Synonyms & DML with Joins

## Lab Description

This lab focuses on advanced **DDL (Data Definition Language)** concepts in SQL Server: user-defined types, rules, defaults, named constraints, schemas, synonyms, and table alterations. It also covers **DML** (`UPDATE`, `DELETE`) using multi-table joins with schema-qualified table names. The lab builds a custom `Company` database from scratch with a `Department`, `Employee`, `Project`, and `Works_On` schema.

## Topics Covered

**Part 1 — DDL & Constraints:**
- User-defined data type with `sp_addtype`
- `CREATE RULE` and `sp_bindrule` — enforce allowed values (location codes, max salary)
- `CREATE DEFAULT` and `sp_bindefault` — default column values
- `CREATE TABLE` with named `CONSTRAINT` (`PRIMARY KEY`, `UNIQUE`, `FOREIGN KEY`)
- `ALTER TABLE` — adding and dropping columns
- Testing referential integrity violations (FK errors)

**Part 2 — Schemas, Synonyms & DML:**
- `CREATE SCHEMA` and `ALTER SCHEMA ... TRANSFER` to move tables between schemas
- `CREATE SYNONYM` for alias access to schema-qualified tables
- Querying `INFORMATION_SCHEMA.TABLE_CONSTRAINTS` to inspect constraints
- `UPDATE` across schema-qualified tables using multi-table join
- `DELETE` across schemas using a 3-table join condition

## Lab Contents

| File | Description |
|------|-------------|
| `Lab.docx` | Full lab problem sheet |
| `Lab.sql` | Complete SQL solution — DDL + DML (two-part lab) |

## Database

**Custom DB (created in lab):** Tables across `Company` and `Human Resource` schemas — `Department`, `Employee`, `Project`, `Works_On`

## SQL Highlights (`Lab.sql`)

### Part 1

| Section | Description |
|---------|-------------|
| `loc` type | User-defined `nchar(2) NOT NULL` type for location |
| `loc_rule` | Restricts location to `'NY'`, `'DS'`, `'KW'` only |
| `loc_default` | Default location value is `'NY'` |
| `salary_rule` | Restricts salary to `< 6000` |
| `Employee` table | 6 named constraints: `c1` PK, `c2` UNIQUE salary, `c3` FK to Department |
| Referential integrity | 4 tests — inserting/updating/deleting violating FK rules |
| `ALTER TABLE` | Add then drop `TelephoneNumber` column |

### Part 2

| Query # | Description |
|---------|-------------|
| 1 | `CREATE SCHEMA Company` + transfer `Department` |
| 2 | `CREATE SCHEMA [Human Resource]` + transfer `Employee` |
| 3 | Query `INFORMATION_SCHEMA.TABLE_CONSTRAINTS` for Employee |
| 4 | `CREATE SYNONYM Emp` for `[Human Resource].Employee` |
| 5 | `UPDATE` project budget × 1.1 for Manager in project via join |
| 6 | Rename department to `'Sales'` for James's department (multi-table update) |
| 7 | Update `Enter_Date` for employees in Sales working on project 1 |
| 8 | `DELETE` from `Works_On` for employees in KW department (3-table join) |
