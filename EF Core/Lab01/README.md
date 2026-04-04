# Lab01

## Lab Description
This lab focuses on building a complete EF Core data model for a book catalog domain.

The main goal is to translate a business domain into a relational database model using EF Core conventions and explicit Fluent API configuration where needed.

## Project

| Project | Description |
|---------|-------------|
| [Task01](Task01/) | Defines Book, Author, Tag, Review, PriceOffer, and BookAuthor entities with DbSet mapping and relationship configuration in BookContext |

## Learning Objectives
- Design entities with appropriate scalar and navigation properties
- Model common database relationship types in a single domain
- Use Fluent API to handle advanced mappings and avoid ambiguous conventions
- Create and apply an initial migration that reflects the model structure

## Domain Relationships Implemented
- **Book 1-to-many Review**: one book can have multiple reviews
- **Book 1-to-1 PriceOffer**: optional single offer record linked to one book
- **Book many-to-many Author via BookAuthor**: join table with composite key and author order

## Why This Lab Matters
This lab builds the foundation for all later EF Core work. If entity modeling and relationships are correct here, querying, updating, and reporting logic become much simpler in larger projects.

## Covered Concepts
- Creating an EF Core DbContext with SQL Server configuration
- Configuring one-to-many relationships (Book and Reviews)
- Configuring one-to-one relationships (Book and PriceOffer)
- Configuring many-to-many relationships with a join entity (BookAuthor)
- Generating and using initial migrations

## Typical Workflow
1. Define entity classes and navigation properties.
2. Add DbSet properties to BookContext.
3. Configure relationships in OnModelCreating.
4. Create migration and update database.
5. Verify generated schema and foreign keys.
