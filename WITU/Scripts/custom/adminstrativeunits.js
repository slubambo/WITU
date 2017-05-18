$(document).ready(function () {
    if ($('.chosen-select').length) {
        InitialiseChosen();
    }
    if ($('.tbl-campus-faculties').length) {
        $(".tbl-campus-faculties").dataTable({
            "iDisplayLength": 25,
            "aoColumnDefs": [
                {
                    "bSearchable": false, //Both Search and Sort
                    "bSortable": false,
                    "aTargets": [1]
                }
            ],
            "bDestroy": true
        });
    }
    
     if ($('#drpdwnfacultieslist').length) {
         $("#drpdwnfacultieslist").change(function () {

             var facultyCoreId = $(this).val();
             if (facultyCoreId == -1) {
                 //TODO: Redirect to AddOrEditFacultyCore page
                 /*
                 var url = "/AdministrativeUnits/AddOrEditFacultyCore/?redirectUrl="+"AddFaculty";
                 window.location.href = url;
                 */
                 //Show dialog

                 EditFacultyCore();
             }

         });
     }

     if ($('#drpdwndepartmentlist').length) {
         $("#drpdwndepartmentlist").change(function () {

             var departmentCoreId = $(this).val();
             if (departmentCoreId == -1) {
                 //TODO: Redirect to AddOrEditDepartmentCore page

                 //var url = "/AdministrativeUnits/AddOrEditDepartmentCore/?redirectUrl=" + "AddDepartment";
                 //window.location.href = url;
                 EditDepartmentCore();
             }

         });
    }
    
});

function AddOrEditDepartment(departmentCoreId, facultyId) {

    var loadUrl = baseUrl + "AdministrativeUnits/AddOrEditDepartment?departmentCoreId=" + departmentCoreId;

    if (departmentCoreId != undefined)
        loadUrl = loadUrl + "&facultyId=" + facultyId;

    BootstrapDialog.show({
        title: name + " Add or Edit Department",
        message: $('<div></div>').load(loadUrl),
    });
}

function InitialiseChosen() {
    var config = {
        '.chosen-select': {disable_search_threshold: 10, search_contains: true, no_results_text: "No results match for"},
        '.chosen-select-deselect': { allow_single_deselect: true },
        '.chosen-select-no-single': { disable_search_threshold: 10 },
        '.chosen-select-no-results': { no_results_text: 'Oops, nothing found!' },
        '.chosen-select-width': { width: "95%" }//,
        //'.chosen-search': {disable_search: false}
    };
    for (var selector in config) {
        $(selector).chosen(config[selector]);
    }

}

function EditFacultyCore() {
    var loadUrl = baseUrl + "AdministrativeUnits/EditFacultyCore/";

    BootstrapDialog.show({
        title: "Add Faculty",
        message: $('<div></div>').load(loadUrl),
        
    });
}

function EditDepartmentCore() {
    var loadUrl = baseUrl + "AdministrativeUnits/EditDepartmentCore/";

    BootstrapDialog.show({
        title: "Add Department",
        message: $('<div></div>').load(loadUrl),

    });
}

function EditFacultyCoreOnBegin() {
    console.log('Here...');

    //disable the button...
    $('#btn-edit-faculty-core').attr("disabled", "disabled");
    $('#sp-loading-icon').removeClass('hide');

    return true;
}

function EditFacultyCoreOnComplete(content) {
    //    var jsonData = content.get_response().get_object();
    //    var response
    //console.log("Content: " + JSON.stringify(content));
    //var response = content.responseText;

    //renable the button..
    $('#btn-edit-faculty-core').removeAttr('disabled');
    $('#sp-loading-icon').addClass('hide');

}

function EditFacultyCoreOnSuccess(content) {
    console.log("Success? " + JSON.stringify(content));
    //show success...
    $('#dv-edit-faculty-core-success').removeClass('hide');
    
   // $('#drpdwn-faculty').append($('<option></option>').val(faculties.Value).html(faculties.Text));
    //$("#drpdwn-faculty").val(facultyId);

    if ($('#drpdwnfacultieslist').length) {
        var object = content;
        $('#drpdwnfacultieslist').append($('<option></option>').val(object.Id).html(object.Name));
        $("#drpdwnfacultieslist").val(object.Id);
        if ($('.chosen-select').length) {
            $("#drpdwnfacultieslist").trigger("chosen:updated");
        }

    }
    if ($('#drpdwndepartmentlist').length) {
        var object = content;
        $('#drpdwndepartmentlist').append($('<option></option>').val(object.Id).html(object.Name));
        $("#drpdwndepartmentlist").val(object.Id);
        if ($('.chosen-select').length) {
            $("#drpdwndepartmentlist").trigger("chosen:updated");
        }

    }
    //reload after some mill seconds..
//    setTimeout(function () {
//        window.location.reload(true);
//    }, 1000);
}

function EditFacultyCoreOnFailure(content, responseR) {
    console.log("Error!!" + content);
    var object = JSON.parse(content.responseText);
    var objMsg = object.Message;
    var message = "<li class='error text-danger'> "+objMsg+" </li>";
    //pass on the notification that something went wrong..
    $('.validation-summary-valid').find('ul').html(message);
    $('.validation-summary-valid').show();
}


