using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WITU.Core.Model;
using WITU.Core.Repositories.Interfaces;
using WITU.Helper.Impls;
using WITU.Helper.Interfaces;
using WITU.Models;
using WITU.Utils;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNet.Identity;

namespace WITU.Controllers
{
    [RequireHttps]
    [Authorize]
    public class StaffController : BaseController
    {
        private readonly ICoreRepository _coreRepo;
        private readonly IAccountRepository _accountRepo;
        private readonly IStaffRepository _staffRepo;
        private readonly IGeneralHelper _generalHelper;
        private readonly IStaffCourseRepository _staffCourseRepo;
        private readonly IStudentCourseRepository _studentCourseRepo;
        private readonly ISemesterRepository _semesterRepository;
        private readonly IAdminUnitRepository _adminUnitRepo;
        private readonly string IMG_HEADER_PATH = "~/Content/images/NdejjeHeader.png";
        private readonly int summaryLength = 150;

        public StaffController(ICoreRepository coreRepository, IAccountRepository accountRepository,
            IStaffRepository staffRepository, IGeneralHelper generalHelper, 
            IStaffCourseRepository staffCourseRepository, IStudentCourseRepository studentCourseRepository, ISemesterRepository semesterRepository, IAdminUnitRepository adminUnitRepository)
            : base(coreRepository, generalHelper)
        {
            _coreRepo = coreRepository;
            _accountRepo = accountRepository;
            _staffRepo = staffRepository;
            _generalHelper = generalHelper;
            _staffCourseRepo = staffCourseRepository;
            _studentCourseRepo = studentCourseRepository;
            _semesterRepository = semesterRepository;
            _adminUnitRepo = adminUnitRepository;
        }

        [UserTypeAccess(UserType = UserTypes.Instructor)]
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();

            var homeView = new StaffHomeView()
            {
                staff = new Instructor(),
                InformationDesks = new List<InformationDesk>(),
                semInfo = new SemesterProgressSummary()
            };
            Instructor staffInfo = _staffRepo.GetStaffByUserId(int.Parse(userId));
            if (staffInfo.Person.ProfilePhotoName != null)
            {
                staffInfo.Person.ProfilePhotoUrl = Path.Combine(ApplicationConstants.StaffResourcesThumbnailUrl,
                    staffInfo.Person.ProfilePhotoName);
            }

            homeView.staff = staffInfo;

            //Need to limit to the number to be displayed (4)
            var ides = _coreRepo.GetAll<InformationDesk>().ToList();
            foreach (var id in ides)
            {
                if (id.ShortDescription == null)
                {
                    var sd = _generalHelper.ConvertHtmlToPlainText(id.Content);
                    id.ShortDescription = sd.Length > summaryLength ? sd.Substring(0, summaryLength) + "... " : sd;
                }
                else
                    id.ShortDescription = id.ShortDescription.Length > summaryLength ? id.ShortDescription.Substring(0, summaryLength) + "... " :
                        id.ShortDescription + "...";

                homeView.InformationDesks.Add(id);
            }

            // Get Semester Progress

            DateTime today = DateTime.Now;
            Cohort currentSemester = new Cohort();
            currentSemester = _semesterRepository.GetCurrentSemester(today);

            homeView.semInfo = currentSemester == null ? null : new SemesterProgressSummary();

            return View(homeView);
        }

        [UserTypeAccess(UserType = UserTypes.Instructor)]
        public ActionResult StaffProfile()
        {
            string userId = User.Identity.GetUserId();
            Instructor staffInfo = _staffRepo.GetStaffByUserId(int.Parse(userId));

            
            bool exist = false;
            try
            {
                string profilePicPath = Path.Combine(ApplicationConstants.StaffResourcesThumbnailUrl,
                    staffInfo.Person.ProfilePhotoName).Replace("\\", "/");

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
                staffInfo.Person.ProfilePhotoUrl = Path.Combine(ApplicationConstants.StaffResourcesThumbnailUrl,
                    staffInfo.Person.ProfilePhotoName).Replace("\\", "/");
            }
            else //ApplicationConstants.ProfilesUrl + ApplicationConstants.ImgAvator)
            {
                staffInfo.Person.ProfilePhotoUrl = Path.Combine(ApplicationConstants.ProfilesUrl,
                    ApplicationConstants.ThumbnailAvator).Replace("\\", "/");
            }
            return View(staffInfo);
        }


        [UserTypeAccess(UserType = UserTypes.Instructor)]
        public ViewResult EditStaffProfile()
        {
            String userId = User.Identity.GetUserId();
            Instructor staff = _staffRepo.GetStaffByUserId(int.Parse(userId));
            var model = new EditPerson(staff);

            model.CurrentProfilePhotoUrl = _generalHelper.GetStaffProfilePhoto(staff);
            model.CountryOptions = new SelectList(_coreRepo.GetAll<Country>().OrderByDescending(x => x.Id), "Id", "Name").ToList();

            return View(model);
        }

