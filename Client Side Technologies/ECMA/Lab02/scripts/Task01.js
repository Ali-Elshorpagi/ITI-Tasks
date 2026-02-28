// 1- Create a base class Person with properties: name and age.
// 2- Create a subclass Teacher with property subject and method teach().
// 3- Create a subclass Student with property major and method study().
// 4- Create objects of Teacher and Student, then call their methods.
// 5- Override a method introduce() in both Teacher and Student.

class Person {
    #name;
    #age;
    constructor(_name, _age) {
        this.Name = _name;
        this.Age = _age;
    }
    set Age(newAge) {
        if (newAge < 5 || newAge > 100)
            console.log("Age must be between 5 and 100");
        else
            this.#age = newAge;
    }
    get Age() {
        return this.#age;
    }
    set Name(newName) {
        if (newName.length < 3)
            console.log("Name must be at least 3 characters");
        else
            this.#name = newName;
    }
    get Name() {
        return this.#name;
    }
    introduce() {
        return `Hello, I am ${this.Name}, and I am ${this.Age} years old.`;
    }
}

class Teacher extends Person {
    #subject;
    constructor(_name, _age, _subject) {
        super(_name, _age);
        this.Subject = _subject;
    }
    set Subject(newSubject) {
        if (newSubject.length < 3)
            console.log("Subject must be at least 3 characters");
        else
            this.#subject = newSubject;
    }
    get Subject() {
        return this.#subject;
    }
    teach() {
        console.log(`${this.Name} is teaching ${this.Subject}`);
    }
    introduce() {
        console.log(`${super.introduce()} and I'm the teacher of ${this.Subject}`);
    }
}
class Student extends Person {
    #major;
    constructor(_name, _age, _major) {
        super(_name, _age);
        this.Major = _major;
    }
    set Major(newMajor) {
        if (newMajor.length < 3)
            console.log("Major must be at least 3 characters");
        else
            this.#major = newMajor;
    }
    get Major() {
        return this.#major;
    }
    study() {
        console.log(`${this.Name} is studying ${this.Major}`);
    }
    introduce() {
        console.log(`${super.introduce()} and the ${this.Major} is my major`);
    }
}

let teacher = new Teacher("Ali", 32, "Math");
teacher.teach();

let student = new Student("Ahmed", 21, "Computer Science");
student.study();

teacher.introduce();
student.introduce();