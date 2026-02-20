#include <iostream>
#include <Windows.h>
#include <conio.h>

#define EXTENED_KEY -32
#define ENTER 13
#define HOME 71
#define ESC 27
#define LEFT_ROW 75
#define RIGHT_ROW 77
#define UP 72
#define DOWN 80
#define SUB_ROWS 3
#define BASE_ROWS 9
#define ROWS 15
#define COLS 20
#define MAX_COPY_SIZE 200

using namespace std;

void textattr(int i);
void gotoxy(int column, int line);
void operations();
void factorial();
void power();
void copy_string(char dest[], char src[], int dest_size);
int get_length(char str[]);
int get_fibonacci_num_by_idx(int idx);

int main()
{
    system("cls");
    char menu[BASE_ROWS][COLS] = {"New", "Display", "Operations", "Factorial", "Power", "Get Length", "Copy String", "Get Fibonacci Index", "Exit"};
    char names[ROWS][COLS] = {""};

    int choiced(0), flag(0), names_count(0);
    char ch;

    do
    {
        system("cls");

        gotoxy(15, 2);
        cout << "========================================";
        gotoxy(20, 3);
        cout << "  MAIN MENU";
        gotoxy(15, 4);
        cout << "========================================";

        for (int i(0); i < BASE_ROWS; ++i)
        {
            if (i == choiced)
                textattr(0xf4);
            gotoxy(20, 6 + i);
            cout << "\t[" << (i + 1) << "]. " << menu[i];
            textattr(0x07);
        }

        gotoxy(15, 16);
        cout << "Use UP/DOWN arrows, HOME to jump to New, ENTER to select, ESC to exit.";

        ch = getch();
        switch (ch)
        {
        case EXTENED_KEY:
        {
            ch = getch();
            switch (ch)
            {
            case UP:
                --choiced;
                if (choiced < 0)
                    choiced = 8;
                break;
            case DOWN:
                ++choiced;
                if (choiced > 8)
                    choiced = 0;
                break;
            case HOME:
                choiced = 0; // jump to "New"
                break;
            default:
                break;
            }
            break;
        }
        case ESC:
            flag = 1;
            break;
        case ENTER:
        {
            switch (choiced)
            {
            case 0: // new
            {
                int sub_choiced(0);
                char submenu[SUB_ROWS][COLS] = {"Add new name", "Print the names", "Back"};
                char sub_ch;

                do
                {
                    system("cls");
                    gotoxy(15, 2);
                    cout << "----------------------------------------";
                    gotoxy(18, 3);
                    cout << "  MAIN => NEW MENU";
                    gotoxy(15, 4);
                    cout << "----------------------------------------";

                    for (int i(0); i < SUB_ROWS; ++i)
                    {
                        if (i == sub_choiced)
                            textattr(0xf2);
                        gotoxy(18, 6 + i);
                        cout << "\t[" << (i + 1) << "]. " << submenu[i];
                        textattr(0x07);
                    }

                    gotoxy(15, 11);
                    cout << "Use UP/DOWN arrows, ENTER to choose, ESC to go back.";

                    sub_ch = getch();
                    if (sub_ch == EXTENED_KEY)
                    {
                        sub_ch = getch();
                        if (sub_ch == UP)
                        {
                            --sub_choiced;
                            if (sub_choiced < 0)
                                sub_choiced = SUB_ROWS - 1;
                        }
                        else if (sub_ch == DOWN)
                        {
                            ++sub_choiced;
                            if (sub_choiced > SUB_ROWS - 1)
                                sub_choiced = 0;
                        }
                        else if (sub_ch == LEFT_ROW)
                            break;
                    }
                    else if (sub_ch == ENTER)
                    {
                        if (!sub_choiced) // Add new name
                        {
                            system("cls");
                            gotoxy(10, 4);
                            cout << "----------------------------------------";
                            gotoxy(10, 5);
                            cout << "\tMAIN => NEW => [ Add New Name ]";
                            gotoxy(10, 6);
                            cout << "----------------------------------------";
                            gotoxy(10, 8);
                            cout << "Enter new name (max " << (COLS - 1) << " chars): ";

                            if (names_count < ROWS)
                            {
                                cin >> names[names_count];

                                if (strlen(names[names_count]) > 0)
                                {
                                    ++names_count;
                                    gotoxy(10, 10);
                                    cout << "\t>>> Name added successfully!";
                                }
                                else
                                {
                                    gotoxy(10, 10);
                                    cout << "\t>>> Empty input. Name not added.";
                                }
                            }
                            else
                            {
                                gotoxy(10, 10);
                                cout << "\t>>> Name list is full!";
                            }

                            gotoxy(10, 12);
                            cout << "Press any key to return...";
                            getch();
                        }
                        else if (sub_choiced == 1) // display all stored names
                        {
                            system("cls");
                            gotoxy(10, 3);
                            cout << "----------------------------------------";
                            gotoxy(10, 4);
                            cout << "\tMAIN => NEW => [ Stored Names ]";
                            gotoxy(10, 5);
                            cout << "----------------------------------------";

                            if (!names_count)
                            {
                                gotoxy(10, 8);
                                cout << "\tNo names to display.";
                            }
                            else
                            {
                                for (int i(0); i < names_count; ++i)
                                {
                                    gotoxy(10, 7 + i);
                                    cout << "\t[" << (i + 1) << "]. " << names[i];
                                }
                            }

                            gotoxy(10, 14);
                            cout << "Press any key to return...";
                            getch();
                        }
                        else if (sub_choiced == 2) // Back
                            break;
                    }
                    else if (sub_ch == ESC)
                        break;

                } while (true);

                break;
            }

            case 1: // display stored names
            {
                system("cls");
                gotoxy(10, 2);
                cout << "----------------------------------------";
                gotoxy(10, 3);
                cout << "\tMAIN => NEW => [ Display Names ]";
                gotoxy(10, 4);
                cout << "----------------------------------------";

                if (!names_count)
                {
                    gotoxy(10, 7);
                    cout << "\tNo names to display.";
                }
                else
                {
                    for (int i(0); i < names_count; ++i)
                    {
                        gotoxy(10, 6 + i);
                        cout << "\t[" << (i + 1) << "]. " << names[i];
                    }
                }

                gotoxy(10, 14);
                cout << "Press any key to return...";
                getch();
                break;
            }

            case 2:
            {
                system("cls");
                gotoxy(10, 2);
                cout << "----------------------------------------";
                gotoxy(10, 3);
                cout << "\tMAIN => NEW => [ Operations ]";
                gotoxy(10, 4);
                cout << "----------------------------------------";
                operations();
                break;
            }

            case 3:
            {
                system("cls");
                gotoxy(10, 2);
                cout << "----------------------------------------";
                gotoxy(10, 3);
                cout << "\tMAIN => NEW => [ Factorial ]";
                gotoxy(10, 4);
                cout << "----------------------------------------";
                factorial();
                break;
            }

            case 4:
            {
                system("cls");
                gotoxy(10, 2);
                cout << "----------------------------------------";
                gotoxy(10, 3);
                cout << "\tMAIN => NEW => [ Power ]";
                gotoxy(10, 4);
                cout << "----------------------------------------";
                power();
                break;
            }

            case 5: // get length of a string
            {
                system("cls");
                gotoxy(10, 2);
                cout << "----------------------------------------";
                gotoxy(10, 3);
                cout << "\tMAIN => NEW => [ Get Length of a String ]";
                gotoxy(10, 4);
                cout << "----------------------------------------";

                char str[COLS];
                gotoxy(10, 6);
                cout << "Enter a string (max " << (COLS - 1) << " chars): ";
                cin >> str;
                int len(get_length(str));
                gotoxy(10, 8);
                cout << "Length = " << len;
                gotoxy(10, 10);
                cout << "Press any key to continue...";
                getch();
                break;
            }

            case 6: // copy string to another
            {
                system("cls");
                gotoxy(10, 2);
                cout << "----------------------------------------";
                gotoxy(10, 3);
                cout << "\tMAIN => NEW => [ Copy String ]";
                gotoxy(10, 4);
                cout << "----------------------------------------";

                char src[MAX_COPY_SIZE], dest[MAX_COPY_SIZE];
                gotoxy(10, 6);
                cout << "Enter a string (max " << (MAX_COPY_SIZE - 1) << " chars): ";
                cin >> src;
                copy_string(dest, src, MAX_COPY_SIZE);
                gotoxy(10, 8);
                cout << "Copied String: \"" << dest << "\"";
                gotoxy(10, 10);
                cout << "Press any key to continue...";
                getch();
                break;
            }

            case 7:
            {
                system("cls");
                gotoxy(10, 2);
                cout << "----------------------------------------";
                gotoxy(10, 3);
                cout << "\tMAIN => NEW => [ Get Fibonacci Index ]";
                gotoxy(10, 4);
                cout << "----------------------------------------";

                int idx;
                gotoxy(10, 6);
                cout << "Enter Fibonacci index (non-negative integer): ";
                cin >> idx;
                int fib = get_fibonacci_num_by_idx(idx);
                gotoxy(10, 8);
                if (fib < 0)
                    cout << "Invalid index.";
                else
                    cout << "Fibonacci(" << idx << ") = " << fib;
                gotoxy(10, 10);
                cout << "Press any key to continue...";
                getch();
                break;
            }

            case 8:
                flag = 1;
                break;

            default:
                break;
            }
            break;
        }
        default:
            break;
        }
    } while (!flag);

    cout << "\n\n\tExiting the application. Goodbye!\n";
    return 0;
}

