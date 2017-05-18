//Reminders code
//
var mainHolder = "#fb_holder";
var inputBox = "#fb_inputbox";
//var ajaxFilePath = baseUrl + "api/UtilsApi/Loadstudents/";
var flag1 = "0";
var flag2 = "0";
var flag3 = "0";

var errorMsge = "";
var errorTitle = "";

var toRecipient = "";
var ccRecipient = "";
var bccRecipient = "";
var fileAttachment = "";

function getInputBox(sel) {
    //specifiying the input box...
    var inVal = "";
    switch (sel) {
    case "0":
        inVal = "#fb_inputbox";
        break;
    case "1":
        inVal = "#fb_inputboxCC";
        break;
    case "1":
        inVal = "#fb_inputboxBCC";
        break;
    }
    console.log("InputBox:" + inVal);
    return inVal;
}

$().ready(function() {

    jQuery.fn.addFriend = function(text, currentData, type, holderClass, inputField) {

        var selectedReceipients = [];
        var receipientOptions = [];

        var selectedReceipientsTo = [];
        $('.js-example-tokenizer :selected').each(function(i, selected) {
            selectedReceipientsTo[i] = $(selected).val();
        });

        var selectedReceipientsCC = [];
        $('.js-example-tokenizerCC :selected').each(function(i, selected) {
            selectedReceipientsCC[i] = $(selected).val();
        });

        var selectedReceipientsBCC = [];
        $('.js-example-tokenizerBCC :selected').each(function(i, selected) {
            selectedReceipientsBCC[i] = $(selected).val();
        });

        selectedReceipients = selectedReceipientsTo.concat(selectedReceipientsCC, selectedReceipientsBCC);

        if ($.inArray(text, selectedReceipients) > -1) {
            var message = "Already added";
            var title = "Recipient already added";
            AlertMessageDialog(message, title);

        } else {
            //Getting options that have already been added but not selected
            $(holderClass + ' option').not(':selected').each(function(i, selected) {
                receipientOptions[i] = $(selected).text();
            });

            if ($.inArray(text, receipientOptions) > -1) {
                selectedReceipients.push(text);
                $(holderClass).val(selectedReceipients).trigger("change");
            } else {
                selectedReceipients.push(text);
                $(holderClass).append($('<option>', { value: text, text: text, id: currentData }));
                $(holderClass).val(selectedReceipients).trigger("change");
            }


        }

    };

    jQuery.fn.addOptionsForUsers = function(currentData, type, holderClass) {

        getReceipietsOptions(currentData, holderClass);

    };
});

