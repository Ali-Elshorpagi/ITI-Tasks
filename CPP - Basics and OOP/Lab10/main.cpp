#include <iostream>
#include <cstring>

using namespace std;

template <class type>
class Stack
{
    int _size;
    int top;
    type *arr;

public:
    Stack() : _size(5), top(-1)
    {
        arr = new type[_size];
    }
    Stack(int size) : _size(size), top(-1)
    {
        arr = new type[_size];
    }
    ~Stack()
    {
        delete[] arr;
    }
    bool isEmpty()
    {
        return top == -1;
    }
    bool isFull()
    {
        return top == _size - 1;
    }
    Stack(Stack &st)
    {
        this->_size = st._size;
        this->top = st.top;

        arr = new type[_size];

        for (int i(0); i <= top; ++i)
            arr[i] = st.arr[i];
    }
    void push(type value)
    {
        if (isFull())
        {
            // cout << "Stack is Full\n";
            return;
        }
        arr[++top] = value;
    }
    bool pop(type &ret)
    {
        if (isEmpty())
        {
            // cout << "Stack is Empty\n";
            return false;
        }
        ret = arr[top--];
        return true;
    }
    void print()
    {
        for (int i(top); i > -1; --i)
            cout << arr[i] << ' ';

        cout << '\n';
    }
};

template <class type>
void Swap(type &first, type &second)
{
    type tmp(first);
    first = second;
    second = tmp;
}

class Person
{
    int _id;
    int _age;
    char _name[50];

public:
    Person()
    {
        //        cout << "\nPerson Constructor Called.\n";
        _id = -1, _age = -1;
    }
    Person(int age, int id, char name[50])
    {
        setId(id);
        setAge(age);
        setName(name);
    }
    void setAge(int age)
    {
        if (age > 0)
            _age = age;
    }
    void setId(int id)
    {
        if (id > 0)
            _id = id;
    }
    void setName(char name[50])
    {
        if (strlen(name) > 0)
            strcpy(_name, name);
    }
    int getId()
    {
        return _id;
    }
    int getAge()
    {
        return _age;
    }
    char *getName()
    {
        return _name;
    }
    void print()
    {
        cout << "ID: " << getId() << ", Name: " << getName() << ", Age: " << getAge();
    }
    ~Person()
    {
        //        cout << "\nPerson Destructor Called.\n";
    }
};

class Employee : public Person
{
    double _salary;

public:
    Employee()
    {
        _salary = 0;
    }
    Employee(double salary)
    {
        setSalary(salary);
    }
    void setSalary(double salary)
    {
        if (salary > -1)
            _salary = salary;
    }
    void setAge(int age)
    {
        if (age > 30)
            Person::setAge(age);
    }
    void print()
    {
        Person::print();
        cout << ", Salary: " << _salary;
    }
};

class Student : public Person
{
    char _grade;

public:
    Student(char grade)
    {
        setGrade(grade);
    }
    void setGrade(char grade)
    {
        if (grade != ' ')
            _grade = grade;
    }
    void print()
    {
        Person::print();
        cout << ", Grade: " << _grade;
    }
};

class Instructor : public Employee
{
    char _subjects[5][20];

public:
    Instructor(char subjects[5][20])
    {
        setSubjects(subjects);
    }
    void setSubjects(char subjects[5][20])
    {
        if (strlen(subjects[0]) > 0)
        {
            for (int i = 0; i < 5; ++i)
                strcpy(_subjects[i], subjects[i]);
        }
    }
    void print()
    {
        Employee::print();
        cout << ", Subjects: ";
        for (int i(0); i < 5; ++i)
            cout << _subjects[i] << ' ';
    }
};

void testSwap()
{
    int x(3), y(7);
    Swap<int>(x, y);
    cout << "\tFirst: " << x << ", Second: " << y << '\n';
    float a(3.5), b(7.3);
    Swap<float>(a, b);
    cout << "\tFirst: " << a << ", Second: " << b << '\n';
    double i(56.5), j(11.3);
    Swap<double>(i, j);
    cout << "\tFirst: " << i << ", Second: " << j << '\n';
}

