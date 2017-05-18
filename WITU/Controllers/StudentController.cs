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

namespace WITU.Controllers
{
    [RequireHttps]
    [Authorize]
    public class StudentController : BaseController
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IStudentRepository _studentRepo;
        private readonly ISemesterRepository _semesterRepo;
        private readonly IAdminUnitRepository _adminUnitRepo;
        private readonly IStudentsHelper _studentsHelper;
        private readonly IStaffRepository _staffRepo;
        private readonly IStudentCourseRepository _studentCourseRepository;
        private const int summaryLength = 150;

        public ApplicationUserManager UserManager { get; private set; }
        public StudentController(IAccountRepository accountRepository, ICoreRepository coreRepository, IStudentRepository studentRepository, IGeneralHelper generalHelper, ISemesterRepository semesterRepository, IAdminUnitRepository adminUnitRepository, IStaffRepository staffRepository, IStudentsHelper studentsHelper, IStudentCourseRepository studentCourseRepository)
            : base(coreRepository, generalHelper)
        {
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
        //
        // GET: /Student/
        [UserTypeAccess(UserType = UserTypes.Student)]
        [Audit]
        public ActionResult Index()
        {
            string username = User.Identity.Name;

            var user = _coreRepository.Get<User>(Convert.ToInt32(User.Identity.GetUserId()));

            //If Staff Member attempts to get to this controller, especially using the breadcrump links
            if (user != null)
            {
                if (user.UserType == (int) UserTypes.Instructor)
                {
                    return RedirectToAction("Lists", "Student");
                }
            }
            
            var homeView = new StudentHomeView()
            {
                student = new Student(),
                InformationDesks = new List<InformationDesk>(),
                semInfo = new SemesterProgressSummary(),
                registrationStatus = false
            };

            Student studentInfo = _studentRepo.GetStudent(username);

            if (studentInfo.Person.ProfilePhotoName != null)
            {
                studentInfo.Person.ProfilePhotoUrl = Path.Combine(ApplicationConstants.StudentResourcesThumbnailUrl,
                    studentInfo.Person.ProfilePhotoName);
            }
            homeView.student = studentInfo;

            var ides = _coreRepository.GetAll<InformationDesk>().ToList();
            foreach (var id in ides)
            {
                if (id.ShortDescription == null)
                {
                    var sd = _generalHelper.ConvertHtmlToPlainText(id.Content);
                    id.ShortDescription = sd.Length > summaryLength ? sd.Substring(0, summaryLength) : sd;
                }
                else
                    id.ShortDescription = id.ShortDescription.Length > summaryLength ? id.ShortDescription.Substring(0, summaryLength):
                        id.ShortDescription;

                homeView.InformationDesks.Add(id);
            }

            // Get Semester Progress

            DateTime today = DateTime.Now;
            Cohort currentSemester = new Cohort();
            currentSemester = _semesterRepo.GetCurrentSemester(today);

            if (currentSemester == null)
            {
                homeView.semInfo = null;
            }
            else
            {
                // Get Students Registration status for current semester
                IEnumerable<CohortRegistration> currentSemRegistration =
                    _coreRepository.GetAll<CohortRegistration>().Where(x => x.Student == studentInfo && x.Cohort == currentSemester);

                if (currentSemRegistration.Any())
                {
                    homeView.registrationStatus = true;
                }
                else
                {
                    homeView.registrationStatus = false;
                }

                homeView.semInfo = new SemesterProgressSummary();

            }

            return View(homeView);
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
            if (CaptchaValid??false)
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
        public ActionResult StudentProfile()
        {
            string username = User.Identity.Name;

            Student studentInfo = _studentRepo.GetStudent(username);

            string profilePicPath = Path.Combine(ApplicationConstants.StudentResourcesThumbnailUrl,
                studentInfo.Person.ProfilePhotoName ?? "").Replace("\\", "/");

            bool exist = false;
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(profilePicPath);
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    exist = response.StatusCode == HttpStatusCode.OK;
                }
            }
            catch
            {
            }

            if (exist)
            {
                studentInfo.Person.ProfilePhotoUrl = Path.Combine(ApplicationConstants.StudentResourcesThumbnailUrl,
                    studentInfo.Person.ProfilePhotoName).Replace("\\", "/");
            }
            else //ApplicationConstants.ProfilesUrl + ApplicationConstants.ImgAvator)
            {
                studentInfo.Person.ProfilePhotoUrl = Path.Combine(ApplicationConstants.ProfilesUrl,
                    ApplicationConstants.ThumbnailAvator).Replace("\\", "/");
            }


            return View(studentInfo);
        }