$(document).ready(function() {
    //Scrolling to particular point
    $.fn.scrollView = function() {
        return this.each(function() {
            $('html, body').animate({
                scrollTop: $(this).offset().top
            }, 250);
        });
    }; //Pre filling Foward Message and Content
    var forwardSubject = $('#forwardSubject').val();
    var forwardContent = $('#forwardContent').val();
    var responseMessageId = $('#forwardMessageId').val();
    var responseMessageType = $('#MessageTypeId').val();

    if (forwardSubject != null && forwardSubject != "") {
        var newForwardSubject;
        if (responseMessageType == 1) {
            newForwardSubject = forwardSubject;
        } else {
            newForwardSubject = "FW: " + forwardSubject;
        }

        $('#msgSubject').val(newForwardSubject);
        $('#msgSubject').addClass("font-bold");
    }

    if (forwardContent != null && forwardContent != "") {
        $('#msgeBody').val(forwardContent);
    }
    
    //Remove User
    $(".removeUser").click(function () {
        
        var id = this.id;
        
        var url = $(location).attr('href');
        console.log("Url: " + url);

        //check if url exisits...
        var urlExisits = UrlExists(url);

        BootstrapDialog.confirm('You are about to Remove User, Are you sure?', function (result) {
            if (result) {
                if (urlExisits) {
                    removeUserFromList(id);
                } else
                    window.history.go(-1);

            } else {

            }
        });
    });

    //Deleting Messages
    var selecteditems = [];

    $("#DeleteReadMeassage").click(function() {
        var messageId = $("#DeleteReadMeassage").attr("messageId");
        selecteditems.push(messageId);

        var url = $(location).attr('href');
        console.log("Url: " + url);

        //check if url exisits...
        var urlExisits = UrlExists(url);

        BootstrapDialog.confirm('You are about to Trash Message(s), Are you sure?', function(result) {
            if (result) {
                if (urlExisits) {
                    moveItemsToTrash(selecteditems);
                } else
                    window.history.go(-1);

            } else {

            }
        });
    });

    //Trash Messages in User Sent Messages
    $("#trashSentItems").click(function() {

        var oTable = $('#inboxTable').dataTable()._('tr', { "filter": "applied" });
        var rowcollection = oTable.$(".selectByRow:checked", { "page": "all" });
        rowcollection.each(function(index, elem) {
            var checkbox_value = $(elem).val();
            selecteditems.push(checkbox_value);
        });

        if (selecteditems.length == 0) {
            errorMsge = "Please select message(s) to move to trash";
            errorTitle = "No Message Selected";
            AlertMessageDialog(errorMsge, errorTitle);
        } else {
            var url = $(location).attr('href');
            console.log("Url: " + url);

            //check if url exisits...
            var urlExisits = UrlExists(url);
            BootstrapDialog.confirm('You are about to Trash Message(s), Are you sure?', function(result) {
                if (result) {
                    if (urlExisits) {
                        //Trash
                        moveSentItemsToTrash(selecteditems);

                    } else
                        window.history.go(-1);

                } else {

                }
            });

        }

    });

    //Deleted Viewed Sent/Draft Message
    $("#DeleteSaveOrDraftMeassage").click(function() {
        var messageId = $("#DeleteSaveOrDraftMeassage").attr("messageId");
        selecteditems.push(messageId);

        var url = $(location).attr('href');
        console.log("Url: " + url);

        //check if url exisits...
        var urlExisits = UrlExists(url);
        BootstrapDialog.confirm('You are about to Trash Message(s), Are you sure?', function(result) {
            if (result) {
                if (urlExisits) {
                    //Trash
                    moveDraftItemsToTrash(selecteditems);

                } else
                    window.history.go(-1);

            }

        });
    });

    $("#trashDraftItems").click(function() {

        var oTable = $('#inboxTable').dataTable()._('tr', { "filter": "applied" });
        var rowcollection = oTable.$(".selectByRow:checked", { "page": "all" });
        rowcollection.each(function(index, elem) {
            var checkbox_value = $(elem).val();
            selecteditems.push(checkbox_value);
        });

        if (selecteditems.length == 0) {
            errorMsge = "Please select message(s) to move to trash";
            errorTitle = "No Message Selected";
            AlertMessageDialog(errorMsge, errorTitle);
        } else {
            var url = $(location).attr('href');
            console.log("Url: " + url);

            //check if url exisits...
            var urlExisits = UrlExists(url);
            BootstrapDialog.confirm('You are about to Trash Message(s), Are you sure?', function(result) {
                if (result) {
                    if (urlExisits) {
                        //Trash
                        moveDraftItemsToTrash(selecteditems);

                    } else
                        window.history.go(-1);

                } else {

                }
            });

        }

    });

    //Trash Messages in User Inbox
    $("#trashItems").click(function() {

        var oTable = $('#inboxTable').dataTable()._('tr', { "filter": "applied" });
        var rowcollection = oTable.$(".selectByRow:checked", { "page": "all" });
        rowcollection.each(function(index, elem) {
            var checkbox_value = $(elem).val();

            selecteditems.push(checkbox_value);
        });

        if (selecteditems.length == 0) {
            errorMsge = "Please select message(s) to move to trash";
            errorTitle = "No Message Selected";
            AlertMessageDialog(errorMsge, errorTitle);
        } else {
            var url = $(location).attr('href');
            console.log("Url: " + url);

            //check if url exisits...
            var urlExisits = UrlExists(url);

            BootstrapDialog.confirm('You are about to Trash Message(s), Are you sure?', function(result) {
                if (result) {
                    if (urlExisits) {
                        moveItemsToTrash(selecteditems);
                    } else
                        window.history.go(-1);

                } else {

                }
            });


        }

    });

    //Deals with Attaching Files
    if ($('#Ms-file-input').length) {
        $("#Ms-file-input").fileupload({
            url: baseUrl + "Api/UtilsApi/UploadMessengerFiles",
            dataType: 'json',
            type: 'post',
            formData: {
                type: $('#UploadedFileType2').val()
            },
            maxFileSize: 10000000, //limiting to 10MB upload..
            //acceptFileTypes: /(\.|\/)(gif|jpe?g|png)$/i,
            //            paramName:'Files',
            add: function(e, data) {
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
            done: function(e, data) {
                //retriving elements...
                var uploadFile2 = data.files[0];
                $("#tmpl-uploaded-file2").tmpl(data.result.files).appendTo("#files2");
                $('.attachment-Name').val(uploadFile2.name);
            },
            progressall: function(e, data) {
                var progress = parseInt(data.loaded / data.total * 100, 10);
                $('#progress2 .progress-bar').css(
                    'width',
                    progress + '%'
                );
            }
        }).prop('disabled', !$.support.fileInput)
            .parent().addClass($.support.fileInput ? undefined : 'disabled');
        
    }


    //To-Receipients
    $(".js-example-tokenizer").select2({
        tags: true,
        tokenSeparators: [',', ' ']
    });

    //CC-Receipients
    $(".js-example-tokenizerCC").select2({
        tags: true,
        tokenSeparators: [',', ' ']
    });

    //BCC-Receipients
    $(".js-example-tokenizerBCC").select2({
        tags: true,
        tokenSeparators: [',', ' ']
    });

    //by default, customlists are loaded..
    //if ($('#customList').length) {
    //    loadCustomLists();
    //}
    
    //Custom-Receipients
    $(".js-users-tokenizerBCC").select2({
        tags: true,
        tokenSeparators: [',', ' ']
    });
    
    $("#filter").show();
    $("#Ms_compose").hide();
    $("#Ms_StaffRoles").hide();
    $("#sendtoType").hide();
    $("#Ms_campus").hide();
    $("#drpfaculty").hide();
    $("#drpdepartment").hide();
    $("#drpprogramme").hide();
    $("#drpAcademicYear").hide();
    $("#drpCourse").hide();
    $("#drpStudyPeriod").hide();
    $("#drpSpecialisation").hide();
    $("#drpSemester").hide();
    $("#drpYearOfStudy").hide();
    $("#drpPosition").hide();

    $("#Ms_faculty").css("display", "none");
    $("#Ms_department").css("display", "none");
    $("#Ms_programme").css("display", "none");
    $("#Ms_specialisation").css("display", "none");
    $("#Ms_studyPeriod").css("display", "none");
    $("#Ms_academicYear").css("display", "none");
    $("#Ms_yearOfStudy").css("display", "none");
    $("#Ms_semester").css("display", "none");
    $("#Ms_course").css("display", "none");
    $("#Ms_position").css("display", "none");
    $("#customList").css("display", "none");

    //Hiding and Showing CC and BCC
    $("#fb_holderCC").css("display", "none");
    $("#fb_holderBCC").css("display", "none");

    $("#cc-show").click(function() {
        $("#fb_holderCC").removeAttr("style");
    });
    $("#bcc-show").click(function() {
        $("#fb_holderBCC").removeAttr("style");
    });

    //Btn to Add Users to List
    $("#btnAddToList").click(function () {
        var userIds = getToStatus();
        
        var customListId = $("#customListId").val();
        
        if (userIds.length == 0) {
            errorMsge = "No Users were Found";
            errorTitle = "Add User";
            AlertMessageDialog(errorMsge, errorTitle);

        } else {
            var url = $(location).attr('href');
                    console.log("Url: " + url);

                    //check if url exisits...
                    var urlExisits = UrlExists(url);

                    BootstrapDialog.confirm('You are about to Add Users to Custom List, Are you sure?', function (result) {
                        if (result) {
                            if (urlExisits) {
                                //Adding Users
                                AddingUser(userIds, customListId);
                            } else
                                window.history.go(-1);

                        } else {

                        }
                    });


              
        }

        return false;
    });

    $("#BtnSendMsg").click(function() {
        var toRecipients = getToStatus();
        var ccRecipients = getCCStatus();
        var bccRecipients = getBCCStatus();
        var fileAttachments = GetAttachmentInformation();
        var messageType = "1";

        var responseMessage = responseMessageId;
        if (responseMessage != null && responseMessage != typeof undefined) {
            messageType = "3";
        }


        var m_title = $("#msgSubject").val(); //title 

        var m_message = tinyMCE.get('msgeBody').getContent(); //message content


        if (toRecipients.length == 0) {
            errorMsge = "Please add a recipient for this email";
            errorTitle = "Add a Recipient";
            AlertMessageDialog(errorMsge, errorTitle);

        } else {

            if (m_title == "") {

                errorMsge = "The Subject for this message is required";
                errorTitle = "Subject Required";
                AlertMessageDialog(errorMsge, errorTitle);

            } else {

                if (m_message == "") {
                    errorMsge = "Please write a body for this email";
                    errorTitle = "Message Required";
                    AlertMessageDialog(errorMsge, errorTitle);

                } else {

                    var url = $(location).attr('href');
                    console.log("Url: " + url);

                    //check if url exisits...
                    var urlExisits = UrlExists(url);

                    BootstrapDialog.confirm('You are about to send the Message, Are you sure?', function(result) {
                        if (result) {
                            if (urlExisits) {
                                //Sending Message
                                sendingMessage(toRecipients, ccRecipients, bccRecipients, m_title, m_message, fileAttachments, responseMessage, messageType);
                            } else
                                window.history.go(-1);

                        } else {

                        }
                    });


                }
            }
        }

        return false;
    });

    $("#saveDraft").click(function() {

        var fileAttachments = GetAttachmentInformation();

        var m_title = $("#msgSubject").val(); //title 

        var m_message = tinyMCE.activeEditor.getContent(); //message content

        if (m_title == "") {

            errorMsge = "The Subject for this message is required";
            errorTitle = "Subject Required";
            AlertMessageDialog(errorMsge, errorTitle);

        } else {

            if (m_message == "") {
                errorMsge = "Please write a body for this email";
                errorTitle = "Message Required";
                AlertMessageDialog(errorMsge, errorTitle);

            } else {

                var url = $(location).attr('href');
                console.log("Url: " + url);

                //check if url exisits...
                var urlExisits = UrlExists(url);

                BootstrapDialog.confirm('You are about to save Message as Draft, Are you sure?', function(result) {
                    if (result) {
                        if (urlExisits) {
                            //Sending Message
                            savingMessage(m_title, m_message, fileAttachments);
                        } else
                            window.history.go(-1);

                    } else {

                    }
                });


            }
        }


        return false;

    });

    $("#showReplyDiv").click(function() {
        $('#replyDiv').removeClass("hidden"); //replyDiv
        $('#replyDiv').scrollView();
    }); 

    $("#BtnReplyMsg").click(function() {

        var recipientId = $('#recipientId').val();
        var replyMessageId = $('#replyMessageId').val();

        var merged = [];

        merged.push(recipientId);

        var toRecipients = merged;
        var ccRecipients = [];
        var bccRecipients = [];
        var fileAttachments = GetAttachmentInformation();

        var messageType = "1";

        var replyMessage = replyMessageId;

        if (replyMessage != null && replyMessage != typeof undefined) {
            messageType = "2";
        }

        var m_title = $("#replySubject").val(); //title 

        var m_message = tinyMCE.activeEditor.getContent(); //message content

        if (m_message == "") {
            errorMsge = "Please write a body for this email";
            errorTitle = "Message Required";
            AlertMessageDialog(errorMsge, errorTitle);

        } else {

            var url = $(location).attr('href');
            console.log("Url: " + url);

            //check if url exisits...
            var urlExisits = UrlExists(url);

            BootstrapDialog.confirm('You are about to send the Message, Are you sure?', function(result) {
                if (result) {
                    if (urlExisits) {
                        //Sending Message
                        sendingMessage(toRecipients, ccRecipients, bccRecipients, m_title, m_message, fileAttachments, replyMessage, messageType);
                    } else
                        window.history.go(-1);

                } else {

                }
            });


        }

        return false;
    });
    
    $("#BtnStudentSend").click(function () {
        
        var recipientId = $('#systemAdminUserId').val();
        
        var replyMessageId = $('#replyMessageId').val();
        
        var merged = [];

        merged.push(recipientId);
        
        var toRecipients = merged;
        var ccRecipients = [];
        var bccRecipients = [];
        var fileAttachments = GetAttachmentInformation();

        var messageType = "1";

        var replyMessage = replyMessageId;

        if (replyMessage != null && replyMessage != typeof undefined) {
            messageType = "2";
        }
        
        var m_title = $('#msgSubject').val(); //title 

        var m_message = tinyMCE.activeEditor.getContent(); //message content
        
        
        if (m_message == "") {
            errorMsge = "Please write a Body for this email";
            errorTitle = "Body Required";
            AlertMessageDialog(errorMsge, errorTitle);

        }
        else {
            
            var url = $(location).attr('href');
            console.log("Url: " + url);

            //check if url exisits...
            var urlExisits = UrlExists(url);

            BootstrapDialog.confirm('You are about to send the Message, Are you sure?', function (result) {
                if (result) {
                    if (urlExisits) {
                        //Sending Message
                        sendingMessage(toRecipients, ccRecipients, bccRecipients, m_title, m_message, fileAttachments, replyMessage, messageType);
                    } else
                        window.history.go(-1);

                } else {

                }
            });


        }

        return false;
    });

    $("#btnAdd").click(function() {
        var recType = $("#drpUserType").val();
        var postnId = $("#drpPosition").val();

        //specifiying the input box...
        var sel = $("select#drpFieldSpecifier").val();
        var holderClass = "";
        var inputBox = "";

        switch (sel) {
        case "0":
            inputBox = ".js-example-tokenizer";
            holderClass = ".js-example-tokenizer";
            break;
        case "1":
            inputBox = ".js-example-tokenizerCC";
            holderClass = ".js-example-tokenizerCC";
            $("#fb_holderCC").removeAttr("style");
            break;
        case "2":
            inputBox = ".js-example-tokenizerBCC";
            holderClass = ".js-example-tokenizerBCC";
            $("#fb_holderBCC").removeAttr("style");
            break;
        }

        var out = "";
        var cmpus = "";

        if ($("select#drpcampus option:selected").val() == 0) {
            cmpus = " ";
        } else {
            cmpus = ", " + $("select#drpcampus option:selected").text() + "  ";
        }

        switch (parseInt(recType)) {
        case 0:
            break;
        case 1:
            //Staff User Type
            if ($("select#sendtoType option:selected").val() == 0) {
                out += " " + "All Staff" + cmpus;

            } else {
                if ($("select#drpPosition option:selected").val() == 0) {

                    out += " " + $("select#sendtoType option:selected").text() + cmpus;

                } else {

                    out += loadPositionLevelId(postnId, cmpus);

                }

            }
            $().addFriend(out, ms_getcurrentdata(), $("select#sendtoType").val(), holderClass, inputBox);
            break;
        case 2:
            out += getStudentReceipientSummary();
            $().addFriend(out, ms_getcurrentdata(), $("select#sendtoType").val(), holderClass, inputBox);
            break;
        }

    }); 

    $("#btnFilter").click(function() {

        //specifiying the input box...
        var sel = $("select#drpFieldSpecifier").val();
        var holderClass = "";

        switch (sel) {
        case "0":
            holderClass = ".js-example-tokenizer";
            break;
        case "1":
            holderClass = ".js-example-tokenizerCC";
            $("#fb_holderCC").removeAttr("style");
            break;
        case "2":
            holderClass = ".js-example-tokenizerBCC";
            $("#fb_holderBCC").removeAttr("style");
            break;
        }

        $().addOptionsForUsers(ms_getcurrentdata(), $("select#sendtoType").val(), holderClass);


    });
    
    $("#btnLoadUsers").click(function () {

        var holderClass = ".js-example-tokenizer";

        $().addOptionsForUsers(ms_getcurrentdata(), $("select#sendtoType").val(), holderClass);


    });
    
    $("#btnFilterUsers").click(function () {
        var holderClass = ".js-users-tokenizer";
        $().addOptionsForUsers(ms_getUserdata(), $("select#sendtoType").val(), holderClass); 
    });

    //loadmsg();

    //User Type Select Options
    $("#drpUserType").change(function() {
        //$("#drpcampus").unbind();
        var val = $("select#drpUserType").val();

        resetDropdown($("#sendtoType"));
        resetDropdown($("#drpCourse"));
        resetDropdown($("#drpSpecialisation"));
        resetDropdown($("#drpprogramme"));
        resetDropdown($("#drpdepartment"));
        resetDropdown($("#drpfaculty"));
        resetDropdown($("#drpcampus"));

        $("#Ms_faculty").css("display", "none");
        $("#Ms_department").css("display", "none");
        $("#Ms_programme").css("display", "none");
        $("#Ms_specialisation").css("display", "none");
        $("#Ms_studyPeriod").css("display", "none");
        $("#Ms_academicYear").css("display", "none");
        $("#Ms_yearOfStudy").css("display", "none");
        $("#Ms_semester").css("display", "none");
        $("#Ms_course").css("display", "none");
        $("#Ms_position").css("display", "none");

        loadcampuss();

        switch (parseInt(val)) {
        case 0:
            $("#Ms_StaffRoles").css("display", "none");
            $("#Ms_campus").css("display", "none");
            break;
        case 1:
            //Staff
            loadroles();
            $("#Ms_campus").removeAttr("style");
            $("#Ms_StaffRoles").removeAttr("style");
            $(".studentOnly").css("display", "none");
            break;
        //Students
        case 2:
            resetDropdown($("#sendtoType"));
            resetDropdown($("#drpCourse"));
            resetDropdown($("#drpSpecialisation"));
            resetDropdown($("#drpprogramme"));
            resetDropdown($("#drpdepartment"));
            resetDropdown($("#drpfaculty"));
            resetDropdown($("#drpcampus"));

            $("#Ms_StaffRoles").css("display", "none");

            //loadcampuss();
            loadFacultys();
            loaddepartments();
            loadprogrammes();
            loadCourses();
            $("#Ms_campus").removeAttr("style");
            $("#Ms_faculty").removeAttr("style");
            $("#Ms_department").removeAttr("style");
            $("#Ms_programme").removeAttr("style");

            $("#Ms_course").removeAttr("style");

            break;
        default:
            $("#Ms_StaffRoles").css("display", "none");
            $("#Ms_campus").css("display", "none");
            break;
        }

    });

    if ($('#Ms_campus').length) {

        loadcampuss();
    }
    
    //loading academic years...
    ////$.ajax({
    ////    url: baseUrl + "api/UtilsApi/LoadAcademicYears/",
    ////    type: "POST",
    ////    contentType: "application/json",
    ////    dataType: "JSON",
    ////    timeout: 10000,
    ////    success: function(vals) {
    ////        result = jQuery.parseJSON(vals);
    ////        $('#drpAcademicYear').append($('<option>', {
    ////            value: 0,
    ////            text: "All"
    ////        }));
    ////        $.each(result, function(val, text) {
    ////            $('#drpAcademicYear').append(new Option(text.Text, text.Value));
    ////        });
    ////        $("#drpAcademicYear").trigger('change');
    ////    },
    ////    error: function(xhr, status) {
    ////    }
    ////});


    $("#sendtoType").change(function() {
        var pId = parseInt($("select#sendtoType").val());
        loadStaffPosition(pId);
        $("#Ms_position").removeAttr("style");
    });

    $("#drpPosition").change(function() {
        var val = parseInt($("select#drpPosition").val());
        loadPositionLevel(val);
    });

    //Handle Callbacks

    $("#Ms_campus").on('change', 'select', function() {
        resetDropdown($("#drpfaculty"));
        resetDropdown($("#drpdepartment"));
        loadFacultys();
    });

    $("#drpfaculty").change(function() {
        resetDropdown($("#drpCourse")); //clearing courses
        //alert($(this).val());
        loaddepartments();
        loadprogrammes();
        //loadCourses(); -> courses will be way to many and make the system slow..
    });
    $("#drpdepartment").change(function() {
        resetDropdown($("#drpCourse")); //clearing courses
        resetDropdown($("#drpSpecialisation"));
        //alert($(this).val());
        loadprogrammes();
    });
    $("#drpprogramme").change(function() {
        //resetDropdown($("#drpSpecialisation"));
        loadSpecialisations();
        loadStudyPeriod();
        loadCourses();

    });
    $("#drpSpecialisation").change(function() {
        loadCourses();
    });
    $("#drpAcademicYear").change(function() {
        //loadCourses();
    });
    $("#drpYearOfStudy").change(function() {
        loadCourses();
    });
    $("#drpSemester").change(function() {
        loadCourses();
    });

    //programme and year dropdown changes..
    $('#drpprogramme').change(function() {
        loadCourses();
    });

    $("#chkRoleCategory").change(function() {
        if ($('#chkRoleCategory').is(':checked') == true) {
            $("#sendtoType").show();
            $("#spnRoles").hide();
        } else {

            $("#sendtoType").hide();
            $("#spnRoles").show();
            $("#Ms_position").css("display", "none");

            //reset the dropdown on close...
            resetDropdown($("#sendtoType"));
        }
    });

    $("#chkPosition").change(function() {
        if ($('#chkPosition').is(':checked') == true) {
            $("#drpPosition").show();
            $("#spnPosition").hide();
        } else {
            $("#drpPosition").hide();
            $("#spnPosition").show();

            //reset the dropdown on close...
            resetDropdown($("#drpPosition"));
        }
    });
    $("#chkFaculty").change(function() {
        if ($('#chkFaculty').is(':checked') == true) {
            $("#drpfaculty").show();
            $("#spnFaculty").hide();
        } else {
            $("#drpfaculty").hide();
            $("#spnFaculty").show();

            //reset the dropdown on close...
            resetDropdown($("#drpfaculty"));
        }
    });
    $("#chkDepartment").change(function() {
        if ($('#chkDepartment').is(':checked') == true) {
            $("#drpdepartment").show();
            $("#spnDepartment").hide();
        } else {
            $("#drpdepartment").hide();
            $("#spnDepartment").show();

            //reset the dropdown on close...
            resetDropdown($("#drpdepartment"));
        }
    });
    $("#chkProgramme").change(function() {
        if ($('#chkProgramme').is(':checked') == true) {
            $("#drpprogramme").show();
            $("#spnProgramme").hide();

            loadSpecialisations();
            loadStudyPeriod();
            loadCourses();

            $("#Ms_specialisation").removeAttr("style");
            $("#Ms_studyPeriod").removeAttr("style");
            $("#Ms_academicYear").removeAttr("style");
            $("#Ms_yearOfStudy").removeAttr("style");
            $("#Ms_semester").removeAttr("style");

        } else {
            $("#drpprogramme").hide();
            $("#spnProgramme").show();

            $("#Ms_specialisation").css("display", "none");
            $("#Ms_studyPeriod").css("display", "none");
            $("#Ms_academicYear").css("display", "none");
            $("#Ms_yearOfStudy").css("display", "none");
            $("#Ms_semester").css("display", "none");

            //reset the dropdown on close...
            resetDropdown($("#drpprogramme"));
        }
    });
    $("#chkAcademicYear").change(function() {
        if ($("#chkAcademicYear").is(':checked') == true) {
            $("#drpAcademicYear").show();
            $("#spnAcademicYear").hide();


        } else {
            $("#drpAcademicYear").hide();
            $("#spnAcademicYear").show();

            //reset the dropdown on close...
            resetDropdown($("#drpAcademicYear"));
        }
    });
    $('#chkCourse').change(function() {
        if ($('#chkCourse').is(':checked') == true) {
            $("#drpCourse").show();
            $("#spnCourse").hide();
        } else {
            $("#drpCourse").hide();
            $("#spnCourse").show();

            //reset the dropdown on close...
            resetDropdown($("#drpCourse"));
        }
    });
    $('#chkStudyPeriod').change(function() {
        if ($('#chkStudyPeriod').is(':checked') == true) {
            $("#drpStudyPeriod").show();
            $("#spnStudyPeriod").hide();
        } else {
            $("#drpStudyPeriod").hide();
            $("#spnStudyPeriod").show();

            //reset the dropdown on close...
            resetDropdown($("#drpStudyPeriod"));
        }
    });
    $('#chkSpecialisation').change(function() {
        if ($('#chkSpecialisation').is(':checked') == true) {
            $("#drpSpecialisation").show();
            $("#spnSpecialisation").hide();
        } else {
            $("#drpSpecialisation").hide();
            $("#spnSpecialisation").show();

            //reset the dropdown on close...
            resetDropdown($("#drpSpecialisation"));
        }
    });
    $('#chkSemester').change(function() {
        if ($('#chkSemester').is(':checked') == true) {
            $("#drpSemester").show();
            $("#spnSemester").hide();
        } else {
            $("#drpSemester").hide();
            $("#spnSemester").show();

            //reset the dropdown on close...
            resetDropdown($("#drpSemester"));
        }
    });
    $('#chkYearOfStudy').change(function() {
        if ($('#chkYearOfStudy').is(':checked') == true) {
            $("#drpYearOfStudy").show();
            $("#spnYearOfStudy").hide();
        } else {
            $("#drpYearOfStudy").hide();
            $("#spnYearOfStudy").show();

            //reset the dropdown on close...
            resetDropdown($("#drpYearOfStudy"));
        }
    });

    $("#selectAllItems").change(selectDeselectAll);

});

function getRecipients(holderInputDivId) {
    var out = "";
    var list = new Array();
    var count = 0;

    $('div' + holderInputDivId + '> div.added').each(function(i, e) {
        out = out + $(e).attr("id") + ",";
        var item = new Object();
        item.Id = $(e).attr("id");
        item.Type = $(e).attr("type");
        list[count] = item;
        count += 1;
    });

    return list;
}

function resetDropdown(dropdown) {
    //clearing all filters...
    $(dropdown).empty();
}

function ms_getUserdata() {
    var out = "";
    var type = $("select#drpUserType").val();
    var drp1 = $("select#drpcampus").val();
    var drp2 = $("select#sendtoType").val();
    out = out + type + "," + drp1 + "," + drp2;
    
    return out;
}

function ms_getcurrentdata() {
    var out = "";
    var type = $("select#drpUserType").val();
    var drp1 = $("select#drpcampus").val();
    var drp2 = $("select#drpfaculty").val();
    var drp3 = $("select#drpdepartment").val();
    var drp4 = $("select#drpprogramme").val();

    var drp5 = $("select#drpSpecialisation").val();
    var drp6 = $("select#drpAcademicYear").val();
    var drp7 = $("select#drpYearOfStudy").val();
    var drp8 = $("select#drpSemester").val();
    var drp9 = $("select#drpCourse").val();
    var drp10 = $("select#drpStudyPeriod").val();
    var drp11 = $("select#drpCustomLists").val();

    var drp12 = $("select#sendtoType").val();
    var drp13 = $("select#drpPosition").val();

    //var drp5 = $("select#drpyear").val();
    out = out + type + "," + drp1 + "," + drp2 + "," + drp3 + "," + drp4 + "," + drp5 + "," + drp6 + ","
        + drp7 + "," + drp8 + "," + drp9 + "," + drp10 + "," + drp11 + "," + drp12 + "," + drp13;
    console.log("Output: " + out);
    return out;
}

function loadroles() {
    //load user types...
    $.ajax({
        url: baseUrl + "api/UtilsApi/LoadStaffRoles/",
        type: "POST",
        contentType: "application/json",
        dataType: "JSON",
        timeout: 10000,
        success: function (vals) {
            $("#sendtoType").empty();
            result = jQuery.parseJSON(vals);
            //det = jQuery.parseJSON(result.d);
            //$('#drpcampus').html("<option value=\"0\">All</option>");
            $('#sendtoType').append($('<option>', {
                value: 0,
                text: "All"
            }));
            //$('#drpcampus').empty().append('<option value=0>All</option>').selectmenu('refresh');
            $.each(result, function (val, text) {
                $('#sendtoType').append(new Option(text.Text, text.Value));

            });
            //$("#drpcampus").trigger('change');
        },
        error: function (xhr, status) {
        }
    });


}
function loadStaffPosition(positionId) {
    $.ajax({
        url: baseUrl + "api/UtilsApi/LoadPositions/" + positionId,
        type: "POST",
        contentType: "application/json",
        dataType: "JSON",
        timeout: 10000,
        success: function (vals) {
            $("#drpPosition").empty();
            result = jQuery.parseJSON(vals);
            $("#drpPosition").append($('<option>', {
                value: 0,
                text: "-- Select Position --"
            }));
            $.each(result, function(val, text) {
                $("#drpPosition").append(new Option(text.Text, text.Value));

            });
        },
        error: function(xhr, status) {
        }
    });

}

function loadPositionLevel(positionId) {
    $.ajax({
        url: baseUrl + "api/UtilsApi/LoadLevel/" + positionId,
        type: "POST",
        contentType: "application/json",
        dataType: "JSON",
        timeout: 10000,
        success: function(vals) {
            result = jQuery.parseJSON(vals);

            loadMoreFilters(result);

        },
        error: function(xhr, status) {
        }
    });

}

function loadPositionLevelId(positionId, cmpus) {
    var resultText;
    $.ajax({
        url: baseUrl + "api/UtilsApi/LoadLevel/" + positionId,
        type: "POST",
        contentType: "application/json",
        dataType: "JSON",
        timeout: 10000,
        async: false,
        success: function(vals) {
            resul = jQuery.parseJSON(vals);
            resultText = loadCustomTexts(resul, cmpus);
            return true;
        },
        error: function(xhr, status) {
        }
    });
    return resultText;
}

function loadcampuss() {
    $.ajax({
        url: baseUrl + "api/UtilsApi/LoadCampus/",
        type: "POST",
        contentType: "application/json",
        dataType: "JSON",
        timeout: 10000,
        success: function (vals) {
            $("#drpcampus").empty();
            result = jQuery.parseJSON(vals);
            //det = jQuery.parseJSON(result.d);
            //$('#drpcampus').html("<option value=\"0\">All</option>");
            $('#drpcampus').append($('<option>', {
                value: 0,
                text: "All"
            }));
            //$('#drpcampus').empty().append('<option value=0>All</option>').selectmenu('refresh');
            $.each(result, function(val, text) {
                $('#drpcampus').append(new Option(text.Text, text.Value));

            });
            //$("#drpcampus").trigger('change');
        },
        error: function(xhr, status) {
        }
    });
}

function loadFacultys() {
    var campusId = parseInt($("select#drpcampus").val()) || 0;
    $.ajax({
        url: baseUrl + "api/UtilsApi/LoadFaculties/" + campusId,
        //data: JSON.stringify({ campus: campus }),
        type: "POST",
        contentType: "application/json",
        dataType: "JSON",
        timeout: 10000,
        success: function (vals) {
            $("#drpfaculty").empty();
            result = jQuery.parseJSON(vals);
            //det = jQuery.parseJSON(result.d);
            //$('#drpfaculty').html("<option value=\"0\">All</option>");
            $('#drpfaculty').append($('<option>', {
                value: 0,
                text: "All"
            }));
            $.each(result, function(val, text) {
                $('#drpfaculty').append(new Option(text.Text, text.Value));

            });
        },
        error: function(xhr, status) {
        }
    });

}

function loaddepartments() {
    var campusId = parseInt($("select#drpcampus").val()) || 0;
    var facultyId = parseInt($("select#drpfaculty").val()) || 0;
    $.ajax({
        url: baseUrl + "api/UtilsApi/LoadDepartments/" + campusId + "/" + facultyId,
        //data: JSON.stringify({ faculty: faculty }),
        type: "POST",
        contentType: "application/json",
        dataType: "JSON",
        timeout: 10000,
        success: function (vals) {
            $("#drpdepartment").empty();
            result = jQuery.parseJSON(vals);
            //det = jQuery.parseJSON(result.d);
            //$('#drpdepartment').html("<option value=\"0\">All</option>");
            $('#drpdepartment').append($('<option>', {
                value: 0,
                text: "All"
            }));
            $.each(result, function(val, text) {
                $('#drpdepartment').append(new Option(text.Text, text.Value));

            });
        },
        error: function(xhr, status) {
        }
    });
}

function loadprogrammes() {
    var campusId = parseInt($("select#drpcampus").val()) || 0;
    var facultyId = parseInt($("select#drpfaculty").val()) || 0;
    var departmentId = parseInt($("select#drpdepartment").val()) || 0;
    $.ajax({
        url: baseUrl + "api/UtilsApi/LoadProgrammes/" + campusId + "/" + facultyId + "/" + departmentId,
        //data: JSON.stringify({ department: out }),
        type: "POST",
        contentType: "application/json",
        dataType: "JSON",
        timeout: 10000,
        success: function (vals) {
            $("#drpprogramme").empty();
            result = jQuery.parseJSON(vals);
            //det = jQuery.parseJSON(result.d);
            //$('#drpprogramme').html("<option value=\"0\">All</option>");
            $('#drpprogramme').append($('<option>', {
                value: 0,
                text: "All"
            }));
            $.each(result, function(val, text) {
                $('#drpprogramme').append(new Option(text.Text, text.Value));
            });

        },
        error: function(xhr, status) {
        }
    });
}

function loadSpecialisations() {
    var programId = parseInt($("select#drpprogramme").val()) || 0;
    $.ajax({
        url: baseUrl + "api/UtilsApi/LoadSpecialisations/" + programId,
        //data: JSON.stringify({ filterResults: filter }),
        type: "POST",
        contentType: "application/json",
        dataType: "JSON",
        timeout: 10000,
        success: function (vals) {
            $("#drpSpecialisation").empty();
            resetDropdown($("#drpSpecialisation")); //clear current options..
            result = jQuery.parseJSON(vals);
            //det = jQuery.parseJSON(result.d);
            //console.log("Result Length: " + det.length);
            if (result.length > 0) {
                //$('#drpSpecialisation').html("<option value=\"0\">All</option>");
                $('#drpSpecialisation').append($('<option>', {
                    value: 0,
                    text: "All"
                }));
                $.each(result, function(val, text) {
                    $('#drpSpecialisation').append(new Option(text.Text, text.Value));
                });
            } else {

                $('#drpSpecialisation').append($('<option>', {
                    value: "",
                    text: "None Exist"
                }));
            }
        },
        error: function(xhr, status) {
        }
    });
}

function loadStudyPeriod() {
    $.ajax({
        url: baseUrl + "api/UtilsApi/LoadStudyPeriod/",
        type: "POST",
        contentType: "application/json",
        dataType: "JSON",
        timeout: 10000,
        success: function (vals) {
            $("#drpStudyPeriod").empty();
            result = jQuery.parseJSON(vals);
            $('#drpStudyPeriod').append($('<option>', {
                value: 0,
                text: "All"
            }));
            $.each(result, function(val, text) {
                $('#drpStudyPeriod').append(new Option(text.Text, text.Value));

            });
        },
        error: function(xhr, status) {
        }
    });
}

function loadCustomLists() {
    
    $.ajax({
        url: baseUrl + "api/UtilsApi/LoadCustomLists/",
        type: "POST",
        dataType: "JSON",
        contentType: "application/json",
        timeout: 10000,
        success: function (vals) {
            $("#drpCustomLists").empty();
            //resetDropdown($("#drpCustomLists")); //clear current options..
            result = jQuery.parseJSON(vals);
            det = jQuery.parseJSON(result.d);
            if (det != null) {
                //$('#drpCustomLists').html("<option value=\"0\">All</option>");
                $.each(det, function(val, text) {
                    $('#drpCustomLists').append(new Option(text.var2, text.var1));
                });
            }
        },
        error: function(xhr, status) {
        }
    });
}

function loadCourses() {
    var programId = parseInt($("select#drpprogramme").val()) || 0;
    var specializationId = parseInt($("select#drpSpecialisation").val()) || 0;
    var yearOfStudyId = parseInt($("select#drpYearOfStudy").val()) || 0;
    var semesterSession = parseInt($("select#drpSemester").val()) || 0;
    //alert(courseFilter);
    $.ajax({
        url: baseUrl + "api/UtilsApi/LoadCourses/" + programId + "/" + specializationId + "/" + yearOfStudyId + "/" + semesterSession,
        //data: JSON.stringify({ filterResults: filterResults }),'
        type: "POST",
        contentType: "application/json",
        dataType: "JSON",
        timeout: 10000,
        success: function (vals) {
            
            $("#drpCourse").empty();
            //resetDropdown($("#drpCourse")); //clear current options..
            result = jQuery.parseJSON(vals);
            //det = jQuery.parseJSON(result.d);
            if (result != null) {

                if (result.length > 0) {
                    //$('#drpCourse').html("<option value=\"0\">All</option>");
                    $('#drpCourse').append($('<option>', {
                        value: 0,
                        text: "All"
                    }));
                    $.each(result, function(val, text) {
                        $('#drpCourse').append(new Option(text.Text, text.Value));
                    });
                } else {
                    $('#drpCourse').append($('<option>', {
                        value: "",
                        text: "No Courses Found"
                    }));
                }
            } else {
                $('#drpCourse').append($('<option>', {
                    value: "",
                    text: "No Courses Found"
                }));
            }
        },
        error: function(xhr, status) {
        }
    });
}

function loadStaffCourses() {
    $("#drpCourse").empty();
    var programId = parseInt($("select#drpprogramme").val()) || 0;
    //var courseFilter = ms_getcurrentdata();
    //var programId = courseFilter[9];
    $.ajax({
        url: baseUrl + "api/UtilsApi/LoadStaffCourses/" + programId,
        //data: JSON.stringify({ filterResults: filterResults }),'
        type: "POST",
        contentType: "application/json",
        dataType: "JSON",
        timeout: 10000,
        success: function(vals) {
            resetDropdown($("#drpCourse")); //clear current options..
            result = jQuery.parseJSON(vals);
            //det = jQuery.parseJSON(result.d);
            if (result != null) {
                //$('#drpCourse').html("<option value=\"0\">All</option>");
                $('#drpCourse').append($('<option>', {
                    value: 0,
                    text: "All"
                }));
                $.each(result, function(val, text) {
                    $('#drpCourse').append(new Option(text.Text, text.Value));
                });
            }
        },
        error: function(xhr, status) {
        }
    });
}

//function loadmsg() {
//    $.ajax({
//        url: "../Messenger/MessengerWebService.asmx/LoadMessage",
//        type: "POST",
//        dataType: "json",
//        contentType: "application/json",
//        timeout: 10000,
//        success: function(vals) {
//            result = jQuery.parseJSON(vals);
//            det = jQuery.parseJSON(vals["d"]);
//            if (det != null) {
//                $("#ctl00_ContentPlaceHolder1_txtTitle").val(det["var2"]);
//                tinyMCE.activeEditor.setContent(det["var1"]);
//            }
//        },
//        error: function(xhr, status) {
//        }
//    });
//}

function loadMoreFilters(result) {
    switch (result) {
    case "University":
        $("#Ms_campus").css("display", "none");
        break;
    case "Campus":
        $("#Ms_campus").removeAttr("style");
        break;
    case "Faculty":
        loadFacultys();
        $("#Ms_campus").removeAttr("style");
        $("#Ms_faculty").removeAttr("style");
        break;
    case "Department":
        loadFacultys();
        loaddepartments();
        $("#Ms_campus").removeAttr("style");
        $("#Ms_faculty").removeAttr("style");
        $("#Ms_department").removeAttr("style");
        break;
    case "Program":
        loadFacultys();
        loaddepartments();
        loadprogrammes();
        $("#Ms_campus").removeAttr("style");
        $("#Ms_faculty").removeAttr("style");
        $("#Ms_department").removeAttr("style");
        $("#Ms_programme").removeAttr("style");
        break;
    case "Course":
        loadFacultys();
        loaddepartments();
        loadprogrammes();
        loadCourses();
        $("#Ms_campus").removeAttr("style");
        $("#Ms_faculty").removeAttr("style");
        $("#Ms_department").removeAttr("style");
        $("#Ms_programme").removeAttr("style");
        $("#Ms_course").removeAttr("style");
        break;
    }
}

function loadCustomTexts(result, cmpus) {
    var customText = "";
    switch (result) {
    case "University":
        customText += " " + $("select#drpPosition option:selected").text() + cmpus;
        break;
    case "Campus":
        customText += " " + $("select#drpPosition option:selected").text() + cmpus;
        break;
    case "Faculty":
        customText += " " + $("select#drpPosition option:selected").text() + ", " + $("select#drpfaculty option:selected").text() + cmpus;
        break;
    case "Department":
        customText += " " + $("select#drpPosition option:selected").text() + ", " + $("select#drpfaculty option:selected").text() + ", "
            + $("select#drpdepartment option:selected").text() + cmpus;
        break;
    case "Program":
        customText += " " + $("select#drpPosition option:selected").text() + ", " + $("select#drpfaculty option:selected").text() + ", "
            + $("select#drpdepartment option:selected").text() + ", " + $("select#drpprogramme option:selected").text() + cmpus;
        break;
    case "Course":
        customText += " " + $("select#drpPosition option:selected").text() + ", " + $("select#drpfaculty option:selected").text() + ", "
            + $("select#drpdepartment option:selected").text() + ", " + $("select#drpprogramme option:selected").text() + cmpus;
        break;
    default:
    }
    console.log("Output: " + customText);
    return customText;
}

function getStudentReceipientSummary() {
    var customText = "";

    customText += "All Students: ";

    if ($("select#drpcampus option:selected").val()) {
        if ($("select#drpcampus option:selected").val() != 0) {
            customText += " " + $("select#drpcampus option:selected").text() + ",";
        }
    }

    if ($("select#drpfaculty option:selected").val()) {
        if ($("select#drpfaculty option:selected").val() != 0) {
            customText += " " + $("select#drpfaculty option:selected").text() + ",";
        }
    }

    if ($("select#drpdepartment option:selected").val()) {
        if ($("select#drpdepartment option:selected").val() != 0) {
            customText += " " + $("select#drpdepartment option:selected").text() + ",";
        }
    }

    if ($("select#drpprogramme option:selected").val()) {
        if ($("select#drpprogramme option:selected").val() != 0) {
            customText += " " + $("select#drpprogramme option:selected").text() + ",";
        }
    }

    if ($("select#drpprogramme option:selected").val() != 0 && $("select#drpprogramme option:selected").val() != null) {

        if ($("select#drpSpecialisation option:selected").val()) {
            if ($("select#drpSpecialisation option:selected").val() != 0) {
                customText += " " + $("select#drpSpecialisation option:selected").text() + ",";
            }
        }

        if ($("select#drpStudyPeriod option:selected").val()) {
            if ($("select#drpStudyPeriod option:selected").val() != 0) {
                customText += " " + $("select#drpStudyPeriod option:selected").text() + ",";
            }
        }

        if ($("select#drpAcademicYear option:selected").val()) {
            if ($("select#drpAcademicYear option:selected").val() != 0) {
                customText += " " + $("select#drpAcademicYear option:selected").text() + ",";
            }
        }

        if ($("select#drpYearOfStudy option:selected").val()) {
            if ($("select#drpYearOfStudy option:selected").val() != 0) {
                customText += " " + $("select#drpYearOfStudy option:selected").text() + ",";
            }
        }

        if ($("select#drpSemester option:selected").val()) {
            if ($("select#drpSemester option:selected").val() != 0) {
                customText += " " + $("select#drpSemester option:selected").text() + ",";
            }
        }

        if ($("select#drpCourse option:selected").val()) {
            if ($("select#drpCourse option:selected").val() != 0) {
                customText += " " + $("select#drpCourse option:selected").text() + ",";
            }
        }
    }

    if (customText == "All Students: ") {
        customText = "All Students";
    }

    return customText;
}

function loadReceipients() {
    $.ajax({
        url: "../Messenger/MessengerWebService.asmx/LoadMessage",
        type: "POST",
        dataType: "json",
        contentType: "application/json",
        timeout: 10000,
        success: function(vals) {
            result = jQuery.parseJSON(vals);
            det = jQuery.parseJSON(vals["d"]);
            if (det != null) {
                $("#ctl00_ContentPlaceHolder1_txtTitle").val(det["var2"]);
                tinyMCE.activeEditor.setContent(det["var1"]);
            }
        },
        error: function(xhr, status) {
        }
    });
}

function getToStatus() {
    var out = "";
    var list = new Array();
    var ids = new Array();

    var idsArray = [];

    var count = 0;

    $('.js-example-tokenizer :selected').each(function(i, e) {
        if ($(e).attr("id") != null) {
            list[count] = $(e).attr("id");
            count += 1;
        }


    });

    var k;
    for (k = 0; k < list.length; ++k) {
        if (list[k] != typeof undefined) {
            var idss = getGroupReceipiets(list[k]);
            idsArray.push(idss);
        }


    }


    $('.js-example-tokenizer :selected').each(function(i, e) {
        if ($(e).attr("idValue") != null && $(e).attr("idValue") != typeof undefined && $(e).attr("idValue") != false) {
            ids[count] = JSON.parse($(e).attr("idValue"));
            idsArray.push(ids[count]);
            count += 1;
        }

    });

    var merged = [];
    merged = merged.concat.apply(merged, idsArray);

    return merged;
}

function getCCStatus() {
    var out = "";
    var list = new Array();
    var ids = new Array();

    var idsArray = [];

    var count = 0;

    $('.js-example-tokenizerCC :selected').each(function(i, e) {
        if ($(e).attr("id") != null) {
            list[count] = $(e).attr("id");
        }
        count += 1;
    });

    var k;
    for (k = 0; k < list.length; ++k) {
        var idss = getGroupReceipiets(list[k]);

        idsArray.push(idss);

    }

    $('.js-example-tokenizerCC :selected').each(function(i, e) {
        if ($(e).attr("idValue") != null && $(e).attr("idValue") != typeof undefined && $(e).attr("idValue") != false) {
            ids[count] = JSON.parse($(e).attr("idValue"));
            idsArray.push(ids[count]);

        }
        count += 1;
    });

    var merged = [];
    merged = merged.concat.apply(merged, idsArray);

    return merged;
}

function getBCCStatus() {
    var out = "";
    var list = new Array();
    var ids = new Array();

    var idsArray = [];

    var count = 0;

    $('.js-example-tokenizerBCC :selected').each(function(i, e) {
        if ($(e).attr("id") != null) {
            list[count] = $(e).attr("id");
        }
        count += 1;
    });

    var k;
    for (k = 0; k < list.length; ++k) {
        var idss = getGroupReceipiets(list[k]);

        idsArray.push(idss);

    }

    $('.js-example-tokenizerBCC :selected').each(function(i, e) {
        if ($(e).attr("idValue") != null && $(e).attr("idValue") != typeof undefined && $(e).attr("idValue") != false) {
            ids[count] = JSON.parse($(e).attr("idValue"));
            idsArray.push(ids[count]);

        }


        count += 1;
    });

    var merged = [];
    merged = merged.concat.apply(merged, idsArray);

    return merged;
}

function getGroupReceipiets(currentData) {
    $.ajax({
        url: baseUrl + "api/UtilsApi/LoadGroupRecipients/" + currentData,
        type: "POST",
        contentType: "application/json",
        dataType: "JSON",
        timeout: 10000,
        async: false,
        success: function(vals) {
            resul = jQuery.parseJSON(vals);


        },
        error: function(xhr, status) {
        }
    });
    return resul;
}

function getReceipietsOptions(currentData, holderClass) {
    
    $.ajax({
        url: baseUrl + "api/UtilsApi/LoadRecipientsOptions/" + currentData,
        type: "POST",
        contentType: "application/json",
        dataType: "JSON",
        timeout: 10000,
        success: function(vals) {
            result = jQuery.parseJSON(vals);
            if (result != null) {
                $.each(result, function(val, text) {
                    $(holderClass).append($('<option>', { value: text.Value, text: text.Text, idValue: text.Value }));
                    //$(holderClass).append(new Option(text.Text, text.Value));
                });
            }
            $(holderClass).select2('open');
        },
        error: function(xhr, status) {
        }
    });
}

function confirmSending() {
    var url = $(location).attr('href');
    console.log("Url: " + url);

    //check if url exisits...
    var urlExisits = UrlExists(url);

    BootstrapDialog.confirm('You are about to send the Message, Are you sure?', function(result) {
        if (result) {
            if (urlExisits)
                window.location.href = url;
            else
                window.history.go(-1);

        } else {

        }
    });
}

function removeTags(txt) {

    var rex = /(<([^>]+)>)/ig;
    var res = txt.replace(rex, "");

    var result = res.toString();
    return result;
}

function sendingMessage(toRecipients, ccRecipients, bccRecipients, m_title, m_message, fileAttachments, responseMessage, messageType) {

    var sendMessageDetails = {
        Subject: m_title,
        Content: m_message,
        ToRecipientIds: toRecipients,
        CcRecipientIds: ccRecipients,
        BccRecipientIds: bccRecipients,
        FileAttachments: fileAttachments,
        MessageType: messageType,
        ResponseMessageId: responseMessage
    };

    $.ajax({
        type: 'POST',
        dataType: 'JSON',
        contentType: 'application/json',
        url: baseUrl + 'api/UtilsApi/SendMessage',
        data: JSON.stringify(sendMessageDetails),

        success: function (data) {
            
            window.location.href = baseUrl + 'Messenger/MessageSent/';
        },
        error: function(xhr, status, error) {
            alert("Error");
        }
    });


}

function AddingUser(userIds, customListId) {

    var customListComponent = {
        RecipientIds: userIds,
        CustomListId: customListId
      };

    $.ajax({
        type: 'POST',
        dataType: 'JSON',
        contentType: 'application/json',
        url: baseUrl + 'api/UtilsApi/AddUserToList',
        data: JSON.stringify(customListComponent),

        success: function (data) {

            window.location.href = baseUrl + 'Messenger/UserAdded/';
        },
        error: function (xhr, status, error) {
            alert("Error");
        }
    });


}


function savingMessage(m_title, m_message, fileAttachments) {

    var saveMessageDetails = {
        Subject: m_title,
        Content: m_message,
        FileAttachments: fileAttachments,
    };

    $.ajax({
        url: baseUrl + 'api/UtilsApi/SaveMessage',
        type: 'POST',
        dataType: 'JSON',
        contentType: 'application/json',
        data: JSON.stringify(saveMessageDetails),
        success: function(data) {
            /* Message Saved */

            var message = "Message sent";
            var title = "Message has been successfully Saved";
            AlertMessageDialog(message, title);

            window.location.href = baseUrl + '/Messenger/MessageSaved/';
        }
    });
}

function RemoveAttachedFile(ele, originalName, Name, IsImage) {

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
                        Name: Name,
                        Id: null,
                        OriginalName: originalName,
                        FileType: null,
                        IsImage: IsImage,
                        AttachmentType: null,
                        FolderPath: null,
                        Size: 0,
                        Url: null,
                        ThumbnailUrl: null,
                        DeleteUrl: "",
                        DeleteType: null,
                    };

                    $.ajax({
                        type: 'POST',
                        url: baseUrl + 'Api/UtilsApi/DeleteMessengerAttachment',

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

function GetAttachmentInformation() {
    var files = new Array();
    var count = 0;

    $('.attachment-Name').each(function(i, e) {
        if ($(e).val() != null) {
            files[count] = $(e).val();
        }
        count += 1;
    });

    return files;
}

function selectDeselectAll() {
    var oTable = $('#inboxTable').dataTable()._('tr', { "filter": "applied" });
    var rowcollection = oTable.$(".selectByRow", { "page": "all" });

    if ($('#selectAllItems').is(':checked') == true) {
        rowcollection.each(function(index, elem) {
            $(elem).is(":checked") ? "" : $(elem).prop('checked', true);
        });
    } else {
        rowcollection.each(function(index, elem) {
            $(elem).is(":checked") ? $(elem).prop('checked', false) : "";
        });
    }
}

function moveItemsToTrash(messageIds) {

    var ids = JSON.stringify(messageIds);
    $.ajax({
        url: baseUrl + "api/UtilsApi/TrashMessage/" + ids,
        type: "POST",
        contentType: "application/json",
        dataType: "JSON",
        timeout: 10000,
        success: function (data) {

            window.location.href = baseUrl + 'Messenger/Messagetrashed/';
        }
    });

} 

function moveSentItemsToTrash(messageIds) {

    var ids = JSON.stringify(messageIds);
    $.ajax({
        url: baseUrl + "api/UtilsApi/TrashSentMessage/" + ids,
        type: "POST",
        contentType: "application/json",
        dataType: "JSON",
        timeout: 10000,
        success: function(data) {
            window.location.href = baseUrl + 'Messenger/SentMessagetrashed/';
        }
    });

}

function moveDraftItemsToTrash(messageIds) {

    var ids = JSON.stringify(messageIds);
    $.ajax({
        url: baseUrl + "api/UtilsApi/TrashDraftMessage/" + ids,
        type: "POST",
        contentType: "application/json",
        dataType: "JSON",
        timeout: 10000,
        success: function(data) {
            window.location.href = baseUrl + 'Messenger/DraftMessagetrashed/';
        }
    });

}

function removeUserFromList(id) {

    var id = JSON.stringify(id);
    
    $.ajax({
        url: baseUrl + "api/UtilsApi/RemoveUser/" + id,
        type: "POST",
        contentType: "application/json",
        dataType: "JSON",
        timeout: 10000,
        success: function (data) {
            window.history.go(0);
        }
    });

}