// --------Module 1: employee.js---------

// 1- Create a class Employee with properties: firstName, lastName, age, salary.
// 2- Add a method to class  getFullName() that returns "FirstName LastName".
// 3- out class Export an array departments with some department names (["IT","HR","Finance","Sales"]).

export class Employee {
    #firstName;
    #lastName;
    #age;
    #salary;
    constructor(_firstName, _lastName, _age, _salary) {
        this.FirstName = _firstName;
        this.LastName = _lastName;
        this.Age = _age;
        this.Salary = _salary;
    }
    set Salary(newSalary) {
        if (newSalary < 0)
            console.log("Salary cannot be negative");
        else
            this.#salary = newSalary;
    }
    get Salary() {
        return this.#salary;
    }
    set Age(newAge) {
        if (newAge < 18)
            console.log("Age must be at least 18");
        else
            this.#age = newAge;
    }
    get Age() {
        return this.#age;
    }
    set FirstName(newFirstName) {
        if (newFirstName.length < 3)
            console.log("FirstName must be at least 3 characters");
        else
            this.#firstName = newFirstName;
    }
    get FirstName() {
        return this.#firstName;
    }
    set LastName(newLastName) {
        if (newLastName.length < 3)
            console.log("LastName must be at least 3 characters");
        else
            this.#lastName = newLastName;
    }
    get LastName() {
        return this.#lastName;
    }
    getFullName() {
        return `${this.FirstName} ${this.LastName}`;
    }
}

export const departments = ["IT", "HR", "Finance", "Sales"];