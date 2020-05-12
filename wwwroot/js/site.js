// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



//ja added Datatables.com jquery block below to target tables with id of tblData - corressponds with
//    @RenderSection("Scripts", required: false) at bottom of _layout master page 

$(document).ready(function () {
    $('#tblData').DataTable();
});
