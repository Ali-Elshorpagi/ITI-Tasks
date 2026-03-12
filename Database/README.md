# Database

## Course Description

This directory contains labs and resources covering concepts in **SQL Server Database** design and programming. Topics progress from conceptual modeling and ERD design to advanced programmability objects like functions, stored procedures, triggers, cursors, and snapshots.

## Labs

| Lab | Topic | Description |
|-----|-------|-------------|
| [Lab01](Lab01/) | **ERD Design & Conceptual Modeling** | Identify entities, attributes, cardinality; draw ERDs from real-world scenarios; weak entities, primary keys |
| [Lab02](Lab02/) | **SQL DDL & Basic SELECT Queries** | `CREATE TABLE` with constraints (PK, FK, UNIQUE), basic `SELECT`, column aliases, arithmetic, `WHERE` filtering |
| [Lab03](Lab03/) | **Joins, DML & Normalization** | Implicit/explicit joins, `LIKE`, `BETWEEN`, `IN`, `ORDER BY`, self-joins, outer joins, `INSERT`, `UPDATE`, 1NF–3NF normalization |
| [Lab04](Lab04/) | **Aggregates, Subqueries & Advanced DML** | `SUM`, `COUNT`, `AVG`, `GROUP BY`, `HAVING`, `UNION ALL`, `EXISTS`/`NOT EXISTS`, `TOP N`, `DELETE` with join cascades |
| [Lab05](Lab05/) | **Window Functions, Built-in Functions & AdventureWorks** | `ROW_NUMBER()`, `RANK()`, `COALESCE`, `ISNULL`, self-joins, `TOP`, `SELECT INTO`, `FORMAT(GETDATE())`, `UNION` date formats |
| [Lab06](Lab06/) | **DDL: Constraints, Schemas, Rules & Synonyms** | User-defined types, rules, defaults, named constraints, `CREATE SCHEMA`, `ALTER SCHEMA TRANSFER`, `CREATE SYNONYM`, `INFORMATION_SCHEMA` |
| [Lab07](Lab07/) | **User-Defined Functions (UDFs)** | Scalar functions, inline TVFs, multi-statement TVFs, date/string/conditional logic, `hierarchyid`, bulk `WHILE` insert |
| [Lab08](Lab08/) | **Views & Indexes** | `CREATE VIEW` with `ENCRYPTION`, `SCHEMABINDING`, `CHECK OPTION`; `ALTER/DROP VIEW`; clustered/unique indexes; `MERGE` statement; temp tables |
| [Lab09](Lab09/) | **Stored Procedures & Triggers** | Procedures with/without params, `TRY/CATCH`; `AFTER`/`INSTEAD OF` triggers; `inserted`/`deleted` tables; `ROLLBACK`; audit logging |
| [Lab10](Lab10/) | **Cursors, Sequences, Snapshots & SP Recap** | `DECLARE CURSOR`, row-by-row processing; `CREATE SEQUENCE`; database snapshots; convert Lab07 UDFs to stored procedures |
