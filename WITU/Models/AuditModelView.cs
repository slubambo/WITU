using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml.Linq;
using WITU.Core.Model;
using WITU.Core.Repositories.Impl;
using WITU.Core.Repositories.Interfaces;
using WITU.UserManagement.Identity;
using WITU.Utils;
using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Impl;
using Ninject;



namespace WITU.Models
{

    public class AuditTrail
    {

        //A new SessionId that will be used to link an entire users "Session" of Audit Logs
        //together to help identifer patterns involving erratic behavior
        public string Message { get; set; }

        public string UserType { get; set; }
        public string IpAddress { get; set; }
        public string UserName { get; set; }
        public string UrlAccessed { get; set; }
        public DateTime TimeAccessed { get; set; }


        //A new Data property that is going to store JSON string objects that will later be able to
        //be deserialized into objects if necessary to view details about a Request
        public string Data { get; set; }

        //public AuditTrail()
        //{
        //}

    }

    public class AuditViewList
    {
        public IList<AuditTrail> AuditRecords { get; set; }
        public IList<AuditTrail> StudentTrail { get; set; }
        public IList<AuditTrail> StaffTrail { get; set; }
    }

    public enum AuditLevel
    {
        View = 0,
        AddOrEdit = 1,
        Delete = 2,
        Advanced = 3,
        Printed = 4
    }

    public class AuditAttribute : ActionFilterAttribute
    {

        //Our value to handle our AuditingLevel
        public AuditLevel AuditingLevel { get; set; }

        [Inject]
        public ICoreRepository _CoreRepository { get; set; }

        //[Inject]
        //public ISession _Session { get; set; }

       
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            //Stores the Request in an Accessible object
            var request = filterContext.HttpContext.Request;

            IEnumerable<FilterAttribute> filterAttribute = filterContext.ActionDescriptor.GetFilterAttributes(true)
               .Where(a => a.GetType() ==
                           typeof(AuditAttribute));

            foreach (AuditAttribute attr in filterAttribute)
            {
                AuditingLevel = attr.AuditingLevel;
            }

            //Generate the appropriate key based on the user's Authentication Cookie
            //This is overkill as you should be able to use the Authorization Key from
            //Forms Authentication to handle this. 
            //r sessionIdentifier = FormsAuthentication.FormsCookieName;

            //Generate an audit

            var auditDetails = new Core.Model.Audit
            {
                Message = GenerateMessage(request),
                UserType = (request.IsAuthenticated) ? filterContext.HttpContext.User.ApplicationUserType().ToString() : "Anonymous",
                IpAddress = request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? request.UserHostAddress,
                UrlAccessed = request.Url != null ? request.Url.OriginalString :" ",
                TimeAccessed = DateTime.UtcNow,
                UserName = (request.IsAuthenticated) ? filterContext.HttpContext.User.Identity.Name : "Anonymous",
                Data = SerializeRequest(request)
            };


            //Stores the Audit in the Database
            _CoreRepository.SaveOrUpdate(auditDetails);

            //this.OnActionExecuting(filterContext);

            //_Session.BeginTransaction();

        }

        //This will serialize the Request object based on the level that you determine
        private string SerializeRequest(HttpRequestBase request)
        {
            switch (AuditingLevel)
            {
                //No Request Data will be serialized
                case 0:
                default:
                    return "";
                //Basic Request Serialization - just stores Data
                case AuditLevel.AddOrEdit:
                    return Json.Encode(new { request.Cookies, request.Headers, request.Files });
                case AuditLevel.Printed:
                    return Json.Encode(new { request.Cookies, request.Headers, request.Files });
                //Middle Level - Customize to your Preferences
                case AuditLevel.Delete:
                    return Json.Encode(new { request.Cookies, request.Headers, request.Files, request.Form, request.QueryString, request.Params });
                //Highest Level - Serialize the entire Request object
                case AuditLevel.Advanced:
                    //We can't simply just Encode the entire request string due to circular references as well
                    //as objects that cannot "simply" be serialized such as Streams, References etc.
                    //return Json.Encode(request);
                    return Json.Encode(new { request.Cookies, request.Headers, request.Files, request.Form, request.QueryString, request.Params });
            }
        }


        private string GenerateMessage(HttpRequestBase request)
        {
            switch (AuditingLevel)
            {
                //Message for Accessing a URL
                case 0:
                default:
                    return "Accessed Information";
                //Message for Adding or Editing Data
                case AuditLevel.AddOrEdit:
                    return "Added or Edited Data";
                //Middle Level - Customize to your Preferences
                case AuditLevel.Delete:
                    return "Deleted Data";
                case AuditLevel.Printed:
                    return "Printed Information";
                //Highest Level - Serialize the entire Request object
                case AuditLevel.Advanced:
                    //Message for High Level Actions

                    return "Performed this Action which is regarded High Level";
            }
        }


    }
}