# Lab01 — Unary RPC & Proto3 Fundamentals

## Lab Description

This lab builds **FleetPulse**, a gRPC server hosted in ASP.NET Core that exposes two independent services for fleet management. All communication uses typed, schema-first contracts defined in `.proto` files and generated as C# code at build time.

## Topics Covered

- Defining proto3 schemas: messages, enums, services, and RPC methods.
- Unary RPC pattern: single request → single response.
- Advanced message features: `repeated` fields, `map<K,V>`, and `oneof` for polymorphic package types.
- Well-Known Types: `Timestamp`, `Duration`, and `StringValue` (nullable wrapper).
- Multi-service gRPC server: registering two independent services on one host.
- In-memory stores registered as singletons and injected into service implementations.
- gRPC error handling with `RpcException` and `StatusCode`.
- Console client projects connecting over HTTPS with a self-signed certificate bypass.

## Projects

| Project | Role | Description |
|---------|------|-------------|
| **FleetPulse.gRPC** | Server | ASP.NET Core gRPC host exposing `VehicleService` and `FleetOperationsService`. |
| **FleetPulse.Client** | Client | Console app exercising all four `FleetOperationsService` RPCs: create, get, update status, and list (with filter). |
| **Vehicle.Client** | Client | Console app exercising both `VehicleService` RPCs: register and get a vehicle. |

## Services & RPCs

### VehicleService (`vehicle.proto`)

| RPC | Request | Response | Description |
|-----|---------|----------|-------------|
| `RegisterVehicle` | `RegisterVehicleRequest` | `VehicleReply` | Creates a new vehicle and stores it in memory. |
| `GetVehicle` | `GetVehicleRequest` | `VehicleReply` | Retrieves a vehicle by ID; throws `NOT_FOUND` if missing. |

### FleetOperationsService (`fleet.proto`)

| RPC | Request | Response | Description |
|-----|---------|----------|-------------|
| `CreateOrder` | `CreateOrderRequest` | `OrderReply` | Creates an order with items, extra info, delivery notes, and a package type. |
| `GetOrder` | `GetOrderRequest` | `OrderReply` | Retrieves an order by ID; throws `NOT_FOUND` if missing. |
| `UpdateOrderStatus` | `UpdateOrderStatusRequest` | `OrderReply` | Changes the delivery status of an existing order. |
| `ListOrders` | `ListOrdersRequest` | `ListOrdersReply` | Returns all orders, optionally filtered by `DeliveryStatus`. |

## Setup & Run

### Prerequisites

| Tool | Version |
|------|---------|
| .NET SDK | 8.0+ |

### 1. Run the server

```bash
dotnet run --project Lab01/FleetPulse.gRPC
```

The server listens on `https://localhost:7172` by default (see `launchSettings.json`).

### 2. Run a client

Open a second terminal and run either client:

```bash
# Order management client
dotnet run --project Lab01/FleetPulse.Client

# Vehicle management client
dotnet run --project Lab01/Vehicle.Client
```

Each client prints the results of its RPC calls to the console and exits.
