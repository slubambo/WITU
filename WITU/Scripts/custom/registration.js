var creditSum = 0;
var maximumSemLoad = 0;

 $(document).ready(function() {
        //Handles interructions with the Registration Module

        maximumSemLoad = $('#MaximumSemLoad').val();

        creditSum = 0;
        //check if any checkbox has loaded...
        $(".course_checked").each(function() {
            if ($(this).is(":checked")) {
                var currentCredit = parseInt($(this).closest('tr').children('td.creditUnitVal').text());
                creditSum += currentCredit;
                console.log("Credit got: " + currentCredit);
            }
        });

        CreditSummationLabel(creditSum);
     
        if ($('.auditCourse').length) {

            $("#LoadAuditCourses").click(function () {

                $('#DisplayAuditCourses').dataTable({
                    "ajax": {
                        "url": "data.json",
                        "dataSrc": "tableData"
                    }
                });
            });
        }
     
        if ($('.RegistrationType').length) {
            $(".Batched-registration").hide();
            $(".interactive-registration").hide();
            $(".reg-proceed").click(function () {

                var drp = $("select.regType").val();
                if (drp == 0) {
                    $(".RegistrationType").hide();
                    $(".Batched-registration").show();
                }

                if (drp == 1) {
                    //$(".RegistrationType").hide();
                    //$(".interactive-registration").show();
                    window.location.href = baseUrl + 'Registration/InteractiveApproval/';
                }

            });
        }
     
     //Deleting Registered Semester I Courses 
        if ($('.DeleteRegCourseSemOne').length) {
            var regCourseDeleteTable = $('.coursesRegTableOne').DataTable();
            $(".DeleteRegCourseSemOne").click(function () {
                var selectedCourses = [];
                var regErrorMsge = "";
                var regErrorTitle = "";
                //var regTable = $('.coursessTable').DataTable()._('tr', { "filter": "applied" });
                var rowcollection = regCourseDeleteTable.$(".courseSelectOne:checked", { "page": "all" });
                rowcollection.each(function(index, elem) {
                 var checkboxValues = $(elem).val();
                 selectedCourses.push(checkboxValues);
             });

             if (selectedCourses.length == 0) {
                 regErrorMsge = "Please select Courses(s) to Delete";
                 regErrorTitle = "No Course Selected";
                 AlertMessageDialog(regErrorMsge, regErrorTitle);
             } else {
                 var url = $(location).attr('href');
                 console.log("Url: " + url);

                 //check if url exisits...
                 var urlExisits = UrlExists(url);

                 BootstrapDialog.confirm('You are about to Delete Course(s), Are you sure?', function(result) {
                     if (result) {
                         if (urlExisits) {
                             DeleteRegisteredCourses(selectedCourses);
                         } else
                             window.history.go(-1);

                     } else {

                     }
                 });


             }

         });
        }
     
     //Deleting Registered Semester II Courses 
        if ($('.DeleteRegCourseSemTwo').length) {
            var reggTable = $('.coursesRegTableTwo').DataTable();
            $(".DeleteRegCourseSemTwo").click(function () {
                var selectedCourses = [];
                var selectedNames = [];
                var regErrorMsge = "";
                var regErrorTitle = "";
                
                var rowcollection = reggTable.$(".courseSelectTwo:checked", { "page": "all" }).closest('tr');
                rowcollection.each(function (index, row) {
                    var checkboxValues = $(row).find(".courseSelectTwo:checked").eq(0).val();
                    var checkboxCourses = $(row).find("td").eq(2).text().trim();
                    selectedCourses.push(checkboxValues);
                    selectedNames.push(checkboxCourses);
                });

               
                    
               


                if (selectedCourses.length == 0) {
                    regErrorMsge = "Please select Courses(s) to Delete";
                    regErrorTitle = "No Course Selected";
                    AlertMessageDialog(regErrorMsge, regErrorTitle);
                } else {
                    var url = $(location).attr('href');
                    console.log("Url: " + url);

                    var message = "You are about to Delete the Following Course(s): <br/><ul>";

                    for (var i = 0; i < selectedNames.length; i++) {
                        message += "<li>" + selectedNames[i] + "</li>";

                    }

                    message += "</ul>";
                    message += "Are you Sure?";
                    
                    //check if url exisits...
                    var urlExisits = UrlExists(url);

                    BootstrapDialog.confirm(message, function (result) {
                        if (result) {
                            if (urlExisits) {
                                DeleteRegisteredCourses(selectedCourses);
                             } else
                                window.history.go(-1);

                        } else {

                        }
                    });


                }

            });
        }

     //Exempting Registered Semester I Courses 
        if ($('.ExemptRegCourseSemOne').length) {
            var reggTable = $('.coursesRegTableOne').DataTable();
            $(".ExemptRegCourseSemOne").click(function () {
                var selectedCourses = [];
                var selectedNames = [];
                var regErrorMsge = "";
                var regErrorTitle = "";

                var rowcollection = reggTable.$(".courseSelectOne:checked", { "page": "all" }).closest('tr');
                rowcollection.each(function (index, row) {
                    var checkboxValues = $(row).find(".courseSelectOne:checked").eq(0).val();
                    var checkboxCourses = $(row).find("td").eq(2).text().trim();
                    selectedCourses.push(checkboxValues);
                    selectedNames.push(checkboxCourses);
                });

                if (selectedCourses.length == 0) {
                    DeleteSuccess();
                    regErrorMsge = "Please select Courses(s) to Exempt";
                    regErrorTitle = "No Course Selected";
                    AlertMessageDialog(regErrorMsge, regErrorTitle);
                } else {
                    var url = $(location).attr('href');
                    console.log("Url: " + url);

                    var message = "You are about to Exempt the Following Course(s): <br/><ul>";

                    for (var i = 0; i < selectedNames.length; i++) {
                        message += "<li>" + selectedNames[i] + "</li>";

                    }

                    message += "</ul>";
                    message += "Are you Sure?";

                    //check if url exisits...
                    var urlExisits = UrlExists(url);

                    BootstrapDialog.confirm(message, function (result) {
                        if (result) {
                            if (urlExisits) {
                                ExemptRegisteredCourses(selectedCourses);
                            } else
                                window.history.go(-1);

                        } else {

                        }
                    });


                }

            });
        }
     
     //Exempting Registered Semester II Courses 
        if ($('.ExemptRegCourseSemTwo').length) {
            var reggTable = $('.coursesRegTableTwo').DataTable();
            $(".ExemptRegCourseSemTwo").click(function () {
                var selectedCourses = [];
                var selectedNames = [];
                var regErrorMsge = "";
                var regErrorTitle = "";

                var rowcollection = reggTable.$(".courseSelectTwo:checked", { "page": "all" }).closest('tr');
                rowcollection.each(function (index, row) {
                    var checkboxValues = $(row).find(".courseSelectTwo:checked").eq(0).val();
                    var checkboxCourses = $(row).find("td").eq(2).text().trim();
                    selectedCourses.push(checkboxValues);
                    selectedNames.push(checkboxCourses);
                });

                if (selectedCourses.length == 0) {
                    regErrorMsge = "Please select Courses(s) to Exempt";
                    regErrorTitle = "No Course Selected";
                    AlertMessageDialog(regErrorMsge, regErrorTitle);
                } else {
                    var url = $(location).attr('href');
                    console.log("Url: " + url);

                    var message = "You are about to Exempt the Following Course(s): <br/><ul>";

                    for (var i = 0; i < selectedNames.length; i++) {
                        message += "<li>" + selectedNames[i] + "</li>";

                    }

                    message += "</ul>";
                    message += "Are you Sure?";

                    //check if url exisits...
                    var urlExisits = UrlExists(url);

                    BootstrapDialog.confirm(message, function (result) {
                        if (result) {
                            if (urlExisits) {
                                ExemptRegisteredCourses(selectedCourses);
                            } else
                                window.history.go(-1);

                        } else {

                        }
                    });


                }

            });
        }
     
     //Abs Registered Semester I Courses 
        if ($('.AbsRegCourseSemOne').length) {
            var reggTable = $('.coursesRegTableOne').DataTable();
            $(".AbsRegCourseSemOne").click(function () {
                var selectedCourses = [];
                var selectedNames = [];
                var regErrorMsge = "";
                var regErrorTitle = "";

                var rowcollection = reggTable.$(".courseSelectOne:checked", { "page": "all" }).closest('tr');
                rowcollection.each(function (index, row) {
                    var checkboxValues = $(row).find(".courseSelectOne:checked").eq(0).val();
                    var checkboxCourses = $(row).find("td").eq(2).text().trim();
                    selectedCourses.push(checkboxValues);
                    selectedNames.push(checkboxCourses);
                    //$(row).remove().draw(false);
                });

                if (selectedCourses.length == 0) {
                    regErrorMsge = "Please select Courses(s) to Abs";
                    regErrorTitle = "No Course Selected";
                    AlertMessageDialog(regErrorMsge, regErrorTitle);
                } else {
                    var url = $(location).attr('href');
                    console.log("Url: " + url);

                    var message = "You are about to Abs the Following Course(s): <br/><ul>";

                    for (var i = 0; i < selectedNames.length; i++) {
                        message += "<li>" + selectedNames[i] + "</li>";

                    }

                    message += "</ul>";
                    message += "Are you Sure?";

                    //check if url exisits...
                    var urlExisits = UrlExists(url);

                    BootstrapDialog.confirm(message, function (result) {
                        if (result) {
                            if (urlExisits) {
                                //DeleteSuccess(rowcollection);
                                AbsRegisteredCourses(selectedCourses);
                            } else
                                window.history.go(-1);

                        } else {

                        }
                    });


                }

            });
        }
     
     //Abs Registered Semester II Courses 
        if ($('.AbsRegCourseSemTwo').length) {
            var reggTable = $('.coursesRegTableTwo').DataTable();
            $(".AbsRegCourseSemTwo").click(function () {
                var selectedCourses = [];
                var selectedNames = [];
                var regErrorMsge = "";
                var regErrorTitle = "";

                var rowcollection = reggTable.$(".courseSelectTwo:checked", { "page": "all" }).closest('tr');
                rowcollection.each(function (index, row) {
                    var checkboxValues = $(row).find(".courseSelectTwo:checked").eq(0).val();
                    var checkboxCourses = $(row).find("td").eq(2).text().trim();
                    selectedCourses.push(checkboxValues);
                    selectedNames.push(checkboxCourses);
                    //$(row).remove().draw(false);
                });

                if (selectedCourses.length == 0) {
                    regErrorMsge = "Please select Courses(s) to Abs";
                    regErrorTitle = "No Course Selected";
                    AlertMessageDialog(regErrorMsge, regErrorTitle);
                } else {
                    var url = $(location).attr('href');
                    console.log("Url: " + url);

                    var message = "You are about to Abs the Following Course(s): <br/><ul>";

                    for (var i = 0; i < selectedNames.length; i++) {
                        message += "<li>" + selectedNames[i] + "</li>";

                    }

                    message += "</ul>";
                    message += "Are you Sure?";

                    //check if url exisits...
                    var urlExisits = UrlExists(url);

                    BootstrapDialog.confirm(message, function (result) {
                        if (result) {
                            if (urlExisits) {
                                //DeleteSuccess(rowcollection);
                                AbsRegisteredCourses(selectedCourses);
                            } else
                                window.history.go(-1);

                        } else {

                        }
                    });


                }

            });
        }

     //Audit Registered Semester I Courses 
        if ($('.AuditRegCourseSemOne').length) {
            var reggTable = $('.coursesRegTableOne').DataTable();
            $(".AuditRegCourseSemOne").click(function () {
                var selectedCourses = [];
                var selectedNames = [];
                var regErrorMsge = "";
                var regErrorTitle = "";

                var rowcollection = reggTable.$(".courseSelectOne:checked", { "page": "all" }).closest('tr');
                rowcollection.each(function (index, row) {
                    var checkboxValues = $(row).find(".courseSelectOne:checked").eq(0).val();
                    var checkboxCourses = $(row).find("td").eq(2).text().trim();
                    selectedCourses.push(checkboxValues);
                    selectedNames.push(checkboxCourses);
                    //$(row).remove().draw(false);
                });

                if (selectedCourses.length == 0) {
                    regErrorMsge = "Please select Courses(s) to Audit";
                    regErrorTitle = "No Course Selected";
                    AlertMessageDialog(regErrorMsge, regErrorTitle);
                } else {
                    var url = $(location).attr('href');
                    console.log("Url: " + url);

                    var message = "You are about to Audit the Following Course(s): <br/><ul>";

                    for (var i = 0; i < selectedNames.length; i++) {
                        message += "<li>" + selectedNames[i] + "</li>";

                    }

                    message += "</ul>";
                    message += "Are you Sure?";

                    //check if url exisits...
                    var urlExisits = UrlExists(url);

                    BootstrapDialog.confirm(message, function (result) {
                        if (result) {
                            if (urlExisits) {
                                //DeleteSuccess(rowcollection);
                                AuditRegisteredCourses(selectedCourses);
                            } else
                                window.history.go(-1);

                        } else {

                        }
                    });


                }

            });
        }

     //Audit Registered Semester II Courses 
        if ($('.AuditRegCourseSemTwo').length) {
            var reggTable = $('.coursesRegTableTwo').DataTable();
            $(".AuditRegCourseSemTwo").click(function () {
                var selectedCourses = [];
                var selectedNames = [];
                var regErrorMsge = "";
                var regErrorTitle = "";

                var rowcollection = reggTable.$(".courseSelectTwo:checked", { "page": "all" }).closest('tr');
                rowcollection.each(function (index, row) {
                    var checkboxValues = $(row).find(".courseSelectTwo:checked").eq(0).val();
                    var checkboxCourses = $(row).find("td").eq(2).text().trim();
                    selectedCourses.push(checkboxValues);
                    selectedNames.push(checkboxCourses);
                    //$(row).remove().draw(false);
                });

                if (selectedCourses.length == 0) {
                    regErrorMsge = "Please select Courses(s) to Audit";
                    regErrorTitle = "No Course Selected";
                    AlertMessageDialog(regErrorMsge, regErrorTitle);
                } else {
                    var url = $(location).attr('href');
                    console.log("Url: " + url);

                    var message = "You are about to Audit the Following Course(s): <br/><ul>";

                    for (var i = 0; i < selectedNames.length; i++) {
                        message += "<li>" + selectedNames[i] + "</li>";

                    }

                    message += "</ul>";
                    message += "Are you Sure?";

                    //check if url exisits...
                    var urlExisits = UrlExists(url);

                    BootstrapDialog.confirm(message, function (result) {
                        if (result) {
                            if (urlExisits) {
                                //DeleteSuccess(rowcollection);
                                AuditRegisteredCourses(selectedCourses);
                            } else
                                window.history.go(-1);

                        } else {

                        }
                    });


                }

            });
        }

     //Configuring New Semester
        if ($('.configureReg').length) {
            $('.configureReg').click(function () {
                
                ConfigureSemesterRegistration();
            });
        }
     
     //Approving Semester I Exemption Requests   
        if ($('.ApproveExpCourseSemOne').length) {
            var regAppExpOneTable = $('.coursesRegTableOne').DataTable();
            $(".ApproveExpCourseSemOne").click(function () {
                var selectedCourses = [];
                var selectedNames = [];
                var regErrorMsge = "";
                var regErrorTitle = "";

                var rowcollection = regAppExpOneTable.$(".courseSelectOne:checked", { "page": "all" }).closest('tr');
                rowcollection.each(function (index, row) {
                    var checkboxValues = $(row).find(".courseSelectOne:checked").eq(0).val();
                    var checkboxCourses = $(row).find("td").eq(2).text().trim();
                    selectedCourses.push(checkboxValues);
                    selectedNames.push(checkboxCourses);
                });

                if (selectedCourses.length == 0) {
                    regErrorMsge = "Please select Courses(s) to Exempt";
                    regErrorTitle = "No Course Selected";
                    AlertMessageDialog(regErrorMsge, regErrorTitle);
                } else {
                    var url = $(location).attr('href');
                    console.log("Url: " + url);

                    var message = "You are about to Exempt the Following Course(s): <br/><ul>";

                    for (var i = 0; i < selectedNames.length; i++) {
                        message += "<li>" + selectedNames[i] + "</li>";

                    }

                    message += "</ul>";
                    message += "Are you Sure?";

                    //check if url exisits...
                    var urlExisits = UrlExists(url);

                    BootstrapDialog.confirm(message, function (result) {
                        if (result) {
                            if (urlExisits) {
                                
                                ApproveExpCourses(selectedCourses);
                            } else
                                alert();
                                window.history.go(-1);

                        } else {

                        }
                    });


                }

            });
        }

     //Approving Semester II Exemption Requests
        if ($('.ApproveExpCourseSemTwo').length) {
            var regAppExpTwoTable = $('.coursesRegTableTwo').DataTable();
            $(".ApproveExpCourseSemTwo").click(function () {
                var selectedCourses = [];
                var selectedNames = [];
                var regErrorMsge = "";
                var regErrorTitle = "";

                var rowcollection = regAppExpTwoTable.$(".courseSelectTwo:checked", { "page": "all" }).closest('tr');
                rowcollection.each(function (index, row) {
                    var checkboxValues = $(row).find(".courseSelectTwo:checked").eq(0).val();
                    var checkboxCourses = $(row).find("td").eq(2).text().trim();
                    selectedCourses.push(checkboxValues);
                    selectedNames.push(checkboxCourses);
                });

                if (selectedCourses.length == 0) {
                    regErrorMsge = "Please select Courses(s) to Exempt";
                    regErrorTitle = "No Course Selected";
                    AlertMessageDialog(regErrorMsge, regErrorTitle);
                } else {
                    var url = $(location).attr('href');
                    console.log("Url: " + url);

                    var message = "You are about to Exempt the Following Course(s): <br/><ul>";

                    for (var i = 0; i < selectedNames.length; i++) {
                        message += "<li>" + selectedNames[i] + "</li>";

                    }

                    message += "</ul>";
                    message += "Are you Sure?";

                    //check if url exisits...
                    var urlExisits = UrlExists(url);

                    BootstrapDialog.confirm(message, function (result) {
                        if (result) {
                            if (urlExisits) {
                                ApproveExpCourses(selectedCourses);
                            } else
                                window.history.go(-1);

                        } else {

                        }
                    });


                }

            });
        }
     
     //Rejecting Semester I Exemption Requests    
        if ($('.RejectExpCourseSemOne').length) {
            var regRejExpOneTable = $('.coursesRegTableOne').DataTable();
            $(".RejectExpCourseSemOne").click(function () {
                var selectedCourses = [];
                var selectedNames = [];
                var regErrorMsge = "";
                var regErrorTitle = "";

                var rowcollection = regRejExpOneTable.$(".courseSelectOne:checked", { "page": "all" }).closest('tr');
                rowcollection.each(function (index, row) {
                    var checkboxValues = $(row).find(".courseSelectOne:checked").eq(0).val();
                    var checkboxCourses = $(row).find("td").eq(2).text().trim();
                    selectedCourses.push(checkboxValues);
                    selectedNames.push(checkboxCourses);
                });

                if (selectedCourses.length == 0) {
                    regErrorMsge = "Please select Courses(s) to Reject Exempt";
                    regErrorTitle = "No Course Selected";
                    AlertMessageDialog(regErrorMsge, regErrorTitle);
                } else {
                    var url = $(location).attr('href');
                    console.log("Url: " + url);

                    var message = "You are about to Reject Exemption for the Following Course(s): <br/><ul>";

                    for (var i = 0; i < selectedNames.length; i++) {
                        message += "<li>" + selectedNames[i] + "</li>";

                    }

                    message += "</ul>";
                    message += "Are you Sure?";

                    //check if url exisits...
                    var urlExisits = UrlExists(url);

                    BootstrapDialog.confirm(message, function (result) {
                        if (result) {
                            if (urlExisits) {
                                RejectExpCourses(selectedCourses);
                            } else
                                window.history.go(-1);

                        } else {

                        }
                    });


                }

            });
        }

     //Rejecting Semester II Exemption Requests
        if ($('.RejectExpCourseSemTwo').length) {
            var regRejExpTwoTable = $('.coursesRegTableTwo').DataTable();
            $(".RejectExpCourseSemTwo").click(function () {
                var selectedCourses = [];
                var selectedNames = [];
                var regErrorMsge = "";
                var regErrorTitle = "";

                var rowcollection = regRejExpTwoTable.$(".courseSelectTwo:checked", { "page": "all" }).closest('tr');
                rowcollection.each(function (index, row) {
                    var checkboxValues = $(row).find(".courseSelectTwo:checked").eq(0).val();
                    var checkboxCourses = $(row).find("td").eq(2).text().trim();
                    selectedCourses.push(checkboxValues);
                    selectedNames.push(checkboxCourses);
                    
                });

                if (selectedCourses.length == 0) {
                    regErrorMsge = "Please select Courses(s) to Reject Exemption";
                    regErrorTitle = "No Course Selected";
                    AlertMessageDialog(regErrorMsge, regErrorTitle);
                } else {
                    var url = $(location).attr('href');
                    console.log("Url: " + url);

                    var message = "You are about to Reject Exemption for the Following Course(s): <br/><ul>";

                    for (var i = 0; i < selectedNames.length; i++) {
                        message += "<li>" + selectedNames[i] + "</li>";

                    }

                    message += "</ul>";
                    message += "Are you Sure?";

                    //check if url exisits...
                    var urlExisits = UrlExists(url);

                    BootstrapDialog.confirm(message, function (result) {
                        if (result) {
                            if (urlExisits) {
                                RejectExpCourses(selectedCourses);
                            } else
                                window.history.go(-1);

                        } else {

                        }
                    });


                }

            });
        }
     
     //Approving Semester I CT Requests   
        if ($('.ApproveCTCourseSemOne').length) {
            var regCTTableOne = $('.coursesRegTableOne').DataTable();
            $(".ApproveCTCourseSemOne").click(function () {
                var selectedCourses = [];
                var selectedNames = [];
                var regErrorMsge = "";
                var regErrorTitle = "";

                var rowcollection = regCTTableOne.$(".courseSelectOne:checked", { "page": "all" }).closest('tr');
                rowcollection.each(function (index, row) {
                    var checkboxValues = $(row).find(".courseSelectOne:checked").eq(0).val();
                    var checkboxCourses = $(row).find("td").eq(2).text().trim();
                    selectedCourses.push(checkboxValues);
                    selectedNames.push(checkboxCourses);
                    });
                
                if (selectedCourses.length == 0) {
                    regErrorMsge = "Please select Courses(s) to Approve Credit Transfer for";
                    regErrorTitle = "No Course Selected";
                    AlertMessageDialog(regErrorMsge, regErrorTitle);
                } else {
                    var url = $(location).attr('href');
                    console.log("Url: " + url);

                    var message = "You are about to Approve Credit Transfer for the Following Course(s): <br/><ul>";

                    for (var i = 0; i < selectedNames.length; i++) {
                        message += "<li>" + selectedNames[i] + "</li>";

                    }

                    message += "</ul>";
                    message += "Are you Sure?";

                    //check if url exisits...
                    var urlExisits = UrlExists(url);

                    BootstrapDialog.confirm(message, function (result) {
                        if (result) {
                            if (urlExisits) {
                                ApproveCTCourses(selectedCourses);
                            } else
                                window.history.go(-1);

                        } else {

                        }
                    });


                }

            });
        }

     //Approving Semester II CT Requests
        if ($('.ApproveCTCourseSemTwo').length) {
            var regCTTableTwo = $('.coursesRegTableTwo').DataTable();
            $(".ApproveCTCourseSemTwo").click(function () {
                var selectedCourses = [];
                var selectedNames = [];
                var regErrorMsge = "";
                var regErrorTitle = "";

                var rowcollection = regCTTableTwo.$(".courseSelectTwo:checked", { "page": "all" }).closest('tr');
                rowcollection.each(function (index, row) {
                    var checkboxValues = $(row).find(".courseSelectTwo:checked").eq(0).val();
                    var checkboxCourses = $(row).find("td").eq(2).text().trim();
                    selectedCourses.push(checkboxValues);
                    selectedNames.push(checkboxCourses);
                });

                if (selectedCourses.length == 0) {
                    regErrorMsge = "Please select Courses(s) to Approve Credit Transfer for";
                    regErrorTitle = "No Course Selected";
                    AlertMessageDialog(regErrorMsge, regErrorTitle);
                } else {
                    var url = $(location).attr('href');
                    console.log("Url: " + url);

                    var message = "You are about to Approve Credit Transfer for the Following Course(s): <br/><ul>";

                    for (var i = 0; i < selectedNames.length; i++) {
                        message += "<li>" + selectedNames[i] + "</li>";

                    }

                    message += "</ul>";
                    message += "Are you Sure?";

                    //check if url exisits...
                    var urlExisits = UrlExists(url);

                    BootstrapDialog.confirm(message, function (result) {
                        if (result) {
                            if (urlExisits) {
                                ApproveCTCourses(selectedCourses);
                            } else
                                window.history.go(-1);

                        } else {

                        }
                    });


                }

            });
        }
     
     //Rejecting Semester I CT Requests   
        if ($('.RejectCTCourseSemOne').length) {
            var regCTTRejableOne = $('.coursesRegTableOne').DataTable();
            $(".RejectCTCourseSemOne").click(function () {
                var selectedCourses = [];
                var selectedNames = [];
                var regErrorMsge = "";
                var regErrorTitle = "";

                var rowcollection = regCTTRejableOne.$(".courseSelectOne:checked", { "page": "all" }).closest('tr');
                rowcollection.each(function (index, row) {
                    var checkboxValues = $(row).find(".courseSelectOne:checked").eq(0).val();
                    var checkboxCourses = $(row).find("td").eq(2).text().trim();
                    selectedCourses.push(checkboxValues);
                    selectedNames.push(checkboxCourses);
                });

                if (selectedCourses.length == 0) {
                    regErrorMsge = "Please select Courses(s) to Reject Credit Transfer for";
                    regErrorTitle = "No Course Selected";
                    AlertMessageDialog(regErrorMsge, regErrorTitle);
                } else {
                    var url = $(location).attr('href');
                    console.log("Url: " + url);

                    var message = "You are about to reject Credit Transfer for the following Course(s): <br/><ul>";

                    for (var i = 0; i < selectedNames.length; i++) {
                        message += "<li>" + selectedNames[i] + "</li>";

                    }

                    message += "</ul>";
                    message += "Are you Sure?";

                    //check if url exisits...
                    var urlExisits = UrlExists(url);

                    BootstrapDialog.confirm(message, function (result) {
                        if (result) {
                            if (urlExisits) {
                                RejectCTCourses(selectedCourses);
                            } else
                                window.history.go(-1);

                        } else {

                        }
                    });


                }

            });
        }

     //Approving Semester II CT Requests
        if ($('.RejectCTCourseSemTwo').length) {
            var regCTRejTableTwo = $('.coursesRegTableTwo').DataTable();
            $(".RejectCTCourseSemTwo").click(function () {
                var selectedCourses = [];
                var selectedNames = [];
                var regErrorMsge = "";
                var regErrorTitle = "";

                var rowcollection = regCTRejTableTwo.$(".courseSelectTwo:checked", { "page": "all" }).closest('tr');
                rowcollection.each(function (index, row) {
                    var checkboxValues = $(row).find(".courseSelectTwo:checked").eq(0).val();
                    var checkboxCourses = $(row).find("td").eq(2).text().trim();
                    selectedCourses.push(checkboxValues);
                    selectedNames.push(checkboxCourses);
                });

                if (selectedCourses.length == 0) {
                    regErrorMsge = "Please select Courses(s) to Reject Credit Transfer for";
                    regErrorTitle = "No Course Selected";
                    AlertMessageDialog(regErrorMsge, regErrorTitle);
                } else {
                    var url = $(location).attr('href');
                    console.log("Url: " + url);

                    var message = "You are about to regCTRejTableTwo Credit Transfer for the Following Course(s): <br/><ul>";

                    for (var i = 0; i < selectedNames.length; i++) {
                        message += "<li>" + selectedNames[i] + "</li>";

                    }

                    message += "</ul>";
                    message += "Are you Sure?";

                    //check if url exisits...
                    var urlExisits = UrlExists(url);

                    BootstrapDialog.confirm(message, function (result) {
                        if (result) {
                            if (urlExisits) {
                                RejectCTCourses(selectedCourses);
                            } else
                                window.history.go(-1);

                        } else {

                        }
                    });


                }

            });
        }

     
     });

 function ms_getCourseFilterdata() {
     var out = "";
     var drp1 = $("select#drpdwn-campus").val();
     var drp2 = $("select#drpdwn-faculty").val();
     var drp3 = $("select#drpdwn-department").val();
     var drp4 = $("select#drpdwn-program").val();
     var drp5 = $("select#drpdwn-specialisation").val();
     out = out + type + "," + drp1 + "," + drp2 + "," + drp3 + "," + drp4 + "," + drp5;

     return out;
 }

