# gRPC

## Course Description
This directory contains labs and resources covering **gRPC** with **ASP.NET Core**. The topics progress from core Protocol Buffers concepts and unary RPC patterns to advanced message modeling using Well-Known Types, `oneof`, `map`, and multi-service server hosting — building a solid foundation for high-performance, contract-first inter-service communication.

## Labs

| Lab | Topic | Description |
|-----|-------|-------------|
| [Lab01](Lab01/) | **Unary RPC & Proto3 Fundamentals** | Building a FleetPulse fleet-management gRPC server with two services — vehicle registration and order management — covering enums, `oneof`, `map`, repeated fields, and Well-Known Types. |
| [Lab02](Lab02/) | **Interceptors, EF Core Persistence & REST/gRPC Gateway** | Extending FleetPulse with EF Core/SQL Server persistence, logging and exception interceptors, JWT auth, and a REST API gateway that proxies to the gRPC backend with credential passthrough and retry policies. |
