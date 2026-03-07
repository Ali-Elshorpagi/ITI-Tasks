# Labs — ASP.NET MVC with EF Core & Repository Pattern

## Lab Description

This lab builds a full-featured **ASP.NET MVC** web application backed by a SQL Server database via **Entity Framework Core**. It demonstrates a clean architecture using the **Repository Pattern** with a generic `IRepository<T>` interface, **ViewModels** with Data Annotations validation, and complete **CRUD** operations across three related entities: Students, Departments, and Courses.

## Topics Covered

- EF Core setup with `DbContext`, Fluent API configurations, and Code-First Migrations
- Generic Repository Pattern (`IRepository<T>`) with concrete implementations
- Dependency Injection — registering repositories in `Program.cs`
- CRUD operations for `Student`, `Department`, and `Course`
- ViewModels with Data Annotations (`[Required]`, `[Range]`, `[EmailAddress]`, `[StringLength]`)
- Server-side model validation with `ModelState.IsValid`
- Many-to-many relationship management (Departments ↔ Courses)
- `ViewBag` for passing dropdown data to views
- Razor views with tag helpers (`asp-for`, `asp-validation-for`, `asp-action`, etc.)

## Project Structure

| Folder / File | Description |
|---|---|
| `Models/` | `Student`, `Department`, `Course`, `StudentCourse` — EF Core entity models |
| `Context/AppDbContext.cs` | EF Core `DbContext` with Fluent API configurations |
| `Context/DataGenerator.sql` | SQL seed script for initial data |
| `Migrations/` | EF Core Code-First migration files |
| `Repositories/IRepositories/IRepository.cs` | Generic interface: `GetAll`, `GetById`, `Add`, `Update`, `Delete`, `Save` |
| `Repositories/Repository.cs` | Generic base repository implementation |
| `Repositories/StudentRepository.cs` | Student-specific repository with eager loading |
| `Repositories/DepartmentRepository.cs` | Department-specific repository (loads Students & Courses) |
| `Repositories/CourseRepository.cs` | Course-specific repository |
| `ViewModels/StudentViewModel.cs` | Student ViewModel with validation attributes |
| `ViewModels/DepartmentCoursesViewModel.cs` | ViewModel for department course management page |
| `Controllers/StudentController.cs` | Full CRUD for Students |
| `Controllers/DepartmentController.cs` | Full CRUD for Departments + course assignment management |
| `Controllers/CourseController.cs` | Full CRUD for Courses |
| `Views/` | Razor views for each entity (Index, Details, Create, Edit) |

## Controllers & Actions

### StudentController
| Action | Method | Description |
|---|---|---|
| `Index` | GET | Lists all students |
| `Details(id)` | GET | Shows student details |
| `Create` | GET | Shows creation form with department dropdown |
| `Create(StudentViewModel)` | POST | Validates & saves new student |
| `Edit(id)` | GET | Shows edit form pre-filled with student data |
| `Edit(StudentViewModel)` | POST | Validates & updates student |
| `Delete(id)` | GET | Deletes student, redirects to Index |

### DepartmentController
| Action | Method | Description |
|---|---|---|
| `Index` | GET | Lists all departments |
| `Details(id)` | GET | Shows department details |
| `Create` / `Edit` / `Delete` | GET/POST | Full CRUD for departments |
| `ManageCourses(id)` | GET | Shows assigned & available courses for a department |
| `AddCourse` | POST | Assigns a course to a department |
| `RemoveCourse` | POST | Removes a course from a department |

### CourseController
| Action | Method | Description |
|---|---|---|
| `Index` / `Details` / `Create` / `Edit` / `Delete` | GET/POST | Full CRUD for courses |

## Key Concepts

| Concept | Details |
|---|---|
| **Generic Repository** | `IRepository<T>` keeps data access logic centralized and reusable |
| **ViewModel Validation** | `StudentViewModel` uses Data Annotations; `ModelState.IsValid` guards saves |
| **Many-to-Many** | `Department` ↔ `Course` managed through `ManageCourses`, `AddCourse`, `RemoveCourse` |
| **EF Core DI** | `DbContext` and repositories registered as scoped services in `Program.cs` |
| **Eager Loading** | Repositories include related entities (e.g., `Department` → `Students`, `Courses`) |
