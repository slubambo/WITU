using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using WITU.Core.Model;
using WITU.Core.Repositories.Interfaces;
using WITU.Filters;
using WITU.Models.UserManagement;
using Microsoft.AspNet.Identity;
using Ninject;

namespace WITU.Models
{
    public class RoleBasedAndLevelAccess
    {
    }

    public class AuthorizeUserAttribute : ActionFilterAttribute
    {
        [Inject]
        public IUserManagementRepository UserManagementRepository { get; set; }

        [Inject]
        public IStaffRepository StaffRepository { get; set; }

        public AccessLevel AccessLevel { get; set; }

        public int Service { get; set; }

        public int Permission { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //ensuring that the current implementation is already done...
            base.OnActionExecuting(filterContext);

            //Checking whether User is Logged in
            bool isAuthorized = filterContext.HttpContext.Request.IsAuthenticated;
            bool Continue = false;

            IEnumerable<FilterAttribute> filterAttribute = filterContext.ActionDescriptor.GetFilterAttributes(true)
                .Where(a => a.GetType() ==
                            typeof (AuthorizeUserAttribute));

            foreach (AuthorizeUserAttribute attr in filterAttribute)
            {
                Permission = attr.Permission;
               // AccessLevel = attr.AccessLevel;
                Service = attr.Service;
            }

            if (!isAuthorized)
            {
                //Redirect
                Redirect(filterContext);
            }

            //List of Roles of a given User

            var userPermissionDetails =
                (SessionUserManagementDetails)filterContext.HttpContext.Session[RequiredSessionVariable.LoggedInUserRoles.ToString()];
             

            List<UserRole> loggedInUserRoles =
                userPermissionDetails.LoggedInUserRoles;

            //Retrieving User Previledge Level
            //Staff staff =
            //    StaffRepository.GetStaffByUserId(int.Parse(filterContext.HttpContext.User.Identity.GetUserId()));

            Instructor instructor =
                userPermissionDetails.Instructor;

            //Initially used to check levels, revised to be ommited
            if (instructor != null)
            {
                Continue = true;
            }
            
            if (isAuthorized && Continue == true)
            {
                if (loggedInUserRoles != null && ((Service) != null) && Permission != null)
                
                {
                    if (loggedInUserRoles.Any(role => role.Role.Service.Id == Service))
                    {
                        //Permission == CanRead
                        if (Permission == 1)
                        {
                            UserRole specificRole =
                                loggedInUserRoles.FirstOrDefault(role => role.Role.Service.Id == Service);

                            if (specificRole != null)
                            {
                                if (
                                    specificRole.Role.CanRead || specificRole.Role.CanUpdate ||
                                    specificRole.Role.CanCreate || specificRole.Role.CanDelete)
                                {
                                    Continue = true;
                                }
                                else
                                {
                                    //Redirect
                                    Continue = false;
                                    Redirect(filterContext);
                                }
                            }
                            else
                            {
                                //Redirect
                                Continue = false;
                                Redirect(filterContext);
                            }

                        }

                        //Permission == CanCreate
                        if (Permission == 2)
                        {
                            UserRole specificRole =
                                loggedInUserRoles.FirstOrDefault(role => role.Role.Service.Id == Service);
                            if (
                                specificRole != null && specificRole
                                    .Role.CanCreate)
                            {
                                Continue = true;
                            }
                            else
                            {
                                //Redirect
                                Continue = false;
                                Redirect(filterContext);
                            }
                        }

                        //Permission == CanEdit
                        if (Permission == 3)
                        {
                            UserRole specificRole =
                                loggedInUserRoles.FirstOrDefault(role => role.Role.Service.Id == Service);
                            if (
                                specificRole != null && specificRole
                                    .Role.CanUpdate)
                            {
                                Continue = true;
                            }
                            else
                            {
                                //Redirect
                                Continue = false;
                                Redirect(filterContext);
                            }
                        }

                        //Permission == CanDelete
                        if (Permission == 4)
                        {
                            UserRole specificRole =
                                loggedInUserRoles.FirstOrDefault(role => role.Role.Service.Id == Service);
                            if (
                                specificRole != null && specificRole
                                    .Role.CanDelete)
                            {
                                Continue = true;
                            }
                            else
                            {
                                //Redirect
                                Continue = false;
                                Redirect(filterContext);
                            }
                        }

                        //Permission == Can Add and Edit
                        if (Permission == 5)
                        {
                            UserRole specificRole =
                                loggedInUserRoles.FirstOrDefault(role => role.Role.Service.Id == Service);
                            if (
                                specificRole != null && specificRole
                                    .Role.CanCreate && specificRole
                                    .Role.CanUpdate)
                            {
                                Continue = true;
                            }
                            else
                            {
                                //Redirect
                                Continue = false;
                                Redirect(filterContext);
                            }
                        }
                    }
                    else
                    {
                        //Redirect
                        Continue = false;
                        Redirect(filterContext);
                    }
                }
                else
                {
                    Continue = false;
                    Redirect(filterContext);
                }
            }


            if (Continue == false)
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