#include <Windows.h>
#include <conio.h>
#include <iostream>

#define EXTENED_KEY -32
#define ENTER 13
#define HOME 71
#define ESC 27
#define LEFT_ROW 75
#define RIGHT_ROW 77
#define UP 72
#define DOWN 80
#define SUB_ROWS 3
#define BASE_ROWS 7
#define ROWS 15
#define COLS 25

using namespace std;

struct Employee
{
  int Id{-1};
  char Name[50]{-1};
  int Age{-1};
};

void textattr(int i);
void gotoxy(int column, int line);
void print_employee(Employee *emp, int idx);
void print_menu(char menu[][COLS], int rows, int choiced, bool is_sub);
void add_employee(Employee *emp);
void _swap(int *first, int *second);

int main()
{
  system("cls");
  char menu[BASE_ROWS][COLS] = {"New",
                                "Display all employees",
                                "Delete  by Id",
                                "Delete by Name",
                                "Deleted Employees",
                                "Swap two integers",
                                "Exit"};
  Employee *ptr = new Employee[ROWS];

  int choiced(0), flag(0), employees_count(0);
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
    cout << "Use UP/DOWN arrows, HOME to jump to New, ENTER to select, ESC to "
            "exit";

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
        int sub_choiced(0);
        char submenu[SUB_ROWS][COLS] = {"Add new employee",
                                        "Display an employee", "Back"};
        char sub_ch;

        do
        {
          // system("cls");
          gotoxy(15, 2);
          cout << "----------------------------------------";
          gotoxy(18, 3);
          cout << "  MAIN => NEW MENU";
          gotoxy(15, 4);
          cout << "----------------------------------------";

          print_menu(submenu, SUB_ROWS, sub_choiced, true);

          // gotoxy(15, 11);
          // cout << "Use UP/DOWN arrows, ENTER to choose, ESC to go back.";

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
            if (!sub_choiced) // Add new employee
            {
              system("cls");
              gotoxy(10, 4);
              cout << "----------------------------------------";
              gotoxy(10, 5);
              cout << "\tMAIN => NEW => [ Add New Employee ]";
              gotoxy(10, 6);
              cout << "----------------------------------------";
              gotoxy(10, 8);
              cout << "Enter the index of place that you want the employee "
                      "added (1 - 15): ";
              int idx;
              cin >> idx;
              if (idx < ROWS)
              {
                if (ptr[idx - 1].Id != -1 && ptr[idx - 1].Id != -2)
                {
                  gotoxy(10, 10);
                  cout << "\n\t Warning: An employee already exists at this "
                          "index. Do you want to override [Y/N]: ";
                  char override_choice;
                  cin >> override_choice;
                  if (override_choice == 'Y' || override_choice == 'y')
                  {
                    add_employee(&ptr[idx - 1]);
                    ++employees_count;
                  }
                  else if (override_choice == 'N' || override_choice == 'n')
                  {
                    gotoxy(10, 12);
                    cout << "Press any key to return...";
                    getch();
                    continue;
                  }
                  else
                  {
                    gotoxy(10, 12);
                    cout << "Invalid choice. Press any key to return...";
                    getch();
                    continue;
                  }
                }
                else if (ptr[idx - 1].Id == -2)
                {
                  gotoxy(10, 8);
                  cout << "\n\t Warning: An employee already exists at this "
                          "index but deleted.";
                  add_employee(&ptr[idx - 1]);
                  ++employees_count;
                }
                else
                {
                  add_employee(&ptr[idx - 1]);
                  ++employees_count;
                }
              }
              else
              {
                gotoxy(10, 10);
                cout << "\n\t Invalid index.";
              }
              gotoxy(10, 17);
              cout << "Press any key to return...";
              getch();
              system("cls");
            }
            else if (sub_choiced == 1) // display all stored employees
            {
              system("cls");
              gotoxy(10, 3);
              cout << "----------------------------------------";
              gotoxy(10, 4);
              cout << "\tMAIN => NEW => [ Stored employees ]";
              gotoxy(10, 5);
              cout << "----------------------------------------";

              if (!employees_count)
              {
                gotoxy(10, 8);
                cout << "\tNo employees to display.";
              }
              else
              {
                for (int i(0); i < ROWS; ++i)
                {
                  if (ptr[i].Id != -1 && ptr[i].Id != -2)
                  {
                    gotoxy(10, 7 + i);
                    print_employee(&ptr[i], i);
                  }
                }
              }
              gotoxy(10, 20);
              cout << "Press any key to return...";
              getch();
              system("cls");
            }
            else if (sub_choiced == 2) // Back
              break;
          }
          else if (sub_ch == ESC)
            break;

        } while (true);

        break;
      }
      case 1: // display stored employees
      {
        system("cls");
        gotoxy(10, 2);
        cout << "----------------------------------------";
        gotoxy(10, 3);
        cout << "\tMAIN => NEW => [ Display employees ]";
        gotoxy(10, 4);
        cout << "----------------------------------------";

        if (!employees_count)
        {
          gotoxy(10, 8);
          cout << "\tNo employees to display.";
        }
        else
        {
          for (int i(0); i < ROWS; ++i)
          {
            if (ptr[i].Id != -1)
            {
              gotoxy(10, 5 + i);
              print_employee(&ptr[i], i);
            }
          }
        }
        gotoxy(10, 20);
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
        cout << "\tMAIN => [ Delete By Id ]";
        gotoxy(10, 4);
        cout << "----------------------------------------";
        int delete_id;
        gotoxy(10, 6);
        cout << "Enter Employee Id to delete: ";
        cin >> delete_id;
        bool found(false);
        for (int i(0); i < ROWS; ++i)
        {
          if (ptr[i].Id == delete_id)
          {
            ptr[i].Id = -2, found = true, --employees_count;
            gotoxy(10, 8);
            cout << "\t>>> Employee with Id " << delete_id
                 << " deleted successfully!";
            break;
          }
        }
        if (!found)
        {
          gotoxy(10, 8);
          cout << "\t>>> Employee with Id " << delete_id << " not found!";
        }
        gotoxy(10, 12);
        cout << "Press any key to return...";
        getch();

        break;
      }
      case 3:
      {
        system("cls");
        gotoxy(10, 2);
        cout << "----------------------------------------";
        gotoxy(10, 3);
        cout << "\tMAIN => [ Delete By Name ]";
        gotoxy(10, 4);
        cout << "----------------------------------------";
        char delete_name[50];
        gotoxy(10, 6);
        cout << "Enter Employee Name to delete: ";
        cin >> delete_name;
        bool found(false);
        for (int i(0); i < ROWS; ++i)
        {
          if (strcmp(ptr[i].Name, delete_name) == 0)
          {
            ptr[i].Id = -2, found = true, --employees_count;
            gotoxy(10, 30);
            cout << "\t>>> Employee with Name " << delete_name
                 << " deleted successfully!";
            break;
          }
        }
        if (!found)
        {
          gotoxy(10, 8);
          cout << "\t>>> Employee with Name " << delete_name << " not found!";
        }
        gotoxy(10, 12);
        cout << "Press any key to return...";
        getch();

        break;
      }
      case 4:
      {
        system("cls");
        gotoxy(10, 2);
        cout << "----------------------------------------";
        gotoxy(10, 3);
        cout << "\tMAIN => [ Deleted Employees ]";
        gotoxy(10, 4);
        cout << "----------------------------------------";
        bool found(false);
        for (int i(0); i < ROWS; ++i)
        {
          if (ptr[i].Id == -2)
          {
            found = true;
            gotoxy(10, 7 + i);
            print_employee(&ptr[i], i);
          }
        }
        if (!found)
        {
          gotoxy(10, 8);
          cout << "\tNo deleted employees to display.";
        }
        gotoxy(10, 12);
        cout << "Press any key to return...";
        getch();

        break;
      }
      case 5:
      {
        system("cls");
        gotoxy(10, 2);
        cout << "----------------------------------------";
        gotoxy(10, 3);
        cout << "\t MAIN => [ Swap Two Integers ]";
        gotoxy(10, 4);
        cout << "----------------------------------------";
        int first(0), second(0);
        gotoxy(10, 6);
        cout << "Enter the first number: ";
        cin >> first;
        gotoxy(10, 7);
        cout << "Enter the second number: ";
        cin >> second;
        _swap(&first, &second);
        gotoxy(10, 10);
        cout << "First Number: " << first << ", Second Number: " << second
             << '\n';

        gotoxy(10, 12);
        cout << "Press any key to return...";
        getch();

        break;
      }
      case 6:
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
  delete[] ptr;
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

void print_employee(Employee *emp, int idx)
{
  cout << "\t[" << (idx + 1) << "]. " << emp->Id << ", " << emp->Name << ", "
       << emp->Age << '\n';
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

void add_employee(Employee *emp)
{
  gotoxy(10, 10);
  cout << "Enter Employee Id: ";
  cin >> emp->Id;
  gotoxy(10, 11);
  cout << "Enter Employee Name: ";
  cin >> emp->Name;
  gotoxy(10, 12);
  cout << "Enter Employee Age: ";
  cin >> emp->Age;
  gotoxy(10, 14);
  cout << "\t>>> Empolyee added successfully!";
}

void _swap(int *first, int *second)
{
  int temp(*first);
  *first = *second;
  *second = temp;
}
