$().ready(function () {
    var group = this.className;
    
    for (i = 0; i < 4; i++) {
        
        $("#Us_campus"+i).hide();
        $("#Us_faculty"+i).hide();
        $("#Us_department"+i).hide();
        $("#Us_program" + i).hide();
        
        // ressetDropdown($(".UsdrpPosition"));
        //ressetDropdown($("#Usdrpscope" + i));
        ressetDropdown($("#Usdrpcampus" + i));
        ressetDropdown($("#Usdrpfaculty" + i));
        ressetDropdown($("#Usdrpdepartment" + i));
        ressetDropdown($("#UsdrpProgram" + i));
    }

    if ($('.Us_staffRoles').length) {
        $.ajax({
            url: "/api/UtilsApi/LoadStaffRoles/",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            dataType: "JSON",
            timeout: 10000,
            success: function(vals) {
                result = jQuery.parseJSON(vals);
                //var det = jQuery.parseJSON(result.d);
                $('.Us_staffRoles').append($('<option>', {
                    value: 0,
                    text: "-- Select Position Category --"
                }));
                $.each(result, function(val, text) {
                    $('.Us_staffRoles').append(new Option(text.Text, text.Value));

                });
                //$("#Us_staffRoles").trigger('change');
            },
            error: function(xhr, status) {
            }
        });

        $(".Us_staffRoles").change(function() {

            var index = $(this).attr("index");
            var val = $("select#Us_staffRoles" + index).val();

            //resetting all filters except send to...
            ressetDropdown($("#UsdrpPosition" + index));
            ressetDropdown($("#Usdrpcampus" + index));
            ressetDropdown($("#Usdrpfaculty" + index));
            ressetDropdown($("#Usdrpdepartment" + index));
            //ressetDropdown($("#UsdrpProgram"+ index));

            loadPositions(val, index);
            //loadUserCampus();
            //loadUserFaculty();
            //loadUserDepartment();

        });
    }
    
    $("#selectAllCourses").change(selectDeselectCourses);
    
    $('#serverSideStaffTable').dataTable({
        "bServerSide": true,
        "sAjaxSource": "StaffAjaxHandler",
        "bProcessing": true,
        "aoColumns": [
                        {
                            "sName": "ID",
                            "bSearchable": false,
                            "bSortable": false,
                            "fnRender": function (oObj) {
                                return '<a href=\"Details/' +
                                oObj.aData[0] + '\">View</a>';
                            }
                        },
                        { "sName": "FULL_NAME" },
                        { "sName": "POSITIONS" },
                        { "sName": "COURSES" },
                        { "sName": "EMAIL" },
                        { "sName": "PHONE" }
        ]
    });
});

function ressetDropdown(dropdown) {
    //clearing all filters...
    $(dropdown).empty();
}

function getcurrentdata(index) {
    var out = "";
    var drp1 = $("select#Usdrpcampus"+index).val();
    var drp2 = $("select#Usdrpfaculty"+index).val();
    var drp3 = $("select#Usdrpdepartment"+index).val();
    var drp4 = $("select#UsdrpProgram" + index).val();
    var drp5 = $("select#UsdrpPosition" + index).val();
    

    out = out + "," + drp1 + "," + drp2 + "," + drp3 + "," + drp4 + "," + drp5;
    console.log("Output: " + out);
    return out;
}

