# Lab02

## Lab Description
This lab focuses on structuring an EF Core solution using layered architecture and practicing common data access operations.

The lab demonstrates how to keep domain models independent from persistence concerns while centralizing EF Core configurations and migrations in the infrastructure layer.

## Projects

| Project | Description |
|---------|-------------|
| [Task01.Domain](Task01.Domain/) | Contains domain entities: Student, Course, Department, and StudentCourse |
| [Task01.Infrustructure](Task01.Infrustructure/) | Contains AppDbContext, Fluent API entity configurations, and migrations |
| [Task01](Task01/) | Console app for data seeding, CRUD scenarios, and relationship loading examples |

## Learning Objectives
- Apply separation of concerns across Domain, Infrastructure, and App layers
- Configure relationships using dedicated IEntityTypeConfiguration classes
- Handle many-to-many association with payload (StudentCourse contains Degree)
- Practice full data lifecycle operations: insert, update, delete, and read
- Understand when to use eager, explicit, or lazy loading

## Architecture Notes
- **Domain layer**: plain entity classes and business shape
- **Infrastructure layer**: DbContext, SQL Server setup, model configuration, migrations
- **Application layer**: execution and interaction logic (seeding, CRUD, output)

This structure improves maintainability and keeps EF-specific code away from business entities.

## Covered Concepts
- Separating domain and infrastructure responsibilities
- Configuring entities with IEntityTypeConfiguration and ApplyConfigurationsFromAssembly
- Defining composite keys and relationship behavior with Fluent API
- Running migrations in a multi-project EF Core setup
- Practicing add, update, and delete operations
- Comparing eager loading, explicit loading, and lazy loading

## Data Operations Demonstrated
- Initial data setup for departments, students, courses, and enrollments
- Update scenarios for entity fields and relationship records
- Delete scenarios with related records and cascade behavior
- Data printing helpers for inspecting the current database state

## Typical Workflow
1. Define or update domain entities.
2. Add/adjust configurations in infrastructure.
3. Generate migration and update database.
4. Run console app to seed data and test CRUD operations.
5. Validate query behavior with different loading strategies.
