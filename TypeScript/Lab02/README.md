# Lab02

## Lab Description
This lab builds a small interactive counter app using TypeScript class syntax and DOM event handling.

## Exercise Summary
The solution introduces a `Counter` class that manages state (`counter`) and exposes three actions:
- increment value
- decrement value
- reset value to zero

Button click events are wired to class methods, then reflected in the page by updating the counter text.

## Core Concepts Practiced
- Class declaration and object instantiation in TypeScript
- Encapsulation of mutable state in a dedicated class
- DOM element querying with null checks
- Event listeners for UI actions (`click`)
- Dynamic text updates with `textContent`

## Files
| File | Role |
|------|------|
| `Lab.ts` | Counter class and event binding logic |
| `Lab.js` | JavaScript compiled from `Lab.ts` |
| `index.html` | UI structure for counter and control buttons |
| `style.css` | Visual styling for card layout and buttons |

## Logic Breakdown
1. Create `Counter` class with `increment()`, `decrement()`, and `reset()` methods.
2. Instantiate a counter object.
3. Select counter display and control buttons from DOM.
4. Attach click handlers to each button.
5. Update displayed value after every state change.

## Typical Workflow
1. Edit behavior in `Lab.ts` (for example, add boundaries or step size).
2. Compile TypeScript and refresh the page.
3. Test increase, decrease, and reset actions from the UI.
4. Confirm counter display always matches internal state.

## Expected Outcome
Users can interact with the buttons to control the counter value in real time with immediate UI feedback.
