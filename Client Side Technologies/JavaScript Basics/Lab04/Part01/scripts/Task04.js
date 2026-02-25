var students = [
    { id: 1, name: "Ali", age: 20, Crs: "CS" },
    { id: 2, name: "Sara", age: 22, Crs: "Math" },
    { id: 3, name: "Omar", age: 19, Crs: "Physics" },
    { id: 4, name: "Laila", age: 21, Crs: "CS" },
    { id: 5, name: "Hassan", age: 23, Crs: "Engineering" },
    { id: 6, name: "Mona", age: 20, Crs: "Biology" },
    { id: 7, name: "Kareem", age: 24, Crs: "Math" },
    { id: 8, name: "Noor", age: 18, Crs: "CS" }
];

var dropdown = document.getElementById("studentDropdown");
var displayDiv = document.getElementById("studentDisplay");

for (var item of students) {
    var option = document.createElement('option');
    option.value = item.id;
    option.textContent = item.name;
    dropdown.appendChild(option);
}

dropdown.addEventListener('change', function () {
    var stdId = parseInt(this.value);
    if (stdId == 0) {
        displayDiv.innerHTML = "<p style='color:red;'>No Student has been Selected</p>";
    }

    var student = students.filter(function (std) {
        return std.id == stdId;
    });

    if (student[0]) {
        displayDiv.innerHTML =
            "<h1>Student Information</h1>" +
            "<p><b style='color:red;'>ID:</b> " + student[0].id + "</p>" +
            "<p><b style='color:red;'>Name:</b> " + student[0].name + "</p>" +
            "<p><b style='color:red;'>Age:</b> " + student[0].age + "</p>" +
            "<p><b style='color:red;'>Course:</b> " + student[0].Crs + "</p>";
    }

});