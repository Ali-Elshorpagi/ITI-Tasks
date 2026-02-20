# Lab03

## Overview

A **menu-driven C++ console application** focusing on **arrays** (1D and 2D) and **C-style string manipulation**. Features include array operations (insert, print, max, min, sum, search, sort), 2D matrix operations (row sums, column averages), string concatenation, student name storage, and case conversion.

---

## Files

```
Lab03/
├── bin/
├── obj/
├── Lab03.cbp
├── Lab03.depend
├── Lab03.layout
└── main.cpp
```

---

## Key Constants (Macros)

| Macro    | Value | Purpose                                      |
|----------|-------|----------------------------------------------|
| `LEN`    | `10`  | Fixed size of the 1D array                   |
| `MAX`    | `100` | Max character length for string buffers      |
| `MAXLEN` | `205` | Buffer size for concatenated string result   |
| `ROWS`   | `3`   | Number of rows in the 2D matrix              |
| `COLS`   | `4`   | Number of columns in the 2D matrix           |

---

## Main Menu Options

| # | Option                     | Description                                               |
|---|----------------------------|-----------------------------------------------------------|
| 1 | **1D Array**               | Opens a sub-menu with 7 array operations                  |
| 2 | **2D Array**               | Opens a sub-menu with 4 matrix operations                 |
| 3 | **Concat Two Strings**     | Concatenates two strings into a third variable            |
| 4 | **Student Names**          | Stores and prints 3 student names                         |
| 5 | **Case Conversion**        | Converts all letters in a string (lowercase ↔ uppercase)  |
| 6 | **Exit**                   | Terminates the application                                |

---

## Feature Details

### 1. One-Dimensional Array (Sub-Menu)

A nested menu operating on a fixed `int arr[10]`:

| # | Sub-Option      | Description                                                        |
|---|-----------------|--------------------------------------------------------------------|
| 1 | **Insert**      | Fills all 10 elements from user input                              |
| 2 | **Print**       | Displays all 10 elements with their indices                        |
| 3 | **Maximum**     | Scans the array and returns the largest element                    |
| 4 | **Minimum**     | Scans the array and returns the smallest element                   |
| 5 | **Sum**         | Calculates the sum of all 10 elements                              |
| 6 | **Search**      | **Linear search** — finds the first occurrence and returns its index, or "not found" |
| 7 | **Bubble Sort** | Sorts the array in ascending order; includes an **early exit** optimization (`swapped` flag) |
| 8 | **Back**        | Returns to the main menu                                           |

#### Bubble Sort with Early Exit

```cpp
for (int i = 0; i < LEN - 1; ++i) {
    bool swapped = false;
    for (int j = 0; j < LEN - i - 1; ++j) {
        if (arr[j] > arr[j + 1])
            swap, swapped = true;
    }
    if (!swapped) break;  // already sorted — stop early
}
```

---

### 2. Two-Dimensional Array (Sub-Menu)

A nested menu operating on a fixed `int matrix[3][4]`:

| # | Sub-Option          | Description                                           |
|---|---------------------|-------------------------------------------------------|
| 1 | **Insert**          | Fills all 3×4 = 12 elements row by row                |
| 2 | **Print**           | Displays the matrix in a grid format                  |
| 3 | **Row Sums**        | Computes and prints the sum of each row               |
| 4 | **Column Averages** | Computes and prints the average of each column (`sum / ROWS`) |
| 5 | **Back**            | Returns to the main menu                              |

---

### 3. Concatenate Two Strings

- Reads two C-style strings (`char[]`).
- Uses `strcpy()` and `strcat()` from `<string.h>` to concatenate them with a space separator.
- Stores the result in a separate buffer (`concatStr[205]`).

---

### 4. Student Names

- Reads 3 student names into a `char students[3][100]` array.
- Prints all names in a numbered list.
- Demonstrates usage of a **2D character array** as an array of strings.

---

### 5. Case Conversion (Full String)

- Reads a C-style string.
- Iterates through every character:
  - `'a'–'z'` → subtract 32 → uppercase
  - `'A'–'Z'` → add 32 → lowercase
- Non-alphabetic characters are left unchanged.
- Prints the converted string.

---

## Key Concepts Demonstrated

- **1D array operations** — insert, print, max, min, sum, search, sort
- **2D array operations** — insert, print, row sums, column averages
- **Bubble Sort** — with early exit optimization via `swapped` flag
- **Linear search** — sequential scan returning index or "not found"
- **C-style strings** — `char[]`, `strcpy()`, `strcat()`, null terminator
- **2D char array** — used as an array of fixed-length strings
- **ASCII arithmetic** — `±32` for case conversion
- **Nested menus** — sub-menus with their own `while` loops

---
