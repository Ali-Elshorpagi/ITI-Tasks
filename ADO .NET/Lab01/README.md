# Lab01 — ADO.NET Connected Architecture

## Lab Description

This lab covers the fundamentals of the **ADO.NET Connected Mode**. It focuses on establishing direct, open connections to a database to perform executing queries, reading streams of records, managing execution environments, and orchestrating database transactions synchronously in **C#**.

## Topics Covered

- Extracting connection strings securely utilizing `appsettings.json` and the `Microsoft.Extensions.Configuration` provider.
- Initializing and securing memory states explicitly using the `using` block surrounding `SqlConnection`.
- Creating generic `SqlCommand` instances mapping basic textual raw queries alongside mapping native stored procedures using `CommandType.StoredProcedure`.
- Safely handling inputs circumventing SQL-Injection via parameterized commands (`cmd.Parameters.AddWithValue()`).
- Iterating live databased rows sequentially maintaining an open connection utilizing the lightweight forward-only `SqlDataReader`.
- Implementing `SqlTransaction` boundaries handling grouped execution commits and safe rollbacks upon explicit computational errors (`BEGIN`, `COMMIT`, `ROLLBACK`).

## Projects

| Project | Description |
|---------|-------------|
| **Task01** | A C# console application operating exclusively in **Connected Mode**. Demonstrates CRUD operations natively referencing a `Categories` database table. Validates `SqlDataReader`, explicit parameterized `SqlCommand` execution (`ExecuteNonQuery`), transaction rollbacks, and invoking database-hosted stored procedures correctly mapping back entity domains in C#. |