        [HttpPost]
        [Audit(AuditingLevel = AuditLevel.AddOrEdit)]
        public ActionResult EditStaffProfile(EditPerson model)
        {
            
            if (ModelState.IsValid)
            {
                var staffToEdit = _coreRepo.Get<Instructor>(model.ParentClassId);

                if (staffToEdit != null)
                {
                    staffToEdit.Person.Title = model.Title;
                    
                    staffToEdit.Person.Gender = model.Gender;
                    staffToEdit.Person.DateOfBirth = model.DateOfBirth;
                    staffToEdit.Person.MaritalStatus = model.MaritalStatus;
                    staffToEdit.Person.Religion = model.Religion;

                    //SET ALL COUNTRIES
                    var nationality =
                    _coreRepository.GetAll<Country>()
                        .FirstOrDefault(x => x.Id.Equals(model.NationalityId));

                    staffToEdit.Person.Country2 = nationality;
                    staffToEdit.Person.Country = nationality;
                    staffToEdit.Person.Country1 = nationality;

                    staffToEdit.Person.PlaceOfBirth = nationality != null ? nationality.Name : null;

                    string newFileName = null;
                    if (model.ProfilePhotoFile != null)
                    {   
                        if (_generalHelper.SaveFile(model.ProfilePhotoFile,
                            ApplicationConstants.StaffResourcesFolder,
                            out newFileName, model.ProfilePhotoName))
                        {
                            _generalHelper.ResizeImage(model.ProfilePhotoFile, ResourceFolders.ThumbnailSmall,
                                ApplicationConstants.StaffResourcesFolder, newFileName, model.ProfilePhotoName);
                            _generalHelper.ResizeImage(model.ProfilePhotoFile, ResourceFolders.Thumbnail,
                                ApplicationConstants.StaffResourcesFolder, newFileName, model.ProfilePhotoName);
                            staffToEdit.Person.ProfilePhotoName = newFileName;
                            staffToEdit.User.ProfilePhotoName = newFileName;
                        }
                    }
                    if (!string.IsNullOrEmpty(model.TelephoneContact))
                        staffToEdit.Person.TelephoneContact = model.TelephoneContact;
                    if (!string.IsNullOrEmpty(model.AltTelephoneContact))
                        staffToEdit.Person.AltTelephoneContact = model.AltTelephoneContact;
                    if (!string.IsNullOrEmpty(model.EmailAddress))
                        staffToEdit.Person.EmailAddress = model.EmailAddress;
                    if (!string.IsNullOrEmpty(model.AltEmailAddress))
                        staffToEdit.Person.AltEmailAddress = model.AltEmailAddress;
                    if (!string.IsNullOrEmpty(model.NextOfKinName))
                        staffToEdit.Person.NextOfKinName = model.NextOfKinName;
                    if (!string.IsNullOrEmpty(model.NextOfKinRelationship))
                        staffToEdit.Person.NextOfKinRelationship = model.NextOfKinRelationship;
                    if (!string.IsNullOrEmpty(model.NextOfKinContact))
                        staffToEdit.Person.NextOfKinContact = model.NextOfKinContact;
                    //Default the occupation to Staff
                    staffToEdit.Person.Occupation = "Staff";
                    
                    if (!string.IsNullOrEmpty(model.PostalAddress))
                        staffToEdit.Person.PostalAddress = model.PostalAddress;

                    staffToEdit.Person.PersonOwnerType = int.Parse(ApplicationConstants.StaffUserType);
                    DateTime savedDateTime = DateTime.Now;
                    staffToEdit.Person.LastModified = savedDateTime;

                    var isSaved = _coreRepo.SaveOrUpdate(staffToEdit);
                    if (isSaved)
                    {
                        if (!string.IsNullOrEmpty(newFileName))
                        {
                            TempData[ApplicationConstants.SuccessNotification] = "The profile photo has been updated. Log out and log back in to view changes!";
                        }
                        return RedirectToAction("Index", "Staff");
                    }
                    else
                    {
                        ModelState.AddModelError("Save Fail", "Saving of Edit failed");
                    }
                }
                else
                {
                    ModelState.AddModelError("StaffNone", "Staff Member doesn't exist");
                }
                
            }
            //Listing errors
                foreach (string error in _generalHelper.GetErrorList(ModelState))
                    ModelState.AddModelError("", error);

            model.CountryOptions = new SelectList(_coreRepo.GetAll<Country>().OrderByDescending(x => x.Id), "Id", "Name").ToList();
            return View(model);
        }

       
    }
}