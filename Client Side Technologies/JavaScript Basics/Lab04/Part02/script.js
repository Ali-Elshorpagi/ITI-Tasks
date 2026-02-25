var students = [
    { name: "Ali", age: 20, Crs: "CS" },
    { name: "Sara", age: 22, Crs: "Math" },
    { name: "Omar", age: 19, Crs: "Physics" },
    { name: "Laila", age: 21, Crs: "CS" },
    { name: "Hassan", age: 23, Crs: "Engineering" },
    { name: "Mona", age: 20, Crs: "Biology" },
    { name: "Kareem", age: 24, Crs: "Math" },
    { name: "Noor", age: 18, Crs: "CS" }
];

var currStudnets = students;

function display(students) {
    var table = "";
    var rows = students.length, cols = 3;
    table += "<tr>";
    table += "<th>Name</th>";
    table += "<th>Age</th>";
    table += "<th>Crs</th>";
    table += "</tr>";
    for (var r = 0; r < rows; ++r) {
        table += "<tr>";
        table += "<td align='center'>" + students[r].name + "</td>";
        table += "<td align='center'>" + students[r].age + "</td>";
        table += "<td align='center'>" + students[r].Crs + "</td>";
        table += "</tr>";
    }
    document.getElementById("tableId").innerHTML = table;
}

function searchByName(students, name) {
    var done = false;
    var founding = [];
    for (var item of students) {
        if (item.name == name) {
            done = true;
            founding.push(item);
        }
    }
    return { done, ans: founding };
}

function searchByCrs(students, crs) {
    var done = false;
    var founding = [];
    for (var item of students) {
        if (item.Crs == crs) {
            done = true;
            founding.push(item);
        }
    }
    return { done, ans: founding };
}
function sortByAge(students) {
    var sortedStudents = Array.from(students);
    sortedStudents.sort(function (a, b) {
        return a.age - b.age;
    });
    return sortedStudents;
}
function sortByName(students) {
    var sortedStudents = Array.from(students);
    sortedStudents.sort(function (a, b) {
        return a.name.localeCompare(b.name);
    });
    return sortedStudents;
}

var searchInput = document.getElementById("searchInt");
var sortSelect = document.getElementById("sortSelect");

display(currStudnets);

searchInput.addEventListener('keyup', function () {
    var searchValue = this.value;

    if (searchValue == "")
        currStudnets = students;
    else {
        var result = searchByName(students, searchValue);
        if (result.done) {
            currStudnets = result.ans;
        } else {
            result = searchByCrs(students, searchValue);
            currStudnets = result.done ? result.ans : [];
        }
    }
    display(currStudnets);
});



sortSelect.addEventListener('change', function () {
    var searchValue = this.value;

    if (searchValue == "")
        currStudnets = students;

    if (searchValue == '1')
        currStudnets = sortByAge(students);
    else if (searchValue == '2')
        currStudnets = sortByName(students);

    display(currStudnets);
});
