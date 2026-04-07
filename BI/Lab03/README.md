# Lab03

## Lab Description
This lab focuses on **SSRS (SQL Server Reporting Services)** report development using shared data sources and shared datasets.

The project contains multiple report styles (tabular, matrix, parameterized) over academic and sales-oriented data scenarios.

## Project Structure

| Item | Description |
|------|-------------|
| `Lab.slnx` | Solution container for the lab |
| `Tasks/Tasks.rptproj` | SSRS report project definition |
| `Tasks/DataSourceITI.rds` | Shared data source |
| `Tasks/*.rsd` | Shared datasets for departments, students, grades, courses, topics, and sales |
| `Tasks/*.rdl` | Report definitions (e.g., `StudentsByDepartment`, `StudentsByAge`, `SalesMatrix`) |

## What This Lab Demonstrates
- Building reusable report datasets and data sources
- Creating parameter-driven reports (for example, by department or age range)
- Designing tabular and matrix report layouts
- Using stored procedures and query-based datasets in reports
- Preparing report projects for local SSRS deployment targets

## Learning Objectives
- Author and organize SSRS report assets in a project
- Build reports with filtering and parameterization
- Separate dataset logic from visualization layout
- Reuse shared resources across multiple reports

## Typical Workflow
1. Open `Lab.slnx` and load the SSRS project.
2. Validate the shared data source connection.
3. Review or update shared datasets (`.rsd`).
4. Design and preview reports (`.rdl`).
5. Deploy to local report server when needed.

## Expected Outcome
You can build maintainable SSRS reporting solutions with reusable datasets and clear report layouts for business users.
