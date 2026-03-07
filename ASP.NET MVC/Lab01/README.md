# Lab01 — ASP.NET MVC Basics

## Lab Description

This lab introduces the fundamentals of the **ASP.NET MVC** framework. It covers the MVC routing system, controllers, action methods, and passing data from controllers to views using `ViewBag`. A simple **Utility** web app is built that accepts two numbers from the user and performs arithmetic operations.

## Topics Covered

- MVC project structure & conventions
- Defining controllers and action methods
- Routing (`{controller}/{action}`) and HTTP verb attributes (`[HttpPost]`)
- HTML forms — `method`, `action`, and form data binding
- Passing data to views with `ViewBag`
- Returning different views from a single controller (`return View("Result")`)

## Project Structure

| File / Folder | Description |
|---|---|
| `Controllers/UtilityController.cs` | Controller with `ShowAddForm`, `Add`, and `Subtract` actions |
| `Controllers/HomeController.cs` | Default home controller |
| `Views/Utility/ShowAddForm.cshtml` | Form view — user inputs two numbers and chooses an operation |
| `Views/Utility/Result.cshtml` | Result view — displays the computed result via `ViewBag` |

## Key Concepts

| Concept | Details |
|---|---|
| **Controller** | `UtilityController` inherits from `Controller` |
| **[HttpPost]** | `Add` and `Subtract` actions handle POST requests from the form |
| **ViewBag** | `ViewBag.X`, `ViewBag.Y`, `ViewBag.Result`, `ViewBag.Operation` passed to the Result view |
| **Shared View** | Both `Add` and `Subtract` return the same `"Result"` view |
