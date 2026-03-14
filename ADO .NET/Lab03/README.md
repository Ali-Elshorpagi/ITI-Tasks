# Lab03 — ADO.NET Disconnected Architecture

## Lab Description

This lab introduces and highlights the **ADO.NET Disconnected Mode** using `SqlDataAdapter` alongside abstracted, cache-based memory patterns operating without maintaining live open SQL connections securely.

## Topics Covered

- Constructing `SqlDataAdapter` structures handling initial database SELECT calls successfully mapping explicit memory caches natively bypassing explicit connection locks securely.
- Using `DataTable` contexts storing entire tables internally allowing modifications logically mapped without actively mutating real SQL servers.
- Utilizing `SqlCommandBuilder` to auto-generate underlying `INSERT`, `UPDATE`, and `DELETE` logic scaling out implicit updates dynamically mapping row states properly (`DataRowState.Added`, `DataRowState.Modified`, `DataRowState.Deleted`).
- Navigating local `DataRow` mechanisms mapping `dt.NewRow()`, updating internal properties, and tracking individual instance changes directly mapped inside Local Memory structures successfully.
- Calling `adapter.Update(DataTable)` executing delayed batch-updates synchronously parsing modified row-states automatically generating matched SQL instructions dynamically.

## Projects

| Project | Description |
|---------|-------------|
| **Task01** | Connects to a `University` database caching `Department` objects disconnected implicitly via `SqlDataAdapter`. Iterates, inserts, modifies and calls `.Delete()` on specific DataRows observing native `RowState` modifications explicitly. Batches these disconnected updates directly calling parameter builder operations back to the SQL database efficiently. |