void textattr(int i)
{
    SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), i);
}

void gotoxy(int column, int line)
{
    COORD coord;
    coord.X = (SHORT)column;
    coord.Y = (SHORT)line;
    SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE), coord);
}

void power()
{
    int x, y;
    cout << "\n\tEnter X (base): ";
    cin >> x;
    cout << "\n\tEnter Y (power): ";
    cin >> y;
    if (y > -1)
    {
        int power(y);
        long long result(1), base(x);
        while (power)
        {
            if (power & 1)
                result *= base;
            power >>= 1;
            base *= base;
        }
        cout << "\n\t" << x << " ^ " << y << " = " << result << "\n\n";
    }

    system("pause");
}

void operations()
{
    double num1, num2;
    char operation;

    cout << "\n\tEnter two numbers and an arithmetic operation (+, -, *, /) respectively (e.g. 14 5 +) : ";
    cin >> num1 >> num2 >> operation;
    double result;
    switch (operation)
    {
    case '+':
        result = num1 + num2;
        cout << "\n\tThe result is: " << result << '\n';
        break;
    case '-':
        result = num1 - num2;
        cout << "\n\tThe result is: " << result << '\n';
        break;
    case '*':
        result = num1 * num2;
        cout << "\n\tThe result is: " << result << '\n';
        break;
    case '/':
        if (num2 != 0)
        {
            result = num1 / num2;
            cout << "\n\tThe result is: " << result << '\n';
        }
        else
            cout << "\n\tError: Division by zero!\n";
        break;
    default:
        cout << "\n\tInvalid operation!\n";
        break;
    }
    system("pause");
}

void factorial()
{
    int n;
    cout << "\n\tEnter a non-negative integer: ";
    cin >> n;
    if (n < 0)
    {
        cout << "\n\tError: Factorial is not defined for negative numbers!\n";
        system("pause");
        return;
    }
    long long result(1);
    for (int i(2); i <= n; ++i)
        result *= i;

    cout << "\n\tThe factorial of " << n << " is: " << result << "\n\n";
    system("pause");
}

int get_length(char str[])
{
    int length(0);
    while (str[length] != '\0')
        ++length;
    return length;
}

void copy_string(char dest[], char src[], int dest_size)
{
    int i(0);
    while (src[i] != '\0' && i < dest_size)
    {
        dest[i] = src[i];
        ++i;
    }
    dest[i] = '\0';
}

int get_fibonacci_num_by_idx(int idx)
{
    if (idx < 0)
        return -1;
    if (!idx)
        return 0;
    if (idx == 1)
        return 1;

    int a(0), b(1), fib(0);
    for (int i(2); i <= idx; ++i)
    {
        fib = a + b;
        a = b;
        b = fib;
    }
    return fib;
} 