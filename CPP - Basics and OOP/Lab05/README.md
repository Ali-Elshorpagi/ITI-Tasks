# Lab05

## Overview

A **Windows console-based Employee Management System** written in C++. The application uses a keyboard-navigable TUI (Text User Interface) and allows the user to **add**, **view**, **delete**, and manage `Employee` records stored in a dynamically allocated array. It also includes a **pointer-based integer swap** utility.

---

## Files

```
Lab05/
├── bin/
├── obj/
├── Lab05.cbp
├── Lab05.depend
├── Lab05.layout
└── main.cpp
```

---

## Data Structure

```cpp
struct Employee {
    int  Id;      // default: -1 (empty slot)
    char Name[50]; // employee name
    int  Age;     // employee age
};
```

### Slot State Convention

| `Id` Value | Meaning                          |
|-----------|----------------------------------|
| `-1`      | Slot is **empty** (never used)   |
| `-2`      | Slot is **soft-deleted**         |
| Any other | Active employee record           |

The application uses **soft deletion** — deleted employees are marked with `Id = -2` instead of being physically removed, which allows a dedicated "Deleted Employees" view.

---

## Navigation

The app uses keyboard input (`getch()`) for menu navigation:

| Key        | Action                                  |
|------------|-----------------------------------------|
| `↑` / `↓` | Move selection up / down                |
| `HOME`     | Jump directly to the first menu item    |
| `ENTER`    | Select the highlighted item             |
| `ESC`      | Exit the application / go back          |
| `←`        | Go back (in sub-menus)                  |

The currently selected menu item is rendered with a highlighted color using `textattr()`.

---

## Main Menu Options

| # | Option                | Description                                          |
|---|-----------------------|------------------------------------------------------|
| 1 | **New**               | Sub-menu to add or view an employee by slot index    |
| 2 | **Display All Employees** | Lists all active (non-deleted, non-empty) employees |
| 3 | **Delete by Id**      | Soft-deletes an employee by their `Id`               |
| 4 | **Delete by Name**    | Soft-deletes an employee by their `Name`             |
| 5 | **Deleted Employees** | Shows all soft-deleted employee records              |
| 6 | **Swap Two Integers** | Swaps two integers using pointers                    |
| 7 | **Exit**              | Frees memory and exits the application               |

---

## Feature Details

### 1. New (Employee Sub-Menu)

A nested sub-menu with two options:

#### ➕ Add New Employee
- Prompts the user for a **1-based slot index** (1–15).
- **If the slot is active** → warns the user and asks for confirmation (`Y/N`) before overriding.
- **If the slot is soft-deleted** (`Id == -2`) → warns the user that it was previously deleted, then allows adding.
- **If the slot is empty** (`Id == -1`) → directly calls `add_employee()`.
- Invalid indices are rejected with an error message.

#### 👁 Display an Employee (sub-option)
- Iterates through all 15 slots.
- Displays only **active** records (i.e., `Id != -1` and `Id != -2`).

---

### 2. Display All Employees

- Directly accessible from the main menu.
- Shows all records where `Id != -1` (includes soft-deleted entries with their deleted state visible).

---

### 3. Delete by Id

- Prompts for an employee `Id`.
- Scans the array linearly for a match.
- On match: sets `Id = -2` (soft delete), decrements `employees_count`.
- Shows a success or "not found" message.

---

### 4. Delete by Name

- Prompts for an employee `Name`.
- Scans the array linearly for a name match using `strcmp(ptr[i].Name, delete_name) == 0`.
- On match: sets `Id = -2` (soft delete), decrements `employees_count`.

---

### 5. Deleted Employees

- Iterates through all 15 slots.
- Displays only records where `Id == -2`.
- Note: deleted employees still retain their `Name` and `Age` fields, making them recoverable in principle.

---

### 6. Swap Two Integers

- Reads two integers from the user.
- Passes them **by pointer** to `_swap()`.
- Prints both values after the swap.

---

## Helper Functions

### `add_employee(Employee* emp)`
Fills an `Employee` struct's fields (`Id`, `Name`, `Age`) by reading from the user via `cin`.

### `print_employee(Employee* emp, int idx)`
Prints a single employee's data in the format:
```
[slot_number]. Id, Name, Age
```

### `print_menu(char menu[][COLS], int rows, int choiced, bool is_sub)`
Renders a menu to the console:
- `choiced` → index of the currently highlighted item.
- `is_sub` → if `true`, renders the menu further to the right (offset by 40 columns) so main and sub-menus can appear side by side.

### `textattr(int i)`
Sets the Windows console text color via `SetConsoleTextAttribute`.

### `gotoxy(int col, int line)`
Moves the cursor to a specific `(X, Y)` position via `SetConsoleCursorPosition`.

### `_swap(int* first, int* second)`
Classic **three-variable pointer swap** using a temporary variable.

---

## Memory Management

- The employee array is allocated dynamically on the heap:
  ```cpp
  Employee* ptr = new Employee[ROWS]; // ROWS = 15
  ```
- It is properly freed at exit:
  ```cpp
  delete[] ptr;
  ```

---

## Key Constants (Macros)

| Macro          | Value | Purpose                                        |
|----------------|-------|------------------------------------------------|
| `EXTENDED_KEY` | `-32` | Sentinel value for arrow/special keys          |
| `ENTER`        | `13`  | ASCII code for Enter                           |
| `ESC`          | `27`  | ASCII code for Escape                          |
| `HOME`         | `71`  | Scan code for Home key                         |
| `UP` / `DOWN`  | `72` / `80` | Scan codes for vertical arrow keys     |
| `LEFT_ROW`     | `75`  | Scan code for left arrow (go back)             |
| `RIGHT_ROW`    | `77`  | Scan code for right arrow                      |
| `SUB_ROWS`     | `3`   | Number of items in the New sub-menu            |
| `BASE_ROWS`    | `7`   | Number of items in the main menu               |
| `ROWS`         | `15`  | Maximum number of employee slots               |
| `COLS`         | `25`  | Max character width for menu item labels       |

---