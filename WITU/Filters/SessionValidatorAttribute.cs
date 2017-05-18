using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WITU.Controllers;
using WITU.Utils;

namespace WITU.Filters
{
    public enum RequiredSessionVariable
    {
        AcademicBoardSession,
        ImportProcessorSession,
        ImportReturnsProcessorSession,
        RegistrationSession,
        ImportStudentsProcessorSession,
        PrintFinancialReportsSession,
        //ImportStudentsProcessorSession,
        ImportSpecialisationProcessorSession,
        InteractiveRegistrationSession,
        StaffRegistrationSession,
        PurgeUploadsSession,
        BatchedRegistrationProcessorSession,
        LoggedInUserRoles,
        PublishResultsSession,
        AddGradStudentsSession
    }
    /// <summary>
    /// Validates if a session that is required for the ActionResult is present.
    /// </summary>
    public class SessionValidatorAttribute : ActionFilterAttribute
    {
        public RequiredSessionVariable RequiredSessionVariable { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (RequiredSessionVariable == null)
            {
                throw new ArgumentNullException(paramName: "RequiredSessionVariable", message: "Please Specify the type of Session component required.");
            }

            if (filterContext.HttpContext.Session != null)
            {
                var sessionComponent = filterContext.HttpContext.Session[RequiredSessionVariable.ToString()];
                if (sessionComponent != null)
                {
                    base.OnActionExecuting(filterContext);
                    return;
                }
            }

            //redirect the user to go back home...
            filterContext.Controller.TempData.Add(ApplicationConstants.ErrorNotification, "Some components required for the process to be " +
                                                                                          "fulfilled are missing or have not been inserted.");
            var controller = (BaseController) filterContext.Controller;
            
            //redirecting to an appropriate action result, well after adding temp data...
            switch (RequiredSessionVariable)
            {
                case RequiredSessionVariable.ImportProcessorSession:
                    filterContext.Result = controller.RedirectToAction("Import", "Result");
                    break;
                case RequiredSessionVariable.AcademicBoardSession:
                    filterContext.Result = controller.RedirectToAction("AcademicBoard", "Result");
                    break;
                case RequiredSessionVariable.ImportReturnsProcessorSession:
                    filterContext.Result = controller.RedirectToAction("Returns", "Financial");
                    break;
                case RequiredSessionVariable.PrintFinancialReportsSession:
                    filterContext.Result = controller.RedirectToAction("Overview", "Financial");
                    break;
                case RequiredSessionVariable.RegistrationSession:
                    filterContext.Result = controller.RedirectToAction("Report", "Registration");
                    break;
                case RequiredSessionVariable.ImportStudentsProcessorSession:
                    filterContext.Result = controller.RedirectToAction("BatchedEnrollment", "Student");
                    break;
                case RequiredSessionVariable.ImportSpecialisationProcessorSession:
                    filterContext.Result = controller.RedirectToAction("BatchedManageSpecialisation", "Student");
                    break;
                case RequiredSessionVariable.InteractiveRegistrationSession:
                    filterContext.Result = controller.RedirectToAction("Approval", "Registration");
                    break;
                case RequiredSessionVariable.PurgeUploadsSession:
                    filterContext.Result = controller.RedirectToAction("PurgeUploads", "Result");
                    break;
                case RequiredSessionVariable.StaffRegistrationSession:
                    filterContext.Result = controller.RedirectToAction("NullSession", "Registration");
                    break;
                case RequiredSessionVariable.BatchedRegistrationProcessorSession:
                    filterContext.Result = controller.RedirectToAction("NullSession", "Registration");
                    break;

                case RequiredSessionVariable.LoggedInUserRoles:
                    filterContext.Result = controller.RedirectToAction("Login", "Account");
                    break;
                case RequiredSessionVariable.PublishResultsSession:
                    filterContext.Result = controller.RedirectToAction("PublishResults", "Result");
                    break;
                case RequiredSessionVariable.AddGradStudentsSession:
                    filterContext.Result = controller.RedirectToAction("Index", "Graduation");
                    break;
            }

        } 
    }
}