function CreditSummationLabel(creditSum) {
    //passing them to the label...
    $("#CreditStatus").html("Total Credit Summation: " + creditSum);

}

function warmonExtraCreditUnits(credit, id) {
    if ($(".course_checked_" + id).is(":checked")) {
        if (creditSum >= maximumSemLoad || (creditSum + credit) > maximumSemLoad) {
            var message = "You have reached the maximum load";
            var title = "Maximum Semester Load";
            AlertMessageDialog(message, title);
            $(".course_checked_" + id).prop('checked', false);
        } else {
            creditSum += credit;
        }
    } else {
        creditSum -= credit;
    }
    //creditSum += credit;
    $("#CreditStatus").html("Total Credit Summation: " + creditSum);
}

function addCreditUnitsSum(credit, id) {

        if ($(".course_checked_" + id).is(":checked")) {
            creditSum += credit;
        } else {
            creditSum -= credit;
        }
        //creditSum += credit;
        $("#CreditStatus").html("Total Credit Summation: " + creditSum);
    
}

function DeleteRegisteredCourses(courseIds) {
    var ids = JSON.stringify(courseIds);
    $.ajax({
        url: baseUrl + "api/UtilsApi/DeleteRegCourses/" + ids,
        type: "POST",
        contentType: "application/json",
        dataType: "JSON",
        timeout: 10000,
        success: function (data) {
            //window.location.href = baseUrl + 'Registration/OperationSuccessful/';
            DeleteSuccess();
        }
    });

} 

