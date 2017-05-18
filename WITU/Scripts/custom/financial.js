/// <reference path="~/Scripts/base/jquery-1.10.2.intellisense.js" />
/// <reference path="../bootstrap-dialog/js/bootstrap-dialog.js" />
function feeDetailComplete(functioanFeeId, year, graduateLevel, studyPeriodCategory, title) {
    var loadUrl = baseUrl + "Financial/FeeDetailComplete?year=" + year + "&graduateLevel="
        + graduateLevel + "&studyPeriodCategory=" + studyPeriodCategory + "&functionalFeeId=" + functioanFeeId;

    BootstrapDialog.show({
        title: title,
        message: $('<div></div>').load(loadUrl)
    });
}

if ($('.importType').length) {
    $(".Batched-returns").hide(); 
    $(".interactive-returns").hide();
    $(".import-proceed").click(function() {

        var drp = $("select.returnsType").val();
        if (drp == 0) {
            $(".importType").hide();
            $(".Batched-returns").show();
        }
        
        if (drp == 1) {
            $(".importType").hide();
            $(".interactive-returns").show();
        }

    });
}

if ($('.editFee').length) {

    function EditFee(id) {
        var loadUrl = baseUrl + "Financial/EditInvoiceOrPayment/" + id;
        BootstrapDialog.show({
            title: "Edit Invoice/Payment",
            message: $('<div></div>').load(loadUrl),
        });
    }
}

function DeleteFee(id) {
        
        var loadUrl = baseUrl + "Financial/DeleteInvoiceOrPayment/" + id;
        
        var url = $(location).attr('href');
        console.log("Url: " + url);

        //check if url exisits...
        var urlExisits = UrlExists(url);

        BootstrapDialog.confirm('You are about to Delete an Optional Charge, Are you sure?', function (result) {
            if (result) {
                if (urlExisits) {
                    DeleteFeeContinue(id);
                } else
                    window.history.go(-1);

            } else {

            }
        });
        
    }
    
function DeleteFeeContinue(ids) {
    var id = JSON.stringify(ids);
    var studentId = $('#studentId').val();
    $.ajax({
            url: baseUrl + "api/UtilsApi/DeleteInvoiceOrPayment/" + id,
            type: "POST",
            contentType: "application/json",
            dataType: "JSON",
            timeout: 10000,
            success: function (data) {
                //window.history.go(0);
                window.location.href = baseUrl + 'Financial/StudentFinancialOverview/' + studentId;
            }
        });
    }


