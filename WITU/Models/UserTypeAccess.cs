using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WITU.Core.Model;
using WITU.Utils;

namespace WITU.Models
{
    public class UserTypeAccess : ActionFilterAttribute
    {
        public UserTypes UserType { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //ensuring that the current implementation is already done...
            base.OnActionExecuting(filterContext);

            //Checking whether User is Logged in
            bool isAuthorized = filterContext.HttpContext.Request.IsAuthenticated;

            IEnumerable<FilterAttribute> filterAttribute = filterContext.ActionDescriptor.GetFilterAttributes(true)
                .Where(a => a.GetType() ==
                            typeof(UserTypeAccess));

            foreach (UserTypeAccess attr in filterAttribute)
            {
                UserType = attr.UserType;
            }

            var User = filterContext.HttpContext.User;

            var loggedInUserType = User.ApplicationUserType();

            if (loggedInUserType != (decimal) UserType)
            {
                //Redirect
                Redirect(filterContext);
            }
     
        }

        protected void Redirect(ActionExecutingContext filterContext)
        {
            //base.HandleUnauthorizedRequest(filterContext);
            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary(
                    new
                    {
                        controller = "Error",
                        action = "Unauthorised"
                    })
                );
        }
    }
}