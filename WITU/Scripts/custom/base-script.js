/// <reference path="../jquery-1.10.2.js" />
/// <reference path="../jquery-1.10.2.min.js" />
/// <reference path="~/Scripts/base/jquery-ui-1.11.1.js" />
/// <reference path="../ckeditor/adapters/jquery.js" />

//variables that are to be restricte

$(document).ready(function () {
    
    // Initialise AutoComplete
    $('*[data-autocomplete-url]')
        .each(function () {
            $(this).autocomplete({
                source: $(this).data("autocomplete-url"), //here
                minLength: 5
            });
        }); 
    // End AutoComplete Initialisations

    var dataTables = null;
     
    $('.tabs').tabs();

    if ($('.dataTable').length) {
        dataTables = $(".dataTable").dataTable();

        //enabling datatables... Feature still so buggy to work with at the moment.
        if ($('.dataTable-responsive').length && dataTables != null) {
            $(".dataTable").dataTable({
                responsive: true,
                "bDestroy": true
            });
        }

        //to disable sorting..
        if ($('.dataTableDisabledSort').length)
            $('.dataTableDisabledSort').dataTable({ "bSort": false, "bDestroy": true });
    }

    //dataTable Filter - For Simon Fred!!
    if ($('.dataTableFilter').length)
        $(".dataTableFilter").dataTable()
            .columnFilter({
                sPlaceHolder: "head:before",
                aoColumns: [{ type: "text" },
                    { type: "text" },
                    { type: "text" },
                    { type: "text" },
                    { type: "date-range" }
                ], "bDestroy": true
            });

    //Datatable with row Grouping and Default Sorting
    var table = $('.responsiblityTable').DataTable({
        "columnDefs": [
            { "visible": false, "targets": 0 }
        ],
        //"order": [[0, 'desc'], [2, 'desc']],
        "displayLength": 25,
        "paging": false, "bDestroy": true ,
        "drawCallback": function(settings) {
            var api = this.api();
            var rows = api.rows({ page: 'current' }).nodes();
            var last = null;

            api.column(0, { page: 'current' }).data().each(function(group, i) {
                if (last !== group) {
                    $(rows).eq(i).before(
                        '<tr class="group"><td colspan="5">' + group + '</td></tr>'
                    );

                    last = group;
                }
            });
        }
    });

    // Order by the grouping
    $('.responsiblityTable tbody').on('click', 'tr.group', function() {
        var currentOrder = table.order()[0];

        if (currentOrder[0] === 0 && currentOrder[1] === 'asc') {
            table.order([0, 'desc']).draw();
        } else {
            table.order([0, 'asc']).draw();
        }

    });

    if ($('.tinymce-textarea').length) {
        tinymce.init({
            selector: ".tinymce-textarea",
            theme: "modern",
            plugins: [
                "advlist autolink lists link image charmap print preview hr anchor pagebreak",
                "searchreplace wordcount visualblocks visualchars code fullscreen",
                "insertdatetime media nonbreaking save table contextmenu directionality",
                "emoticons template paste textcolor colorpicker textpattern"
            ],
            toolbar1: "insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image",
            toolbar2: "print preview media | forecolor backcolor emoticons",
            image_advtab: true,
            templates: [
                { title: 'Test template 1', content: 'Test 1' },
                { title: 'Test template 2', content: 'Test 2' }
            ]
        });
    }

    if ($('.ckeditor-textarea').length) {
        $('textarea.ckeditor-textarea').ckeditor();
    }

    //spinner for loading items...
    if ($('.spinner-loading').length) {
        $('.spinner-loading').hide();
    }

    $('.date-picker').datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        dateFormat: "dd/mm/yy",
        yearRange: "c-100:c+80",
    });
    $('.birthday-date-picker').datepicker({
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,
        dateFormat: "dd/mm/yy",
        yearRange: "c-100:c+80",
        maxDate: 0
    });

    //Works with date-end-date class to provide the range within start and and end date.
    $(".date-start-date").datepicker({
        defaultDate: "+1w",
        changeMonth: true,
        changeYear: true,
        numberOfMonths: 1,
        dateFormat: 'dd/mm/yy',
        onClose: function(selectedDate) {
            $(".txtEntryTo").datepicker("option", "minDate", selectedDate);
        }
    });
    $(".date-end-date").datepicker({
        defaultDate: "+1w",
        changeMonth: true,
        changeYear: true,
        numberOfMonths: 1,
        dateFormat: 'dd/mm/yy',
        onClose: function(selectedDate) {
            $(".txtEntryFrom").datepicker("option", "maxDate", selectedDate);
        }
    });

    //showing icons on accordions...
    $('.accordion-with-icons').on('hidden.bs.collapse', toggleChevron);
    $('.accordion-with-icons').on('shown.bs.collapse', toggleChevron);

    /* Spinning Effect */
    $.fn.spins = function(opts) {
        this.each(function() {
            var $this = $(this),
                data = $this.data();

            if (data.spinner) {
                data.spinner.stop();
                delete data.spinner;
            }
            if (opts !== false) {
                data.spinner = new Spinner($.extend({ color: $this.css('color') }, opts)).spin(this);
            }
        });
        return this;
    };

    // Shows spinning Loader for every ajax load

    $(document).hide().ajaxStart(function() {
        $('#loadingAjax').show();
        $('#preview1').spins();
        //showLoadingDialogOnAjax(true);
    })
        .ajaxStop(function() {
            $('#loadingAjax').hide();
            $('#preview1').spins(false);
        });

    //clickable row...
    if ($('.table-clickable-row').length) {
        $(".table-clickable-row tr").click(function() {
            var url = $(this).data("url");
            if (url != undefined)
                window.location.href = url;
        });
    }

    //when we are to load components...
    if ($('.frm-show-dialog').length) {
        $('.frm-show-dialog').submit(function(e) {
            showLoadingDialog();
            console.log("Form being submitted..");
        });
    }

    //If a create student form exists, add asterisk for required fields
    if ($('.interactive-student-form').length) {
        setRequired();
    }

    //slick slider
    $(".regular").slick({
        dots: true,
        infinite: false,
        speed: 300,
        slidesToShow: 3,
        slidesToScroll: 1,
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

    $(".regular-two").slick({
        dots: true,
        infinite: false,
        speed: 300,
        slidesToShow: 4,
        slidesToScroll: 1,
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

    if ($('.slick-slider').length) {

        $('.slick-slider').slick({
            dots: true,
            infinite: false,
            speed: 300,
            slidesToShow: 4,
            slidesToScroll: 1,
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

    if ($('#tile-wrapper').length) {
        $(function () {
            $('#tile-wrapper').tiles();
        });
    }


});

function activatePilledTabs() {
    $('.tabs-pilled').on("tabsactivate", function(event, ui) {
        ui.newTab.addClass('active');
        ui.newPanel.addClass('active');

        ui.oldTab.removeClass('active');
        ui.oldTab.removeClass('active');
    });
}

function toggleChevron(e) {
    $(e.target)
        .prev('.panel-heading')
        .find('i.indicator')
        .toggleClass('glyphicon-chevron-down glyphicon-chevron-up');
}

function cancelSubmission(url, message) {
    console.log("Url: " + url);

    if (message == undefined)
        message = 'You are about to cancel submission. Are you sure?';

    //check if url exisits...
    var urlExisits = UrlExists(url);

    BootstrapDialog.confirm(message, function(result) {
        if (result) {
            if (urlExisits)
                window.location.href = url;
            else
                window.history.go(-1);

        } else {

        }
    });
}

function UrlExists(url) {
    var http = new XMLHttpRequest();
    http.open('HEAD', url, false);
    http.send();
    return http.status == 200;
}

function AlertMessageDialog(message, title) {

    var actualTitle = title == undefined ? "Message" : title;

    BootstrapDialog.alert({
        title: actualTitle,
        message: message,
        type: BootstrapDialog.TYPE_WARNING, // <-- Default value is BootstrapDialog.TYPE_PRIMARY
        closable: true, // <-- Default value is true
        //buttonLabel: 'Roar! Why!', // <-- Default value is 'OK',
        //callback: function (result) {
        //    // result will be true if button was click, while it will be false if users close the dialog directly.
        //    alert('Result is: ' + result);
        //}
    });
}

/*
This basically makes a confirmation before using the call back function to process the information.
*/

function ConfirmBeforeSubmission(title, message, callBackFunction) {
    BootstrapDialog.show({
        title: title,
        message: message,
        type: BootstrapDialog.TYPE_WARNING, // <-- Default value is BootstrapDialog.TYPE_PRIMARY
        closable: true, // <-- Default value is true
        buttons: [
            {
                label: 'Ok',
                action: function(dialog) {
                    callBackFunction(dialog);
                },
                cssClass: 'btn-warning'
            },
            {
                label: 'Cancel',
                action: function(dialog) {
                    dialog.close();
                }
            }
        ]
    });
}

function showToast(message, toastType, title) {
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": "toast-top-full-width",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };

    //when no message is provided...
    if (message == undefined)
        throw ("At least a message is required for the Toast to work.");

    //when no toast time is provided...
    if (toastType == undefined)
        toastType = "info";

    if (title == undefined)
        toastr[toastType](message);
    else
        toastr[toastType](message, title);


}

//showing the loading dialog for the script...

function showLoadingDialog(message, title) {

    var loadingMessage = $('<div><span class="fa fa-spin fa-spinner fa-2x"></span>');

    if (message != undefined)
        loadingMessage.append('<label>' + message + '</label>');
    else
        loadingMessage.append('<label>Loading Results... </label>');

    console.log("Form being submitted.. " + message);


    BootstrapDialog.show({
        title: title == undefined ? "Please Wait" : title,
        message: loadingMessage,
        closable: false
    });

    return true;
}

function showLoadingDialogOnAjax(opts) {

    var loadingMessage = $('<div><span class="fa fa-spin fa-spinner fa-2x"></span>');

    var message = "Loading ...";

    var title = "Please Wait";

    loadingMessage.append('<label>' + message + '</label>');

    console.log("Form being submitted.. " + message);


    if (opts !== false) {
        BootstrapDialog.show({
            title: title,
            message: loadingMessage,
            closable: false,
            timeout: 100
        });
    } else {
        BootstrapDialog.closeAll();
        //BootstrapDialog.stop();
    }

    if (opts !== false) {
        return true;
    } else {

        return false;
    }
}

function setRequired() {
    var $form = $('form');
    $('form').find("[data-val-required]").each(function(index) {
        var $input = $(this);
        var requiredAsterisk = "<span class=\"required\">*</span>";
        var id = $input.attr('id');
        var $label = $form.find("label[for='" + id + "']");
        if ($label.length > 0 && id != "DateOfBirth") {
            var html = $label.html() + "";
            if (html.indexOf(requiredAsterisk) <= 0) $label.html(html + requiredAsterisk);
        }
    });
}

;