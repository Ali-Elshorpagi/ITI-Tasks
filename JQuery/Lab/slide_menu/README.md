# slide_menu

## Overview
This exercise demonstrates a lightweight interactive menu using hover expansion and click-based sub-item toggling.

## What It Does
- Expands the menu container on hover by adding/removing a CSS class
- Lets users click menu items (`Item 1`, `Item 2`) to toggle sub-items
- Inserts dynamic text (`Hello` or `Wow`) based on clicked item
- Removes the sub-item if clicked again (toggle behavior)

## Concepts Covered
- Hover handlers with separate enter/leave callbacks
- Class-based UI state changes (`.addClass()` / `.removeClass()`)
- Dynamic element creation and insertion (`$('<p>')`, `.insertAfter()`)
- Conditional DOM toggling based on element existence

## Files
- `index.html`: menu container and clickable items
- `style.css`: collapsed/expanded menu visuals
- `script.js`: hover and click interaction logic

## Expected Outcome
The menu responds to pointer movement and item clicks by showing/hiding contextual sub-items.