void testStack()
{
    cout << "\t Stack of integer: ";
    Stack<int> stint(5);
    for (int i(10); i < 60; i += 10)
        stint.push(i);
    stint.print();

    cout << "\t Stack of double: ";
    Stack<double> stdoub(6);
    for (double i(10.6); i < 70; i += 10.3)
        stdoub.push(i);
    stdoub.print();

    cout << "\t Stack of float: ";
    Stack<float> stfloat(6);
    for (float i(9.4); i < 70; i += 10.1)
        stfloat.push(i);
    stfloat.print();
}

void testInheritance()
{
    Person p1(22, 101, "Ali");
    p1.print();
    cout << "\n\n";

    Employee emp;
    emp.setId(102);
    emp.setName("AboIsmail");
    emp.setAge(34);
    emp.setSalary(1500);
    emp.print();
    cout << "\n\n";

    char subs[5][20] = {"C++", "Algorithms", "Data Structures", "Operating Systems", "OOP"};
    Instructor inst(subs);
    inst.setId(103);
    inst.setName("Eng.Hend");
    inst.setAge(31);
    inst.setSalary(3000);
    inst.print();
    cout << "\n\n";
}

void _insert(Stack<Employee> &st)
{
    if (st.isFull())
    {
        cout << "\n\tThe Stack is full\n";
        return;
    }
    int id, age;
    char name[50];
    double salary;
    cout << "\n\tEnter Id: ";
    cin >> id;
    cout << "\n\tEnter Name: ";
    cin >> name;
    cout << "\n\tEnter Age: ";
    cin >> age;
    cout << "\n\tEnter Salary: ";
    cin >> salary;
    Employee emp;
    emp.setId(id), emp.setName(name), emp.setAge(age), emp.setSalary(salary);
    st.push(emp);
}

void _update(Stack<Employee> &st)
{
    if (st.isEmpty())
    {
        cout << "\n\tNo employees to update\n";
        return;
    }
    int id;
    cout << "\n\tEnter Id: ";
    cin >> id;

    Stack<Employee> tmp;
    Employee cur;
    bool isFound(false);

    while (st.pop(cur))
    {
        if (cur.getId() == id && !isFound)
        {
            int choice(-1);
            while (choice != 5)
            {
                system("cls");
                cout << "\n\n\t >> Update Operation << \n\n"
                     << "\n\t[1]. Update Id"
                     << "\n\t[2]. Update Name"
                     << "\n\t[3]. Update Age"
                     << "\n\t[4]. Update Salary"
                     << "\n\t[5]. Exit: "
                     << "\n\tEnter Your Choice: ";
                cin >> choice;
                switch (choice)
                {
                case 1:
                {
                    int id;
                    cout << "\n\tEnter Id: ";
                    cin >> id;
                    cur.setId(id);
                    cout << "\nEmployee Id updated successfuly.\n\n";
                    system("pause");
                    break;
                }
                case 2:
                {
                    char name[50];
                    cout << "\n\tEnter Name: ";
                    cin >> name;
                    cur.setName(name);
                    cout << "\nEmployee Name updated successfuly.\n\n";
                    system("pause");
                    break;
                }
                case 3:
                {
                    int age;
                    cout << "\n\tEnter Age: ";
                    cin >> age;
                    cur.setAge(age);
                    cout << "\nEmployee Age updated successfuly.\n\n";
                    system("pause");
                    break;
                }
                case 4:
                {
                    double sal;
                    cout << "\n\tEnter Salary: ";
                    cin >> sal;
                    cur.setSalary(sal);
                    cout << "\nEmployee Salary updated successfuly.\n\n";
                    system("pause");
                    break;
                }
                case 5:
                    break;
                default:
                    cout << "\n Invalid choice, plz enter a number between 1 and 5.\n";
                    system("pause");
                    break;
                }
            }
            isFound = true;
        }
        tmp.push(cur);
    }
    while (tmp.pop(cur))
        st.push(cur);

    if (!isFound)
        cout << "\n\n\tEmployee with given ID not found\n";
    else
        cout << "\n\tEmployee updated successfully\n";
}

