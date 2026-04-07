# Lab01

## Lab Description
This lab focuses on **SSIS (SQL Server Integration Services)** package development for ETL workflows.

The project includes multiple packages (`Task01.dtsx` to `Task05.dtsx`) under one SSIS project, allowing you to practice different integration scenarios in a structured way.

## Project Structure

| Item | Description |
|------|-------------|
| `Lab.slnx` | Solution container for the lab |
| `Task01/Tasks.dtproj` | SSIS project definition |
| `Task01/Task01.dtsx ... Task05.dtsx` | Individual ETL packages |
| `Task01/Project.params` | Project-level parameters for configurable execution |

## What This Lab Demonstrates
- Building SSIS projects with multiple executable packages
- Managing SQL Server connection managers and project parameters
- Executing package-based ETL tasks for data movement and transformation
- Organizing package versions and entry points within one deployment model

## Learning Objectives
- Understand SSIS project/package structure
- Configure connections for local SQL Server environments
- Build repeatable ETL flows as separate task packages
- Use parameters to avoid hard-coded runtime values

## Typical Workflow
1. Open `Lab.slnx` in Visual Studio with SSIS support.
2. Inspect or edit package logic in each `.dtsx` file.
3. Configure connection settings and parameters.
4. Execute packages in debug mode and validate outputs.

## Expected Outcome
You can run and maintain a multi-package SSIS project that performs ETL operations in a controlled, reusable way.
