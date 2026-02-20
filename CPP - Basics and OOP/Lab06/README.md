# Lab06

## Overview

A **Windows console application** written in C++ featuring a menu-driven TUI with three core features: **1D dynamic array**, **2D dynamic array (matrix)**, and an interactive **Line Editor**. It also introduces an `Employee` **class** with encapsulated fields and getter/setter methods (OOP foundations).

---

## Files

```
Lab06/
├── bin/
├── obj/
├── Lab06.cbp
├── Lab06.depend
├── Lab06.layout
└── main.cpp
```

---

## Employee Class

```cpp
class Employee {
    int id, age;
    char *name;
    double salary;
public:
    // Getters & Setters with basic validation
};
```

| Field    | Type      | Validation                  |
|----------|-----------|-----------------------------|
| `id`     | `int`     | Must be > 0                 |
| `age`    | `int`     | Must be > 0                 |
| `name`   | `char*`   | Length must be > 0           |
| `salary` | `double`  | Must be > 0                 |

- All setters perform **input validation** (reject non-positive values / empty names).
- Getters return `const`-qualified values.
- A `print_employee()` free function outputs an employee's data in the format: `[idx]. Id, Name, Age, Salary`.

---

## Navigation

| Key          | Action                                |
|--------------|---------------------------------------|
| `↑` / `↓`   | Move selection up / down              |
| `HOME`       | Jump to the first menu item           |
| `ENTER`      | Select the highlighted item           |
| `ESC`        | Exit the application / cancel         |

---

## Main Menu Options

| # | Option              | Description                                          |
|---|---------------------|------------------------------------------------------|
| 1 | **1D Dynamic Array** | Allocate, fill, and display a 1D `int` array         |
| 2 | **2D Dynamic Array** | Allocate, fill, and display a 2D `int` matrix        |
| 3 | **Line Editor**      | Interactive single-line text editor                  |
| 4 | **Exit**             | Terminates the application                           |

---

## Feature Details

### 1. 1D Dynamic Array

1. Prompts the user for a **positive integer size**.
2. Allocates an `int` array on the heap: `new int[_size]`.
3. Reads each element one by one.
4. Displays the entered array.
5. Properly frees memory with `delete[] arr`.

---

### 2. 2D Dynamic Array (Matrix)

1. Prompts for the number of **rows** and **columns** (both must be positive).
2. Allocates a **pointer-to-pointer** matrix:
   ```cpp
   int **matrix = new int*[rows];
   // each row: matrix[i] = new int[cols];
   ```
3. Reads the matrix row by row.
4. Displays the full matrix.
5. Frees memory correctly — deletes each row first, then the row-pointer array:
   ```cpp
   for (int i = 0; i < rows; ++i) delete[] matrix[i];
   delete[] matrix;
   ```

---

### 3. Line Editor

A fully interactive, **single-line text editor** in the console. The user specifies the maximum character length, then edits text in real-time.

#### Editor Controls

| Key            | Action                                              |
|----------------|-----------------------------------------------------|
| `←` / `→`     | Move cursor left / right                            |
| `HOME`         | Jump cursor to the beginning of the line            |
| `↑` / `↓`     | **Toggle case** of the character at cursor position |
| `Backspace`    | Delete the character **before** the cursor          |
| `Delete`       | Delete the character **at** the cursor              |
| `ENTER`        | Accept and finalize the text                        |
| `ESC`          | Cancel editing (discards the text)                  |
| Printable char | Insert at cursor, shifting existing text right      |

#### How It Works

- A `char*` buffer of size `_size + 1` is allocated and initialized with spaces.
- Characters are inserted at the cursor position by shifting all subsequent characters right.
- The line is rendered on each keystroke with a highlighted background (`textattr(0xf0)`).
- On **ENTER**, trailing spaces are trimmed and the final string is displayed.
- On **ESC**, the editor shows "Editor was cancelled." and discards input.
- The buffer is freed with `delete[]` after completion.

---

## Helper Functions

| Function | Description |
|---|---|
| `textattr(int i)` | Sets console text color using `SetConsoleTextAttribute`. |
| `gotoxy(int col, int line)` | Positions the cursor at `(X, Y)` using `SetConsoleCursorPosition`. |
| `print_employee(Employee* emp, int cnt)` | Prints an employee's fields: `Id, Name, Age, Salary`. |
| `print_menu(char menu[][COLS], int rows, int choiced, bool is_sub)` | Renders a menu with the highlighted selection. Sub-menus are offset to the right. |
| `convert(char* ch)` | Toggles a single character's case: `A↔a`, `B↔b`, etc. Uses ASCII offset (`±32`). |
| `delete_char(char* arr, int* cursor, int* len, bool is_backspace)` | Deletes a character by shifting subsequent characters left and replacing the last with a space. Handles both `Backspace` (before cursor) and `Delete` (at cursor). |

---

## Key Constants (Macros)

| Macro          | Value | Purpose                                  |
|----------------|-------|------------------------------------------|
| `EXTENDED_KEY` | `-32` | Sentinel for arrow/special keys          |
| `ENTER`        | `13`  | ASCII code for Enter                     |
| `ESC`          | `27`  | ASCII code for Escape                    |
| `BACKSPACE`    | `8`   | ASCII code for Backspace                 |
| `DELETE_KEY`   | `83`  | Scan code for Delete key                 |
| `HOME`         | `71`  | Scan code for Home key                   |
| `UP` / `DOWN`  | `72` / `80` | Scan codes for vertical arrow keys |
| `LEFT_ROW` / `RIGHT_ROW` | `75` / `77` | Scan codes for horizontal arrow keys |
| `BASE_ROWS`    | `4`   | Number of main menu items                |
| `ROWS`         | `15`  | Employee array capacity (unused in menu) |
| `COLS`         | `25`  | Max width for menu item labels           |

---
