$(document).ready(function () {
    $("#test_delete").click(function () {
        message();
    });
    $("#cancel").click(function () {
        hide();
    });
});

function message() {
    $("#delete_message").slideDown();
}

function hide() {
    $("#delete_message").slideUp();
}