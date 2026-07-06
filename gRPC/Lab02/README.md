# Lab02 — Interceptors, EF Core Persistence & REST/gRPC Gateway

## Lab Description

This lab extends **FleetPulse** from Lab01 into a more production-shaped architecture. Orders are now persisted to a real SQL Server database via EF Core instead of an in-memory store, gRPC calls are wrapped with server-side interceptors for logging and centralized exception handling, and a REST API acts as a gateway/BFF that authenticates callers with JWT and forwards their credentials to the gRPC backend.

## Topics Covered

- gRPC server interceptors: cross-cutting logging (`LoggingInterceptor`) and centralized error translation to `RpcException`/`StatusCode` (`ExceptionInterceptor`).
- EF Core persistence: `FleetDbContext`, entity models, and code-first migrations against SQL Server (replacing Lab01's in-memory store).
- Repository pattern: `IOrderRepository`/`OrderRepository` and `IUserRepo`/`UserRepo` mediating between services and the database.
- JWT authentication and role-based authorization (`[Authorize]`, `[Authorize(Roles = "Admin")]`) shared across the REST API and the gRPC server.
- REST-to-gRPC gateway pattern: an ASP.NET Core Web API (`FleetPulse.API`) exposes REST endpoints that map DTOs to proto messages and invoke a typed gRPC client.
- Credential passthrough: the API's gRPC client forwards the caller's `Authorization` bearer token to the gRPC server via `AddCallCredentials`.
- Resiliency: a gRPC retry policy (`ServiceConfig`/`RetryPolicy`) with exponential backoff on `StatusCode.Unavailable`.

## Projects

| Project                     | Role         | Description                                                                                                                                                     |
| --------------------------- | ------------ | --------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **FleetPulse.OrderService** | gRPC server  | Hosts `FleetOperationsService` and `VehicleService`; persists orders via EF Core/SQL Server; applies logging and exception interceptors; issues/validates JWTs. |
| **FleetPulse.API**          | REST gateway | ASP.NET Core Web API exposing `OrdersController` (proxies to the gRPC server) and `AuthenticationController` (register/login, JWT issuing).                     |
| **FleetPulse.Client**       | Client       | Console app exercising all four `FleetOperationsService` RPCs directly over gRPC.                                                                               |
| **Vehicle.Client**          | Client       | Console app exercising both `VehicleService` RPCs directly over gRPC.                                                                                           |

## Services & RPCs

### VehicleService (`vehicle.proto`)

| RPC               | Request                  | Response       | Description                                               |
| ----------------- | ------------------------ | -------------- | --------------------------------------------------------- |
| `RegisterVehicle` | `RegisterVehicleRequest` | `VehicleReply` | Creates a new vehicle.                                    |
| `GetVehicle`      | `GetVehicleRequest`      | `VehicleReply` | Retrieves a vehicle by ID; throws `NOT_FOUND` if missing. |

### FleetOperationsService (`fleet.proto`)

| RPC                 | Request                    | Response          | Description                                                               |
| ------------------- | -------------------------- | ----------------- | ------------------------------------------------------------------------- |
| `CreateOrder`       | `CreateOrderRequest`       | `OrderReply`      | Creates and persists an order with items, extra info, and a package type. |
| `GetOrder`          | `GetOrderRequest`          | `OrderReply`      | Retrieves an order by ID; throws `NOT_FOUND` if missing.                  |
| `UpdateOrderStatus` | `UpdateOrderStatusRequest` | `OrderReply`      | Changes the delivery status of an existing order.                         |
| `ListOrders`        | `ListOrdersRequest`        | `ListOrdersReply` | Returns all orders, optionally filtered by `DeliveryStatus`.              |

`Order` reuses Lab01's `oneof package_details` (fragile/cold/standard), `map<string, string> extra_info`, and the `Timestamp`/`Duration`/`StringValue` well-known types.

## Interceptors

| Interceptor            | Purpose                                                                                                                                                                          |
| ---------------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `LoggingInterceptor`   | Times every unary call with a `Stopwatch` and logs the method name, start, completion, and duration.                                                                             |
| `ExceptionInterceptor` | Catches unhandled exceptions; maps `FormatException` to `StatusCode.InvalidArgument`, everything else to `StatusCode.Internal`, and rethrows existing `RpcException`s unchanged. |

## Setup & Run

### 1. Apply database migrations

```bash
dotnet ef database update --project Lab02/FleetPulse.OrderService
```

### 2. Run the gRPC server

```bash
dotnet run --project Lab02/FleetPulse.OrderService
```

The server listens on `https://localhost:7172` (see `launchSettings.json`).

### 3. Run the REST gateway

```bash
dotnet run --project Lab02/FleetPulse.API
```

The API listens on `https://localhost:7188` and forwards authenticated requests to the gRPC server at `https://localhost:7172`.

### 4. Run a console client

```bash
# Order management client
dotnet run --project Lab02/FleetPulse.Client

# Vehicle management client
dotnet run --project Lab02/Vehicle.Client
```

Each client prints the results of its RPC calls to the console and exits.
