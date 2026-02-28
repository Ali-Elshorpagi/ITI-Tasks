// Write a function printNames(...names) that accepts any number of student objects and prints their name using spread operator .

function printNames(...students) {
    for (let student of students) {
        console.log(student.name);
    }
}

// printNames(...students);