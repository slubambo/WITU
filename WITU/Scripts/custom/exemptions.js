

$(document).ready(function () {

    if ($('#btn-add-orphaned').length) {

        //deleting the selected items...
        $('#btn-add-orphaned').click(function () {



            var message = "<div> <div id='dv-success-notification'></div>" +
                "The following items from the student shall be given a reason for exemption. Are you sure?<ul>";

            var collection = collectSelectedTableRowsInOrphanedTable();
            console.log('Collection Orphaned:  ' + JSON.stringify(collection));

            //looping through the elements of the clas...
            var items = [];

            $(collection).each(function (i, item) {
                message += "<li>" + item[1] + " " + item[2] + "</li>";
                items.push({
                    StudentCourseId: item[7],
                    Code: item[1],
                    Name: item[2]
                });
            });

            message += "</ul>";

            //including a text area after...
            message += "<div class='form-group'><label class='control-label'>Please Provide a Reason for Exemption (*)</label>" +
                "<div class=''><textarea class='form-control' id='txt-exempt-student-course-reason' placeholder='e.g. AN ORDINARY DIPLOMA IN ELECTRICAL ENGINEERING - MAKERERE UNIVERSITY'></textarea> </div>" +
                "</div> </div>";

            //showing items..
            console.log("Items Orphaned: " + JSON.stringify(items));
            //console.log("rows: " + JSON.stringify(rows));

            //finally we delete all..?
            var addReasonCallBackfn = function confirmReason($dialog) { //pass the dialog for usage...
                console.log("then we delete...");

                var reasonForExempting = $('#txt-exempt-student-course-reason').val();
                var studentCoursesToAddReasonTo = {
                    items: items,
                    reason: reasonForExempting,
                    studentId: $("#student-id-input").val(),
                    userId: $("#user-id-input").val()
                };

                $.ajax({
                    url: baseUrl + "Api/UtilsApi/AddReasonToExemptStudentCourses",
                    type: 'POST',
                    data: studentCoursesToAddReasonTo,
                    success: function (data) {
                        console.log(data);

                        //adding a status notification
                        $('#dv-success-notification').html("<div class='alert alert-success well-sm hide' id='dv-edit-student-course-success'>" +
                            + "<button type='button' class='close' data-dismiss='alert'>×</button>" +
                            + "<span>"
                            + "<span class='glyphicon glyphicon-saved'></span>" +
                            + "<span class='' id='sp-edit-student-course-success'>" +
                            $('#orphaned-exemptions .selected').length + " item(s) have been successfully given reason for exemption and are awaiting approval." +
                            "</span>" +
                            "</span>" +
                            "</div>");

                        //RESULTS that are deleted wont be removed but disabled...
                        //$('.selected').each(function (i, obj) {
                        //    var dataTable = $(obj).closest('table').DataTable();
                        //    var row = dataTable.row(obj);
                        //    row.remove().draw(false);
                        //});

                        //reload after some mill seconds..
                        setTimeout(function () {
                            //close the dialog...
                            $dialog.close();

                            window.location.reload(true);
                        }, 1000);

                    },
                    error: function (err) {
                        console.log('Just an error: ' + err);
                    }
                });
            };

            ConfirmBeforeSubmission("Add Reason for Exemption", message, addReasonCallBackfn);

        });
    }


    //datatables for student details...
    if ($('.tbl-exemption-request').length) {

        //enable and disable buttons...
        enableAndDisableButtonsForRequestTable();

        $('.tbl-exemption-request').DataTable({
            'processing': true,
            'ordering': false,
            'info': false,
            'bFilter': false,
            'paging': false,
            'columnDefs': [
                {
                    'targets': [0, 5],
                    'visible': false,
                    'searchable': false
                }
            ],
            'rowCallBack': function (row, data) {
                if ($.inArray(data._DT_RowIndex, selected) !== -1) {
                    $(row).addClass('selected');
                }
            }
        });

        $('.tbl-exemption-request tbody').on('click', 'tr', function () {

            var aDataRow = $(this).closest('table').DataTable().row(this).data();

            console.log("aData: " + JSON.stringify(aDataRow));

            var isSelectable = aDataRow[5].toLowerCase() == 'true';
            console.log("Selectabe Status: " + isSelectable);

            if (isSelectable)
                $(this).toggleClass('selected');
            else
                showToast("This exemption cannot be modified at this point.", "warning");

            //enable and disable buttons...
            enableAndDisableButtonsForRequestTable();
        });
    }

    if ($('.tbl-courses-to-exempt-from').length) {

        //enable and disable buttons...
        //enableAndDisableButtonsForRequestTable();

        $('.tbl-courses-to-exempt-from').DataTable({
            'processing': true,
            'ordering': false,
            'info': false,
            'bFilter': false,
            'paging': false
        });

        
    }

    if ($('#btn-approval-exempt-approve').length) {

        //deleting the selected items...
        $('#btn-approval-exempt-approve').click(function () {



            var message = "<div> <div id='dv-success-notification'></div>" +
                "The following courses shall be exempted from the student. Are you sure?<ul>";

            var collection = collectSelectedTableRowsInRequestTables();
            console.log('Collection:  ' + JSON.stringify(collection));

            //looping through the elements of the clas...
            var items = [];

            $(collection).each(function (i, item) {
                message += "<li>" + item[1]  + "</li>";
                items.push({
                    StudentCourseId: item[0],
                    Name: item[1],
                    Code: item[2]
                });
            });

            message += "</ul>";

            //showing items..
            console.log("Items: " + JSON.stringify(items));
            

            //finally we delete all..?
            var exemptCallBackfn = function confirmExemption($dialog) { //pass the dialog for usage...
                console.log("then we exempt...");

                
                var exemptStudentCourses = {
                    items: items,
                    studentId: $("#student-id-input").val(),
                    userId: $("#user-id-input").val()
                };

                $.ajax({
                    url: baseUrl + "Api/UtilsApi/ExemptStudentCourses",
                    type: 'POST',
                    data: exemptStudentCourses,
                    success: function (data) {
                        console.log(data);

                        //adding a status notification
                        $('#dv-success-notification').html("<div class='alert alert-success well-sm hide' id='dv-edit-student-course-success'>" +
                            + "<button type='button' class='close' data-dismiss='alert'>×</button>" +
                            + "<span>"
                            + "<span class='glyphicon glyphicon-saved'></span>" +
                            + "<span class='' id='sp-edit-student-course-success'>" +
                            $('.tbl-exemption-request .selected').length + " item(s) have been successfully exempted." +
                            "</span>" +
                            "</span>" +
                            "</div>");

                        //RESULTS that are deleted wont be removed but disabled...
                        //$('.selected').each(function (i, obj) {
                        //    var dataTable = $(obj).closest('table').DataTable();
                        //    var row = dataTable.row(obj);
                        //    row.remove().draw(false);
                        //});

                        //reload after some mill seconds..
                        setTimeout(function () {
                            //close the dialog...
                            $dialog.close();

                            window.location.reload(true);
                        }, 1000);

                    },
                    error: function (err) {
                        console.log('Just an error: ' + err);
                    }
                });
            };

            ConfirmBeforeSubmission("Confirm Exemption Approval", message, exemptCallBackfn);

        });
    }

    if ($('#btn-approval-exempt-reject').length) {

        //deleting the selected items...
        $('#btn-approval-exempt-reject').click(function () {



            var message = "<div> <div id='dv-success-notification'></div>" +
                "The following course exemption requests shall be rejected. Are you sure?<ul>";

            var collection = collectSelectedTableRowsInRequestTables();
            console.log('Collection:  ' + JSON.stringify(collection));

            //looping through the elements of the clas...
            var items = [];

            $(collection).each(function (i, item) {
                message += "<li>" + item[1] + "</li>";
                items.push({
                    StudentCourseId: item[0],
                    Name: item[1],
                    Code: item[2]
                });
            });

            message += "</ul>";

            //showing items..
            console.log("Items: " + JSON.stringify(items));


            //finally we delete all..?
            var exemptRejectCallBackfn = function confirmExemptRejection($dialog) { //pass the dialog for usage...
                console.log("then we reject...");


                var exemptRejectStudentCourses = {
                    items: items,
                    studentId: $("#student-id-input").val(),
                    userId: $("#user-id-input").val()
                };

                $.ajax({
                    url: baseUrl + "Api/UtilsApi/RejectExemptStudentCourses",
                    type: 'POST',
                    data: exemptRejectStudentCourses,
                    success: function (data) {
                        console.log(data);

                        //adding a status notification
                        $('#dv-success-notification').html("<div class='alert alert-success well-sm hide' id='dv-edit-student-course-success'>" +
                            + "<button type='button' class='close' data-dismiss='alert'>×</button>" +
                            + "<span>"
                            + "<span class='glyphicon glyphicon-saved'></span>" +
                            + "<span class='' id='sp-edit-student-course-success'>" +
                            $('.tbl-exemption-request .selected').length + " item(s) have been successfully rejected from exemption." +
                            "</span>" +
                            "</span>" +
                            "</div>");

                        //RESULTS that are deleted wont be removed but disabled...
                        //$('.selected').each(function (i, obj) {
                        //    var dataTable = $(obj).closest('table').DataTable();
                        //    var row = dataTable.row(obj);
                        //    row.remove().draw(false);
                        //});

                        //reload after some mill seconds..
                        setTimeout(function () {
                            //close the dialog...
                            $dialog.close();

                            window.location.reload(true);
                        }, 1000);

                    },
                    error: function (err) {
                        console.log('Just an error: ' + err);
                    }
                });
            };

            ConfirmBeforeSubmission("Confirm Exemption Rejection", message, exemptRejectCallBackfn);

        });
    }
    
    if ($('.tbl-courses-to-exempt-from').length) {

        enableAndDisableExemptButton();
    }

});

