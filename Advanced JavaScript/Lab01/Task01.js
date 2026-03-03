class Employee {
    constructor(name, age, salary, dep) {
        this.name = name;
        this.age = age;
        this.salary = salary;
        this.dep = dep;
    }
}

let employees = [
    new Employee("Ahmed", 25, 4000, "IT"),
    new Employee("Mohamed", 30, 5000, "HR"),
    new Employee("Sara", 28, 4500, "Finance"),
    new Employee("Laila", 32, 5500, "IT"),
    new Employee("Omar", 29, 4800, "HR")
];

// for (const employee of employees) {
//     console.log(`Name: ${employee.name}, Age: ${employee.age}, Salary: ${employee.salary}, Department: ${employee.dep}`);
// }

//  Q1: Create a function that returns another function that Take Emp and Return it’s Name e
function getEmployeeName() {
    return function (employee) {
        return employee.name;
    }
}

let getName = getEmployeeName();
console.log(getName(employees[0]));


// Q2: Create a counter function that increases every time it’s called.
function counter() {
    let counter = 0;
    return function () {
        ++counter;
        return counter;
    }
}

let getCounter = counter();
console.log(getCounter());
console.log(getCounter());
console.log(getCounter());
console.log(getCounter());
console.log(getCounter());

// Q3: Create a function that tracks how many times a button is clicked, each Time Clicked To change Body Background.
let button = document.getElementById("btn");
let clickCount = 0;

button.addEventListener("click", function () {
    ++clickCount;
    document.body.style.backgroundColor = getRandomColor();

    function getRandomColor() {
        const letters = '0123456789ABCDEF';
        let color = '#';
        for (let i = 0; i < 6; ++i) {
            color += letters[Math.floor(Math.random() * 16)];
        }
        return color;
    }
    console.log(`Button clicked ${clickCount} times`);
});

// Q4: Create a closure that adds a fixed number to any number.
function addFixedNumber(fixedNum) {
    // const fixedNum = 11;
    return function (num) {
        return num + fixedNum;
    }
}

let fixedAdded = addFixedNumber(11);
console.log(fixedAdded(9));

// Q5: Create a closure that keeps track of how many employees have been added.
function employeeTracker() {
    let empCount = 0;
    return function () {
        ++empCount;
        return empCount;
    }
}

let trackEmployee = employeeTracker();
console.log(trackEmployee());
console.log(trackEmployee());
console.log(trackEmployee());

// Q6: Create a closure that Takea Bonus percentage and applies it To Emp Salary.
function addBonus() {
    return function (employee, bonusPercent) {
        let bonusAmount = (employee.salary * bonusPercent) / 100;
        return employee.salary + bonusAmount;
    }
}

let applyBonus = addBonus();
console.log(applyBonus(employees[0], 10));

// Q7: Create a closure that remembers a department name and returns a greeting.
function rememberDepartment() {
    return function (deptName) {
        return `Welcome to the ${deptName} department!`;
    }
}
let greetDepartment = rememberDepartment();
console.log(greetDepartment("IT"));
console.log(greetDepartment("HR"));