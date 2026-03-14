# Lab02 — Multi-Tier Architectures (DAL & BLL)

## Lab Description

This lab transitions away from monolithic scripts by introducing structured **N-Tier Architecture** utilizing ADO.NET. It strictly isolates UI formatting, Business Logic, and Database Access logic into distinct contextual projects reducing implicit domain coupling successfully.

## Topics Covered

- Architecting modern layered directory boundaries creating dedicated Class Libraries for Data Access (`DAL`), Business Logic (`BLL`), alongside a generalized `Presentation` layer.
- **DAL (Data Access Layer)**: Housing a centralized `DBManager` class abstracting all direct `SqlConnection` parameters. It accepts commands generally returning filled `DataTable` structures minimizing database-lock lifespans naturally.
- **BLL (Business Logic Layer)**: Storing exact Entity models (e.g., `Department`) and configuring static `EntityManagers` processing DataTables mapped securely into strongly-typed standard `List<Entity>` structures validating inputs gracefully.
- Consuming standard entities uniformly decoupling backend configurations completely from frontend GUI processes.

## Projects

| Project | Description |
|---------|-------------|
| **DAL** | Data Access Library containing abstract `DBManager` mapping universal `ExecuteNonQuery` parameters and `GetTable` logic mapping internal `SqlDataAdapters`. |
| **BLL** | Business library referencing standard DAL dependencies. Contains domain `Department` entities and `DepartmentManager` performing strictly valid `AddNewDept`, `GetDeptById`, and `DeleteDept` behaviors asynchronously retrieving data sets utilizing connected components. |
| **Presentation** | Standard Console endpoint injecting logic strictly from abstract `BLL` managers effectively achieving UI/DB architectural separation perfectly mapping full console-hosted CRUD logic without touching an ADO.NET object directly. |
