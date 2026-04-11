# todo_list

## Overview
This exercise creates a basic interactive to-do list with add and delete actions.

## What It Does
- Reads task text from an input field
- Validates input to prevent empty tasks
- Appends each task as a list item with a delete button
- Removes tasks using delegated click handling

## Concepts Covered
- Form-like input handling with jQuery
- Input validation and early return logic
- Dynamic list item creation and composition
- Event delegation using `.on('click', '.deleteBtn', ...)`

## Files
- `index.html`: input, add button, and task list container
- `style.css`: task list styling
- `script.js`: add/delete behavior

## Expected Outcome
Users can add tasks instantly and delete any task from the list without reloading the page.