        [UserTypeAccess(UserType = UserTypes.Student)]
        public ActionResult EditStudentProfile()
        {

            String username = User.Identity.Name;
            Student student = _studentRepo.GetStudent(username);

            //If student has graduated, no access to edit profile
            if (student.StudentStatus == StudentStatus.Graduated)
            {
                TempData[ApplicationConstants.Error404Notification] = "Sorry you can't edit your profile, since you have already graduated!";
                return RedirectToAction("PageNotFound", "Error");
            }

            
            var model = new EditPerson(student);

            model.CurrentProfilePhotoUrl = _generalHelper.GetStudentProfilePhoto(student);
            model.CountryOptions = new SelectList(_coreRepository.GetAll<Country>().OrderByDescending(x => x.Id), "Id", "Name").ToList();

            return View(model);
        }

        [HttpPost]
        [Audit(AuditingLevel = AuditLevel.AddOrEdit)]
        public ActionResult EditStudentProfile(EditPerson model)
        {
            
            if (ModelState.IsValid)
            {
                var studentToEdit = _coreRepository.Get<Student>(model.ParentClassId);

                if (studentToEdit != null)
                {
                    studentToEdit.Person.Title = model.Title;

                    studentToEdit.Person.Gender = model.Gender;
                    studentToEdit.Person.DateOfBirth = model.DateOfBirth;
                    studentToEdit.Person.MaritalStatus = model.MaritalStatus;
                    studentToEdit.Person.Religion = model.Religion;

                    //SET ALL COUNTRIES
                    var nationality =
                    _coreRepository.GetAll<Country>()
                        .FirstOrDefault(x => x.Id.Equals(model.NationalityId));

                    studentToEdit.Person.Country2 = nationality;
                    studentToEdit.Person.Country = nationality;
                    studentToEdit.Person.Country1 = nationality;

                    studentToEdit.Person.PlaceOfBirth = nationality != null ? nationality.Name : null;

                    string newFileName = null;
                    if (model.ProfilePhotoFile != null)
                    {
                        
                        if (_generalHelper.SaveFile(model.ProfilePhotoFile,
                            ApplicationConstants.StudentResourcesFolder,
                            out newFileName, model.ProfilePhotoName))
                        {
                            _generalHelper.ResizeImage(model.ProfilePhotoFile, ResourceFolders.ThumbnailSmall,
                                ApplicationConstants.StudentResourcesFolder, newFileName, model.ProfilePhotoName);
                            _generalHelper.ResizeImage(model.ProfilePhotoFile, ResourceFolders.Thumbnail,
                                ApplicationConstants.StudentResourcesFolder, newFileName, model.ProfilePhotoName);
                            studentToEdit.Person.ProfilePhotoName = newFileName;
                            studentToEdit.User.ProfilePhotoName = newFileName;
                        }
                    }
                    if (!string.IsNullOrEmpty(model.TelephoneContact))
                        studentToEdit.Person.TelephoneContact = model.TelephoneContact;
                    if (!string.IsNullOrEmpty(model.AltTelephoneContact))
                        studentToEdit.Person.AltTelephoneContact = model.AltTelephoneContact;
                    if (!string.IsNullOrEmpty(model.EmailAddress))
                        studentToEdit.Person.EmailAddress = model.EmailAddress;
                    if (!string.IsNullOrEmpty(model.AltEmailAddress))
                        studentToEdit.Person.AltEmailAddress = model.AltEmailAddress;
                    if (!string.IsNullOrEmpty(model.NextOfKinName))
                        studentToEdit.Person.NextOfKinName = model.NextOfKinName;
                    if (!string.IsNullOrEmpty(model.NextOfKinRelationship))
                        studentToEdit.Person.NextOfKinRelationship = model.NextOfKinRelationship;
                    if (!string.IsNullOrEmpty(model.NextOfKinContact))
                        studentToEdit.Person.NextOfKinContact = model.NextOfKinContact;
                    //Default the occupation to Student
                    studentToEdit.Person.Occupation = "Student";

                    if (!string.IsNullOrEmpty(model.PostalAddress))
                        studentToEdit.Person.PostalAddress = model.PostalAddress;

                    studentToEdit.Person.PersonOwnerType = int.Parse(ApplicationConstants.StudentUserType);
                    DateTime savedDateTime = DateTime.Now;
                    studentToEdit.Person.LastModified = savedDateTime;

                    
                    var isSaved = _coreRepository.SaveOrUpdate(studentToEdit);
                    if (isSaved)
                    {
                        //Profile photo was edited, student will have to log out and log back in to see new profile photo
                        if (!string.IsNullOrEmpty(newFileName))
                        {
                            TempData[ApplicationConstants.SuccessNotification] = "The profile photo has been updated. Log out and log back in to view changes!";
                        }
                        return RedirectToAction("Index", "Student");
                    }
                    else
                    {
                        ModelState.AddModelError("Save Fail", "Saving of Edit failed");
                    }
                }
                else
                {
                    ModelState.AddModelError("StudentNone", "Student doesn't exist");
                }
            }
            //listing the errors..
            foreach (string error in _generalHelper.GetErrorList(ModelState))
                ModelState.AddModelError("", error);

            
            model.CountryOptions = new SelectList(_coreRepository.GetAll<Country>().OrderByDescending(x => x.Id), "Id", "Name").ToList();
            return View(model);
        }

        #endregion

        
    }
}