void _delete(Stack<Employee> &st)
{
    if (st.isEmpty())
    {
        cout << "\n\tNo employees to delete\n";
        return;
    }
    int id;
    cout << "\n\tEnter Id: ";
    cin >> id;

    Stack<Employee> tmp;
    Employee cur;
    bool isDeleted(false);

    while (st.pop(cur))
    {
        if (cur.getId() == id)
            isDeleted = true;
        else
            tmp.push(cur);
    }

    while (tmp.pop(cur))
        st.push(cur);

    if (isDeleted)
        cout << "\nEmployee deleted.\n";
    else
        cout << "\nEmployee with given ID not found.\n";
}
void _search(Stack<Employee> &st)
{
    if (st.isEmpty())
    {
        cout << "\n\tNo employees to search\n";
        return;
    }
    int id;
    cout << "\n\tEnter Id: ";
    cin >> id;

    Stack<Employee> tmp;
    Employee cur;
    bool isFound(false);

    while (st.pop(cur))
    {
        if (cur.getId() == id)
        {
            cout << "\nEmployee details:\n\n";
            cur.print();
            cout << "\n\n";
            isFound = true;
        }
        tmp.push(cur);
    }

    while (tmp.pop(cur))
        st.push(cur);

    if (!isFound)
        cout << "\nEmployee with given ID not found.\n\n";
}
void _sort(Stack<Employee> &st)
{
    if (st.isEmpty())
    {
        cout << "\n\tNo employees to sort\n";
        return;
    }

    Employee arr[100];
    int n(0);
    Employee cur;

    while (st.pop(cur))
        arr[n++] = cur;

    for (int i(0); i < n - 1; ++i)
    {
        for (int j(0); j < n - 1 - i; ++j)
        {
            if (arr[j].getId() > arr[j + 1].getId())
                Swap(arr[j], arr[j + 1]);
        }
    }

    for (int i(0); i < n; ++i)
    {
        cout << "\n\n";
        arr[i].print();
        st.push(arr[i]);
    }

    cout << "\nEmployees sorted by ID successfully.\n";
}
void TestCRUD()
{
    Stack<Employee> st(100);
    int choice(-1);
    while (choice != 6)
    {
        system("cls");

        cout << "\n\n\t >> CRUD Operation << \n\n"
             << "\n\t[1]. Insert a new employee: "
             << "\n\t[2]. Update an employee: "
             << "\n\t[3]. Delete an employee: "
             << "\n\t[4]. Search for an employee: "
             << "\n\t[5]. Sort the: employees upon their Id: "
             << "\n\t[6]. Exit: "
             << "\n\tEnter Your Choice: ";

        cin >> choice;
        switch (choice)
        {
        case 1:
            _insert(st);
            cout << "\n Employee Added Successfully\n";
            system("pause");
            break;
        case 2:
            _update(st);
            cout << "\n Employee Updated Successfully\n";
            system("pause");
            break;
        case 3:
            _delete(st);
            cout << "\n Employee Deleted Successfully\n";
            system("pause");
            break;
        case 4:
            _search(st);
            system("pause");
            break;
        case 5:
            _sort(st);
            system("pause");
            break;
        case 6:
            cout << "\nExit the application....\n";

            break;
        default:
            cout << "\n Invalid choice, plz enter a number between 1 and 6.\n";
            system("pause");
            break;
        }
    }
}

int main()
{
    // cout << "\n\n\t >> Swap Test << \n\n";
    // testSwap();
    // cout << "\n-------------------------------------------------------------\n";
    // cout << "\n\t >> Stack Test << \n\n";
    // testStack();
    // cout << "\n-------------------------------------------------------------\n";
    // cout << "\n\t >> Inheritance Test << \n\n";
    // testInheritance();

    TestCRUD();
    return 0;
}