function ExemptRegisteredCourses(courseIds) {
    var ids = JSON.stringify(courseIds);
    $.ajax({
        url: baseUrl + "api/UtilsApi/ExemptRegCourses/" + ids,
        type: "POST",
        contentType: "application/json",
        dataType: "JSON",
        timeout: 10000,
        success: function (data) {
            //window.location.href = baseUrl + 'Registration/OperationSuccessful/';
            DeleteSuccess();
        }
    });

} 

function AbsRegisteredCourses(courseIds) {
    var ids = JSON.stringify(courseIds);
    $.ajax({
        url: baseUrl + "api/UtilsApi/AbsRegCourses/" + ids,
        type: "POST",
        contentType: "application/json",
        dataType: "JSON",
        timeout: 10000,
        success: function (data) {
            //window.location.href = baseUrl + 'Registration/OperationSuccessful/';
            DeleteSuccess();
        }
    });

}

function AuditRegisteredCourses(courseIds) {
    var ids = JSON.stringify(courseIds);
    $.ajax({
        url: baseUrl + "api/UtilsApi/AuditRegCourses/" + ids,
        type: "POST",
        contentType: "application/json",
        dataType: "JSON",
        timeout: 10000,
        success: function (data) {
            //window.location.href = baseUrl + 'Registration/OperationSuccessful/';
            DeleteSuccess();
        }
    });

}

