$(document).ready(function () {
    $('#addBtn').click(function () {
        let inputVal = $('#taskInput').val().trim();
        if (inputVal === '' || inputVal === null) {
            alert('Invalid');
            return;
        }
        let listItem = $('<li></li>').text(inputVal);
        let deleteBtn = $('<button></button>').text('Delete').addClass('deleteBtn');

        listItem.append(deleteBtn);
        $('#taskList').append(listItem);
        $('#taskInput').val('');
    });

    $('#taskList').on('click', '.deleteBtn', function () {
        $(this).parent().remove();
    });
});
