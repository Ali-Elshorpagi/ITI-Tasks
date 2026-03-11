# Lab03 — SQL Joins, DML & Normalization

## Lab Description

This lab covers two major areas: **advanced SQL querying** using joins, `LIKE`, `BETWEEN`, `IN`, `ORDER BY`, self-joins, outer joins, and DML operations (`INSERT`, `UPDATE`); and **Database Normalization** (1NF → 2NF → 3NF). It works against the `Company_SD` database.

## Topics Covered

**Part 1 — Joins & DML (`LabP01.sql`):**
- Implicit JOIN syntax (comma-separated in `FROM`) and explicit `INNER JOIN` / `LEFT OUTER JOIN`
- `CONCAT_WS` for full name formatting
- Filtering with `IN`, `LIKE`, `BETWEEN`, `ORDER BY`
- Multi-table joins (Employee + Project + Works_for)
- Self-join for supervisor–employee relationships
- `INSERT INTO` — inserting partial and full rows
- `UPDATE` — salary raise with percentage

**Part 2 — Normalization (`LabP02Normalization.doc`):**
- First Normal Form (1NF) — eliminate repeating groups
- Second Normal Form (2NF) — remove partial dependencies
- Third Normal Form (3NF) — remove transitive dependencies

## Lab Contents

| File | Description |
|------|-------------|
| `LabP01.doc` | Part 1 problem sheet — joins and DML queries |
| `LabP01.sql` | SQL solutions for Part 1 (15 queries) |
| `LabP02Normalization.doc` | Part 2 — normalization problems and solutions |
| `01.jpeg` | Diagram image for normalization exercise 1 |
| `02.jpeg` | Diagram image for normalization exercise 2 |

## Database

**`Company_SD`** — Tables: `Employee`, `Departments`, `Project`, `Works_for`, `Dependent`

## SQL Highlights (`LabP01.sql`)

| Query # | Description |
|---------|-------------|
| 1 | Departments with manager name via join on `MGRSSN = SSN` |
| 2 | Department ↔ Project join |
| 3 | All department columns + employee full name |
| 4 | Projects in Cairo or Alex (`IN`) |
| 5 | Projects with name starting with `'a'` (`LIKE`) |
| 6 | Employees in dept 30 with salary between 1000–2000 |
| 7 | Employees in dept 10 working ≥ 10 hours on 'AL Rabwah' (3-table join) |
| 8 | Employees supervised by 'Kamel Mohamed' (self-join) |
| 9 | Employee ↔ Project via `Works_for`, ordered by project name |
| 10 | Managers of Cairo projects with their address and birthdate |
| 11 | Employees who are department managers |
| 12 | All employees with their dependents (`LEFT OUTER JOIN`) |
| 13–14 | `INSERT` employees with partial and full data |
| 15 | `UPDATE` salary by 20% for a specific employee |

## Normalization Concepts

| Form | Rule Applied |
|------|-------------|
| **1NF** | Each column holds atomic values; no repeating groups |
| **2NF** | Every non-key attribute is fully functionally dependent on the whole primary key |
| **3NF** | No non-key attribute is transitively dependent on the primary key |
