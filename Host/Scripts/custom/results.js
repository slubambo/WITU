
$(document).ready(function() {

    if ($('#academic-board-table').length) {
        //dataTable without intial sort...
        $('#academic-board-table').dataTable({
            dom: 'Bfrtip',
            'aaSorting': [],
            'scrollX': true,
            buttons: [
             'csv'
            ]
        });
    }

    if ($('#progression-students-table').length) {
        $('#progression-students-table').DataTable({
            dom: 'Bfrtip',
            'aaSorting': [],
            buttons: [
              'csv'
            ]
        });
    }
    if ($('#students-table').length) {
        
        $('#students-table').DataTable({
            dom: 'Bfrtip',
            'aaSorting': [],
            buttons: [
              'csv'
            ]
        });

    }


    //datatables for student details...
    if ($('.tbl-student-result').length) {

        //enable and disable buttons...
        enableAndDisableButtons();

        $('.tbl-student-result').DataTable({
            'processing': true,
            'ordering': false,
            'info': false,
            'bFilter': false,
            'paging': false,
            'columnDefs': [
                {
                    'targets': [0, 10],
                    'visible': false,
                    'searchable' : false
                }
            ],
            'rowCallBack': function(row, data) {
                if ($.inArray(data._DT_RowIndex, selected) !== -1) {
                    $(row).addClass('selected');
                }
            }
        });

        $('.tbl-student-result tbody').on('click', 'tr', function () {
            
            var aDataRow = $(this).closest('table').DataTable().row(this).data();

            console.log("aData: " + JSON.stringify(aDataRow));

            var isSelectable = aDataRow[10].toLowerCase() == 'true';
            console.log("Selectabe Status: " + isSelectable);

            if (isSelectable)
                $(this).toggleClass('selected');
            else
                showToast("This result cannot be modified at this point.", "warning");

            //enable and disable buttons...
            enableAndDisableButtons();
        });

        if ($('#btn-delete-courses').length) {
            
            //deleting the selected items...
            $('#btn-delete-courses').click(function () {

                

                var message = "<div> <div id='dv-success-notification'></div>" +
                    "The following items shall be deleted from the student. Are you sure?<ul>";

                var collection = collectSelectedTableRows();
                console.log('Collection:  ' + JSON.stringify(collection));

                //looping through the elements of the clas...
                var items = [];

                $(collection).each(function (i, item) {
                    message += "<li>" + item[1] + " " + item[2] + "</li>";
                    items.push({
                        StudentCourseId: item[0],
                        Code: item[1],
                        Name: item[2]
                    });
                });

                message += "</ul>";

                //including a text area after...
                message += "<div class='form-group'><label class='control-label'>Please Provide a Reason for approval (Optional)</label>" +
                    "<div class=''><textarea class='form-control' id='txt-delete-student-course-reason' placeholder='Reason'></textarea> </div>" +
                    "</div> </div>";

                //showing items..
                console.log("Items: " + JSON.stringify(items));
                //console.log("rows: " + JSON.stringify(rows));

                //finally we delete all..?
                var deleteCallBackfn = function confirmDeletion($dialog) { //pass the dialog for usage...
                    console.log("then we delete...");

                    var reasonForDeleting = $('#txt-delete-student-course-reason').val();
                    var removeStudentCourses = {
                        items: items,
                        reason: reasonForDeleting
                    };

                    $.ajax({
                        url: baseUrl + "Api/UtilsApi/RemoveStudentCourses",
                        type: 'DELETE',
                        data: removeStudentCourses,
                        success: function(data) {
                            console.log(data);
                            
                            //adding a status notification
                            $('#dv-success-notification').html("<div class='alert alert-success well-sm hide' id='dv-edit-student-course-success'>" +
                                + "<button type='button' class='close' data-dismiss='alert'>×</button>" +
                                + "<span>"
                                + "<span class='glyphicon glyphicon-saved'></span>" +
                                + "<span class='' id='sp-edit-student-course-success'>" +
                                $('.selected').length + " item(s) have been successfully sent for deleting and are awaiting approval." +
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
                        error: function(err) {
                            console.log('Just an error: ' + err);
                        }
                    });
                };

                ConfirmBeforeSubmission("Confirm Deletion", message, deleteCallBackfn);

            });
        }

        if ($('#btn-edit-course').length) {
            $('#btn-edit-course').click(function() {
                var item = {};
                $('.selected').each(function(i, obj) {
                    var rowItem = $(obj).closest('table').DataTable().row(obj).data();
                    console.log("Item Row: " + JSON.stringify(rowItem));

                    item.StudentCourseId = rowItem[0];
                    item.Code = rowItem[1];
                    item.Name = rowItem[2];

                });

                //after which we load the dialog...
                EditStudentCourse(item);
            });
        }
    }
    
    //when the course has changed...
    if ($('#in-add-student-course-student-id').length && $('#drpdwn-course').length) {
        var studentId = $('#in-add-student-course-student-id').val();
        var courseId = $('#drpdwn-course :selected').val();

        //loading itmes...
        studentCourseOnChangeIsCourseTypeModifiable(courseId, studentId);

        //if (courseId > 0) {
        //    studentCourseOnChangeIsCourseTypeModifiable(courseId, studentId);
        //}

        $('#drpdwn-course').on('change', function () {
            
            courseId = $(this).val();
            console.log('Student: ' + studentId + ' Course: ' + courseId);

            studentCourseOnChangeIsCourseTypeModifiable(courseId, studentId);
        });
    }

    //table-changes-pending approvals...
    if ($('#tbl-changes-pending').length) {

        //enable and disable buttons...
        enableAndDisableButtons();

        $('#tbl-changes-pending').DataTable({
            'processing': true,
            'ordering': false,
            'info': false,
            'bFilter': false,
            'paging': false,
            'columnDefs': [
                {
                    'targets': [9],
                    'visible': false,
                    'searchable': false
                }
            ] 
        });

        //opening the selected items...
        if ($('#btn-approval-change-approve').length) {
            $('#btn-approval-change-approve').click(function () {

                onApproveRejectBtn("Approve Changes","true");

                //var rowItem = collectSelectedTableRows()[0];

                //var resultChangeId = rowItem[0];

                ////reloading the page...
                //window.location.href = baseUrl + "Result/ApprovalDetail/" + resultChangeId;

            });
        }

        //on approval/disapproval selected...
        if ($('#btn-approval-change-reject').length) {
            $('#btn-approval-change-reject').click(function () {
                onApproveRejectBtn("Reject Changes", "false");
            });
        }

    }

    if ($('.chkbx-tbl-changes-pending').length) {

        //enable and disable buttons...
        enableAndDisableButtons();
        
        $('input.chkbx-tbl-changes-pending').change(function() {
            $(this).closest('tr').toggleClass('selected');
            
            enableAndDisableButtons();
        });

        //for all...
        $('input#tbl-chbx-changes-pending-all').change(function (e) {

            var table = $(e.target).closest('table');
            if (this.checked) {
                $('td input:checkbox', table).prop('checked', this.checked);
                $('td input:checkbox', table).closest('tr').addClass('selected');
            } else {
                $('td input:checkbox', table).removeAttr('checked');
                $('td input:checkbox', table).closest('tr').removeClass('selected');
            }

            enableAndDisableButtons();
        });

    }
    
    if ($('.tbl-publish-sc').length) {

        

        $('.tbl-publish-sc').DataTable({
            'processing': true,
            'ordering': false,
            "oTableTools": {
                "sRowSelect": "multi"
            },
            'columnDefs': [
            ]
        });
   

        if ($('.tbl-chbx-publish-sc-all').length) {

            enableAndDisableButtons();

            //selecting all checked items on load...
            $('table input:checked').each(function (i, obj) {
                //retrieve the checkbox...
                $(this).closest('tr').toggleClass('selected');

                //firefox disable checkbox on load 
                //$(this).attr("autocomplete", "off");
            });
        

            //$('table').on('change', 'input.tbl-chbx-publish-sc', function () {
            //    $(this).closest('tr').toggleClass('selected');
            //    console.log('Item selected: ' + JSON.stringify(this));

            //    var oTable = $(this).closest('table').DataTable();
            //    var row = oTable.row($(this).closest('tr')).node();
            //    $(row).toggleClass('selected');

            //    enableAndDisableButtons();//
            //});

            //for all...
            $('input.tbl-chbx-publish-sc-all').change(function (e) {

                var oTable = $(e.target).closest('table').dataTable();  
                var nNodes = oTable.fnGetNodes();

                $(nNodes).find(':checkbox').prop('checked', this.checked);
                if (this.checked) {
                    $(nNodes).addClass('selected');
                } else {
                    $(nNodes).removeClass('selected');
                }

                enableAndDisableButtons();
            });
        }

        //on publishing..
        $('#btn-publish').click(function () {

            //first we collect the necessar items..
            var rows = [];

            $('.tbl-publish-sc').each(function(i, table) {
                var oTable = $(table).dataTable();
                var nNodes = oTable.fnGetNodes();

                $(nNodes).find(':checkbox:checked').each(function (i, obj) {
                    //console.log('Selected Value: ' + $(obj).val());
                    rows.push($(obj).val());
                });
            });
            

//            var allTables = $('.tbl-publish-sc').DataTable();
//            var allRows = allTables.rows().nodes();
//            $(allRows).each(function (i, obj) {
//                //var node = obj.node();
//                var isPresent = $(obj).find('input:checkbox').prop('checked');
//                if (isPresent) {
//                    console.log("Row: " + JSON.stringify(obj));
//                    rows.push(allTables.row($(obj)).data());
//                }
//            });

            //$('table').each(function() {
            //    var oTable = $(this).DataTable();
            //    var nNodes = oTable.fnGetNodesItem: #1

            //    $(nNodes).each(function (i, obj) {
            //        console.log('Get Checked item:' + i);
            //        rows.push(oTable.row(obj).data());
            //    });
            //});

            //$("input:checked", $('table').dataTable().fnGetNodes()).each(function () {
            //    console.log($(this).val());
            //});

            //var selectedRows = collectSelectedTableRows();

            console.log('Items selected: ' + rows.length);

            var content = $('<div><div id="dv-publish-response"></div></div>');
            content.append('<p class="voffset-2">You are about to Publish <b>' + rows.length + '</b> items.');
            content.append('<textarea id="txt-publish-comment" class="form-control" placeholder="Please leave a Comment"></textarea>');

            ConfirmBeforeSubmission('Publish Results', content, function ($dialog) {
                //getting the loading dialog...
                $('#dv-publish-response').html(
                    '<div class"alert alert-info well-sm col-md-12">' +
                        '<span class="col-md-4"><span class="fa fa-2x fa-spin fa-spinner"></span></span>' +
                        '<span class="col-md-offset-2 col-md-6 text-left">Publishing, Please Wait... </span>' +
                    '<div>'
                );

                var response = {
                    StudentCourseIds: rows,
                    SemesterId: $('#in-publish-semester-id').val(),
                    PublishComment: $('#txt-publish-comment').val()
                };

                $.ajax({
                    url: baseUrl + 'Api/UtilsApi/PublishResults',
                    type: 'POST',
                    data: response,
                    success: function(data) {
                        if (data.SaveStatus) {
                            $('#dv-publish-response').html(
                                '<div class"alert alert-success well-sm col-md-12">' +
                                    '<span class="col-md-4"><span class="glyphicon glyphicon-saved"></span></span>' +
                                    '<span class="col-md-offset-2 col-md-6 text-left">Results have been successfully published. </span>' +
                                '<div>'
                            );
                        } else {
                            $('#dv-publish-response').html(
                                '<div class"alert alert-danger well-sm col-md-12">' +
                                    '<span class="col-md-4"><span class="glyphicon glyphicon-warning-sign"></span></span>' +
                                    '<span class="col-md-offset-2 col-md-6 text-left">Results Could not be Published. </span>' +
                                '<div>'
                            );
                        }
                    },
                    error: function() {

                    }
                });
                
            });
        });
    }

    if ($('#tbl-publish-result').length) {
        $('#tbl-publish-result').DataTable({
            'processing': true,
            'ordering': false,
            "oTableTools": {
                "sRowSelect": "multi"
            },
            'columnDefs': [
            ]
        });

        if ($('#tbl-chbx-publish-all').length) {

            enableAndDisableButtons();

            //selecting all checked items on load...
            $('table input:checked').each(function (i, obj) {
                //retrieve the checkbox...
                $(this).closest('tr').toggleClass('selected');
            });

            //for all...
            $('input#tbl-chbx-publish-all').change(function (e) {

                var oTable = $(e.target).closest('table').dataTable();
                var nNodes = oTable.fnGetNodes();

                $(nNodes).find(':checkbox').prop('checked', this.checked);
                if (this.checked) {
                    $(nNodes).addClass('selected');
                } else {
                    $(nNodes).removeClass('selected');
                }

                enableAndDisableButtons();
            });
        }


        //on publishing..
        $('#btn-publish-result').click(function () {

            //first we collect the necessar items..
            var rows = [];

            var oTable = $('#tbl-publish-result').DataTable();
            var nNodes = oTable.rows().nodes();

            $(nNodes).find(':checkbox:checked').each(function (i, obj) {
                //console.log('Selected Value: ' + $(obj).val());
                rows.push($(obj).val());
            });
            
            console.log('Items selected: ' + rows.length);

            var content = $('<div><div id="dv-publish-response"></div></div>');
            content.append('<p class="voffset-2">You are about to Publish <b>' + rows.length + '</b> items.');
            content.append('<textarea id="txt-publish-comment" class="form-control" placeholder="Please leave a Comment"></textarea>');

            ConfirmBeforeSubmission('Publish Results', content, function ($dialog) {
                //getting the loading dialog...
                $('#dv-publish-response').html(
                    '<div class"alert alert-info well-sm col-md-12">' +
                        '<span class="col-md-4"><span class="fa fa-2x fa-spin fa-spinner"></span></span>' +
                        '<span class="col-md-offset-2 col-md-6 text-left">Publishing, Please Wait... </span>' +
                    '<div>'
                );

                var response = {
                    PublishResultIds: rows,
                    PublishComment: $('#txt-publish-comment').val()
                };

                $.ajax({
                    url: baseUrl + 'Api/UtilsApi/PublishResults',
                    type: 'POST',
                    data: response,
                    success: function (data) {
                        if (data.SaveStatus) {
                            $('#dv-publish-response').html(
                                '<div class"alert alert-success well-sm col-md-12">' +
                                    '<span class="col-md-4"><span class="glyphicon glyphicon-saved"></span></span>' +
                                    '<span class="col-md-offset-2 col-md-6 text-left">Results have been successfully published. </span>' +
                                '<div>'
                            );

                            //reload after some mill seconds..
                            setTimeout(function () {
                                //close the dialog...
                                $dialog.close();

                                window.location.reload(true);
                            }, 1000);

                        } else {
                            $('#dv-publish-response').html(
                                '<div class"alert alert-danger well-sm col-md-12">' +
                                    '<span class="col-md-4"><span class="glyphicon glyphicon-warning-sign"></span></span>' +
                                    '<span class="col-md-offset-2 col-md-6 text-left">Results Could not be Published. </span>' +
                                '<div>'
                            );
                        }
                    },
                    error: function () {
                        $('#dv-publish-response').html(
                            '<div class"alert alert-danger well-sm col-md-12">' +
                                '<span class="col-md-4"><span class="glyphicon glyphicon-warning-sign"></span></span>' +
                                '<span class="col-md-offset-2 col-md-6 text-left">Server Access Error. </span>' +
                            '<div>'
                        );
                    }
                });

            });
        });
    }
    
    //Purge Upload stuff
    if ($('#tbl-purge-result').length) {
        $('#tbl-purge-result').DataTable({
            'processing': true,
            'ordering': false,
            "oTableTools": {
                "sRowSelect": "multi"
            },
            'columnDefs': [
            ]
        });

        if ($('#tbl-chbx-purge-all').length) {

            enableAndDisableButtons();

            //selecting all checked items on load...
            $('table input:checked').each(function (i, obj) {
                //retrieve the checkbox...
                $(this).closest('tr').toggleClass('selected');
            });

            //for all...
            $('input#tbl-chbx-purge-all').change(function (e) {

                var oTable = $(e.target).closest('table').dataTable();
                var nNodes = oTable.fnGetNodes();

                $(nNodes).find(':checkbox').prop('checked', this.checked);
                if (this.checked) {
                    $(nNodes).addClass('selected');
                } else {
                    $(nNodes).removeClass('selected');
                }

                enableAndDisableButtons();
            });
        }


        //on publishing..
        $('#btn-purge-result').click(function () {

            //alert($('#ResultSemId').val() + " - " + $('#ResultSemLevelId').val());
            //first we collect the necessar items..
            var rows = [];

            var oTable = $('#tbl-purge-result').DataTable();
            var nNodes = oTable.rows().nodes();

            $(nNodes).find(':checkbox:checked').each(function (i, obj) {
                //console.log('Selected Value: ' + $(obj).val());
                rows.push($(obj).val());
            });

            console.log('Items selected: ' + rows.length);

            var content = $('<div><div id="dv-purge-response"></div></div>');
            content.append('<p class="voffset-2">You are about to Purge <b>' + rows.length + '</b> items.');
            //content.append('<textarea id="txt-purge-comment" class="form-control" placeholder="Please leave a Comment"></textarea>');

            ConfirmBeforeSubmission('Purge Results', content, function ($dialog) {
                //getting the loading dialog...
                $('#dv-purge-response').html(
                    '<div class"alert alert-info well-sm col-md-12">' +
                        '<span class="col-md-4"><span class="fa fa-2x fa-spin fa-spinner"></span></span>' +
                        '<span class="col-md-offset-2 col-md-6 text-left">Purging, Please Wait... </span>' +
                    '<div>'
                );

                var response = {
                    PurgeResultIds: rows,
                    SemesterId: $('#ResultSemId').val(),
                    SemesterLevelId: $('#ResultSemLevelId').val()
                };
                
                $.ajax({
                    url: baseUrl + 'Api/UtilsApi/PurgeResults',
                    type: 'POST',
                    data: response,
                    success: function (data) {
                        if (data.SaveStatus) {
                            $('#dv-purge-response').html(
                                '<div class"alert alert-success well-sm col-md-12">' +
                                    '<span class="col-md-4"><span class="glyphicon glyphicon-saved"></span></span>' +
                                    '<span class="col-md-offset-2 col-md-6 text-left">Results have been successfully purged. </span>' +
                                '<div>'
                            );

                            //reload after some mill seconds..
                            setTimeout(function () {
                                //close the dialog...
                                $dialog.close();

                                window.location.reload(true);
                            }, 1000);

                        } else {
                            $('#dv-purge-response').html(
                                '<div class"alert alert-danger well-sm col-md-12">' +
                                    '<span class="col-md-4"><span class="glyphicon glyphicon-warning-sign"></span></span>' +
                                    '<span class="col-md-offset-2 col-md-6 text-left">Results Could not be Purged. </span>' +
                                '<div>'
                            );
                        }
                    },
                    error: function () {
                        $('#dv-purge-response').html(
                            '<div class"alert alert-danger well-sm col-md-12">' +
                                '<span class="col-md-4"><span class="glyphicon glyphicon-warning-sign"></span></span>' +
                                '<span class="col-md-offset-2 col-md-6 text-left">Server Access Error. </span>' +
                            '<div>'
                        );
                    }
                });

            });
        });
    }
});

function publishCheckBoxChange(obj) {

    var oTable = $(obj).closest('table').DataTable();
    var row = oTable.row($(obj).closest('tr')).node();
    $(row).toggleClass('selected');

    enableAndDisableButtons();

    //uncheck the checkbox for all if not all checkboxes are checked..
    var nNodes = oTable.rows().nodes();

    if ($(nNodes).find('.tbl-chbx-publish-sc:checked').length == $(nNodes).find('.tbl-chbx-publish-sc').length) {
        //if true then we are checking the master dude..
        $('#tbl-chbx-publish-all').prop('checked', true);
    } else {
        $('#tbl-chbx-publish-all').prop('checked', false);
    }
}

function purgeCheckBoxChange(obj) {

    var oTable = $(obj).closest('table').DataTable();
    var row = oTable.row($(obj).closest('tr')).node();
    $(row).toggleClass('selected');

    enableAndDisableButtons();

    //uncheck the checkbox for all if not all checkboxes are checked..
    var nNodes = oTable.rows().nodes();

    if ($(nNodes).find('.tbl-chbx-purge-sc:checked').length == $(nNodes).find('.tbl-chbx-purge-sc').length) {
        //if true then we are checking the master dude..
        $('#tbl-chbx-purge-all').prop('checked', true);
    } else {
        $('#tbl-chbx-purge-all').prop('checked', false);
    }
}

function onApproveRejectBtn(title, approvalStatus) {
    var rowItems = collectSelectedTableRows();
    var items = [];
    $(rowItems).each(function (i, obj) {
        var item = {
            ResultChangeId: obj[9],
            RegistrationNumber: obj[1],
            StudentName: obj[2],
            CourseCode: obj[3],
            CourseName: obj[4],
            ChangeStatus: obj[5],
            OldMark: obj[6],
            NewMark: obj[7]
        };
        items.push(item);
    });

    console.log("items: " + JSON.stringify(items));

    var message = $(
        '<div class="col-md-12">' +
            '<div class="text-justified"> Please approve/disapprove the following <b>' + items.length + ' change(s) </b> </div>' +
            '<hr/> ' +
            '<div id="dv-changes-notification"></div>' +
            '<div class="form-horizontal">' +
                '<input value="'+ approvalStatus +'" name="HasApproved" id="HasApproved" class="hidden">' +
                //'<div class="form-group">' +
                //    '<label class="control-label col-md-2" for="HasApproved">Approve</label>' +
                //    '<div class="col-md-10"> ' +
                //        '<label class="radio-inline">' +
                //            '<input class="radio" data-val="true" id="HasApproved" name="HasApproved" type="radio" value="True"> Approve ' +
                //        '</label>' +
                //        '<label class="radio-inline">' +
                //            '<input checked="checked" class="radio" id="HasApproved" name="HasApproved" type="radio" value="False"> Disapprove' +
                //        '</label>' +
                //    '</div>' +
                //'</div>' +
                '<div class="form-group">' +
                    '<label class="control-label col-md-2" for="Reason">Reason</label>' +
                    '<div class="col-md-10">' +
                        '<textarea class="form-control" cols="20" id="Reason" name="Reason" placeholder="Reason" rows="2"></textarea>' +
                        '<span class="text-danger field-validation-valid" data-valmsg-for="Reason" data-valmsg-replace="true"></span>' +
                    '</div>' +
                '</div>' +
            '</div>' +
        '</div>'
    );

    console.log("Change status: " + approvalStatus);

    var fnConfirmCallBack = function ($dialog) {
        var reason = $('#Reason').val();
        var selectedApproval = approvalStatus/*$('#HasApproved').val()*/;

        console.log("Selected: " + selectedApproval + " Reason: " + reason);

        if (/*selectedApproval.trim() == 'False' &&*/ reason.trim() == '') {
            $('.field-validation-valid').html('Please specify a reason when Approval has been denied.');
            $('.field-validation-valid').show();

            return;
        } else {
            $('.field-validation-valid').hide();
        }

        //finally we collect all the elements and send them...
        var model = {
            Reason: reason,
            HasApproved: selectedApproval,
            Items: items
        };

        //populating the spinner..
        $('#dv-changes-notification').html(
            '<div class="alert alert-info well-sm">' +
                '<span class="fa fa-spinner fa-spin fa-2x"></span>' +
                'Saving, Please wait...' +
            '</div>'
        );

        $.ajax({
            url: baseUrl + "API/UtilsApi/ConfirmChanges",
            type: 'PUT',
            data: model,
            success: function (data) {
                console.log(data);

                //adding a status notification
                $('#dv-changes-notification').html("<div class='alert alert-success well-sm'>" +
                    //"<button type='button' class='close' data-dismiss='alert'>×</button>" +
                    "<span>" +
                        "<span class='glyphicon glyphicon-saved'></span>" +
                        "<span class='' id='sp-edit-student-course-success'>" +
                            items.length + " item(s) have been successfully " + (selectedApproval == 'True' ? 'Approved.' : 'Denied. ') +
                        "</span>" +
                    "</span>" +
                "</div>");

                //reload after some mill seconds..
                setTimeout(function () {
                    //close the dialog...
                    $dialog.close();

                    window.location.reload(true);
                }, 1000);

            },
            error: function (err) {
                console.log('Just an error: ' + JSON.stringify(err));

                //error notification...
                $('#dv-changes-notification').html(
                    "<div class='alert alert-danger well-sm'>" +
                        "<button type='button' class='close' data-dismiss='alert'>×</button>" +
                        "<span>" +
                            "<span class='glyphicon glyphicon-warning-sign'></span>" +
                            "<span>" +
                                "Could Not Process Request. Something went wrong." +
                            "</span>" +
                        "</span>" +
                    "</div>");
            }
        });
    };

    //dialog for items...
    ConfirmBeforeSubmission(title, message, fnConfirmCallBack);
}

function collectSelectedTableRows() {
    var rows = [];

    $('.selected').each(function (i, obj) {

        var dataTable = $(obj).closest('table').DataTable();
        var row = dataTable.row(obj).data();
        rows.push(row);
    });

    return rows;
}

function enableAndDisableButtons() {
    var selectableLength = $('.selected').length;

    if (selectableLength > 0) {
        $('.btn-multiselect').removeAttr('disabled');
        $('.btn-single-select').removeAttr('disabled');
        if (selectableLength > 1) {
            $('.btn-single-select').attr('disabled', 'disabled');
        }
    } else {
        $('.btn-multiselect').attr('disabled', 'disabled');
        $('.btn-single-select').attr('disabled', 'disabled');
    }
}

function EditStudentCourse(item) {
    var loadUrl = baseUrl + "Result/EditStudentCourse/" + item.StudentCourseId;

    BootstrapDialog.show({
        title: "Edit Student Course - " + item.Code + ": " + item.Name,
        message: $('<div></div>').load(loadUrl),
        //onshown: function () {
        //    console.log("Dialog now showing...");
        //    $.validator.unobtrusive.parse("#fm-edit-specail-core");
        //}
    });
}

function EditStudentCourseOnBegin() {
    console.log('Here...');
    
    //disable the button...
    $('#btn-edit-student-course').attr("disabled", "disalbed");
    $('#sp-loading-icon').removeClass('hide');

    return true;
}

function EditStudentCourseOnComplete(content) {
//    var jsonData = content.get_response().get_object();
    //    var response
    //console.log("Content: " + JSON.stringify(content));
    //var response = content.responseText;

    //renable the button..
    $('#btn-edit-student-course').removeAttr('disabled');
    $('#sp-loading-icon').addClass('hide');

}

function EditStudentCourseOnSuccess(content) {
    console.log("Success? " + JSON.stringify(content));
    //show success...
    $('#dv-edit-student-course-success').removeClass('hide');

    //reload after some mill seconds..
    setTimeout(function () {
        window.location.reload(true);
    }, 1000);
}

function EditStudentCourseOnFailure() {
    console.log("Error!!");

    //pass on the notification that something ent wrong..
    $('.validation-summary-valid').find('ul').append("<li class='error text-danger'>Something Went wrong, results could not be saved.</li>");
    $('.validation-summary-valid').show();
}

function studentCourseOnChangeIsCourseTypeModifiable(courseId, studentId) {
    $.get(baseUrl + 'API/UtilsApi/IsCourseTypeModifiable/' + courseId + '/' + studentId, function(data) {
        console.log('Result: ' + JSON.stringify(data));
        //enable or disable or disable the dropdown option...
        if (data == true) {
            console.log("Enabling");
            $('#dv-student-course-course-type').removeAttr('disabled');
            $('#dv-student-course-course-type').find('select').removeAttr('disabled');
        } else {
            console.log("disabling");
            //set the dropdown to none...
            $('#drpdwn-add-student-course-type').val("");
            $('#dv-student-course-course-type').attr('disabled', 'disabled');
            $('#dv-student-course-course-type').find('select').attr('disabled', 'disabled');
        }
    });
}

function publishHistoryDetail(urlDetial) {
    BootstrapDialog.show({
        title: 'Publish Result Details',
        message: $('<div class="text-center"><span class="fa fa-spin fa-spinner"></span> Loading...</div>').load(urlDetial)
    });
}

function YearCalculationIconAccordionChanger(awardTypeId, year) {

    $('.year-accordion-' + year + '-at-'+awardTypeId).on('shown.bs.collapse', function () {
        $("#year-accordion-icon-" + year + '-at-' + awardTypeId).removeClass("glyphicon-plus").addClass("glyphicon-minus");
    });

    $('.year-accordion-' + year + '-at-' + awardTypeId).on('hidden.bs.collapse', function () {
        $("#year-accordion-icon-" + year + '-at-' + awardTypeId).removeClass("glyphicon-minus").addClass("glyphicon-plus");
    });
}

function UnPublishResult(id) {
    
    var response = {
        publishResultId: id
    };

    $.ajax({
        url: baseUrl + 'Result/UnPublishResults',
        type: 'POST',
        data: response,
        success: function (data) {
            if (data.SaveStatus) {
                $('#dv-unpublish-response').html(
                    '<div class"alert alert-success well-sm col-md-12">' +
                        '<span class="col-md-4"><span class="glyphicon glyphicon-saved"></span></span>' +
                        '<span class="col-md-offset-2 col-md-6 text-left">Results have been successfully unpublished. </span>' +
                    '<div>'
                );

                //reload after some mill seconds..
                setTimeout(function () {
                    //close the dialog...
                    //Closing was commented out because it was buggy and not working. So we go ahead and just reload the page
                    window.location.reload(true);
                }, 1000);

            } else {
                $('#dv-unpublish-response').html(
                    '<div class"alert alert-danger well-sm col-md-12">' +
                        '<span class="col-md-4"><span class="glyphicon glyphicon-warning-sign"></span></span>' +
                        '<span class="col-md-offset-2 col-md-6 text-left">Results Could not be UnPublished. </span>' +
                    '<div>'
                );
            }
        },
        error: function () {
            $('#dv-unpublish-response').html(
                '<div class"alert alert-danger well-sm col-md-12">' +
                    '<span class="col-md-4"><span class="glyphicon glyphicon-warning-sign"></span></span>' +
                    '<span class="col-md-offset-2 col-md-6 text-left">Server Access Error. </span>' +
                '<div>'
            );
        }
    });
    
}