function DeleteSuccess() {
   console.log("Operation Successful");
   
    //show success...
   $('#dv-delete-course-success').removeClass('hide');
   
   location.reload(); 
}
function ConfigureSemesterRegistration() {
    var loadUrl = baseUrl + "Registration/ConfigureRegistration/";
    
    BootstrapDialog.show({
        title: "Configure Semester Registration",
        message: $('<div class="form-horizontal"></div>').load(loadUrl),

    });
   
}

function deleteSemesterReg(semesterRegistrationId) {
    var url = $(location).attr('href');
    console.log("Url: " + url);

    //check if url exisits...
    var urlExisits = UrlExists(url);

    BootstrapDialog.confirm('You are about to Reject a Semester Registration and Delete all the Courses for the Semester, Are you sure?', function (result) {
        if (result) {
            if (urlExisits) {
                
                //var ids = JSON.stringify(semesterRegistrationId);
                $.ajax({
                    url: baseUrl + "api/UtilsApi/DeleteSemesterRegistration/" + semesterRegistrationId,
                    type: "POST",
                    contentType: "application/json",
                    dataType: "JSON",
                    timeout: 10000,
                    success: function (data) {
                        //window.location.href = baseUrl + 'Registration/OperationSuccessful/';
                        DeleteSuccess();
                    }
                });
              
            } else
                window.history.go(-1);

        } else {

        }
    });

} 

