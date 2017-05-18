$(document).ready(function () {

    if ($('#dateDiv').length) {
        $("#dateDiv").css("display", "none");
        $("#chkDateRange").change(function () {
            if ($('#chkDateRange').is(':checked') == true) {
                $("#dateDiv").show();
                $("#spnDateRange").hide();
            } else {
                $("#dateDiv").hide();
                $("#spnDateRange").show();

            }
        });
    }
    // Used to set the Date End of a Semester to atleast be a month after the Selected Start Date
    if ($('#date-start-date0').length) {
        $("#date-start-date0").datepicker({
            dateFormat: 'dd/mm/yy',
            onSelect: function(date) {
                var dt2 = $('#date-end-date0');
                //Set a variable Start Date to be equal to the selected Date
                var startDate = $(this).datepicker('getDate');
                //Sets minimum Date
                dt2.datepicker('setDate', minDate);
                //Adds 30 days to the selected Date
                startDate.setDate(startDate.getDate() + 30);
                var minDate = startDate;

                dt2.datepicker('option', 'minDate', minDate);

            }
        });
        $('#date-end-date0').datepicker({
            dateFormat: 'dd/mm/yy',
        });
    }

    if ($('#date-start-date1').length) {
        $("#date-start-date1").datepicker({
            dateFormat: 'dd/mm/yy',
            onSelect: function(date) {
                var dt2 = $('#date-end-date1');
                //Set a variable Start Date to be equal to the selected Date
                var startDate = $(this).datepicker('getDate');
                //Sets minimum Date
                dt2.datepicker('setDate', minDate);
                //Adds 30 days to the selected Date
                startDate.setDate(startDate.getDate() + 30);
                var minDate = startDate;

                dt2.datepicker('option', 'minDate', minDate);

            }
        });
    }

    $("#date-end-date1").datepicker({
        dateFormat: 'dd/mm/yy',
    });
    if ($('.semstats').length) {
        getSemesterProgress();
    }
    
    //disable search and sort on students grading scheme table
    $(".tbl-student-grading-scheme").dataTable({
        "iDisplayLength": 15,
        "aoColumnDefs": [
            {
                "bSearchable": false, //Both Search and Sort
                "bSortable": false,
                "aTargets": [0, 1, 2]
            }
        ],
        "bDestroy": true
    });
    
});

function getSemesterProgress() {
    $.getJSON(baseUrl + 'Api/UtilsApi/GetSemesterProgress', function (data) {

        if (data != null)
        {
             
            var currentSemExists = data[0];
            if (currentSemExists) {
                console.log("Semester exists");
                
                var semProgressSumary = data[1];
                var elapsedDays = semProgressSumary.elapsedDays;
                var totalDays = semProgressSumary.totalDays;
                var percent = semProgressSumary.percent;
                $('#elapseddays').html(elapsedDays);
                $('#totaldays').html(totalDays);
                $('#sem-progress-bar').css('width', percent + '%').attr('aria-valuenow', percent);
                
            } else {
                console.log("Semester doesn't exist");
            }
            
            
        }
    });
}