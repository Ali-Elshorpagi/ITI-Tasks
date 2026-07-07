# Lab02 — Authenticated Chat with Groups & Presence

## Lab Description

This lab builds a full-stack real-time chat system across three projects. **Chat.API** is the ASP.NET Core backend that hosts a JWT-secured SignalR hub alongside a REST API. **Chat.Web** is an ASP.NET Core MVC client. **Chat.Desktop** is a WinForms desktop client. Both clients connect to the same hub and REST API simultaneously.

## Topics Covered

- JWT Bearer authentication on a SignalR hub — token passed via query string (`access_token`).
- SignalR Groups: users join/leave named groups (`room-{id}`) for room-scoped broadcast.
- Online presence: `OnConnectedAsync` / `OnDisconnectedAsync` update a DB flag and broadcast `UserConnected` / `UserDisconnected` to all clients.
- Direct private messaging by tracking connection IDs in a `ConcurrentDictionary`.
- Hybrid architecture: REST API for CRUD + history retrieval, hub for real-time push.
- Hub methods triggering REST-side side-effects (room creation notifies all via `IHubContext<ChatHub>`).
- ASP.NET Core Identity with `IdentityRole` and EF Core SQL Server persistence.
- Audit logging: every significant action (login, join room, send message) is recorded.
- CORS configured to allow credentials from specific origins (required for SignalR).
- Paginated message history (room and private) via REST.

## Projects

| Project | Role | Description |
|---------|------|-------------|
| **Chat.API** | Backend | ASP.NET Core API hosting `ChatHub` at `/hubs/chat`, JWT auth, Identity, SQL Server, REST controllers, and audit logging. |
| **Chat.Web** | Web client | ASP.NET Core MVC app that consumes the API and connects to the hub via the browser SignalR JS client. |
| **Chat.Desktop** | Desktop client | WinForms app that connects to the hub via the .NET SignalR client library. |

## Hub Methods (Client → Server)

| Method | Parameters | Description |
|--------|------------|-------------|
| `JoinRoom` | `roomId` | Adds the caller to the SignalR group and records membership in DB. |
| `LeaveRoom` | `roomId` | Removes the caller from the group and deletes the membership record. |
| `SendRoomMessage` | `roomId`, `content` | Persists the message and broadcasts it to the room group. |
| `SendPrivateMessage` | `receiverId`, `content` | Persists the message and delivers it to all connections of the receiver and the caller. |
| `NotifyUserOnline` | — | Re-broadcasts the caller's online status (used on reconnect). |
| `NotifyUserOffline` | — | Broadcasts the caller's offline status before a clean disconnect. |

## Hub Events (Server → Client)

| Event | Payload | When fired |
|-------|---------|------------|
| `UserConnected` | `userId`, `userName` | A user connects or calls `NotifyUserOnline`. |
| `UserDisconnected` | `userId`, `userName` | A user's last connection drops or calls `NotifyUserOffline`. |
| `UserJoinedRoom` | `userId`, `userName`, `roomId` | A user joins a room (sent to room group). |
| `UserLeftRoom` | `userId`, `userName`, `roomId` | A user leaves a room (sent to room group). |
| `RoomsUpdated` | — | Any room change (create, delete, join, leave) — clients refresh the room list. |
| `ReceiveRoomMessage` | message object | A new public message in a room (sent to room group). |
| `ReceivePrivateMessage` | message object | A new private message (sent to sender and receiver). |

## REST API Endpoints

### Auth (`/api/auth`)

| Method | URL | Auth | Description |
|--------|-----|------|-------------|
| POST | `/api/auth/register` | — | Register a new user, returns JWT. |
| POST | `/api/auth/login` | — | Login by email or username, returns JWT. |
| POST | `/api/auth/logout` | JWT | Record logout in audit log. |

### Rooms (`/api/rooms`)

| Method | URL | Auth | Description |
|--------|-----|------|-------------|
| GET | `/api/rooms` | JWT | List all rooms with member count and membership flag. |
| GET | `/api/rooms/{id}` | JWT | Get a single room. |
| POST | `/api/rooms` | JWT | Create a room (creator auto-joined). |
| DELETE | `/api/rooms/{id}` | JWT | Delete a room (creator only). |
| POST | `/api/rooms/{id}/join` | JWT | Join a room. |
| POST | `/api/rooms/{id}/leave` | JWT | Leave a room. |

### Messages (`/api/messages`)

| Method | URL | Auth | Description |
|--------|-----|------|-------------|
| GET | `/api/messages/room/{roomId}` | JWT | Paginated history of public messages in a room. |
| GET | `/api/messages/private/{userId}` | JWT | Paginated private message history with a user. |

## Setup & Run

### Prerequisites

| Tool | Version |
|------|---------|
| .NET SDK | 8.0+ |
| SQL Server | LocalDB / Express / Full |

### 1. Configure the connection string

Open `Chat.API/appsettings.json` and confirm `DefaultConnection` points at your SQL Server instance (LocalDB is pre-configured):

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ChatDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

### 2. Apply migrations

```bash
dotnet ef database update --project Lab02/Chat.API
```

### 3. Run the API

```bash
dotnet run --project Lab02/Chat.API
```

The API listens on `https://localhost:7172` by default.

### 4. Run a client

```bash
# MVC web client
dotnet run --project Lab02/Chat.Web

# WinForms desktop client
dotnet run --project Lab02/Chat.Desktop
```

Open the web client in two browser tabs side by side to observe real-time events across sessions.
