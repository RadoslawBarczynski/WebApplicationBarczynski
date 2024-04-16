$(document).ready(function () {
    $("#addTaskBtn").click(function () {
        var taskDescription = $("#taskDescription").val();
        var taskDate = $("#taskDate").val();

        $.ajax({
            url: '/Tasks/Add', 
            type: 'POST',
            data: {
                description: taskDescription,
                date: taskDate
            },
            success: function (data) {
                $("#taskDescription").val("");
                $("#taskDate").val("");

                $("#taskListContainer").html(data);
            },
            error: function (xhr, textStatus, errorThrown) {
                console.log('Error: ' + errorThrown);
            }
        });
    });

    $('#filterTasksBtn').click(function () {
        var startDate = $('#startDate').val();
        var endDate = $('#endDate').val();

        $.ajax({
            url: '/Tasks/Filter',
            type: 'GET',
            data: {
                startDate: startDate,
                endDate: endDate
            },
            success: function (data) {
                $("#taskListContainer").html(data);
            }
        });
    });

    $('#clearFiltersBtn').click(function () {
        $('#startDate').val("");
        $('#endDate').val("");

        $.ajax({
            url: '/Tasks/ClearFilters',
            type: 'GET',
            success: function (data) {
                $("#taskListContainer").html(data);
            }
        });
    });
});

$("#taskListContainer").on("click", ".deleteTask", function () {
    var taskId = $(this).data("task-id");

    $.ajax({
        url: '/Tasks/Delete/' + taskId,
        type: 'POST',
        success: function (data) {
            $("#taskListContainer").html(data);
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log('Error: ' + errorThrown);
        }
    });
});

$("#taskListContainer").on("change", ".completedCheckbox", function () {
    var taskId = $(this).data('task-id');
    var taskCompleted = $(this).prop('checked');

    $.ajax({
        url: '/Tasks/DoneSelection',
        method: 'POST',
        data: {
            id: taskId,
            completedP: taskCompleted
        },
        success: function (data) {

        },
        error: function (xhr, status, error) {
            console.error('Wystąpił błąd podczas aktualizacji stanu zadania:', error);
        }
    });
});

$("#taskListContainer").on("click", ".editTask", function () {
    var taskId = $(this).data("task-id");
    var taskDescription = $(this).data("task-description");
    var taskDate = $(this).data("task-date");

    var formattedDate = formatDateForInput(taskDate);

    $("#editTaskId").val(taskId);
    $("#editTaskDescription").val(taskDescription);
    $("#editTaskDate").val(formattedDate);

    $("#editTaskModal").modal("show");
});

$("#taskListContainer").on("click", "#updateTaskBtn", function () {
    var taskId = $("#editTaskId").val();
    var taskDescription = $("#editTaskDescription").val();
    var taskDate = $("#editTaskDate").val();
    var taskCompleted = $("#editTaskCompleted").prop("checked");

    $.ajax({
        url: '/Tasks/Update/' + taskId,
        type: 'POST',
        data: {
            description: taskDescription,
            date: taskDate
        },
        success: function (data) {
            $("#taskListContainer").html(data);
            $("#editTaskModal").modal("hide");
            $(".modal-backdrop").remove();
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log('Error: ' + errorThrown);
        }
    });
})

$(document).on("click", ".close", function () {
    $("#editTaskModal").modal("hide");
})

function formatDateForInput(dateString) {
    var parts = dateString.split(' ');
    var datePart = parts[0].split('/');
    var timePart = parts[1].split(':');

    var formattedDate = datePart[2] + '-' + datePart[1] + '-' + datePart[0] + 'T' + timePart[0] + ':' + timePart[1];
    return formattedDate;
}

$(document).on("click", ".completedCheckbox", function () {
    $('.table').on('change', '.completedCheckbox', function () {
        var row = $(this).closest('tr');
        if ($(this).is(':checked')) {
            row.addClass('completed');
        } else {
            row.removeClass('completed');
        }
    });
});

