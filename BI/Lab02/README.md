# Lab02

## Lab Description
This lab focuses on **SSAS Multidimensional** modeling for analytical processing.

The project builds a dimensional model around sales analysis using separate dimensions, cubes, data source objects, and partitions.

## Project Structure

| Item | Description |
|------|-------------|
| `Tasks.slnx` | Solution container for the lab |
| `Tasks/Tasks.dwproj` | SSAS multidimensional project definition |
| `Tasks/Sales.ds` | Data source configuration |
| `Tasks/Sales.dsv` | Data source view used for model relationships |
| `Tasks/* Dim.dim` | Dimension definitions (Product, Time, Customer, Salesman, Channel) |
| `Tasks/*.cube` | Cube definitions (`Product Cube`, `Prod_Cust`, `Sales Cube`) |
| `Tasks/*.partitions` | Partition configurations for cubes |

## What This Lab Demonstrates
- Designing star-schema-like analytical models
- Creating reusable dimensions for slicing and dicing measures
- Building multiple cubes for different analytical perspectives
- Linking fact tables to dimensions through relationships
- Using partitions to organize cube processing/storage

## Learning Objectives
- Build and maintain SSAS dimensions and hierarchies
- Configure cubes for multidimensional analysis
- Understand role of data source and data source view in cube projects
- Prepare models for query performance and maintainability

## Typical Workflow
1. Open `Tasks.slnx` and inspect `Tasks.dwproj`.
2. Validate data source and data source view mappings.
3. Review dimensions and cube measure groups.
4. Process dimensions/cubes and test analysis in browser/query tools.

## Expected Outcome
You can model an SSAS analytical solution with well-defined dimensions and cubes suitable for BI reporting and OLAP queries.