function AddSemesterCourse(semesterRegistrationId) {
    
    //var loadingSymbol = "";
    //loadingSymbol += "<div id=\"loadingCourses\" class=\"\">";
    //loadingSymbol += "                        <p class=\"alert alert-success well-sm\">";
    //loadingSymbol += "                            <button data-dismiss=\"alert\" class=\"close\" type=\"button\">×<\/button>";
    //loadingSymbol += "                            <span>";
    //loadingSymbol += "                                <span class=\"fa fa-spin fa-spinner fa-2x\"><\/span>&nbsp; &nbsp; &nbsp;Loading Content, kindly wait ...";
    //loadingSymbol += "                            <\/span>";
    //loadingSymbol += "                        <\/p>";
    //loadingSymbol += "    <\/div>";
    
    var loadUrl = baseUrl + "Registration/AddCourse/" + semesterRegistrationId;
    
    BootstrapDialog.show({
        spinsautospin: true,
        title: "Add Course(s)",
        message: $('<input type="hidden" value= ' + semesterRegistrationId + ' id="semRegId" /><div class="form-horizontal"></div>').load(loadUrl),
        //onshow: function (dialogRef) {
        //    alert('Dialog is popping up, its message is ' + dialogRef.getMessage());
        //},
        onshown: function (dialogRef) {
            $('#loadingAjax').spins();
        }

    });
    
    
}

