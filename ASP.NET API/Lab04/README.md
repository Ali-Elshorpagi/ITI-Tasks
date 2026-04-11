# Lab04 - ASP.NET Core Identity & JWT Authentication

## Description

This lab focuses on implementing robust stateless authentication and authorization mechanisms in an **ASP.NET Core Web API** utilizing **ASP.NET Core Identity** alongside **JSON Web Tokens (JWT)**.

It demonstrates how to configure endpoints to require valid authorization, safely register and log in users, map credentials to tokens, and segregate operations via standard RESTful routes.

### Key Concepts & Implementations

- **ASP.NET Core Identity & EF Core**: 
  - Subclassing default identity models alongside normal application entities inside `ITIContext`.
  - Managing seamless, secure user profiles locally using Entity Framework Core with SQL Server.
- **JWT Authentication via AccountController**: 
  - Utilizing an `AccountController` equipped with `Register` and `Login` functionalities via Data Transfer Objects (`AddStudentDTO`, `LoginDataDTo`).
  - Generating and serving encrypted Bearer JSON Web Tokens (JWT) which strictly authorize the caller to access protected HTTP endpoints across subsequent requests.
- **Repository Pattern abstraction**: 
  - Injecting database transactions through `IAccounttRepository` and `IStudentRepository`, ensuring separation of concerns between Controllers and the Data Access Layer.
- **Data Transfer Objects (DTOs)**: 
  - Structuring external communication with robust formats (`AddStudentDTO`, `UpdateStudentDTO`) that cleanly decouple client input/presentation from the database structure.

### Directory Structure
- **Controllers**: Responsible for endpoint exposure (`AccountController` for auth, `StudentController` for standard generic routing).
- **Models**: Defines raw DB tables (`Student`) and the foundational `ITIContext` configurations.
- **DTOs**: Payload packaging instances transferring validation structures safely.
- **Repositories**: Implementing Entity Framework Core mapping details encapsulating transactions away from the top-level API handlers.
- **Migrations**: Keeping track of local database evolutionary schema changes.