"use strict";

const apiUrl = 'http://localhost:5289/api/Students';

function renderStudents(items) {
    const rowsHtml = items
        .map(function (student) {
            const fullName = (student.firstName || "") + " " + (student.lastName || "");

            return `
				<tr>
					<td>${student.id}</td>
					<td>${fullName.trim()}</td>
					<td>${student.age}</td>
					<td>${student.departmentName || "-"}</td>
					<td>${student.supervisorName || "-"}</td>
				</tr>
			`;
        })
        .join("");

    $("#studentsBody").html(rowsHtml || "<tr><td colspan='5'>No students found.</td></tr>");
}

function loadStudents() {
    $("#status").text("Loading...");

    $.ajax({
        url: apiUrl,
        method: "GET"
    })
        .done(function (response) {
            renderStudents(response.items || []);
            $("#status").text(
                "Page " + response.pageNumber +
                " | Page Size " + response.pageSize +
                " | Total " + response.totalCount
            );
        })
        .fail(function (xhr) {
            const message = xhr.responseText || "Request failed. Check API is running and CORS is allowed.";
            $("#status").text(message);
            $("#studentsBody").html("<tr><td colspan='5'>Could not load students.</td></tr>");
        });
}

$(function () {
    $("#loadStudentsBtn").on("click", loadStudents);
    loadStudents();
});
