#include <iostream>
#include <string>

class Employee
{
    int id{};
    std::string name;
    double salary{};

public:
    Employee() = default;
    Employee(int id, std::string name, double salary) : id(id), name(name), salary(salary) {}

    int getId() { return id; }
    std::string getName() { return name; }
    double getSalary() { return salary; }

    void setId(int id)
    {
        if (id > 0)
            this->id = id;
    }
    void setName(std::string name)
    {
        this->name = name;
    }
    void setSalary(double salary)
    {
        if (salary > -1)
            this->salary = salary;
    }
    void display()
    {
        std::cout << "ID: " << id << ", Name: " << name << ", Salary: " << salary << "\n";
    }
    bool operator==(const Employee &other) const
    {
        return id == other.id;
    }
    bool matchesName(const std::string &searchName) const
    {
        return name == searchName;
    }
    friend std::ostream &operator<<(std::ostream &os, const Employee &emp)
    {
        os << "ID: " << emp.id << ", Name: " << emp.name << ", Salary: " << emp.salary;
        return os;
    }
};