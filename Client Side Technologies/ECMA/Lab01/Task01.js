const students = [
    { id: 1, name: "Ali", grade: 80, city: "Cairo" },
    { id: 2, name: "Sara", grade: 92, city: "Alexandria" },
    { id: 3, name: "Omar", grade: 74, city: "Giza" },
    { id: 4, name: "Mona", grade: 88, city: "Cairo" }
];

// [1]. Students Names
let studentsNames = students.map(st => st.name);

// [2]. Students have a grade >= 85
let studentsGrade85 = students.filter(st => st.grade >= 85);

// [3]. Student with name Sara
let studentSara = students.find(st => st.name === 'Sara');

// [4]. Calculate the average grade
let avaGrade = students.reduce((sum, st) => sum + st.grade, 0) / students.length;

// [5]. Sort students by grade (descending)
// let sortedStudents = students.sort((a, b) => b.grade - a.grade);

// [6]. Print Students using forEach
// let arrStudents = students.forEach(st => console.log(st));

// [7]. Make a Deep copy of the students array using spread.
let deepCopy = []; // [...students];
for (let item of students) {
    deepCopy.push({...item});
}

// [8]. Add a new student object into the array using spread.
const newStudent = { id: 5, name: "Samar", grade: 95, city: "Giza" };
let updatedStudents = [...students, newStudent];
// console.log(updatedStudents);

// [9]. Suppose you have another array of students, merge it with the first array using spread
const newStudents = [
    { id: 21, name: "Ahmed", grade: 66, city: "Cairo" },
    { id: 22, name: "Aziz", grade: 95, city: "Alexandria" },
    { id: 23, name: "Aya", grade: 70, city: "Giza" },
    { id: 24, name: "Samia", grade: 65, city: "Cairo" }
];
let updatedArrayStudents = [...students, ...newStudents];
// console.log(updatedArrayStudents);

// [10]. Update "Omar"’s grade to 95 without mutating the original array (use spread inside map).
let updatedStudentOmar = students.map(st => st.name === 'Omar' ? { ...st, grade: 95 } : st);
// console.log(updatedStudentOmar);

// [11]. Remove the student with id = 2 using filter + spread
let updatedStudentsRemoving = [...students.filter(st => st.id !== 2)];
// console.log(updatedStudentsRemoving);

// [12]. Take out the first student and keep the rest in another array using destructuring + spread
[a, ...restStudents] = students;
// console.log(restStudents);

// [13]. Destruct and Extract the first student into a variable, and keep the rest into another array using rest 
[b, ...restStudents] = students;
// console.log(restStudents);

// [14]. Skip the first two students and store the third one in a variable.
[, , c] = students;
console.log(c);
