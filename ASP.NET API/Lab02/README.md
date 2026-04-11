# Lab02 - EF Core, DTOs & JS Client

## Description

This lab dives into advanced data-access strategies and web-standard data sharing. It introduces **Entity Framework Core** as the Object-Relational Mapper (ORM), configuring complex models and relations. Crucially, it explores building **DTOs (Data Transfer Objects)** to separate internal database structures from public APIs, enhancing security and versioning. We consume these optimized endpoints correctly with an asynchronous Vanilla JavaScript web client.

### Key Concepts & Implementations

- **The Repository Pattern**: Structuring decoupled and testable code via interfaces (`IStudentRepository`, `IDepartmentRepository`) instead of querying the internal DbContext directly within controllers.
- **Data Transfer Objects (DTOs)**: Enforcing contract sizes (preventing over-posting and accidental data exposure) by exposing models like `StudentDto` and hiding sensitive entity attributes.
- **AutoMapper Integration**: Automating tedious properties copying maps between Entity Classes and DTO models via strongly-typed `MappingProfile` setups added to the builder pipeline in `Program.cs`.
- **CORS Application & Pipeline Configs**: Explaining how Cross-Origin Resource Sharing works by setting an `AllowAll` policy enabling an external HTML site hosted independently to fetch data.
- **API Documentation**: Adding and exploring Swashbuckle tools (OpenAPI / Swagger UI) for real-time sandbox endpoint testing and contract visualization.
- **Client-Side Data Hydration**: Employing browsers' built-in `fetch` API (`script.js`) relying on Promises and the JS DOM to present `api/Students` updates asynchronously.

### Directory Structure
- **Task**: The central ASP.NET Core project loaded with `EntityFrameworkCore` packages, the Data access definitions (`ItiDbContext`), Repositories implementations, API Controllers, and Object mapping classes (AutoMapper).
- **Client**: A purely single-page static HTML/JS/CSS client executing AJAX-like `.fetch()` calls mimicking modern decoupled web UI architectures.