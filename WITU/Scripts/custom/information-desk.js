/// <reference path="../dataTables/js/jquery.dataTables.min.js" />
/// <reference path="base-script.js" />


$(document).ready(function () {
    
    if ($('.timeline-view').length) {
        viewTimeline();
    }

    $('#table-timeline-dates').dataTable({ "bSort": false, "bDestroy": true });

    //items pertaining to uploading attachments...
    if ($('#fileUpload').length) {
        $("#fileUpload").fileupload({
            url: baseUrl + "Api/UtilsApi/UploadInformationDeskFiles",
            dataType: 'json',
            type: 'post',
            formData: {
                type: $('#UploadedFileType').val()
            },
            maxFileSize: 10000000, //limiting to 10MB upload..
            //acceptFileTypes: /(\.|\/)(gif|jpe?g|png)$/i,
            //            paramName:'Files',
            add: function (e, data) {
                console.log("you be in add...");

                var goUpload = true;
                var uploadFile = data.files[0];
                if (!(/\.(gif|jpg|jpeg|tiff|png|pdf|doc|docx|xls|xlsx|ppt|pptx)$/i).test(uploadFile.name)) {
                    AlertMessageDialog("The file uploaded is not valid.", "Error on File upload");

                    goUpload = false;
                }
                //if (uploadFile.size > 2000000) { // 2mb
                //    common.notifyError('Please upload a smaller image, max size is 2 MB');
                //    goUpload = false;
                //}
                if (goUpload == true) {
                    data.submit();
                }
            },
            done: function (e, data) {
                //retriving elements...
                $("#tmpl-uploaded-file").tmpl(data.result.files).appendTo("#files");
            },
            progressall: function (e, data) {
                var progress = parseInt(data.loaded / data.total * 100, 10);
                $('#progress .progress-bar').css(
                    'width',
                    progress + '%'
                );
            }
        }).prop('disabled', !$.support.fileInput)
        .parent().addClass($.support.fileInput ? undefined : 'disabled');
     
        //showing uploaded files when they are already present..
        if ($("#UploadedFileType") != undefined && $("#UploadFileId") != undefined) {
            //make an ajax call..
            $.ajax({
                url: baseUrl + "api/UtilsApi/Attachments/" + $("#UploadedFileType").val() + "/" + $("#UploadFileId").val(),
                type: "GET",
                dataType: 'JSON',
                contentType: 'application/json',
                success: function (data) {
                    $("#tmpl-uploaded-file").tmpl(data).appendTo("#files");
                }
            });
        }
    }
    

    //previewing files uploaded...
    if ($("#info-attached-files").length && $('#info-attach-actual-files').length) {
        //make an ajax call..
        $.ajax({
            url: baseUrl + "api/UtilsApi/Attachments/" + $("#UploadedFileType").val() + "/" + $("#UploadFileId").val() + "/" + 3,
            type: "GET",
            dataType: 'JSON',
            contentType: 'application/json',
            success: function (data) {
                $("#tmpl-uploaded-file-view").tmpl(data).appendTo("#info-attach-actual-files");
            }
        });
    }

    //slick slider
    if ($('.slick-slider').length) {

        $('.slick-slider').slick({
            dots: true,
            infinite: false,
            speed: 300,
            slidesToShow: 4,
            slidesToScroll: 4,
            responsive: [
              {
                  breakpoint: 1024,
                  settings: {
                      slidesToShow: 3,
                      slidesToScroll: 3,
                      infinite: true,
                      dots: true
                  }
              },
              {
                  breakpoint: 600,
                  settings: {
                      slidesToShow: 2,
                      slidesToScroll: 2
                  }
              },
              {
                  breakpoint: 480,
                  settings: {
                      slidesToShow: 1,
                      slidesToScroll: 1
                  }
              }
            ]
        });
    }
});

function viewTimeline() {
    $.getJSON(baseUrl + 'Api/UtilsApi/Timeline', function (data) {
        var today = new Date();
        if (data != null) {
            //SETTING THE DEFAULT INDEX: 
            if (data.timeline != null) {
                var timeline_dates = data.timeline.date;
            }
            //if (timeline_dates == null) {
            //    timeline_dates = today.setDate(15);
            //}
            
            var start_index = 0;
            var targe_date = new Date();
            if (timeline_dates != null) {
                for (x in timeline_dates) {
                    var slide_date = new Date(timeline_dates[x].startDate);
                    if (slide_date < targe_date)
                        start_index++;
                }

                createStoryJS({
                    type: 'timeline',
                    height: 400,
                    source: data,
                    embed_id: 'timeline-view',
                    debug: false,
                    start_at_slide: start_index,
                    start_zoom_adjust: 2
                });
            }
        }
    });
}