function approveSemesterReg(semesterRegistrationId) {
    var url = $(location).attr('href');
    console.log("Url: " + url);

    //check if url exisits...
    var urlExisits = UrlExists(url);

    BootstrapDialog.confirm('You are about to Approve a Semester Registration, Are you sure?', function (result) {
        if (result) {
            if (urlExisits) {

                //var ids = JSON.stringify(semesterRegistrationId);
                $.ajax({
                    url: baseUrl + "api/UtilsApi/ApproveSemesterRegistration/" + semesterRegistrationId,
                    type: "POST",
                    contentType: "application/json",
                    dataType: "JSON",
                    timeout: 10000,
                    success: function (data) {
                        //window.location.href = baseUrl + 'Registration/OperationSuccessful/';
                        DeleteSuccess();
                    }
                });

            } else
                window.history.go(-1);

        } else {

        }
    });

}

function ApproveExpCourses(courseIds) {
    var ids = JSON.stringify(courseIds);
    $.ajax({
        url: baseUrl + "api/UtilsApi/ApproveExpCourses/" + ids,
        type: "POST",
        contentType: "application/json",
        dataType: "JSON",
        timeout: 10000,
        success: function (data) {
            //window.location.href = baseUrl + 'Registration/OperationSuccessful/';
            DeleteSuccess();
        }
    });

}

