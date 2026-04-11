# ASP.NET API

## Course Description
This directory contains fully functional labs and educational resources exploring modern **ASP.NET Core Web API** development architecture. The timeline of projects logically scales—starting from basic RESTful endpoints consuming vanilla clients, to establishing enterprise-level abstractions with the Repository & Unit of Work patterns, culminating in robust authorization setups mapped around complex AI integrations using Background Queues. 

## Labs Overview and Deep Dive

| Lab | Main Topic | Key Learnings & Implementations |
|-----|------------|-------------|
| [Lab01](Lab01/) | **API Basics & WinForms Client** | **The Fundamentals:** Developing simple REST endpoints (`CourseController` GET/POST). Exploring `[FromBody]` data bindings and setting up basic Dependency Injection limits (`ICourseRepository`). Interfacing backend rules externally by making WinForm UI `HttpClient` AJAX-like requests tracking DB entities remotely. |
| [Lab02](Lab02/) | **EF Core, DTOs & JS Client** | **Data Boundaries:** Utilizing Entity Framework Core ORM to construct data models safely. Implementing decoupled logic by splitting database rules out of controllers using **Data Transfer Objects (DTOs)**. Learning to leverage AutoMapper for entity manipulation and handling cross-server requests by setting strict CORS (`AllowAll`) policies for Vanilla JS `fetch()` requests on separated frontend architectures. |
| [Lab03](Lab03/) | **Unit of Work Pattern** | **Transactional Consistency:** Standardizing enterprise data mutations by refactoring classic repository dependencies inside the massive generic abstraction that is the **Unit of Work (UoW)**. Injecting `IUnitOfWork` seamlessly ensures complex cross-table processes roll back universally on failure. Scoped lifecycle setups are highly emphasized. |
| [Lab04](Lab04/) | **Identity, JWT & RAG AI Integration** | **Complex Security & Generative AI flows:** Equipping endpoints with solid JWT token generation (ASP.NET Core Identity verification pipelines). Demonstrating massive computational background processing (combining `IHostedService` with `DocumentIngestionQueue`) handling RAG chunks. Extracting files (`.pdf`/`.txt`), slicing inputs securely, communicating with **OpenAI APIs** via internal pre-configured `HttpClient` policies (`OpenAiOptions`), saving vectorized outputs into persistence layers, and building real-world prompt search layers. |
