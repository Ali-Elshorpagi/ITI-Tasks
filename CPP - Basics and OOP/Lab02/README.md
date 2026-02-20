# Lab02

## Overview

A **menu-driven C++ console application** covering intermediate programming exercises: **primality testing**, **grade classification**, **number manipulation** (reverse, binary), **fast exponentiation**, and a **Magic Square** generator using `gotoxy()` for console positioning.

---

## Files

```
Lab02/
├── bin/
├── obj/
├── Lab02.cbp
├── Lab02.depend
├── Lab02.layout
└── main.cpp
```

---

## Main Menu Options

| #  | Option                          | Description                                           |
|----|---------------------------------|-------------------------------------------------------|
| 1  | **Prime Check**                 | Determines if a number is prime                       |
| 2  | **Grade Classifier**            | Maps a numeric degree to a letter grade               |
| 3  | **Sum of 5 Numbers**            | Sums five user-input integers                         |
| 4  | **Word & Character Counter**    | Counts words and non-space characters in a text line  |
| 5  | **Factorial**                   | Computes `n!` iteratively                             |
| 6  | **Power (X ^ Y)**               | Computes X raised to Y using fast exponentiation      |
| 7  | **Reverse a Number**            | Reverses the digits of an integer                     |
| 8  | **Decimal to Binary**           | Converts a positive integer to its binary string      |
| 9  | **Magic Square**                | Generates an N×N magic square (odd N only)            |
| 10 | **Exit**                        | Terminates the application                            |

---

## Feature Details

### 1. Prime Check

- Tests divisibility from `2` up to `√n` (optimized: `i * i <= n`).
- Numbers less than 2 are immediately classified as non-prime.

---

### 2. Grade Classifier

| Degree Range | Grade |
|-------------|-------|
| 90–100      | A     |
| 80–89       | B     |
| 70–79       | C     |
| 60–69       | D     |
| Below 60    | F     |

---

### 3. Sum of 5 Numbers

- Uses a `for` loop to read 5 integers and accumulate their sum.

---

### 4. Word & Character Counter

- Reads a full line using `getline()` (with `cin.ignore()` to clear the buffer).
- Iterates character by character:
  - Counts **non-space characters**.
  - Tracks transitions from space → non-space to count **words**.
- Handles multiple consecutive spaces correctly.

---

### 5. Factorial

- Computes `n!` iteratively using `long long` for large results.
- Rejects negative numbers with an error message.

---

### 6. Power (Fast Exponentiation)

Uses the **binary exponentiation** algorithm:

```
3^5 = 3^(101₂) = 3^4 × 3^1 = 243
```

- Checks each bit of the exponent (`power & 1`).
- Squares the base each iteration (`base *= base`).
- Time complexity: **O(log Y)**.
- Only supports non-negative exponents.

---

### 7. Reverse a Number

- Extracts digits using `% 10` and builds the reversed number with `reversed * 10 + digit`.
- Handles **negative numbers** — saves the sign, reverses the absolute value, then restores the sign.

---

### 8. Decimal to Binary

- Extracts bits using `% 2` and right-shifts (`>>= 1`).
- Builds the binary string in reverse, then reverses it for correct MSB-first order.
- Handles `0` as a special case.
- Rejects negative numbers.

---

### 9. Magic Square

Generates an **N×N magic square** for odd positive N using the **Siamese method**:

1. Start at the middle column, top row.
2. For each subsequent number:
   - If the previous number is a multiple of N → move **down**.
   - Otherwise → move **diagonally up-left** (with wrapping).
3. Uses `gotoxy()` to place each number at the correct console position.

**Property**: Every row, column, and diagonal sums to the same **magic constant** = `N × (N² + 1) / 2`.

---

## Helper Function

| Function                   | Description                                               |
|----------------------------|-----------------------------------------------------------|
| `gotoxy(int column, int line)` | Positions the console cursor at `(X, Y)` using `SetConsoleCursorPosition` (Windows API). |

---

## Key Concepts Demonstrated

- **Optimized primality test** — `O(√n)` trial division
- **Binary (fast) exponentiation** — `O(log n)`
- **Digit manipulation** — reverse, binary conversion
- **String processing** — word/character counting with `getline()`
- **Magic square algorithm** — Siamese method with modular arithmetic
- **Console positioning** — `gotoxy()` for 2D console output

---