$(".Usdrpscope").change(function () {

    var index = $(this).attr("index");
    
    ressetDropdown($("#Usdrpcampus" + index));
    ressetDropdown($("#Usdrpfaculty" + index));
    ressetDropdown($("#Usdrpdepartment" + index));
    ressetDropdown($("#UsdrpProgram" + index));

    var level = $("select#Usdrpscope" + index).val();
    
    loadPositionFilters(level, index);
    
    //loadPositionLevelAttr(val, index);
});

    $("#Usdrpcampus0").change(function () {
        
        //resetting all filters except send to...
        ressetDropdown($("#Usdrpfaculty0"));
        ressetDropdown($("#Usdrpdepartment0"));


        loadUserFaculty(0);

    });
    
    $("#Usdrpcampus1").change(function () {

        //resetting all filters except send to...
        ressetDropdown($("#Usdrpfaculty1"));
        ressetDropdown($("#Usdrpdepartment1"));


        loadUserFaculty(1);

    });
    
    $("#Usdrpcampus2").change(function () {

        //resetting all filters except send to...
        ressetDropdown($("#Usdrpfaculty2"));
        ressetDropdown($("#Usdrpdepartment2"));


        loadUserFaculty(2);

    });
    
    $("#Usdrpcampus3").change(function () {

        //resetting all filters except send to...
        ressetDropdown($("#Usdrpfaculty3"));
        ressetDropdown($("#Usdrpdepartment3"));


        loadUserFaculty(3);

    });
    
    $("#Usdrpcampus4").change(function () {

        //resetting all filters except send to...
        ressetDropdown($("#Usdrpfaculty4"));
        ressetDropdown($("#Usdrpdepartment4"));


        loadUserFaculty(4);

    });

    $("#Usdrpfaculty0").change(function () {

        //resetting all filters except send to...
        ressetDropdown($("#Usdrpdepartment0"));
        ressetDropdown($("#UsdrpProgram0"));

        loadUserDepartment(0);
        loadUserFacultyProgram(0);

    });
    
    $("#Usdrpfaculty1").change(function () {

        //resetting all filters except send to...
        ressetDropdown($("#Usdrpdepartment1"));
        ressetDropdown($("#UsdrpProgram1"));

        loadUserDepartment(1);
        loadUserFacultyProgram(1);

    });

    $("#Usdrpfaculty2").change(function () {

        //resetting all filters except send to...
        ressetDropdown($("#Usdrpdepartment2"));
        ressetDropdown($("#UsdrpProgram2"));

        loadUserDepartment(2);
        loadUserFacultyProgram(2);

    });

    $("#Usdrpfaculty3").change(function () {

        //resetting all filters except send to...
        ressetDropdown($("#Usdrpdepartment3"));
        ressetDropdown($("#UsdrpProgram3"));

        loadUserDepartment(3);
        loadUserFacultyProgram(3);

    });

    $("#Usdrpfaculty4").change(function () {

        //resetting all filters except send to...
        ressetDropdown($("#Usdrpdepartment4"));
        ressetDropdown($("#UsdrpProgram4"));

        loadUserDepartment(4);
        loadUserFacultyProgram(4);

    });
    
    $("#Us_department0").change(function () {

        //resetting all filters except send to...
        ressetDropdown($("#UsdrpProgram0"));

        loadUserProgram(0);

    });

    $("#Us_department1").change(function () {

        //resetting all filters except send to...
        ressetDropdown($("#UsdrpProgram1"));

        loadUserProgram(1);

    });

    $("#Us_department2").change(function () {

        //resetting all filters except send to...
        ressetDropdown($("#UsdrpProgram2"));

        loadUserProgram(2);

    });

    $("#Us_department3").change(function () {

        //resetting all filters except send to...
       ressetDropdown($("#UsdrpProgram3"));

       loadUserProgram(3);

    });

    $("#Us_department4").change(function () {

        //resetting all filters except send to...
        ressetDropdown($("#UsdrpProgram4"));

        loadUserProgram(4);

    });
    

function loadPositions(val, index) {
    
   $.ajax({
        url: baseUrl + "api/UtilsApi/LoadPositions/" + val,
        type: "POST",
        contentType: "application/json",
        dataType: "JSON",
        timeout: 10000,
        success: function (vals) {
            result = jQuery.parseJSON(vals);
            $("#UsdrpPosition"+index).append($('<option>', {
                value: 0,
                text: "-- Select Position --"
            }));
            $.each(result, function(val, text) {
                $("#UsdrpPosition"+index).append(new Option(text.Text, text.Value));

            });
        },
        error: function(xhr, status) {
        }
    });

}

function loadUserCampus(index) {
   $.ajax({
        url: baseUrl + "api/UtilsApi/LoadCampus/",
        type: "POST",
        contentType: "application/json",
        dataType: "JSON",
        timeout: 10000,
        success: function(vals) {
            result = jQuery.parseJSON(vals);
            if (result != null) {
                $("#Usdrpcampus"+index).append($('<option>', {
                    value: 0,
                    text: "-- Select Campus --"
                }));
                $.each(result, function(val, text) {
                    $("#Usdrpcampus"+index).append(new Option(text.Text, text.Value));

                });
            }
        },
        error: function(xhr, status) {
        }
    });
}

function loadUserFaculty(index) {
    //var filter = getcurrentdata(index);
    var campusId = parseInt($("select#Usdrpcampus" + index).val()) || 0;
    $.ajax({
        url: baseUrl + "api/UtilsApi/LoadFaculties/" + campusId,
        type: "POST",
        contentType: "application/json",
        dataType: "JSON",
        timeout: 10000,
        success: function(vals) {
            result = jQuery.parseJSON(vals);
            $("#Usdrpfaculty"+index).append($('<option>', {
                value: 0,
                text: "-- Select Faculty --"
            }));
            $.each(result, function(val, text) {
                $("#Usdrpfaculty"+index).append(new Option(text.Text, text.Value));

            });

        },
        error: function(xhr, status) {
        }
    });

}

function loadUserDepartment(index) {
    var campusId = parseInt($("select#Usdrpcampus" + index).val()) || 0;
    var facultyId = parseInt($("select#Usdrpfaculty" + index).val()) || 0;
    $.ajax({
        url: baseUrl + "api/UtilsApi/LoadDepartments/" + campusId + "/" + facultyId,
        type: "POST",
        contentType: "application/json",
        dataType: "JSON",
        timeout: 10000,
        success: function(vals) {
            result = jQuery.parseJSON(vals);
            $("#Usdrpdepartment" + index).append($('<option>', {
                value: 0,
                text: "-- Select Department --"
            }));
            $.each(result, function(val, text) {
                $("#Usdrpdepartment" + index).append(new Option(text.Text, text.Value));

            });

        },
        error: function(xhr, status) {
        }
    });

}

