#include <iostream>
#include <Windows.h>
#include <conio.h>

#define EXTENED_KEY -32
#define ENTER 13
#define HOME 71
#define DELETE_KEY 83
#define BACKSPACE 8
#define ESC 27
#define LEFT_ROW 75
#define RIGHT_ROW 77
#define UP 72
#define DOWN 80
#define BASE_ROWS 4
#define ROWS 15
#define COLS 25

using namespace std;

class Employee
{
    int id, age;
    char *name;
    double salary;

public:
    void setId(int _id)
    {
        if (_id > 0)
            id = _id;
    }
    const int getId()
    {
        return id;
    }
    void setAge(int _age)
    {
        if (_age > 0)
            age = _age;
    }
    const int getAge()
    {
        return age;
    }
    void setName(char *_name)
    {
        if (strlen(_name) > 0)
            name = _name;
    }
    const char *getName()
    {
        return name;
    }
    void setSalary(double _salary)
    {
        if (_salary > 0)
            salary = _salary;
    }
    const double getSalary()
    {
        return salary;
    }
    void print()
    {
    }
};

void textattr(int i);
void gotoxy(int column, int line);
void print_employee(Employee *emp, int cnt);
void print_menu(char menu[][COLS], int rows, int choiced, bool is_sub);
void convert(char *ch);
void delete_char(char *arr, int *cursor, int *len, bool is_backspase);