function RejectExpCourses(courseIds) {
    var ids = JSON.stringify(courseIds);
    $.ajax({
        url: baseUrl + "api/UtilsApi/RejectExpCourses/" + ids,
        type: "POST",
        contentType: "application/json",
        dataType: "JSON",
        timeout: 10000,
        success: function (data) {
            //window.location.href = baseUrl + 'Registration/OperationSuccessful/';
            DeleteSuccess();
        }
    });

}

function AddSemesterExpCourse(semesterRegistrationId) {
    var loadUrl = baseUrl + "Registration/AddExpCourse/" + semesterRegistrationId;

    BootstrapDialog.show({
        title: "Add Course(s) to Exempt",
        message: $('<input type="hidden" value= ' + semesterRegistrationId + ' id="semRegId" /><div class="form-horizontal"></div>').load(loadUrl),

    });
}

function ApproveCTCourses(courseIds) {
    
    var ids = JSON.stringify(courseIds);

    var loadUrl = baseUrl + "Registration/ApproveCTCourses/" + ids;

    BootstrapDialog.show({
        spinsautospin: true,
        title: "Add Credit Transfer Results",
        message: $('<div class="form-horizontal"></div>').load(loadUrl),
        //onshow: function (dialogRef) {
        //    alert('Dialog is popping up, its message is ' + dialogRef.getMessage());
        //},
        //onshown: function (dialogRef) {
        //    $('#loadingAjax').spins();
        //}

    });
    
   //$.ajax({
   //    url: baseUrl + "Registration/ApproveCTCourses/" + ids,
   //     type: "POST",
   //     contentType: "application/json",
   //     dataType: "JSON",
   //     timeout: 10000,
   //     success: function (data) {
   //         //window.location.href = baseUrl + 'Registration/OperationSuccessful/';
   //         DeleteSuccess();
   //     }
   // });

} 

function RejectCTCourses(courseIds) {
    var ids = JSON.stringify(courseIds);
    $.ajax({
        url: baseUrl + "api/UtilsApi/RejectCTCourses/" + ids,
        type: "POST",
        contentType: "application/json",
        dataType: "JSON",
        timeout: 10000,
        success: function (data) {
            //window.location.href = baseUrl + 'Registration/OperationSuccessful/';
            DeleteSuccess();
        }
    });

}

function AddSemesterCTCourse(semesterRegistrationId) {
    var loadUrl = baseUrl + "Registration/AddCtCourse/" + semesterRegistrationId;

    BootstrapDialog.show({
        title: "Add Credit Transfer Course(s)",
        message: $('<input type="hidden" value= ' + semesterRegistrationId + ' id="semRegId" /><div class="form-horizontal"></div>').load(loadUrl),

    });
}


