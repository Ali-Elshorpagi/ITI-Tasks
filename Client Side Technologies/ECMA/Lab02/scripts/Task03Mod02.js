// --------Module 2: employeeOps.js------
// 1- Import the Employee class.
// 2- Create functions to:
//      - Add employee(s) to an array.
// 3- Find employee by name.
// 4- Increase salary for an employee.

import { Employee } from './Task03Mod01.js';

const employees = [];

export function addEmployee(newEmployee) {
    const employee = new Employee(newEmployee.FirstName, newEmployee.LastName, newEmployee.Age, newEmployee.Salary);
    employees.push(employee);
}

export function findEmployeeByName(name) {
    return employees.find(emp => emp.getFullName() === name);
}

export function increaseSalary(name, amount) {
    const employee = findEmployeeByName(name);
    if (employee) {
        employee.Salary += amount;
        return true;
    }
    return false;
}   