int main()
{
    system("cls");
    char menu[BASE_ROWS][COLS] = {"1D Dynamic Array", "2D Dynamic Array", "Line Editor", "Exit"};

    int choiced(0), flag(0);
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

        print_menu(menu, BASE_ROWS, choiced, false);

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
                    choiced = BASE_ROWS - 1;
                break;
            case DOWN:
                ++choiced;
                if (choiced > BASE_ROWS - 1)
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
                system("cls");

                gotoxy(15, 2);
                cout << "========================================";
                gotoxy(20, 3);
                cout << "  MAIN MENU => [ 1D Dynamic Array ]";
                gotoxy(15, 4);
                cout << "========================================";

                int _size(0);
                gotoxy(10, 6);
                cout << "Enter the size of the 1D dynamic array: ";
                cin >> _size;
                if (_size < 1)
                {
                    gotoxy(10, 8);
                    cout << "Size must be a positive integer.";
                    gotoxy(10, 11);
                    system("pause");
                }
                else
                {
                    int *arr(new int[_size]);
                    for (int i(0); i < _size; ++i)
                    {
                        gotoxy(10, 7 + i);
                        cout << "Enter element number " << i + 1 << ": ";
                        cin >> arr[i];
                    }
                    gotoxy(10, 8 + _size);
                    cout << "the array that you entered:  ";
                    for (int i(0); i < _size; ++i)
                    {
                        gotoxy(38 + i * 2, 8 + _size);
                        cout << arr[i];
                    }
                    delete[] arr;
                    gotoxy(10, 10 + _size);
                    system("pause");
                }
                break;
            }
            case 1:
            {
                system("cls");

                gotoxy(15, 2);
                cout << "========================================";
                gotoxy(20, 3);
                cout << "  MAIN MENU => [ 2D Dynamic Array ]";
                gotoxy(15, 4);
                cout << "========================================";
                int rows(0), cols(0);
                gotoxy(10, 6);
                cout << "Enter number of rows: ";
                cin >> rows;
                gotoxy(10, 7);
                cout << "Enter number of cols: ";
                cin >> cols;

                if (rows < 1 || cols < 1)
                {
                    gotoxy(10, 9);
                    cout << "Rows and columns must be positive integers.";
                    gotoxy(10, 11);
                    system("pause");
                }
                else
                {
                    int **matrix(new int *[rows]);
                    gotoxy(10, 9);
                    cout << "Enter the matrix row by row....\n";
                    for (int i(0); i < rows; ++i)
                    {
                        gotoxy(10, 11 + i);
                        cout << "row number " << i + 1 << ": ";
                        matrix[i] = new int[cols];
                        gotoxy(24, 11 + i);
                        for (int j(0); j < cols; ++j)
                            cin >> matrix[i][j];
                    }
                    gotoxy(10, 13 + rows);
                    cout << "the matrix that you entered:  ";
                    for (int i(0); i < rows; ++i)
                    {
                        gotoxy(10, 15 + rows + i);

                        for (int j(0); j < cols; ++j)
                            cout << matrix[i][j] << ' ';
                    }
                    for (int i(0); i < rows; ++i)
                        delete[] matrix[i];
                    delete[] matrix;

                    gotoxy(10, 17 + rows * 2);
                    system("pause");
                }
                break;
            }
            case 2:
            {
                system("cls");

                gotoxy(15, 2);
                cout << "========================================";
                gotoxy(20, 3);
                cout << "  MAIN MENU => [ Line Editor ]";
                gotoxy(15, 4);
                cout << "========================================";

                int _size(0);
                gotoxy(10, 6);
                cout << "Enter the size of characters: ";
                cin >> _size;
                if (_size < 1)
                {
                    gotoxy(10, 8);
                    cout << "Size must be a positive integer.";
                    gotoxy(10, 11);
                    system("pause");
                }
                else
                {
                    char *arr(new char[_size + 1]);
                    for (int i(0); i < _size; ++i)
                        arr[i] = ' ';
                    arr[_size] = '\0';

                    int cursor(0), len(0);
                    bool done(false);
                    char ch;

                    gotoxy(10, 8);
                    cout << "Edit line (max " << _size << " chars). Use arrows, Home, Backspace, Del. ENTER to accept, ESC to cancel.";
                    while (!done)
                    {
                        textattr(0xf0);
                        gotoxy(10, 10);
                        for (int i(0); i < _size; ++i)
                            cout << arr[i];
                        textattr(0x07);

                        gotoxy(10 + cursor, 10);

                        ch = getch();
                        if (ch == EXTENED_KEY)
                        {
                            ch = getch();
                            switch (ch)
                            {
                            case LEFT_ROW:
                                if (cursor > 0)
                                    --cursor;
                                break;
                            case RIGHT_ROW:
                                if (cursor < len)
                                    ++cursor;
                                break;
                            case DOWN:
                                convert(&arr[cursor - 1]);
                                break;
                            case UP:
                                convert(&arr[cursor - 1]);
                                break;
                            case HOME:
                                cursor = 0;
                                break;
                            case DELETE_KEY:
                                if (cursor < len)
                                    delete_char(arr, &cursor, &len, false);
                                break;
                            default:
                                break;
                            }
                        }
                        else
                        {
                            switch (ch)
                            {
                            case ESC:
                                done = true;
                                len = -1;
                                break;
                            case ENTER:
                                done = true;
                                break;
                            case BACKSPACE:
                                if (cursor > 0 && len > 0)
                                    delete_char(arr, &cursor, &len, true);
                                break;
                            default:
                                if (ch > 31 && ch < 127)
                                {
                                    if (len < _size)
                                    {
                                        for (int i(len); i > cursor; --i)
                                            arr[i] = arr[i - 1];
                                        arr[cursor] = ch;
                                        ++cursor, ++len;
                                    }
                                    else
                                    {
                                        gotoxy(10, 12);
                                        cout << "You have reached the max of char\n";
                                    }
                                }
                                break;
                            }
                        }
                    }

                    gotoxy(10, 13);
                    if (len == -1)
                        cout << "Editor was cancelled.";
                    else
                    {
                        int end_pos(len - 1);
                        while (end_pos > -1 && arr[end_pos] == ' ')
                            --end_pos;
                        arr[end_pos + 1] = '\0';

                        cout << "Final text: \"" << arr << "\"";
                    }

                    delete[] arr;
                    gotoxy(10, 15);
                    system("pause");
                }
                break;
            }
            case 3:
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

void print_employee(Employee *emp, int cnt)
{
    cout << "\t[" << (cnt + 1) << "]. "
         << emp->getId() << ", "
         << emp->getName() << ", "
         << emp->getAge() << ", "
         << emp->getSalary() << '\n';
}

void print_menu(char menu[][COLS], int rows, int choiced, bool is_sub)
{
    int base_x(20);
    if (is_sub)
        base_x += 40;

    for (int i(0); i < rows; ++i)
    {
        if (i == choiced)
            textattr(0xf4);
        gotoxy(base_x, 6 + i);
        cout << "\t[" << (i + 1) << "]. " << menu[i];
        textattr(0x07);
    }
}

void convert(char *ch)
{
    if (*ch >= 'A' && *ch <= 'Z')
        *ch += 32;
    else if (*ch >= 'a' && *ch <= 'z')
        *ch -= 32;
}

void delete_char(char *arr, int *cursor, int *len, bool is_backspase)
{
    if (is_backspase)
        --*cursor;

    for (int i(*cursor); i < *len - 1; ++i)
        arr[i] = arr[i + 1];

    arr[*len - 1] = ' ', --(*len);
}
