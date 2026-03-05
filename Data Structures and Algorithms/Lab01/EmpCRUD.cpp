#ifndef EMPLOYEE_OPERATIONS_H
#define EMPLOYEE_OPERATIONS_H

#include "Helper.cpp"
#include "Employee.cpp"
#include "DoublyLinkedList.cpp"
#include <conio.h>

void addEmployee(Doubly_LinkedList<Employee> &empList)
{
    system("cls");
    int id;
    std::string name;
    double salary;

    Helper::gotoxy(20, 5);
    std::cout << "=== Add New Employee ===\n";
    Helper::gotoxy(20, 7);
    std::cout << "Enter Employee ID: ";
    std::cin >> id;

    Helper::gotoxy(20, 8);
    std::cout << "Enter Employee Name: ";
    std::cin >> name;

    Helper::gotoxy(20, 9);
    std::cout << "Enter Employee Salary: ";
    std::cin >> salary;

    Employee emp(id, name, salary);
    empList.insert_end(emp);

    Helper::gotoxy(20, 11);
    Helper::textattr(0x0A);
    std::cout << "Employee added successfully!";
    Helper::textattr(0x07);
    Helper::gotoxy(20, 12);
    std::cout << "Press any key to continue...";
    _getch();
}

void displayAllEmployees(Doubly_LinkedList<Employee> &empList)
{
    system("cls");
    Helper::gotoxy(20, 5);
    std::cout << "=== All Employees ===\n";

    if (empList.is_empty())
    {
        Helper::gotoxy(20, 7);
        Helper::textattr(0x0C);
        std::cout << "No employees found!";
        Helper::textattr(0x07);
    }
    else
    {
        Helper::gotoxy(20, 7);
        empList.print();
    }

    Helper::gotoxy(20, 10);
    std::cout << "Press any key to continue...";
    _getch();
}

void searchById(Doubly_LinkedList<Employee> &empList)
{
    system("cls");
    int id;

    Helper::gotoxy(20, 5);
    std::cout << "=== Search Employee by ID ===\n";
    Helper::gotoxy(20, 7);
    std::cout << "Enter Employee ID: ";
    std::cin >> id;

    Employee searchEmp;
    searchEmp.setId(id);
    Node<Employee> *found(empList.search(searchEmp));

    Helper::gotoxy(20, 9);
    if (found)
    {
        Helper::textattr(0x0A);
        std::cout << "Employee Found:\n";
        Helper::textattr(0x07);
        Helper::gotoxy(20, 10);
        found->data.display();
    }
    else
    {
        Helper::textattr(0x0C);
        std::cout << "Employee not found!";
        Helper::textattr(0x07);
    }

    Helper::gotoxy(20, 12);
    std::cout << "Press any key to continue...";
    _getch();
}

void searchByName(Doubly_LinkedList<Employee> &empList)
{
    system("cls");
    std::string name;

    Helper::gotoxy(20, 5);
    std::cout << "=== Search Employee by Name ===\n";
    Helper::gotoxy(20, 7);
    std::cout << "Enter Employee Name: ";
    std::cin >> name;

    bool found(false);
    int row(9);

    for (Node<Employee> *cur(empList.get_head()); cur; cur = cur->next)
    {
        if (cur->data.matchesName(name))
        {
            if (!found)
            {
                Helper::gotoxy(20, row++);
                Helper::textattr(0x0A);
                std::cout << "Employee(s) Found:\n";
                Helper::textattr(0x07);
            }
            Helper::gotoxy(20, row++);
            cur->data.display();
            found = true;
        }
    }

    if (!found)
    {
        Helper::gotoxy(20, 9);
        Helper::textattr(0x0C);
        std::cout << "No employee found with that name!";
        Helper::textattr(0x07);
    }

    Helper::gotoxy(20, row + 1);
    std::cout << "Press any key to continue...";
    _getch();
}

void deleteById(Doubly_LinkedList<Employee> &empList)
{
    system("cls");
    int id;

    Helper::gotoxy(20, 5);
    std::cout << "=== Delete Employee by ID ===\n";
    Helper::gotoxy(20, 7);
    std::cout << "Enter Employee ID: ";
    std::cin >> id;

    Employee deleteEmp;
    deleteEmp.setId(id);
    bool deleted = empList.delete_node_with_key(deleteEmp);

    Helper::gotoxy(20, 9);
    if (deleted)
    {
        Helper::textattr(0x0A);
        std::cout << "Employee deleted successfully!";
    }
    else
    {
        Helper::textattr(0x0C);
        std::cout << "Employee not found!";
    }
    Helper::textattr(0x07);
    Helper::gotoxy(20, 10);
    std::cout << "Press any key to continue...";
    _getch();
}

void deleteByName(Doubly_LinkedList<Employee> &empList)
{
    system("cls");
    std::string name;

    Helper::gotoxy(20, 5);
    std::cout << "=== Delete Employee by Name ===\n";
    Helper::gotoxy(20, 7);
    std::cout << "Enter Employee Name: ";
    std::cin >> name;

    bool deleted(false);
    for (Node<Employee> *cur(empList.get_head()); cur; cur = cur->next)
    {
        if (cur->data.matchesName(name))
        {
            Employee temp(cur->data);
            deleted = empList.delete_node_with_key(temp);
            break;
        }
    }

    Helper::gotoxy(20, 9);
    if (deleted)
    {
        Helper::textattr(0x0A);
        std::cout << "Employee deleted successfully!";
    }
    else
    {
        Helper::textattr(0x0C);
        std::cout << "Employee not found!";
    }
    Helper::textattr(0x07);
    Helper::gotoxy(20, 10);
    std::cout << "Press any key to continue...";
    _getch();
}

void deleteAll(Doubly_LinkedList<Employee> &empList)
{
    system("cls");
    Helper::gotoxy(20, 5);
    std::cout << "=== Delete All Employees ===\n";
    Helper::gotoxy(20, 7);
    Helper::textattr(0x0E);
    std::cout << "Are you sure you want to delete all employees? (Y/N): ";
    Helper::textattr(0x07);

    char confirm(_getch());
    if (confirm == 'Y' || confirm == 'y')
    {
        empList.delete_all();
        Helper::gotoxy(20, 9);
        Helper::textattr(0x0A);
        std::cout << "All employees deleted!";
        Helper::textattr(0x07);
    }
    else
    {
        Helper::gotoxy(20, 9);
        std::cout << "Operation cancelled.";
    }

    Helper::gotoxy(20, 11);
    std::cout << "Press any key to continue...";
    _getch();
}

void showEmployeeCount(Doubly_LinkedList<Employee> &empList)
{
    system("cls");
    Helper::gotoxy(20, 5);
    std::cout << "=== Employee Count ===\n";
    Helper::gotoxy(20, 7);
    Helper::textattr(0x0B);
    std::cout << "Total Employees: " << empList.get_size();
    Helper::textattr(0x07);
    Helper::gotoxy(20, 9);
    std::cout << "Press any key to continue...";
    _getch();
}

#endif