function collectSelectedTableRowsInOrphanedTable() {
    var rows = [];

    $('#orphaned-exemptions .selected').each(function (i, obj) {

        var dataTable = $(obj).closest('table').DataTable();
        var row = dataTable.row(obj).data();
        rows.push(row);
    });

    return rows;
}

function addOrphanedCheckBoxChange(obj) {

    var oTable = $(obj).closest('table').DataTable();
    var row = oTable.row($(obj).closest('tr')).node();
    $(row).toggleClass('selected');

    enableAndDisableAddReasonButton();

    //uncheck the checkbox for all if not all checkboxes are checked..
    var nNodes = oTable.rows().nodes();

    if ($(nNodes).find('.tbl-chbx-addorphaned-sc:checked').length == $(nNodes).find('.tbl-chbx-addorphaned-sc').length) {
        //if true then we are checking the master dude..
        $('#tbl-chbx-addorphaned-all').prop('checked', true);
    } else {
        $('#tbl-chbx-addorphaned-all').prop('checked', false);
    }
}

function exemptCourseCheckBoxChange(obj) {

    var oTable = $(obj).closest('table').DataTable();
    var row = oTable.row($(obj).closest('tr')).node();
    $(row).toggleClass('selected');

    enableAndDisableExemptButton();
}


function enableAndDisableAddReasonButton() {
    var selectableLength = $('#orphaned-exemptions .selected').length;

    if (selectableLength > 0) {
        $('.btn-multiselect-orphaned').removeAttr('disabled');

    } else {
        $('.btn-multiselect-orphaned').attr('disabled', 'disabled');
    }
}

