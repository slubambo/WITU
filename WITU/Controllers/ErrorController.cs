using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WITU.Utils;

namespace WITU.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Unauthorised()
        {
            TempData[ApplicationConstants.ErrorNotification] = "You are not authorized to perform the requested action";
            return View();
        }
        public ActionResult PageNotFound()
        {
            return View();
        }
	}
}