function loadPositionLevelAttr(positionId, index) {
    
   $.ajax({
        url: baseUrl + "api/UtilsApi/LoadLevel/" + positionId,
        type: "POST",
        contentType: "application/json",
        dataType: "JSON",
        timeout: 10000,
        success: function (vals) {
            result = jQuery.parseJSON(vals);
            loadPositionFilters(result, index);

        },
        error: function (xhr, status) {
        }
    });
    
}

function loadPositionFilters(level, index) {
    switch (level) {
    case "1":
            $("#Us_campus" + index).css("display", "none");
            $("#Us_faculty" + index).css("display", "none");
            $("#Us_department" + index).css("display", "none");
            $("#Us_program" + index).css("display", "none");
            break;
        case "2":
           $("#Us_faculty" + index).css("display", "none");
           $("#Us_department" + index).css("display", "none");
           $("#Us_program" + index).css("display", "none");
            
           loadUserCampus(index);
           $("#Us_campus"+ index).show();
           break;
            
        case "3":
            loadUserFaculty(index);
            $("#Us_department" + index).css("display", "none");
            $("#Us_program" + index).css("display", "none");

             loadUserCampus(index);
             loadUserFaculty(index);

             $("#Us_campus"+ index).show();
             $("#Us_faculty"+ index).show();
             break;
            
        case "4":
            $("#Us_program" + index).css("display", "none");
            
            loadUserCampus(index);
            loadUserFaculty(index);
            loadUserDepartment(index);

            $("#Us_campus" + index).show();
            $("#Us_faculty" + index).show();
            $("#Us_department" + index).show();

            break;
            
        case "5":
            loadUserCampus(index);
            loadFacultys(index);
            loaddepartments(index);
            loadUserProgram(index);
            
            $("#Us_campus" + index).show();
            $("#Us_faculty" + index).show();
            $("#Us_department" + index).show();
            $("#Us_program" + index).show();
            break;
            
        case "6":
            loadUserCampus(index);
            loadFacultys(index);
            loaddepartments(index);
            loadUserProgram(index);

            $("#Us_campus" + index).show();
            $("#Us_faculty" + index).show();
            $("#Us_department" + index).show();
            $("#Us_program" + index).show();
            break;
            
            }
}

function loadUserProgram(index) {
    //var filter = getcurrentdata(index);
    var campusId = parseInt($("select#Usdrpcampus" + index).val()) || 0;
    var facultyId = parseInt($("select#Usdrpfaculty" + index).val()) || 0;
    var departmentId = parseInt($("select#Usdrpdepartment" + index).val()) || 0;
    $.ajax({
        url: baseUrl + "api/UtilsApi/LoadProgrammes/" + campusId + "/" + facultyId + "/" + departmentId,
        type: "POST",
        contentType: "application/json",
        dataType: "JSON",
        timeout: 10000,
        success: function (vals) {
            result = jQuery.parseJSON(vals);
            $('#UsdrpProgram' + index).append($('<option>', {
                value: 0,
                text: "-- Select Program --"
            }));
            $.each(result, function (val, text) {
                $('#UsdrpProgram' + index).append(new Option(text.Text, text.Value));

            });

        },
        error: function (xhr, status) {
        }
    });

}

function loadUserFacultyProgram(index) {
    //var filter = getcurrentdata(index);
    var campusId = parseInt($("select#Usdrpcampus" + index).val()) || 0;
    var facultyId = parseInt($("select#Usdrpfaculty" + index).val()) || 0;
    
    $.ajax({
        url: baseUrl + "api/UtilsApi/LoadProgrammes/" + campusId + "/" + facultyId,
        type: "POST",
        contentType: "application/json",
        dataType: "JSON",
        timeout: 10000,
        success: function (vals) {
            result = jQuery.parseJSON(vals);
            $('#UsdrpProgram' + index).append($('<option>', {
                value: 0,
                text: "-- Select Program --"
            }));
            $.each(result, function (val, text) {
                $('#UsdrpProgram' + index).append(new Option(text.Text, text.Value));

            });

        },
        error: function (xhr, status) {
        }
    });

}

function selectDeselectCourses() {
    var oTable = $('#assignCoursesTable').dataTable()._('tr', { "filter": "applied" });
    var rowcollection = oTable.$(".selectByRow", { "page": "all" });

    if ($('#selectAllCourses').is(':checked') == true) {
        rowcollection.each(function (index, elem) {
            $(elem).is(":checked") ? "" : $(elem).prop('checked', true);
        });
    } else {
        rowcollection.each(function (index, elem) {
            $(elem).is(":checked") ? $(elem).prop('checked', false) : "";
        });
    }
}
