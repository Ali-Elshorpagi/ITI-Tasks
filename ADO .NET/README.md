# ADO.NET

## Course Description
This directory contains labs and resources covering core concepts in **ADO.NET** programming. The topics cover database connectivity fundamentals exploring synchronous and decoupled data access patterns establishing structured `Connected Architecture`, mapping internal logic into robust `Multi-Tier Architecture` (DAL, BLL, UI), and executing large memory-mapped cache modifications leveraging powerful `Disconnected Architecture` capabilities smoothly.

## Labs

| Lab | Topic | Description |
|-----|-------|-------------|
| [Lab01](Lab01/) | **ADO.NET Connected Architecture** | Extrapolating `SqlConnection` structures strictly utilizing `SqlCommand` and iteration streams (`SqlDataReader`). Explicit mapping of parameterized outputs, basic Stored Procedures, and secure native `SqlTransaction` boundaries correctly maintaining ACID consistency. |
| [Lab02](Lab02/) | **Multi-Tier Architectures (DAL & BLL)** | Structuring modern applications seamlessly using scalable N-Tier patterns isolating business rules cleanly. Implements custom backend libraries (`DAL.DBManager`) explicitly accessed by strong typed business rules mapping standard entity lists directly via static methods. |
| [Lab03](Lab03/) | **ADO.NET Disconnected Architecture** | Leveraging the decoupled `SqlDataAdapter` capabilities manipulating `DataTable` caches locally. Tracking modified table row states without locking server threads natively executing batch data updates through implicitly built `SqlCommandBuilder` scripts dynamically matching modifications reliably. |
