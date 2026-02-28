// ---------Main File: index.js--------
// 1- Import everything from both modules.
// 2- Create a few employees, store them in an array.
// 3- Print all employees’ info on Document not console.

import { Employee } from './Task03Mod01.js';
import { addEmployee } from './Task03Mod02.js';

const employees = [
    new Employee("Ahmed", "Atya", 30, 5000),
    new Employee("Omar", "Khaled", 25, 6000),
    new Employee("Hazem", "Shaker", 28, 5500),
    new Employee("Amr", "Wahba", 28, 15000)

];

for (const emp of employees) {
    addEmployee(emp);
}

addEventListener('DOMContentLoaded', () => {
    document.writeln("<h2>Employee List:</h2>");
    var cnt = 1;
    for (const emp of employees) {
        document.writeln(`<p>[${cnt++}]. <b>Name:</b> ${emp.getFullName()}, <b>Age:</b> ${emp.Age}, <b>Salary:</b> ${emp.Salary}</p>`);
    }

});