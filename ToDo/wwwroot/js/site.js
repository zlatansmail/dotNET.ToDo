// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function deleteToDo(i) {
  $.ajax({
    url: "Home/Delete",
    type: "POST",
    data: { id: i },
    success: function () {
      window.location.reload();
    }
  });
}

function populateForm(i) { 
    $.ajax({
        url: "Home/PopulateForm",
        type: "GET",
        data: { id: i },
        success: function (response) {
        $("#ToDo_Name").val(response.name);
        $("#ToDo_Id").val(response.id);
        $("#form-button").val("Update ToDo");
        $("#form-button").attr("asp-action", "Update");
        }
    });
}