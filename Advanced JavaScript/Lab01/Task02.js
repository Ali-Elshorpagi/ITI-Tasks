// Q1: Use map to get an array of employee names.
let employeeNames = employees.map(employee => employee.name);
console.log(employeeNames);

// Q2: Use filter to get only employees who earn more than 4500.
let filterMoreThan4500 = employees.filter(emp => emp.salary > 4500);
console.log(filterMoreThan4500);

// Q3: Use reduce to calculate the total Salaries.
let totalSalaries = employees.reduce((total, emp) => total += emp.salary, 0);
console.log(totalSalaries);

// Q4: Create a pure function that increases an employee salary by 10%.
function increasesSalaryby10(employee) {
    let newEmployee = { ...employee };
    newEmployee.salary *= 1.1;
    return newEmployee;
}

let updatedEmployee = increasesSalaryby10(employees[0]);
console.log(updatedEmployee);
console.log(employees[0]);


// Q5: Add a new employee to EmpArray immutably(without changing the original, use map).
function addNewEmployee() {
    let empArr = employees.map(emp => emp);
    return function (newEmp) {
        empArr.push(newEmp);
        return empArr;
    }
}
let newEmployee = new Employee("Ali", 29, 4800, "Marketing");
let updatedEmployees = addNewEmployee();
console.log(updatedEmployees(newEmployee));
console.log(employees);

// Q6: Write a higher-order function applyBonus(fn).
function applyBonus_(fn) {
    return function (salary) {
        const bonus = salary * 0.1;
        return fn(salary + bonus);
    };
}

function calculateTax(amount) {
    return amount * 0.8;
}

const salaryWithBonusAndTax = applyBonus_(calculateTax);
console.log(salaryWithBonusAndTax(50000));

// Q7: Filter employees by department using a reusable curried function.
function filterByDepartment(department) {
    return function (employees) {
        return employees.filter(emp => emp.dep === department);
    };
}

let filterEmplopeesByDept = filterByDepartment('IT');
console.log(filterEmplopeesByDept(employees));

// Q8: Use map to update salaries (+5%) without modifying the original.
let updatedSalaries = employees.map(emp => {
    let newEmp = { ...emp };
    newEmp.salary *= 1.05;
    return newEmp;
});
console.log(updatedSalaries);
console.log(employees);