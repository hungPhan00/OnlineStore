// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$('#create').on('click', function () {
    $('#createModal').modal('show');
});
$('#createCloseBtnTop').on('click', function () {
    $('#createModal').modal('toggle');
});
$('#createCloseBtnBottom').on('click', function () {
    $('#createModal').modal('toggle');
});
//edit
$('#edit').on('click', function () {
    $('#editModal').modal('show');
});
$('#editCloseBtnTop').on('click', function () {
    $('#editModal').modal('toggle');
});
$('#editCloseBtnBottom').on('click', function () {
    $('#editModal').modal('toggle');
});
//delete
$('#delete').on('click', function () {
    $('#deleteModal').modal('show');
});
$('#deleteCloseBtnTop').on('click', function () {
    $('#deleteModal').modal('toggle');
});
$('#deleteCloseBtnBottom').on('click', function () {
    $('#deleteModal').modal('toggle');
});