# Lab04

## Overview

A comprehensive **console-based menu-driven application** written in C++ (Windows-specific). It provides an interactive keyboard-navigable main menu and a set of utility operations including arithmetic, string manipulation, and mathematical computations.

---

## Files

```
Lab04/
├── bin/
├── obj/
├── Lab04.cbp
├── Lab04.depend
├── Lab04.layout
└── main.cpp
```

---

## How It Works

### Navigation

The application uses a **keyboard-driven TUI (Text User Interface)**:

| Key         | Action                              |
|-------------|-------------------------------------|
| `↑` / `↓`  | Move selection up / down            |
| `HOME`      | Jump directly to the first item     |
| `ENTER`     | Select the highlighted menu item    |
| `ESC`       | Exit the application / go back      |
| `←`         | Go back (in sub-menus)              |

The currently highlighted menu item is rendered with a distinct color (`textattr`) to indicate focus.

---

## Main Menu Options

| #  | Option               | Description                                      |
|----|----------------------|--------------------------------------------------|
| 1  | **New**              | Opens a sub-menu to add or print stored names    |
| 2  | **Display**          | Displays all currently stored names              |
| 3  | **Operations**       | Performs arithmetic on two numbers               |
| 4  | **Factorial**        | Computes the factorial of a non-negative integer |
| 5  | **Power**            | Computes X raised to the power Y (fast exponentiation) |
| 6  | **Get Length**       | Returns the character count of an input string   |
| 7  | **Copy String**      | Copies one string into another manually          |
| 8  | **Get Fibonacci Index** | Returns the Fibonacci number at a given index |
| 9  | **Exit**             | Terminates the application                       |

---

## Feature Details

### 1. New (Name Manager Sub-Menu)

A nested sub-menu with two options:
- **Add New Name** — Reads a name from the user and stores it in a fixed-size array (`ROWS = 15`, max `COLS - 1 = 19` characters per name). Rejects empty input and alerts when the list is full.
- **Print the Names** — Displays all currently stored names in a numbered list.

### 2. Display

Directly accessible from the main menu; shows all names stored in the session.

### 3. Operations

Prompts for two `double` numbers and an arithmetic operator (`+`, `-`, `*`, `/`).
- Performs the selected operation and prints the result.
- Guards against **division by zero**.

### 4. Factorial

Computes `n!` iteratively for any non-negative integer `n`.
- Returns an error message for negative inputs.
- Result stored as `long long` to handle larger values.

### 5. Power

Computes `X ^ Y` using the **fast (binary) exponentiation** algorithm:
- Works by checking each bit of the exponent.
- Time complexity: **O(log Y)** instead of naïve O(Y).
- Only supports non-negative exponents (`Y >= 0`).

### 6. Get Length

A manual reimplementation of `strlen`:
- Iterates through the character array until `'\0'` is reached.
- Returns the count as the string length.

### 7. Copy String

A manual reimplementation of `strcpy` with a safe size bound:
- Copies characters from `src` to `dest` one by one.
- Stops at `'\0'` or when `dest_size` is reached.
- Always null-terminates the destination buffer.

### 8. Get Fibonacci Index

Returns the Fibonacci number at a given zero-based index using **iterative computation**:
- Base cases: `F(0) = 0`, `F(1) = 1`.
- Returns `-1` for negative indices (used as an error sentinel).

---

## Utility Functions

| Function | Description |
|---|---|
| `textattr(int i)` | Sets the Windows console foreground/background color using `SetConsoleTextAttribute`. |
| `gotoxy(int col, int line)` | Positions the console cursor at a specific `(X, Y)` coordinate using `SetConsoleCursorPosition`. |

---

## Key Constants (Macros)

| Macro | Value | Purpose |
|---|---|---|
| `EXTENDED_KEY` | `-32` | Sentinel for arrow/special keys from `getch()` |
| `ENTER` | `13` | ASCII code for Enter key |
| `ESC` | `27` | ASCII code for Escape key |
| `HOME` | `71` | Scan code for Home key |
| `UP` / `DOWN` | `72` / `80` | Scan codes for arrow keys |
| `LEFT_ROW` / `RIGHT_ROW` | `75` / `77` | Scan codes for left/right arrow keys |
| `SUB_ROWS` | `3` | Number of items in the New sub-menu |
| `BASE_ROWS` | `9` | Number of items in the main menu |
| `ROWS` | `15` | Maximum number of names that can be stored |
| `COLS` | `20` | Maximum character length per name (including null terminator) |
| `MAX_COPY_SIZE` | `200` | Buffer size for the Copy String feature |

---