using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WITU.Core.Model;
using WITU.Core.Repositories.Impl;
using WITU.Core.Repositories.Interfaces;
using WITU.Filters;
using WITU.Models;
using WITU.Models.UserManagement;
using WITU.UserManagement;
using WITU.UserManagement.Identity;
using WITU.UserManagement.Store;
using WITU.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace WITU.Controllers
{
    [RequireHttps]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly ICoreRepository _coreRepo;
        private readonly IAccountRepository _accountRepo;
        private readonly IStudentRepository _studentRepo;
        private readonly IUserManagementRepository _userManagementRepository;


        public ApplicationUserManager UserManager { get; private set; }

        public SessionUserManagementDetails SessionUserManagementDetails{ get; private set; }

        public AccountController(ICoreRepository coreRepo, IAccountRepository accountRepo, IStudentRepository studentRepo, IUserManagementRepository userManagementRepository)
            : this(new ApplicationUserManager(new UserStore(accountRepo, coreRepo)))
        {
            _coreRepo = coreRepo;
            _accountRepo = accountRepo;
            _studentRepo = studentRepo;
            _userManagementRepository = userManagementRepository;
        }

        public AccountController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
            UserManager.UserValidator = new UserValidator<IdentityUser, int>(UserManager)
            {
                AllowOnlyAlphanumericUserNames = false
            };
        }


        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            //Preventing loading login page if use ris already logged in
            string userId = User.Identity.GetUserId();
            if (userId != null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await UserManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    Session[RequiredSessionVariable.LoggedInUserRoles.ToString()] = null;
                    if (!user.IsLockedOut)
                    {
                        await SignInAsync(user, model.RememberMe);

                        if (user.UserType == (decimal) UserTypes.Instructor)
                        {
                            var roles = _accountRepo.UserRoles(user.Id);
                            var instructor = _accountRepo.GetUserInstructor(user.Id);

                            var userPermissionDetails = new SessionUserManagementDetails
                            {
                                LoggedInUserRoles = roles,
                                Instructor = instructor,
                                HighestLevel =
                                    instructor.StaffPositions.OrderBy(x => x.LevelId).FirstOrDefault() != null
                                        ? (int)instructor.StaffPositions.OrderBy(x => x.LevelId).FirstOrDefault().LevelId
                                        : 0
                            };

                            Session[RequiredSessionVariable.LoggedInUserRoles.ToString()] = userPermissionDetails;

                        }


                        //taking the user to a given direction...
                        switch (user.UserType)
                        {
                            case (int) UserTypes.Instructor:
                                return string.IsNullOrEmpty(returnUrl)? RedirectToAction("Index", "Staff"): RedirectToLocal(returnUrl);

                            case (int) UserTypes.Student:
                                return string.IsNullOrEmpty(returnUrl)? user.IsFirstTimeUser
                                    ? RedirectToAction("FirstTimeUser", "Learner")
                                    : RedirectToAction("Index", "Learner") : RedirectToLocal(returnUrl);

                            case (int) UserTypes.ProspectiveStudent:
                                return string.IsNullOrEmpty(returnUrl)? RedirectToAction("Index", "ProspectiveStudent") : RedirectToLocal(returnUrl);

                            default:
                                return RedirectToLocal(returnUrl);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Your account is not active, please contact the system administrator.");
                        return View(model);
                    }

                }

                ModelState.AddModelError("", "Invalid username or password.");
            }

            if (!string.IsNullOrEmpty(returnUrl))
            {
                ViewBag.ReturnUrl = returnUrl;
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.UserName,
                    UserType = (int) UserTypes.ProspectiveStudent,
                    IsApproved = true,
                    IsLockedOut = false,
                    IsFirstTimeUser = false,
                    Email = model.UserName
                };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //from here we create prospective student who shall be attached to this user...
                    User coreUser = _accountRepo.GetUserByName(model.UserName);

                    var prospectiveStudent = new ProspectiveStudent
                    {
                        User = coreUser,
                        Person = new Person
                        {
                            Surname = model.Surname,
                            GivenName = model.FirstName,
                            EmailAddress = model.UserName
                        },
                        CreatedOn = DateTime.Now
                    };

                    _coreRepo.SaveOrUpdate(prospectiveStudent);

                    await SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/Disassociate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            ManageMessageId? message = null;
            IdentityResult result =
                await
                    UserManager.RemoveLoginAsync(int.Parse(User.Identity.GetUserId()),
                        new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("Manage", new {Message = message});
        }

        //
        // GET: /Account/Manage
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess
                    ? "Your password has been changed."
                    : message == ManageMessageId.SetPasswordSuccess
                        ? "Your password has been set."
                        : message == ManageMessageId.RemoveLoginSuccess
                            ? "The external login was removed."
                            : message == ManageMessageId.Error
                                ? "An error has occurred."
                                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result =
                        await
                            UserManager.ChangePasswordAsync(int.Parse(User.Identity.GetUserId()), model.OldPassword,
                                model.NewPassword);
                    if (result.Succeeded)
                    {
                        TempData[ApplicationConstants.SuccessNotification] = ApplicationConstants.SuccessSaveMessage;
                        return RedirectToAction("Manage");
                    }
                    AddErrors(result);
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    IdentityResult result =
                        await UserManager.AddPasswordAsync(int.Parse(User.Identity.GetUserId()), model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new {Message = ManageMessageId.SetPasswordSuccess});
                    }
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider,
                Url.Action("ExternalLoginCallback", "Account", new {ReturnUrl = returnUrl}));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            ExternalLoginInfo loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            IdentityUser user = await UserManager.FindAsync(loginInfo.Login);
            if (user != null)
            {
                await SignInAsync(user, false);
                return RedirectToLocal(returnUrl);
            }
            // If the user does not have an account, then prompt the user to create an account
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
            return View("ExternalLoginConfirmation",
                new ExternalLoginConfirmationViewModel {UserName = loginInfo.DefaultUserName});
        }

        //
        // POST: /Account/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }

        //
        // GET: /Account/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            ExternalLoginInfo loginInfo =
                await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("Manage", new {Message = ManageMessageId.Error});
            }
            IdentityResult result =
                await UserManager.AddLoginAsync(int.Parse(User.Identity.GetUserId()), loginInfo.Login);
            if (result.Succeeded)
            {
                return RedirectToAction("Manage");
            }
            return RedirectToAction("Manage", new {Message = ManageMessageId.Error});
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model,
            string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                ExternalLoginInfo info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new IdentityUser {UserName = model.UserName};
                IdentityResult result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInAsync(user, false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session[RequiredSessionVariable.LoggedInUserRoles.ToString()] = null;
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            IList<UserLoginInfo> linkedAccounts = UserManager.GetLogins(int.Parse(User.Identity.GetUserId()));
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return PartialView("_RemoveAccountPartial", linkedAccounts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

        #region Helpers

        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        private async Task SignInAsync(IdentityUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            ClaimsIdentity identity =
                await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

            identity.AddClaim(new Claim(ApplicationConstants.UserTypeClaim, user.UserType.ToString()));

            if (user.ProfilePhotoName == null)
                user.ProfilePhotoName = "";

            bool exist = false;
            string photoPath = null;
            string existingProfilePhoto = null;
            string avartarUrl = ApplicationConstants.ProfilesUrl + ApplicationConstants.ImgAvator;

            switch (user.UserType)
            {
                case (int) UserTypes.Instructor:

                    if (!string.IsNullOrEmpty(user.ProfilePhotoName))
                    {
                        try
                        {
                            photoPath = Path.Combine(ApplicationConstants.StaffResourcesThumbnailUrl,
                                user.ProfilePhotoName)
                                .Replace("\\", "/");

                            var request = (HttpWebRequest)WebRequest.Create(photoPath);
                            using (var response = (HttpWebResponse)request.GetResponse())
                            {
                                exist = response.StatusCode == HttpStatusCode.OK;
                            }
                        }
                        catch
                        {
                        }

                        existingProfilePhoto = exist ? photoPath : avartarUrl;
                    }

                    else
                    {
                        existingProfilePhoto = avartarUrl;
                    }

                    identity.AddClaim(new Claim(ApplicationConstants.UserPhotoClaim,
                        user.ProfilePhotoName = existingProfilePhoto));
                    
                    
                //Adding Roles List String
                    //var loggedInUser = _accountRepo.GetUserByName(user.UserName);
                    ////if (!user.UserRoles.Any())
                    ////{
                    //user.UserRoles = _userManagementRepository.GetUserRolesByUserId(loggedInUser.Id)
                    //    .ToList();
                    ////}
                    //var k = user.UserRoles.ToString();
                    //identity.AddClaim(new Claim(ApplicationConstants.RolesClaim, user.UserRoles.ToString()));


                    break;

                case (int) UserTypes.Student:
                    var userTest = user;
                    if (!string.IsNullOrEmpty(user.ProfilePhotoName))
                    {
                        
                        try
                        {
                            photoPath = Path.Combine(ApplicationConstants.StudentResourcesThumbnailUrl,
                                user.ProfilePhotoName)
                                .Replace("\\", "/");

                            var request = (HttpWebRequest)WebRequest.Create(photoPath);
                            using (var response = (HttpWebResponse)request.GetResponse())
                            {
                                exist = response.StatusCode == HttpStatusCode.OK;
                            }
                        }
                        catch
                        {
                        }

                        existingProfilePhoto = exist ? photoPath : avartarUrl;
                    }

                    else
                    {
                        existingProfilePhoto = avartarUrl;
                    }

                    identity.AddClaim(new Claim(ApplicationConstants.UserPhotoClaim,
                        user.ProfilePhotoName = existingProfilePhoto));

                    //Add Student's program id as a claim
                    Student loggedInStudent = _studentRepo.GetStudent(user.UserName);
                    //Program studentsProgram = loggedInStudent.Program;
                    //identity.AddClaim(new Claim(ApplicationConstants.StudentProgramIdClaim, studentsProgram.Id.ToString()));
                    ////identity.AddClaim(new Claim(ApplicationConstants.StudentSpecialisationIdClaim, loggedInStudent.Specialisation == null? "0" : loggedInStudent.Specialisation.Id.ToString()));
                    

                    break;

                case (int) UserTypes.ProspectiveStudent:

                    if (user.ProfilePhotoName != "")
                    {
                       try
                        {
                            photoPath = Path.Combine(ApplicationConstants.ProspectiveStudentsResourcesThumbnailSmallUrl,
                                user.ProfilePhotoName)
                                .Replace("\\", "/");

                            var request = (HttpWebRequest) WebRequest.Create(photoPath);
                            using (var response = (HttpWebResponse) request.GetResponse())
                            {
                                exist = response.StatusCode == HttpStatusCode.OK;
                            }
                        }
                        catch
                        {
                        }

                        existingProfilePhoto = exist ? photoPath : avartarUrl;
                    }

                    else
                    {
                        existingProfilePhoto = avartarUrl;
                    }

                    identity.AddClaim(new Claim(ApplicationConstants.UserPhotoClaim,
                        user.ProfilePhotoName = existingProfilePhoto));

                    break;
            }

            AuthenticationManager.SignIn(new AuthenticationProperties {IsPersistent = isPersistent}, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            IdentityUser user = UserManager.FindById(int.Parse(User.Identity.GetUserId()));
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri) : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties {RedirectUri = RedirectUri};
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }

        #endregion
    }
}