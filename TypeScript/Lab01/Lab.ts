interface Student {
    id: number;
    name: string;
    email?: string;
    isActive: boolean;
    grades: number[];
}

let students: Student[] = [];

function addStudent(std: Student): void {
    students.push(std);
}

function calcAvgGrades(std: Student): number {
    if (std.grades.length === 0)
        return 0;

    let sum = std.grades.reduce((total, g) => total += g, 0);

    return sum / std.grades.length;
}

function getStudentStatus(avg: number): string {
    if (avg > 89)
        return "Excellent";
    else if (avg > 69)
        return "Good";
    else if (avg > 49)
        return "Average";
    else
        return "Needs improvement";
}

function printStudentReport(student: Student): void {
    const avg = calcAvgGrades(student);
    const status = getStudentStatus(avg);

    console.log(`Adding student: ${student.name} [${student.grades.join(", ")}]`);
    console.log(`Average grade: ${avg.toFixed(2)}`);
    console.log(`Performance: ${status}`);
    console.log("----------------------");
}

const std1: Student = {
    id: 1,
    name: "Gojo Saturo",
    isActive: true,
    grades: [92, 50, 60, 20]
};

const std2: Student = {
    id: 2,
    name: "Uchiha Itachi",
    isActive: true,
    grades: [55, 76, 85, 96]
};

addStudent(std1);
addStudent(std2);

students.forEach(printStudentReport);