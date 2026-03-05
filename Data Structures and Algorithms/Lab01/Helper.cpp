#ifndef HELPER_CPP
#define HELPER_CPP

#include <iostream>
#include <Windows.h>

#define SUB_ROWS 4
#define BASE_ROWS 6
#define COLS 25
#define EXTENED_KEY -32
#define ENTER 13
#define ESC 27
#define UP 72
#define DOWN 80

class Helper
{
public:
    static void textattr(int i)
    {
        SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), i);
    }
    static void gotoxy(int column, int line)
    {
        COORD coord;
        coord.X = (SHORT)column;
        coord.Y = (SHORT)line;
        SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE), coord);
    }
    static void print_menu(char menu[][COLS], int rows, int choiced, bool is_sub)
    {
        int base_x(20);
        if (is_sub)
            base_x += 50;

        for (int i(0); i < rows; ++i)
        {
            if (i == choiced)
                textattr(0xf4);
            gotoxy(base_x, 6 + i);
            std::cout << "\t[" << (i + 1) << "]. " << menu[i];
            textattr(0x07);
        }
    }
};
#endif