# Lab01 - API Basics & WinForms Client

## Description

This lab covers the fundamentals of building a RESTful service using **ASP.NET Core Web API**. It establishes the baseline for building web services and securely connecting backend business logic to different frontend clients. The main focus is creating a solid architectural foundation using Controllers, and integrating a Windows Forms desktop application to consume the exposed endpoints.

### Key Concepts & Implementations

- **Controller-based API Setup**: Configuring an `ApiController` (`CourseController`) decorated with the necessary routing attributes (`[Route("api/[controller]")]`).
- **RESTful Endpoints Creation**: 
  - Standard `GET` and `POST` actions to retrieve all items, retrieve by ID, and save new data.
  - Usage of `[FromBody]` to properly bind incoming JSON payloads to C# POCO objects (`Course`).
- **Dependency Injection (DI)**: Best-practice utilization of the built-in DI container to inject dependencies like `ICourseRepository` directly into controller constructors, promoting loose coupling and testability.
- **Client Consumption**: 
  - Using an external desktop app (**WinForms**) to act as the primary consumer.
  - Employing `HttpClient` with standard asynchronous requests to communicate with the REST API.
  - Parsing and mapping returned JSON objects into desktop display elements using models like `CourseMap`.

### Directory Structure
- **Task**: The main ASP.NET Core Web API backend application responsible for HTTP handling, routing, database connections, and running the `CourseController`.
- **WinFormsApp**: The simple frontend Windows Forms client created to visualize API data and interact with its HTTP methods programmatically.