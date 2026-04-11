# Lab03 - Unit of Work Pattern

## Description

This lab is a comprehensive deep dive into scalable database transaction management. It enhances typical Repository Pattern models by incorporating the **Unit of Work (UoW)** design pattern. By ensuring all database operations affecting multiple repositories execute under a single shared transaction layer, consistency is guaranteed, and performance bottlenecks are minimized by batched commits.

### Key Concepts & Implementations

- **The Unit of Work Abstraction**: Constructing an `IUnitOfWork` interface enforcing atomic commits (`SaveAsync()`) acting as a central hub over all repositories (`Students`, `Departments`, etc.).
- **Avoiding Repository Side-effects**: Shifting the responsibility of `DbContext.SaveChanges()` solely to the Unit Of Work. Removing hidden premature database commits inside basic `Add` or `Update` method logic inside repositories.
- **Transactional Consistency**: Ensuring database rollbacks happen effectively when executing multi-entity business use cases across isolated tables—if saving one table fails, everything reverts safely.
- **Injecting Context via Scoped DI**: Setting up reliable Dependency Injection pipelines whereby the same `DbContext` instance is shared seamlessly between multiple concrete Repositories tied to the single Unit Of Work per API request scope.
- **Optimizing REST API Workflows**: Continuing best practices with explicit **DTOs** inside updated `StudentController` endpoints that orchestrate complex queries, allowing controllers to rely entirely on the UoW facade rather than multiple detached dependencies. 

### Directory Structure
- **Lab03**: A fully refactored API layer structured around the Unit of Work architecture separating concerns between Controllers, Data Transfer Objects (DTOs), Repository contracts, and the newly integrated UoW implementation managing SQL data seamlessly.