function RemoveDate(id, headline) {

    //var message = "Are you Sure you want to delete <b>'" + headline
    //    + "'</b> event? This will permentaly be deleted.";

    //BootstrapDialog.show({
    //    title: "Deleting " + headline + " Event",
    //    message: message,
    //    buttons: [{
    //        label: 'Ok',
    //        action: function (dialogRef) {

    //            $.ajax({
    //                url: baseUrl + 'InformationDesk/DeleteTimelineDate/' + id,
    //                success: function (data) {
    //                    console.log("Delete Status: " + data);

    //                    if (data) {
    //                        popDiv.dialog("close");

    //                        //removing contact...
    //                        tr_id = "tr#tr-timeline-date-" + id;
    //                        tb_id = "table#table-timeline-dates";

    //                        var thisDatTable = $("#table-timeline-dates").dataTable();
    //                        thisDatTable.fnDeleteRow($('#tr-timeline-date-' + id)[0]);

    //                        viewTimeline();
    //                    }
    //                }
    //            });

    //            dialogRef.close();
    //        }
    //    },
    //    {
    //        label: 'Cancel',
    //        action: function (dialogRef) {
    //            dialogRef.close();
    //        }
    //    }]
    //});

    var popDiv = $("#div-pop-up").dialog({
        title: "Deleting " + headline + " Event",
        minHeight: 100,
        minWidth: 480,
        autoOpen: false,
        show: 'fade',
        hide: 'fade',
        buttons: [
            {
                text: "Ok", click: function () {
                    $.ajax({
                        url: baseUrl + 'InformationDesk/DeleteTimelineDate/' + id,
                        success: function(data) {
                            console.log("Delete Status: " + data);

                            if (data) {
                                popDiv.dialog("close");

                                //removing contact...
                                tr_id = "tr#tr-timeline-date-" + id;
                                tb_id = "table#table-timeline-dates";

                                var thisDatTable = $("#table-timeline-dates").dataTable();
                                thisDatTable.fnDeleteRow($('#tr-timeline-date-' + id)[0]);

                                viewTimeline();
                            }
                        }
                    });
                }
            }
        ],
        closeOnEscape: true
    });

    //adding the content to the div...
    popDiv.html();

    //opening the dialog...
    popDiv.dialog('open');
}

function _removeTableRow(tr_id, tb_id) {
    var combined = tb_id + " " + tr_id;
    console.log("tr " + combined);
    $(combined).css("background-color", "#FF3700");
    $(combined).fadeOut(400, function () {
        $(combined).remove();
    });
}

function RemoveFile(ele,originalName, name, id, isImage, attachmentType) {
    BootstrapDialog.show({
        title: 'Remove ' + originalName,
        type: BootstrapDialog.TYPE_DANGER,
        message: 'Are you sure you want to remove this file?',
        buttons: [
            {
                icon: 'glyphicon glyphicon-trash',
                label: 'Ok',
                cssClass: 'btn-danger',
                action: function(dialog) {
                    
                    var deleteObj = {
                        Name: name,
                        Id: id,
                        OriginalName: originalName,
                        FileType: $("#UploadedFileType").val(),
                        IsImage: isImage,
                        AttachmentType: attachmentType,
                        FolderPath: null,
                        Size: 0,
                        Url: null,
                        ThumbnailUrl: null,
                        DeleteUrl: "",
                        DeleteType: null,

                    };

                    $.ajax({
                        type: 'POST',
                        url: baseUrl + 'Api/UtilsApi/DeleteInformationDeskFile',

                        dataType: 'json',
                        contentType: "application/json",

                        data: JSON.stringify(deleteObj),
                        success: function(data) {
                            //remove the div...
                            if (data) {
                                
                                $(ele).closest('.upload-div-temp').css("background-color", "#FF3700")
                                    .fadeOut(400, function() {
                                        $(this).remove();
                                    });
                            } else {
                                //lets tell them somethiing went wrong while saving the items...
                                BootstrapDialog.alert('Could Not Delete Image, something went wrong.');

                            }
                            dialog.close();
                        }
                    });

                }
            }, {
                label: 'Cancel',
                action: function(dialog) {
                    dialog.close();
                }
            }
        ]
    });
}

