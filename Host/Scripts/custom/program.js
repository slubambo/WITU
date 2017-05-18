/// <reference path="~/Scripts/base/bootstrap.min.js" />
/// <reference path="../bootstrap-dialog/js/bootstrap-dialog.js" />

var nameOptions = new Array("Essential", "Relevant", "Desirable");

$(document).ready(function () {

    for (var i = 0; i < nameOptions.length; i++) {
        //on first load...
        var selectedItem = $("#drp-criteria-element-" + nameOptions[i] + " option:selected").val();
        changeSubjectKind(selectedItem, nameOptions[i]);
    }

    //disable sorting on program all table...
    $(".tbl-campus-programs").dataTable({
        "iDisplayLength": 25,
        "aoColumnDefs": [
            {
                "bSearchable": false, //Both Search and Sort
                "bSortable": false,
                "aTargets": [2]
            }
        ],
        "bDestroy": true
    });

    $(".tbl-program-courses").dataTable({
        "bFilter": false,
        "bInfo": false,
        "bDestroy": true
    });

    //tuition dropdown options...
    if ($('#drdTuitition').length) {
        var selecteDrpDwn = $('#drdTuitition option:selected').val();

        selectTuition(selecteDrpDwn);
        $('#drdTuitition').change(function () {
            var selected = $('#drdTuitition option:selected').val();
            selectTuition(selected);
        });
    }
});

function selectTuition(drpdwnSelected) {
    if (drpdwnSelected == "0") {
        $(".tuition-ugx").show();
        $(".tuition-dollar").hide();
    } else {
        $(".tuition-ugx").hide();
        $(".tuition-dollar").show();
    }
}

function onCriteriaDropDownChange(selectedDrpDwn, nameOfElement) {
    var valueSelected = selectedDrpDwn.options[selectedDrpDwn.selectedIndex].value;

    changeSubjectKind(valueSelected, nameOfElement);

    //unchecking all checkboxes in this context...
    $(".chk-subject-" + nameOfElement).removeAttr('checked');

    //clearing the text area..
    clearSummaryTextbox(nameOfElement);
}

function clearSummaryTextbox(nameOfElement) {
    $("#txtArea-Summary-" + nameOfElement).html("");
}

function changeSubjectKind(valueSelected, nameOfElement) {
    var idName = "#div-subject-options-" + nameOfElement + "-";
    var divSubjectNameAppended = "#div-subject-numbers-";

    switch (valueSelected) {
        case "0":
            $(idName + "1").hide();
            $(idName + "2").hide();
            $(idName + "3").hide();

            //hide specific subject...
            $(divSubjectNameAppended + nameOfElement).hide();
            break;
        case "1":  //when Specific Subjects is selected.
            $(idName + "1").show();
            $(idName + "2").hide();
            $(idName + "3").hide();

            //select subjects...
            $(".lbl-subject-options-title-1").html("Select Subjects:");

            //hide specific subject...
            $(divSubjectNameAppended + nameOfElement).hide();

            break;
        case "2": //combinational subjects
            $(idName + "1").show();
            $(idName + "2").hide();
            $(idName + "3").show();

            //otherwise show the subject number...
            $(divSubjectNameAppended + nameOfElement).show();
            break;
        case "3": //all subjects
            $(idName + "3").hide();
            $(idName + "2").show();
            $(idName + "1").show();

            //otherwise show the subject number...
            $(divSubjectNameAppended + nameOfElement).show();
            break;

        default:
            $(idName + "3").show();
            $(idName + "2").show();
            $(idName + "1").show();

            //otherwise show the subject number...
            $(divSubjectNameAppended + nameOfElement).show();
            break;
    }
}

function AddSpecialisationCore(programCoreId, programId) {
    var loadUrl = baseUrl + "Program/EditSpecialisationCore?programCoreId=" + programCoreId;

    if (programCoreId != undefined)
        loadUrl = loadUrl + "&id=" + programId;

    BootstrapDialog.show({
        title: "Add/Edit Specialisation Category",
        message: $('<div></div>').load(loadUrl),
        //onshown: function () {
        //    console.log("Dialog now showing...");
        //    $.validator.unobtrusive.parse("#fm-edit-specail-core");
        //}
    });
}

function ViewSpecialisationCore(id, name) {

    var loadUrl = baseUrl + "Program/ViewSpecialisationCore/" + id;

    BootstrapDialog.show({
        title: name + " Details",
        message: $('<div></div>').load(loadUrl),
    });
}