function enableAndDisableExemptButton() {
    var selectableLength = $('.tbl-courses-to-exempt-from .selected').length;

    if (selectableLength > 0) {
        $('.btn-multiselect-exemptcourses').removeAttr('disabled');

    } else {
        $('.btn-multiselect-exemptcourses').attr('disabled', 'disabled');
    }
}

function collectSelectedTableRowsInRequestTables() {
    var rows = [];

    $('.tbl-exemption-request .selected').each(function (i, obj) {

        var dataTable = $(obj).closest('table').DataTable();
        var row = dataTable.row(obj).data();
        rows.push(row);
    });

    return rows;
}

function enableAndDisableButtonsForRequestTable() {
    var selectableLength = $('.tbl-exemption-request .selected').length;

    if (selectableLength > 0) {
        $('.btn-multiselect-requests').removeAttr('disabled');
        $('.btn-single-select-requests').removeAttr('disabled');
        if (selectableLength > 1) {
            $('.btn-single-select-requests').attr('disabled', 'disabled');
        }
    } else {
        $('.btn-multiselect-requests').attr('disabled', 'disabled');
        $('.btn-single-select-requests').attr('disabled', 'disabled');
    }
}

function deleteExemptedOrRejectedCourse(obj) {


    var oTable = $(obj).closest('table').DataTable().row($(obj).closest('tr')).data();

    var scId = oTable[0];
    console.log("This was clicked: " + scId);

    var message = "<div> <div id='dv-success-notification'></div>" +
            "Are you sure you would like to delete " + oTable[1] + " from the student's exemptions?";


    var deletionCallBackfn = function confirmDeletion($dialog) { //pass the dialog for usage...
        console.log("then we delete...");
        var studentCourseToDelete = {
            studentCourseId: scId,
            studentId: $("#student-id-input").val(),
            userId: $("#user-id-input").val()
        };

        $.ajax({
            url: baseUrl + "Api/UtilsApi/DeleteExemptedOrRejectedStudentCourse",
            type: 'DELETE',
            data: studentCourseToDelete,
            success: function (data) {
                console.log(data);

                //adding a status notification
                $('#dv-success-notification').html("<div class='alert alert-success well-sm hide' id='dv-edit-student-course-success'>" +
                    + "<button type='button' class='close' data-dismiss='alert'>×</button>" +
                    + "<span>"
                    + "<span class='glyphicon glyphicon-saved'></span>" +
                    + "<span class='' id='sp-edit-student-course-success'>" +
                    "1  " + " item has been successfully deleted." +
                    "</span>" +
                    "</span>" +
                    "</div>");

                //RESULTS that are deleted wont be removed but disabled...
                //$('.selected').each(function (i, obj) {
                //    var dataTable = $(obj).closest('table').DataTable();
                //    var row = dataTable.row(obj);
                //    row.remove().draw(false);
                //});

                //reload after some mill seconds..
                setTimeout(function () {
                    //close the dialog...
                    $dialog.close();

                    window.location.reload(true);
                }, 1000);

            },
            error: function (err) {
                console.log('Just an error: ' + err);
            }
        });
    };

    ConfirmBeforeSubmission("Delete Exemption", message, deletionCallBackfn);

}