function SaveFileInformation (){
    //first we show the loader...
    $("#sp-load-spin").show();

    var files = [];
    var filesWithoutNames = [];

    $(".upload-div-temp").map(function (i, e) {
        //uploading Details...

        //Emphasis on uploaded files need to have a friendly name:
        if ($(this).find(".upload-friendly-name").val() == '') {
            filesWithoutNames.push($(this).find(".upload-original-name").val());
//            WarningBootstrapAlert('Please Specify a Friendly name for File ' + $(this).find(".upload-original-name").val());
//            $("#sp-load-spin").hide();
//            return;
        }

        files.push({
            FriendlyName: $(this).find(".upload-friendly-name").val(),
            AcademicYear: $(this).find(".upload-academic-year :selected").val(),
            Id: $(this).find(".upload-id").val(),
            Name: $(this).find(".upload-Name").val(),
            IsImage: $(this).find(".upload-is-image").val(),
            AttachmentType: $(this).find(".upload-attachment-type").val(),
            OriginalFileName: $(this).find(".upload-original-name").val()
        });
    });

    //incase the files are empty, then we warn that not files have been added!!
    console.log("No. of Files: " + files.length);
    console.log("Files: " + JSON.stringify(files));

    if (files.length <= 0) {

        WarningBootstrapAlert('No Files have been added!');

        //hiding the spinner again...
        $("#sp-load-spin").hide();

        return;
    }

    
    //validate the friendly name..
    if (filesWithoutNames.length > 0) {
        var message = "Please Specify a Friendly name for the following File(s): <br/><ul>";

        for (var i = 0; i < filesWithoutNames.length; i++) {
            message += "<li>" +filesWithoutNames[i] + "</li>";
            //if (i != (filesWithoutNames.length - 1))
            //    message += ", ";
        }

        message += "</ul>";

        WarningBootstrapAlert(message, "Friendly Name Required");

        //hiding the spinner again...
        $("#sp-load-spin").hide();
        return;
    }

    //now lets save the items...
    var saveFileUploadDetails = {
        AttachmentType: $("#UploadedFileType").val(),
        Id: $("#UploadFileId").val(),
        Files: files
    };

    //now doing the upload...
    $.ajax({
        type: 'POST',
        dataType: 'JSON',
        contentType: 'application/json',

        url: baseUrl + 'api/UtilsApi/SaveFileUploadDetails',
        data: JSON.stringify(saveFileUploadDetails),
        success: function(data) {
            //hiding the spinner again...
            $("#sp-load-spin").hide();

            if (data) {
                var returnUrl = baseUrl + "InformationDesk/GeneralInformation/" + saveFileUploadDetails.Id;

                BootstrapDialog.alert({
                    title: 'Success',
                    message: 'File(s) have been added Successfully.',
                    type: BootstrapDialog.TYPE_SUCCESS,
                    buttonLabel: "Ok",
                    callback: function(result) {
                        window.location.replace(returnUrl);
                    }
                });

                //returning to the page withouth the need for the redirect...
                setTimeout(function() {
                    window.location.replace(returnUrl);
                }, 2000);

            } else {
                BootstrapDialog.alert({
                    title: 'Error',
                    message: 'File(s) have been not been added Successfully. Something went wrong.',
                    type: BootstrapDialog.TYPE_WARNING,
                    buttonLabel: "Ok",
                    callback: function (result) {
                        result.close();
                    }
                });
            }
        }
    });
}

function WarningBootstrapAlert(message, title) {
    BootstrapDialog.alert({
        title: title == undefined ? 'WARNING' : title,
        message: message,
        type: BootstrapDialog.TYPE_WARNING, // <-- Default value is BootstrapDialog.TYPE_PRIMARY
        closable: true, // <-- Default value is true
        buttonLabel: 'Ok', // <-- Default value is 'OK',
        callback: function (result) {

        }
    });
}

function AllAttachedFiles(id, type)
{
    var loadUrl = baseUrl + "InformationDesk/AllAttachedFiles/" + type + "/" + id;
    
    BootstrapDialog.show({
        title: "All Files",
        message: $('<div></div>').load(loadUrl)
    });
}