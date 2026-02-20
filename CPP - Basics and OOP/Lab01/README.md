# Lab01

## Overview

A **menu-driven C++ console application** introducing fundamental C++ concepts: **ASCII encoding**, **arithmetic operations**, and **character case conversion**. All features are accessed through a numbered menu in a `while` loop.

---

## Files

```
Lab01/
├── bin/
├── obj/
├── Lab01.cbp
├── Lab01.depend
├── Lab01.layout
└── main.cpp
```

---

## Main Menu Options

| # | Option                         | Description                                         |
|---|--------------------------------|-----------------------------------------------------|
| 1 | **Get ASCII code of a char**   | Reads a character and prints its integer ASCII value |
| 2 | **Get char from its integer**  | Reads an integer and prints the corresponding ASCII character |
| 3 | **Arithmetic operation**       | Performs `+`, `-`, `*`, `/` on two numbers           |
| 4 | **Convert char case**          | Converts lowercase ↔ uppercase for a single character |
| 5 | **Exit**                       | Terminates the application                          |

---

## Feature Details

### 1. Get ASCII Code of a Character

- Reads a single `char` from the user.
- Uses `int(character)` casting to display its ASCII code.
- Example: `'A'` → `65`

---

### 2. Get Character from ASCII Code

- Reads an integer from the user.
- Uses `char(ascii_Code)` casting to display the corresponding character.
- Example: `65` → `'A'`

---

### 3. Arithmetic Operation

- Reads two `double` operands and a `char` operator (`+`, `-`, `*`, `/`).
- Uses a `switch` statement to perform the selected operation.
- **Division by zero** guard: prints an error and `continue`s back to the menu.
- **Invalid operator** guard: prints error and `continue`s.

---

### 4. Convert Character Case

- Reads a single character.
- Uses ASCII arithmetic (`±32`) for case conversion:
  - `'a'–'z'` → subtract 32 → `'A'–'Z'`
  - `'A'–'Z'` → add 32 → `'a'–'z'`
- Rejects non-alphabetic characters with an error message.

---

## Key Concepts Demonstrated

- **Type casting** — `int()` and `char()` for ASCII ↔ character conversion
- **Switch statement** — for menu and operator selection
- **While loop** — menu-driven program loop
- **ASCII arithmetic** — `±32` for case conversion
- **Input validation** — division by zero, invalid operator, non-alphabetic character

---