function AddExemptionRequest(studentId, userId) {
    var loadUrl = baseUrl + "Registration/AddExemptionRequest?studentId="+studentId+"&&userId="+userId;

    BootstrapDialog.show({
        title: "Add Exemption Request",
        message: $('<div></div>').load(loadUrl),

    });
}

function AddExemptionRequestByStudent() {
    var loadUrl = baseUrl + "Student/AddExemptionRequest";

    BootstrapDialog.show({
        title: "Add Exemption Request",
        message: $('<div></div>').load(loadUrl),

    });
}

function AddExemptionRequestOnBegin() {
    console.log('Here...');

    //disable the button...
    $('#btn-add-exemption-request').attr("disabled", "disabled");
    $('#sp-loading-icon').removeClass('hide');

    return true;
}

function AddExemptionRequestOnComplete(content) {
    //    var jsonData = content.get_response().get_object();
    //    var response
    //console.log("Content: " + JSON.stringify(content));
    //var response = content.responseText;

    //renable the button..
    $('#btn-add-exemption-request').removeAttr('disabled');
    $('#sp-loading-icon').addClass('hide');

}

function AddExemptionRequestOnSuccess(content) {
    console.log("Success? " + JSON.stringify(content));
    //show success...
    $('#dv-add-exemption-request-success').removeClass('hide');

    // $('#drpdwn-faculty').append($('<option></option>').val(faculties.Value).html(faculties.Text));
    //$("#drpdwn-faculty").val(facultyId);

    if ($('#drpdwn-exp-request').length) {
        var object = content;
        $('#drpdwn-exp-request').append($('<option></option>').val(object.Id).html(object.Reason));
        $("#drpdwn-exp-request").val(object.Id);
        if ($('.chosen-select').length) {
            $("#drpdwn-exp-request").trigger("chosen:updated");
        }

    }
    
    //reload after some mill seconds..
    //    setTimeout(function () {
    //        window.location.reload(true);
    //    }, 1000);
}

