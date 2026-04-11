var students = [];
function addStudent(std) {
    students.push(std);
}
function calcAvgGrades(std) {
    if (std.grades.length === 0)
        return 0;
    var sum = std.grades.reduce(function (total, g) { return total += g; }, 0);
    return sum / std.grades.length;
}
function getStudentStatus(avg) {
    if (avg > 89)
        return "Excellent";
    else if (avg > 69)
        return "Good";
    else if (avg > 49)
        return "Average";
    else
        return "Needs improvement";
}
function printStudentReport(student) {
    var avg = calcAvgGrades(student);
    var status = getStudentStatus(avg);
    console.log("Adding student: ".concat(student.name, " [").concat(student.grades.join(", "), "]"));
    console.log("Average grade: ".concat(avg.toFixed(2)));
    console.log("Performance: ".concat(status));
    console.log("----------------------");
}
var std1 = {
    id: 1,
    name: "Gojo Saturo",
    isActive: true,
    grades: [92, 50, 60, 20]
};
var std2 = {
    id: 2,
    name: "Uchiha Itachi",
    isActive: true,
    grades: [55, 76, 85, 96]
};
addStudent(std1);
addStudent(std2);
students.forEach(printStudentReport);
