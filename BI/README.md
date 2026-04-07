# BI (Business Intelligence)

## Course Description
This directory contains practical labs for core **Business Intelligence** workflows using Microsoft BI stack components.

The labs are organized to cover the standard BI pipeline:

1. **Data Integration (SSIS)** in Lab01
2. **Data Modeling and Cubes (SSAS Multidimensional)** in Lab02
3. **Reporting and Visualization (SSRS)** in Lab03

## Labs

| Lab | Topic | Description |
|-----|-------|-------------|
| [Lab01](Lab01/) | **SSIS ETL Packages** | Builds and organizes multiple SSIS packages for extraction, transformation, and loading between SQL Server databases |
| [Lab02](Lab02/) | **SSAS Dimensions and Cubes** | Designs dimensions, data source views, and multidimensional cubes with partitions for analytical querying |
| [Lab03](Lab03/) | **SSRS Reports and Shared Datasets** | Creates parameterized and tabular/matrix reports using shared data sources and reusable datasets |

## Learning Outcomes

After completing this BI folder, you should be able to:

1. Build ETL packages and manage connections for controlled data movement.
2. Model analytical structures (dimensions, cubes, partitions) for OLAP analysis.
3. Create operational and analytical reports using SSRS report projects.
4. Separate data preparation, analytical modeling, and presentation concerns.
5. Deploy BI artifacts to local SQL Server/SSRS environments for testing.

## Practice Focus

- **End-to-end BI flow**: from raw data movement to analysis and reporting.
- **Reusability**: shared datasets, shared data sources, and project-based organization.
- **Maintainability**: separating ETL, cube modeling, and report authoring by lab.

