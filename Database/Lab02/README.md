# Lab02 — SQL DDL & Basic SELECT Queries

## Lab Description

This lab introduces **SQL DDL (Data Definition Language)** — creating database schemas from ERD designs — alongside foundational **DML SELECT** queries. The lab works against the `Company_SD` database and covers building tables, defining constraints, and writing basic queries to retrieve and filter data.

## Topics Covered

- Translating an ERD into physical SQL tables (DDL)
- `CREATE TABLE` with data types, `PRIMARY KEY`, `FOREIGN KEY`, `NOT NULL`, `UNIQUE`
- Basic `SELECT` statements — all columns, specific columns, column aliases
- Arithmetic expressions in queries (`salary * 0.1 * 12`)
- `WHERE` clause filtering with comparison operators (`>`, `<`, `=`)
- Filtering using annual calculations (`salary * 12 > 10000`)
- Filtering by sex, department number, and project location

## Lab Contents

| File | Description |
|------|-------------|
| `LabP01.doc` | Part 1 — DDL: create tables from ERD (Company schema) |
| `LabP02.doc` | Part 2 — SELECT queries problem sheet |
| `LabP02.sql` | SQL solutions for Part 2 SELECT queries |
| `problem 1.png` | ERD problem diagram 1 |
| `problem 2.jpg` | ERD problem diagram 2 |
| `problem 3.jpg` | ERD problem diagram 3 |
| `problem_4.png` | ERD problem diagram 4 |

## Database

**`Company_SD`** — Tables: `Employee`, `Departments`, `Project`, `Works_for`, `Dependent`

## SQL Highlights (`LabP02.sql`)

| Query # | Description |
|---------|-------------|
| 1 | `SELECT *` from Employee |
| 2 | Select specific columns: `Fname`, `Lname`, `salary`, `DNO` |
| 3 | Select project name, location, department number |
| 4 | Full name as alias + annual commission calculation |
| 5 | Employees earning more than 1000 (with SSN + full name) |
| 6 | Employees with annual salary > 10000 |
| 7 | Female employees |
| 8 | Department managed by a specific SSN |
| 9 | Projects belonging to department 10 |