function AddExemptionRequestOnFailure(content, responseR) {
    console.log("Error!!" + content);
    var object = JSON.parse(content.responseText);
    var objMsg = object.Message;
    var message = "<li class='error text-danger'> " + objMsg + " </li>";
    //pass on the notification that something went wrong..
    $('.validation-summary-valid').find('ul').html(message);
    $('.validation-summary-valid').show();
}

function DeleteExemptionRequest(exemptionRequestId, exemptionRequestReason)
{
    console.log("This was clicked for deletion: " + exemptionRequestId + " with Reason " + exemptionRequestReason);
    
    var message = "<div> <div id='dv-success-notification'></div>" +
            "Are you sure you would like to remove the exemption request '" + exemptionRequestReason + "' from the student's exemptions?";
    
    var deletionCallBackfn = function confirmDeletion($dialog) { //pass the dialog for usage...
        console.log("then we delete...");
        var exemptionRequestToDelete = {
            id: exemptionRequestId
        };

        $.ajax({
            url: baseUrl + "Registration/DeleteExemptionRequest",
            type: 'POST',
            data: exemptionRequestToDelete,
            success: function (data) {
                console.log(data);

                if (data.isDeleted) {
                    showToast(data.Message, "success");
                    
                    //reload after some mill seconds..
                    setTimeout(function () {
                        //close the dialog...
                        $dialog.close();

                        window.location.reload(true);
                    }, 1000);
                    
                } else {
                    showToast(data.Message, "error");
                    //reload after some mill seconds..
                    setTimeout(function () {
                        //close the dialog...
                        $dialog.close();

                        //window.location.reload(true);
                    }, 1000);
                }
                //adding a status notification
                //$('#dv-success-notification').html("<div class='alert alert-success well-sm hide' id='dv-edit-student-course-success'>" +
                //    + "<button type='button' class='close' data-dismiss='alert'>×</button>" +
                //    + "<span>"
                //    + "<span class='glyphicon glyphicon-saved'></span>" +
                //    + "<span class='' id='sp-edit-student-course-success'>" +
                //    "1  " + " item has been successfully deleted." +
                //    "</span>" +
                //    "</span>" +
                //    "</div>");

                //RESULTS that are deleted wont be removed but disabled...
                //$('.selected').each(function (i, obj) {
                //    var dataTable = $(obj).closest('table').DataTable();
                //    var row = dataTable.row(obj);
                //    row.remove().draw(false);
                //});

                

            },
            error: function (err) {
                console.log('Just an error: ' + err);
            }
        });
    };

    ConfirmBeforeSubmission("Delete Exemption Request", message, deletionCallBackfn);
    
}