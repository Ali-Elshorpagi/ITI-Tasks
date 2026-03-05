#include "EmpCRUD.cpp"
#include <conio.h>

int main()
{
    Doubly_LinkedList<Employee> empList;

    char menu[BASE_ROWS][COLS] = {"Add New Employee", "Display all employees", "Search", "Delete", "Employee Count", "Exit"},
         search_menu[SUB_ROWS - 1][COLS] = {"Search by Id", "Search by Name", "Back"},
         delete_menu[SUB_ROWS][COLS] = {"Delete by Id", "Delete by Name", "Delete All", "Back"};

    int choice(0), sub_choice(0), current_submenu(0); // 0 for none, 1 for search, 2 for delete
    bool in_sub_menu(false), show_sub_menu(false);
    char key;

    do
    {
        system("cls");

        if (show_sub_menu)
        {
            Helper::textattr(0x08);
            for (int i(0); i < BASE_ROWS; ++i)
            {
                Helper::gotoxy(20, 6 + i);
                std::cout << "\t[" << (i + 1) << "]. " << menu[i];
            }
            Helper::textattr(0x07);

            if (current_submenu == 1)
                Helper::print_menu(search_menu, SUB_ROWS - 1, sub_choice, true);
            else if (current_submenu == 2)
                Helper::print_menu(delete_menu, SUB_ROWS, sub_choice, true);
        }
        else
            Helper::print_menu(menu, BASE_ROWS, choice, false);

        key = _getch();

        if (key == EXTENED_KEY)
        {
            key = _getch();
            if (!in_sub_menu)
            {
                if (key == UP)
                    choice = (choice - 1 + BASE_ROWS) % BASE_ROWS;
                else if (key == DOWN)
                    choice = (choice + 1) % BASE_ROWS;
            }
            else
            {
                if (key == UP)
                    sub_choice = (sub_choice - 1 + SUB_ROWS) % SUB_ROWS;
                else if (key == DOWN)
                    sub_choice = (sub_choice + 1) % SUB_ROWS;
            }
        }
        else if (key == ENTER)
        {
            if (!in_sub_menu)
            {
                if (choice == 0)
                    addEmployee(empList);
                else if (choice == 1)
                    displayAllEmployees(empList);
                else if (choice == 2)
                    show_sub_menu = true, in_sub_menu = true, sub_choice = 0, current_submenu = 1;
                else if (choice == 3)
                    show_sub_menu = true, in_sub_menu = true, sub_choice = 0, current_submenu = 2;
                else if (choice == 4)
                    showEmployeeCount(empList);
                else if (choice == 5) // exit
                {
                    std::cout << "\n\n\t\tExiting the program...\n\n";
                    break;
                }
            }
            else
            {
                if (current_submenu == 1) // search menu
                {
                    if (sub_choice == 0)
                    {
                        searchById(empList);
                        show_sub_menu = false;
                        in_sub_menu = false;
                        current_submenu = 0;
                    }
                    else if (sub_choice == 1)
                    {
                        searchByName(empList);
                        show_sub_menu = false;
                        in_sub_menu = false;
                        current_submenu = 0;
                    }
                    else if (sub_choice == 2) // Back
                        show_sub_menu = false, in_sub_menu = false, current_submenu = 0;
                }
                else if (current_submenu == 2) // delete menu
                {
                    if (sub_choice == 0)
                    {
                        deleteById(empList);
                        show_sub_menu = false;
                        in_sub_menu = false;
                        current_submenu = 0;
                    }
                    else if (sub_choice == 1)
                    {
                        deleteByName(empList);
                        show_sub_menu = false;
                        in_sub_menu = false;
                        current_submenu = 0;
                    }
                    else if (sub_choice == 2)
                    {
                        deleteAll(empList);
                        show_sub_menu = false;
                        in_sub_menu = false;
                        current_submenu = 0;
                    }
                    else if (sub_choice == 3) // Back
                        show_sub_menu = false, in_sub_menu = false, current_submenu = 0;
                }
            }
        }
        else if (key == ESC)
        {
            if (in_sub_menu)
                show_sub_menu = false, in_sub_menu = false, current_submenu = 0;
            else
            {
                std::cout << "\n\n\t\tExiting the program...\n\n";
                break;
            }
        }

    } while (true);

    return (0);
}
