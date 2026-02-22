#include <iostream>
#include <cstring>

using namespace std;

class Complex
{
    int _real;
    int _img;

public:
    Complex(int real, int img) : _real(real), _img(img) { cout << "The Complext Object is created\n"; }
    void set_real(int real)
    {
        _real = real;
    }
    int get_real()
    {
        return _real;
    }
    void set_img(int img)
    {
        _img = img;
    }
    int get_img()
    {
        return _img;
    }
    void print()
    {
        if (!_img)
            cout << _real;
        else if (_img < 0)
            cout << _real << _img << 'j';
        else
            cout << _real << '+' << _img << 'j';
    }
    ~Complex()
    {
        cout << "The Complex Obeject is Destoroied\n";
    }
};

class Employee
{
    int _id;
    char _name[50];
    double _salary{0.00};

public:
    Employee(int id, char name[50], double salary)
    {
        set_id(id);
        set_name(name);
        set_salary(salary);

        cout << "The Employee Object is created 1\n";
    }
    Employee(int id, char name[50])
    {
        set_id(id);
        set_name(name);

        cout << "The Employee Object is created 2\n";
    }
    Employee(int id)
    {
        set_id(id);
        cout << "The Employee Object is created 3\n";
    }

    void set_id(int id)
    {
        if (id > 0)
            _id = id;
    }
    int get_id()
    {
        return _id;
    }
    void set_name(char name[50])
    {
        if (strlen(name) > 0)
        {
            strcpy(_name, name);
        }
    }
    double get_salary()
    {
        return _salary;
    }
    void set_salary(double salary)
    {
        if (salary > 0)
            _salary = salary;
    }
    char* get_name()
    {
        return _name;
    }
    void print()
    {
        cout << "Id: " << get_id() << ", Name: " << get_name() << ", Salary: " << get_salary() << '\n';
    }
    ~Employee()
    {
        cout << "The Employee Obeject is Destoroied\n";
    }
};

void TestComplex()
{
    Complex com(34, 0);
    com.print();
    cout << '\n';
}

void test_value(Employee emp)
{
    emp.print();
}
void test_pointer(Employee *emp)
{
    emp->print();
}
void test_reference(Employee &emp)
{
    emp.print();
}

void TestEmployee()
{
    Employee emp(34, "Ali", 324.23);
    cout << "\nBy Value: \n";
    test_value(emp);

    cout << "\nBy Pointer: \n";
    test_pointer(&emp);

    cout << "\nBy Reference: \n";
    test_reference(emp);
    cout << '\n';
}

int main()
{
    TestComplex();
    cout << "\n-------------------------------------\n\n";
    TestEmployee();
    return 0;
}
