﻿var popup, dataTable;
var entity = 'Others';
var apiurl = '/api/' + entity;

$(document).ready(function () {
    var organizationId = $('#organizationId').val();
    dataTable = $('#grid').DataTable({
        "ajax": {
            "url": apiurl + '/' + organizationId,
            "type": 'GET',
            "datatype": 'json'
        },
        "columns": [
            { "data": "items" },
            { "data": "price" },
            {
                "data": function (data) {
                    var d = new Date(data["date"]);
                    var output = monthNames[d.getMonth()] + " " + d.getDate() + ", " + d.getFullYear() + " - " + setClockTime(d);;
                    return output;
                }
            },
            { "data": "remarks" },
            {
                "data": function (data) {
                    var btnEdit = "<a class='btn btn-default btn-xs' onclick=ShowPopup('/Others/AddEditIndex?id=" + data["id"] + "')><i class='fa fa-pencil' title='Edit'></i></a>";
                    var btnDelete = "<a class='btn btn-danger btn-xs' style='margin-left:5px' onclick=Delete('" + data["id"] + "')><i class='fa fa-trash' title='Delete'></i></a>";
                    var btnView = "<a class='btn btn-default btn-xs' style='margin-left:5px' onclick=ShowPopup('/Others/ViewOthersIndex?id=" + data["id"] + "')><i class='fa fa-external-link' title='More'></i></a>";
                    var btnBuy = "<a class='btn btn-success btn-xs btnBuy' data-id='" + data["id"] + "'>Buy</a>";

                    return btnEdit + btnView + btnDelete + " " + btnBuy;
                }
            }
        ],
        "language": {
            "emptyTable": "no data found."
        },
        "lengthChange": false,
    });
});

function ShowPopup(url) {
    var modalId = 'modalDefault';
    var modalPlaceholder = $('#' + modalId + ' .modal-dialog .modal-content');
    $.get(url)
        .done(function (response) {
            modalPlaceholder.html(response);
            popup = $('#' + modalId + '').modal({
                keyboard: false,
                backdrop: 'static'
            });
        });
}
$("#grid").on("click", ".btnBuy", function (e) {
    e.preventDefault();
    var id = $(this).attr("data-id");
    //alert(id);
    var param = { id: id };
    swal({
        title: "Are you sure want to complete this transaction?",
        text: "You will not be able to restore the file!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#dd4b39",
        confirmButtonText: "Yes, update it!",
        closeOnConfirm: true
    }, function () {
        $.ajax({
            type: 'POST',
            url: apiurl + '/PostBuy',
            data: param,
            success: function (data) {
                if (data.success) {
                    ShowMessage(data.message);
                    dataTable.ajax.reload();
                } else {
                    ShowMessageError(data.message);
                }
            }
        });
    });
});

function SubmitAddEdit(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        var data = $(form).serializeJSON();
        data = JSON.stringify(data);
        $.ajax({
            type: 'POST',
            url: apiurl,
            data: data,
            contentType: 'application/json',
            success: function (data) {
                if (data.success) {
                    popup.modal('hide');
                    ShowMessage(data.message);
                    dataTable.ajax.reload();
                } else {
                    ShowMessageError(data.message);
                }
            }
        });

    }
    return false;
}
const monthNames = ["January", "February", "March", "April", "May", "June",
    "July", "August", "September", "October", "November", "December"
];
function setClockTime(d) {
    var h = d.getHours();
    var m = d.getMinutes();
    var s = d.getSeconds();
    var suffix = "AM";
    if (h > 11) { suffix = "PM"; }
    if (h > 12) { h = h - 12; }
    if (h == 0) { h = 12; }
    if (h < 10) { h = "0" + h; }
    if (m < 10) { m = "0" + m; }
    return h + ":" + m + " " + suffix;
}
function Delete(id) {
    swal({
        title: "Are you sure want to Delete?",
        text: "You will not be able to restore the data!",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#dd4b39",
        confirmButtonText: "Yes, delete it!",
        closeOnConfirm: true
    }, function () {
        $.ajax({
            type: 'DELETE',
            url: apiurl + '/' + id,
            success: function (data) {
                if (data.success) {
                    ShowMessage(data.message);
                    dataTable.ajax.reload();
                } else {
                    ShowMessageError(data.message);
                }
            }
        });
    });


}



