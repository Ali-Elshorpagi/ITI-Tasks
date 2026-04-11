# ASP.NET API

## Course Description
This directory contains fully functional labs and educational resources exploring modern **ASP.NET Core Web API** development architecture. The timeline of projects logically scales—starting from basic RESTful endpoints consuming vanilla clients, to establishing enterprise-level abstractions with the Repository & Unit of Work patterns, culminating in robust authorization setups using ASP.NET Core Identity and JWT Authentication. 

## Labs Overview and Deep Dive

| Lab | Main Topic | Key Learnings & Implementations |
|-----|------------|-------------|
| [Lab01](Lab01/) | **API Basics & WinForms Client** | **The Fundamentals:** Developing simple REST endpoints (`CourseController` GET/POST). Exploring `[FromBody]` data bindings and setting up basic Dependency Injection limits (`ICourseRepository`). Interfacing backend rules externally by making WinForm UI `HttpClient` AJAX-like requests tracking DB entities remotely. |
| [Lab02](Lab02/) | **EF Core, DTOs & JS Client** | **Data Boundaries:** Utilizing Entity Framework Core ORM to construct data models safely. Implementing decoupled logic by splitting database rules out of controllers using **Data Transfer Objects (DTOs)**. Learning to leverage AutoMapper for entity manipulation and handling cross-server requests by setting strict CORS (`AllowAll`) policies for Vanilla JS `fetch()` requests on separated frontend architectures. |
| [Lab03](Lab03/) | **Unit of Work Pattern** | **Transactional Consistency:** Standardizing enterprise data mutations by refactoring classic repository dependencies inside the massive generic abstraction that is the **Unit of Work (UoW)**. Injecting `IUnitOfWork` seamlessly ensures complex cross-table processes roll back universally on failure. Scoped lifecycle setups are highly emphasized. |
| [Lab04](Lab04/) | **ASP.NET Core Identity & JWT Authentication** | **Robust Security flows:** Equipping endpoints with solid JWT token generation and validation using ASP.NET Core Identity. Securing endpoints, managing user registrations securely via DTOs, safely mapping database operations using the Repository Pattern, and protecting CRUD actions behind strict `[Authorize]` policies. |