function ViewCourse(id, name) {
    var loadUrl = baseUrl + "Program/Course/" + id;


    BootstrapDialog.show({
        title: name + " Details",
        message: $('<div></div>').load(loadUrl)
    });
}

function FacultyDepartmentIconAccordionChanger(index) {

    $('.faculty-accordion-' + index).on('shown.bs.collapse', function () {
        $("#faculty-accordion-icon-" + index).removeClass("glyphicon-plus").addClass("glyphicon-minus");
    });

    $('.faculty-accordion-' + index).on('hidden.bs.collapse', function () {
        $("#faculty-accordion-icon-" + index).removeClass("glyphicon-minus").addClass("glyphicon-plus");
    });
}

function AddTuition(programId, studyPeriodProgramId, year, title) {
    var loadUrl = baseUrl + "Program/AddOrEditProgramTuition?programId=" + programId + "&studyPeriodProgramId=" + studyPeriodProgramId + "&year=" + year;
    BootstrapDialog.show({
        title: title,
        message: $('<div></div>').load(loadUrl),
        closable: true,

    });
}

function OnBeginProgramTuitionPost() {
    $("#dvLoadEleProgramTuition").show();
}

function OnSuccessProgramTuitionPost(result) {
    console.log("Here is the outcome!!");
    $("#dvLoadEleProgramTuition").hide();
    window.location.href = baseUrl + result.url;
}

function OnFailureProgramTuitionPost(xhr) {
    console.log("Some fail for sure!!");
    $("#dvLoadEleProgramTuition").hide();
    //preload the errors...
    $('#dvAddEditProgTuitionContent').html(xhr.responseText);
}

function AddProgramRequirement(programId, semester, year, title) {
    var loadUrl = baseUrl + "Program/AddOrEditProgramRequirement?programId=" + programId + "&semesterSession=" + semester + "&year=" + year;
    BootstrapDialog.show({
        title: title,
        message: $('<div></div>').load(loadUrl),
        closable: true,

    });
}

function OnBeginProgramRequirementPost() {
    $("#dvLoadEleProgramRequirement").show();
}

function OnSuccessProgramRequirementPost(result) {
    console.log("Here is the outcome!!");
    $("#dvLoadEleProgramRequirement").hide();
    window.location.href = baseUrl + result.url;
}

function OnFailureProgramRequirementPost(xhr) {
    console.log("Some fail for sure!!");
    $("#dvLoadEleProgramRequirement").hide();
    //preload the errors...
    $('#dvAddEditProgRequirementContent').html(xhr.responseText);
}

function DeleteCourse(courseId, courseName, programName, noOfResultsToBeDeleted) {
    console.log("This was clicked for course deletion: " + courseId + " with Name " + courseName);

    var message = "<div> <div id='dv-success-notification'></div>" +
            "Are you sure you would like to delete '" + courseName + "' from '"+programName+"'? <strong><small>"+noOfResultsToBeDeleted+" student results shall be deleted <small></strong>";

    var deletionCallBackfn = function confirmDeletion($dialog) { //pass the dialog for usage...
        console.log("then we delete...");
        var courseToDelete = {
            //id: courseId
        };

        $.ajax({
            url: baseUrl + "Program/DeleteCourse/"+courseId +"?progName="+programName + "&&courseName="+ courseName,
            type: 'POST',
            data: courseToDelete,
            success: function (data) {
                console.log(data);

                if (data.isDeleted) {
                    showToast(data.message, "success");

                    //reload after some mill seconds..
                    setTimeout(function () {
                        //close the dialog...
                        $dialog.close();

                        window.location.reload(true);
                    }, 1000);

                } else {
                    showToast(data.message, "error");
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

    ConfirmBeforeSubmission("Delete Course", message, deletionCallBackfn);

}

function OnSpecialisatonCoreEditSaveBegin() {
    console.log('Here...');

    //disable the button...
    $('#btn-edit-special-core-loader').attr("disabled", "disabled");
    $('#div-edit-special-core-loader').removeClass('hide');

    return true;
}

function OnSpecialisationCoreEditSaveComplete(content) {
    //    var jsonData = content.get_response().get_object();
    //    var response
    //console.log("Content: " + JSON.stringify(content));
    //var response = content.responseText;

    //renable the button..
    $('#btn-edit-special-core-loader').removeAttr('disabled');
    $('#div-edit-special-core-loader').addClass('hide');

}