using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using WITU.Core.Model;
using WITU.Core.Repositories.Impl;
using WITU.Core.Repositories.Interfaces;
using WITU.Filters;
using WITU.Helper.Impls;
using WITU.Helper.Interfaces;
using WITU.Models;
using WITU.Models.student;
using WITU.Models.widgets;
using WITU.UserManagement;
using WITU.UserManagement.Identity;
using WITU.UserManagement.Store;
using WITU.Utils;
using FluentNHibernate.Conventions;
using FluentNHibernate.Utils;
using Gma.QrCodeNet.Encoding.DataEncodation;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using WebGrease;
using WITU.Models.Learner;
using WITU.Models.Courses;

namespace WITU.Controllers
{
    [RequireHttps]
    [Authorize]
    public class LearnerController : BaseController
    {

        private readonly IAccountRepository _accountRepository;
        private readonly IStudentRepository _studentRepo;
        private readonly ISemesterRepository _semesterRepo;
        private readonly IAdminUnitRepository _adminUnitRepo;
        private readonly IStudentsHelper _studentsHelper;
        private readonly IStaffRepository _staffRepo;
        private readonly IStudentCourseRepository _studentCourseRepository;
        private readonly ICourseRepository _courseRepository;
        private const int summaryLength = 150;

        public ApplicationUserManager UserManager { get; private set; }
        public LearnerController(IAccountRepository accountRepository, ICoreRepository coreRepository, IStudentRepository studentRepository, IGeneralHelper generalHelper, ISemesterRepository semesterRepository, IAdminUnitRepository adminUnitRepository, IStaffRepository staffRepository, IStudentsHelper studentsHelper, IStudentCourseRepository studentCourseRepository, ICourseRepository courseRepository)
            : base(coreRepository, generalHelper)
        {
            _courseRepository = courseRepository;
            _studentRepo = studentRepository;
            _semesterRepo = semesterRepository;
            _adminUnitRepo = adminUnitRepository;
            _accountRepository = accountRepository;
            _staffRepo = staffRepository;
            _studentsHelper = studentsHelper;
            _studentCourseRepository = studentCourseRepository;
            UserManager = new ApplicationUserManager(new UserStore(accountRepository, coreRepository));
            UserManager.UserValidator = new UserValidator<IdentityUser, int>(UserManager)
            {
                AllowOnlyAlphanumericUserNames = false
            };

        }

        #region Student Portal

        // GET: Learner
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult FirstTimeUser()
        {
            string username = User.Identity.Name;
            Student studentInfo = _studentRepo.GetStudent(username);

            var model = new FirstTimeUserModel()
            {
                //EmailAddress = studentInfo.User.Email,
                ConfirmPassword = string.Empty,
                NewPassword = string.Empty,
                OldPassword = string.Empty,

            };
            return View(model);
        }

        [HttpPost]
        [RecaptchaValidator]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> FirstTimeUser(FirstTimeUserModel model, bool? CaptchaValid)
        {
            if (CaptchaValid ?? false)
            {
                // ViewBag.ReturnUrl = Url.Action("FirstTimeUser");

                if (ModelState.IsValid)
                {
                    IdentityResult result =
                        await
                            UserManager.ChangePasswordAsync(int.Parse(User.Identity.GetUserId()), model.OldPassword,
                                model.NewPassword);
                    if (result.Succeeded)
                    {
                        var user = _coreRepository.Get<User>(int.Parse(User.Identity.GetUserId()));
                        user.IsFirstTimeUser = false;
                        //user.Email = model.EmailAddress;
                        user.LastPasswordChangeDate = DateTime.Now;
                        var update = _coreRepository.SaveOrUpdate(user);
                        TempData[ApplicationConstants.ErrorNotification] = null;
                        TempData[ApplicationConstants.SuccessNotification] = "Password successfully changed";
                        return RedirectToAction("Index");
                    }
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                TempData[ApplicationConstants.ErrorNotification] = "Something went wrong while changing password";

                // If we got this far, something failed, redisplay form
                return View(model);
            }
            else
            {
                ModelState.AddModelError("reCaptcha", "Invalid reCaptcha");
                return View(model);
            }
        }

        [UserTypeAccess(UserType = UserTypes.Student)]
        [Audit]
        public ActionResult ViewCourses(CourseListModel model, int? courseDisplayType) {
            //The variable courseDisplayType is defined as:
            //1 = All
            //2 = InProgress
            //3 = Completed

            if (model != null) {
                List<Course> courses = new List<Course>(); 
                if (courseDisplayType == null || courseDisplayType == 1) {
                    courses = _courseRepository.GetCourses();
                    model.CourseList = courses;
                }
            }
            return View(model);
        }

        [UserTypeAccess(UserType = UserTypes.Student)]
        [Audit]
        public ActionResult ViewCourse(int courseId)
        {
            //The variable courseDisplayType is defined as:
            //1 = All
            //2 = InProgress
            //3 = Completed

            if (courseId > 0)
            {
                CourseModel model = new CourseModel();
                Course course = _coreRepository.Get<Course>(courseId);
                model.Course = course;
                return View(model);
            }
            return null;
        }

        #endregion
    }
}