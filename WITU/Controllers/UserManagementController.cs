using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WITU.Core.Model;
using WITU.Core.Repositories.Interfaces;
using WITU.Helper.Impls;
using WITU.Helper.Interfaces;
using WITU.Models;
using WITU.Models.UserManagement;
using WITU.Models.widgets;
using WITU.UserManagement;
using WITU.UserManagement.Identity;
using WITU.UserManagement.Store;
using WITU.Utils;
using FluentNHibernate.Conventions;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;

namespace WITU.Controllers
{
    [RequireHttps]
    [RoutePrefix("UserManagement")]
    [Authorize]
    public class UserManagementController : BaseController
    {
        private readonly ICoreRepository _coreRepo;
        private readonly IGeneralHelper _generalHelper;
        private readonly IStaffRepository _staffRepo;
        private readonly IStudentRepository _studentRepository;
        private readonly IUserManagementRepository _userManagementRepository;
        private readonly ISemesterRepository _semesterRepository;

        public ApplicationUserManager UserManager { get; private set; }

        public int[] InactiveServiceCategories { get; private set; }

        public UserManagementController(ICoreRepository coreRepository, IGeneralHelper generalHelper,
            IStaffRepository staffRepository, IStudentRepository studentRepository, IUserManagementRepository userManagementRepository, ISemesterRepository semesterRepository, IAccountRepository accountRepository)
            : base(coreRepository, generalHelper)
        {
            _coreRepo = coreRepository;
            _generalHelper = generalHelper;
            _staffRepo = staffRepository;
            _userManagementRepository = userManagementRepository;
            _studentRepository = studentRepository;
            _semesterRepository = semesterRepository;

            UserManager = new ApplicationUserManager(new UserStore(accountRepository, coreRepository));
            UserManager.UserValidator = new UserValidator<IdentityUser, int>(UserManager)
            {
                AllowOnlyAlphanumericUserNames = false
            };

            InactiveServiceCategories = new int[]
            {
                (int) ServiceCategory.AboutArms, (int) ServiceCategory.CourseAllocation, (int) ServiceCategory.Messenger,
                (int) ServiceCategory.MyProfile, (int) ServiceCategory.Notifications,
                (int) ServiceCategory.PrivacyPolicy,
                (int) ServiceCategory.Reports, (int) ServiceCategory.Teach, (int) ServiceCategory.TermsOfService
            };
        }


        [Route("Index")]
        [UserTypeAccess(UserType = UserTypes.Instructor)]
        //[AuthorizeUser(AccessLevel = AccessLevel.Faculty, Service = (int)ServiceDetail.CreateStaffAccountsAndRoles,
        //    Permission = ApplicationConstants.CanRead)]
        public ActionResult Index()
        {
            int userType = User.ApplicationUserType();

            if (userType == (int)UserTypes.Instructor)
            {
                return RedirectToAction("StaffAccounts");
            }
            if ((userType == (int)UserTypes.Student))
            {
                return RedirectToAction("Home");
            }
            return RedirectToAction("Home");
        }


        [UserTypeAccess(UserType = UserTypes.Instructor)]
        [AuthorizeUser(Service = (int)ServiceDetail.CreateStaffAccountsAndRoles,
            Permission = ApplicationConstants.CanRead)]
        public ActionResult StaffDetails(int id = 0) //userid 
        {
            if (id > 0)
            {
                //Edit existing Staff Account
                var user = _coreRepo.Get<User>(id);
                Instructor staff = _staffRepo.GetStaffByUserId(id);

                if (staff != null)
                {
                    staff.Person.ProfilePhotoUrl = _generalHelper.GetStaffProfilePhoto(staff);
                }

                var model = new UserInfoViewModel() { User = user, Person = staff.Person };

                return View(model);
            }
            else
            {
                //Create new Staff Account
                var model = new UserInfoViewModel() { User = new User(), Person = new Person() };

                return View(model);
            }

        }


        [UserTypeAccess(UserType = UserTypes.Instructor)]
        [AuthorizeUser(Service = (int)ServiceDetail.CreateStaffAccountsAndRoles,
            Permission = ApplicationConstants.CanRead)]
        public ActionResult StaffPersonalDetails(UserInfoViewModel model, string buttonClicked, HttpPostedFileBase file)
        {
            try
            {
                //When no staff details exist i.e Add new staff details
                if (model.User.Id <= 0 && model.Person.Surname == null)
                {
                    ViewBag.CountryId = ViewBag.CountryId ?? new SelectList(_coreRepo.GetAll<Country>(), "Id", "Name", model);

                    return PartialView("_editStaffPersonalDetails", model);
                }

                //if (model.User.Id > 0)
                //{
                //Check whether username already exists 
                if (model.User.Id <= 0 && _coreRepo.Get<User>("Username", model.User.Username) != null)
                {
                    ModelState.AddModelError("Error", "The username chosen already exists!");
                    ViewBag.CountryId = ViewBag.CountryId ??
                                        new SelectList(_coreRepo.GetAll<Country>(), "Id", "Name", model);

                    return PartialView("_editStaffPersonalDetails", model);
                }

                var user = _coreRepo.Get<User>(model.User.Id);

                //Retrieving Existing Staff Member
                Instructor staff = _staffRepo.GetStaffByUserId(model.User.Id);

                if (buttonClicked == "Save")
                {
                    

                    //Saving or Updating Person
                    Person person = staff != null ? staff.Person : new Person();

                    person.Title = model.Person.Title;
                    person.Surname = model.Person.Surname;
                    person.GivenName = model.Person.GivenName;
                    person.OtherName = model.Person.OtherName;
                    person.Gender = model.Person.Gender;
                    person.DateOfBirth = model.Person.DateOfBirth;
                    person.MaritalStatus = model.Person.MaritalStatus;
                    person.Religion = model.Person.Religion;
                    person.Country2 = model.Person.Country2 != null ? model.Person.Country2.Id > 0 ? _coreRepo.Get<Country>(model.Person.Country2.Id) : model.Person.Country : null;
                    person.Country = model.Person.Country2 != null ? model.Person.Country2.Id > 0 ? _coreRepo.Get<Country>(model.Person.Country2.Id) : model.Person.Country : null;
                    person.Country1 = model.Person.Country2 != null ? model.Person.Country2.Id > 0 ? _coreRepo.Get<Country>(model.Person.Country2.Id) : model.Person.Country : null;
                    person.PlaceOfBirth = model.Person.Country2 != null ? model.Person.Country2.Id > 0 ? _coreRepo.Get<Country>(model.Person.Country2.Id).Name : null
                        : null;

                    string newFileName = null;
                    if (file != null)
                    {

                        if (_generalHelper.SaveFile(file, ApplicationConstants.StaffResourcesFolder,
                            out newFileName, model.User.ProfilePhotoName))
                            _generalHelper.ResizeImage(file, ResourceFolders.ThumbnailSmall,
                                ApplicationConstants.StaffResourcesFolder, newFileName, !string.IsNullOrEmpty(model.User.ProfilePhotoName)? model.User.ProfilePhotoName : string.IsNullOrEmpty(model.Person.ProfilePhotoName) ? null : model.Person.ProfilePhotoName);
                        _generalHelper.ResizeImage(file, ResourceFolders.Thumbnail,
                            ApplicationConstants.StaffResourcesFolder, newFileName, !string.IsNullOrEmpty(model.User.ProfilePhotoName) ? model.User.ProfilePhotoName : string.IsNullOrEmpty(model.Person.ProfilePhotoName) ? null : model.Person.ProfilePhotoName);
                        model.User.ProfilePhotoName = newFileName;
                        person.ProfilePhotoName = newFileName;
                    }

                    person.PostalAddress = model.Person.PostalAddress;
                    person.EmailAddress = model.Person.EmailAddress;
                    person.AltEmailAddress = model.Person.AltEmailAddress;
                    person.TelephoneContact = model.Person.TelephoneContact;
                    person.NextOfKinName = model.Person.NextOfKinName;
                    person.NextOfKinRelationship = model.Person.NextOfKinRelationship;
                    person.NextOfKinContact = model.Person.NextOfKinContact;
                    person.NextOfKinAddress = model.Person.NextOfKinAddress;
                    person.WebsiteUrl = model.Person.WebsiteUrl;
                    person.PersonOwnerType = int.Parse(ApplicationConstants.StaffUserType);
                    person.LastModified = DateTime.Now;

                    if (model.User.Id <= 0)
                        person.CreatedOn = DateTime.Now;

                    //now saving User Information
                    if (_coreRepo.SaveOrUpdate(person))
                    {
                        //Saving or Updating the User Information in the User Table
                        User userAccount = user ?? new User();
                        userAccount.Username = model.User.Username;
                        userAccount.Email = person.EmailAddress;
                        userAccount.UserType = int.Parse(ApplicationConstants.StaffUserType);
                        userAccount.IsApproved = true;
                        userAccount.IsLockedOut = false;
                        userAccount.IsFirstTimeUser = user == null;

                        if (!string.IsNullOrEmpty(newFileName))
                        {
                            userAccount.ProfilePhotoName = newFileName;
                        }

                        if (_coreRepo.SaveOrUpdate(userAccount))
                        {
                            staff = staff ?? new Instructor();

                            staff.Person = person;
                            staff.User = userAccount;
                            staff.CreatedOn = DateTime.Now;

                            if (_coreRepo.SaveOrUpdate(staff))
                            {
                                //Set Password to Default Password if new user
                                var userAccountSaved = _coreRepo.Get<User>(userAccount.Id);
                                if (userAccountSaved != null && userAccountSaved.Password == null &&
                                    userAccountSaved.PasswordKey == null)
                                {
                                    IdentityResult result = UserManager.AddPassword(userAccountSaved.Id,
                                        ApplicationConstants.DEFAULT_STUDENT_PASSWORD);
                                    if (result.Succeeded) ;
                                }
                            }

                            //Redirect so as to refresh the view details
                            return JavaScript("window.location = '" + Url.Action("StaffDetails", "UserManagement", new { id = userAccount.Id }) + "'");
                        }
                    }
                }
                if (user != null)
                {
                    staff = _staffRepo.GetStaffByUserId(model.User.Id);
                    model.User = user;
                    model.Person = staff.Person;

                    if (model.Person.ProfilePhotoName != null)
                    {
                        model.Person.ProfilePhotoUrl = Path.Combine(ApplicationConstants.StaffResourcesThumbnailUrl,
                            staff.Person.ProfilePhotoName);
                    }
                    else if (model.User.ProfilePhotoName != null)
                    {
                        model.Person.ProfilePhotoUrl = Path.Combine(ApplicationConstants.StaffResourcesThumbnailUrl,
                            staff.User.ProfilePhotoName);
                    }
                    else
                    {
                        model.Person.ProfilePhotoUrl = ApplicationConstants.ProfilePhotoAvartarUrl;
                    }
                    ViewBag.CountryId = ViewBag.CountryId ??
                                        new SelectList(_coreRepo.GetAll<Country>(), "Id", "Name", model);
                }

                if (buttonClicked == "Edit")
                {
                    ViewBag.CountryId = ViewBag.CountryId ?? new SelectList(_coreRepo.GetAll<Country>(), "Id", "Name", model);

                    return PartialView("_editStaffPersonalDetails", model);
                }
                //}
                return PartialView("_StaffPersonalDetails", model);

            }
            catch (Exception e)
            {

                throw e;
            }

        }

        public ActionResult StaffPositions(UserInfoViewModel model, string buttonClicked, ProgramSelectItem filterModel, List<int> selection, int levelId = 0)
        {
            if (model.User.Id > 0)
            {
                var user = _coreRepo.Get<User>(model.User.Id);
                Instructor instructor = _staffRepo.GetStaffByUserId(model.User.Id);

                model.User = user;
                model.StaffPositions = instructor != null ? instructor.InstructorPositions.ToList() : null;

                if (buttonClicked == "Add Position")
                {
                    model.StaffPosition = new InstructorPosition() { Position = new Position() };
                    ViewBag.Positions = _coreRepo.GetAll<Position>();
                    return PartialView("_addStaffPosition", model);
                }

                if (buttonClicked == "Save")
                {
                    //Saving Staff Position 
                    if (levelId > 0)
                    {
                        if (levelId == (int)AccessLevel.Campus)
                        {
                            if (filterModel.CampusId <= 0)
                            {
                                ModelState.AddModelError("Error", "Kindly select a Campus!");
                                ViewBag.Positions = _coreRepo.GetAll<Position>();
                                model.StaffPosition.LevelId = AccessLevel.Campus;
                                return PartialView("_addStaffPosition", model);
                            }
                        }
                        //if (levelId == (int)AccessLevel.Faculty)
                        //{
                        //    if (filterModel.FacultyId <= 0)
                        //    {
                        //        ModelState.AddModelError("Error", "Kindly select a Faculty!");
                        //        ViewBag.Positions = _coreRepo.GetAll<Position>();
                        //        model.StaffPosition.LevelId = AccessLevel.Faculty;
                        //        return PartialView("_addStaffPosition", model);
                        //    }
                        //}
                        //if (levelId == (int)AccessLevel.Department)
                        //{
                        //    if (filterModel.DepartmentId <= 0)
                        //    {
                        //        ModelState.AddModelError("Error", "Kindly select a Department!");
                        //        ViewBag.Positions = _coreRepo.GetAll<Position>();
                        //        model.StaffPosition.LevelId = AccessLevel.Department;

                        //        return PartialView("_addStaffPosition", model);
                        //    }
                        //}
                        //if (levelId == (int)AccessLevel.Program)
                        //{
                        //    if (filterModel.ProgramId <= 0)
                        //    {
                        //        ModelState.AddModelError("Error", "Kindly select a Program!");
                        //        ViewBag.Positions = _coreRepo.GetAll<Position>();
                        //        model.StaffPosition.LevelId = AccessLevel.Program;

                        //        return PartialView("_addStaffPosition", model);
                        //    }
                        //}

                        //if (levelId == (int)AccessLevel.Course && selection != null)
                        //{
                        //    if (!selection.Any())
                        //    {
                        //        ModelState.AddModelError("Error", "Kindly select a Course!");
                        //        ViewBag.Positions = _coreRepo.GetAll<Position>();
                        //        model.StaffPosition.LevelId = AccessLevel.Course;

                        //        return PartialView("_addStaffPosition", model);
                        //    }
                        //}

                        Campus campus = null;
                       

                        if (filterModel.CampusId > 0)
                        {
                            campus = _coreRepo.Get<Campus>(Convert.ToInt32(filterModel.CampusId));
                        }
                        
                        //if (levelId == (int)AccessLevel.Course && selection != null)
                        //{
                        //    if (selection.Any())
                        //    {
                        //        var course = _coreRepo.Get<Course>(selection.FirstOrDefault());
                        //        //campus = program.Campus;
                        //    }
                        //}
                        if (model.StaffPosition.Id == 0)
                        {
                            var staffPosition = new InstructorPosition()
                            {

                                AssignedDate = DateTime.Now,
                                IsActive = model.StaffPosition.IsActive,
                                LevelId = (AccessLevel)Convert.ToInt32(levelId),
                                Staff = instructor,
                                IdNumber = model.StaffPosition.IdNumber,
                                WorkStatus = model.StaffPosition.WorkStatus,
                                Position = _coreRepo.Get<Position>(model.StaffPosition.Position.Id),
                                Campus = campus
                            };
                            int staffpositionId = (int)_coreRepo.Save(staffPosition);
                            //if (staffpositionId > 0 && (AccessLevel)Convert.ToInt32(levelId) == AccessLevel.Course && selection != null
                            //   )
                            //{
                            //    if (selection.Any())

                            //        TempData[ApplicationConstants.SuccessNotification] =
                            //            ApplicationConstants.SuccessSaveMessage;

                            //    var selectedCourses = _coreRepo.GetAll<Course>(selection);
                            //    var newStaffPosition = _coreRepo.Get<InstructorPosition>(staffpositionId);

                            //    var newStaffCourses = selectedCourses.Select(course => new StaffCourse()
                            //    {
                            //        StaffPosition = newStaffPosition,
                            //        Course = course
                            //    }).ToList();

                            //    if (_coreRepo.SaveOrUpdateAll(newStaffCourses))
                            //    {
                            //        TempData[ApplicationConstants.SuccessNotification] =
                            //            "Assigned Courses have been submitted";
                            //    }
                            //}
                        }
                        else
                        {
                            var staffPosition = _coreRepo.Get<InstructorPosition>(model.StaffPosition.Id);
                            {
                                staffPosition.AssignedDate = DateTime.Now;
                                staffPosition.IsActive = model.StaffPosition.IsActive;
                                staffPosition.LevelId = (AccessLevel)Convert.ToInt32(levelId);
                                staffPosition.IdNumber = model.StaffPosition.IdNumber;
                                staffPosition.WorkStatus = model.StaffPosition.WorkStatus;
                                staffPosition.Position = _coreRepo.Get<Position>(model.StaffPosition.Position.Id);
                                staffPosition.Campus = campus;
                                staffPosition.Staff = instructor;
                            }

                            //if ((AccessLevel)Convert.ToInt32(levelId) == AccessLevel.Course && selection != null
                            //    )
                            //{
                            //    if (selection.Any())
                            //        //Delete old course assignments
                            //        _coreRepo.Delete<StaffCourse>(staff.StaffPositions.SelectMany(a => a.StaffCourses.Where(x => x.StaffPosition.Id == staffPosition.Id).Select(b => b.Id)).ToList<int>());

                            //    foreach (var courseId in selection)
                            //    {
                            //        staffPosition.StaffCourses.Add(new StaffCourse() { Course = _coreRepo.Get<Course>(courseId) });
                            //    }
                            //}
                            if (_coreRepo.SaveOrUpdate(staffPosition))
                            {// return View("_StaffPositions", model);
                                TempData[ApplicationConstants.SuccessNotification] =
                                    ApplicationConstants.SuccessSaveMessage;
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Kindly select a level for this position!");
                        ViewBag.Positions = _coreRepo.GetAll<Position>();
                        return PartialView("_addStaffPosition", model);
                    }
                    model.StaffPositions =
                        _coreRepo.GetAll<InstructorPosition>().Where(ress => ress.Staff.Id == instructor.Id).ToList();

                    return PartialView("_StaffPositions", model);
                }
                else
                {
                    return PartialView("_StaffPositions", model);
                }
            }
            return null; //PartialView("_StaffPositions", new UserInfoViewModel());
        }

        public ActionResult StaffResponsibilities(UserInfoViewModel model, string buttonClicked)
        {
            if (model.User.Id > 0)
            {
                var user = _coreRepo.Get<User>(model.User.Id);
                model.User = user;

                List<UserRole> userRoles = user.UserRoles.ToList();

                model.UserRoles = userRoles;

                if (buttonClicked == "Edit")
                {

                    model.Services = _userManagementRepository.GetActiveServices(InactiveServiceCategories);

                    Service[] services = model.Services as Service[] ?? model.Services.ToArray();

                    var resposibilities = new List<Responsibility>();

                    for (int i = 0; i < services.Count(); i++)
                    {
                        resposibilities.Add(new Responsibility
                        {
                            Service = services[i]
                        });
                    }

                    if (userRoles.Count != 0)
                    {
                        UserRole[] userRolesArray = userRoles.ToArray();
                        for (int i = 0; i < userRolesArray.Count(); i++)
                        {
                            foreach (
                                Responsibility res in
                                    resposibilities.Where(ress => ress.Service.Id == userRolesArray[i].Role.Service.Id))
                            {
                                res.Id = userRolesArray[i].Role.Id;
                                res.Type = userRolesArray[i].Role.Type;
                                res.RequiresAccessScope = userRolesArray[i].Role.RequiresAccessScope;
                                res.ServiceId = userRolesArray[i].Role.Service.Id;
                                res.Service = userRolesArray[i].Role.Service;
                                res.CanCreate = userRolesArray[i].Role.CanCreate;
                                res.CanDelete = userRolesArray[i].Role.CanDelete;
                                res.CanRead = userRolesArray[i].Role.CanRead;
                                res.CanUpdate = userRolesArray[i].Role.CanUpdate;
                            }
                        }

                        model.Responsibilities = resposibilities;
                    }
                    else
                    {
                        model.Responsibilities = resposibilities;
                    }

                    return PartialView("_editStaffResponsibilities", model);

                }
                if (buttonClicked == "Save")
                {
                    _coreRepo.Delete<UserRole>(user.UserRoles.Select(a => a.Id).ToList());
                    _coreRepo.Delete<Role>(user.UserRoles.Select(a => a.Role.Id).ToList());

                    if (model.Responsibilities.Any(a => a.CanRead || a.CanCreate || a.CanUpdate || a.CanDelete))
                    {
                        foreach (
                            Responsibility roles in
                                model.Responsibilities.Where(a => a.CanRead || a.CanCreate || a.CanUpdate || a.CanDelete)
                            )
                        {
                            var newRole = new Role
                            {
                                CanCreate = roles.CanCreate,
                                CanDelete = roles.CanDelete,
                                CanRead = roles.CanRead,
                                CanUpdate = roles.CanUpdate,
                                RequiresAccessScope = roles.RequiresAccessScope,
                                Service = _coreRepo.Get<Service>(roles.Service.Id),
                                //Id = roles.Id
                            };
                            if (_coreRepo.SaveOrUpdate(newRole))
                            {
                                var userRole = new UserRole { User = user, Role = newRole };

                                _coreRepo.SaveOrUpdate(userRole);
                            }
                            ;
                        }

                    }
                    TempData[ApplicationConstants.SuccessNotification] = ApplicationConstants.SuccessSaveMessage;

                    model.UserRoles = _coreRepo.GetAll<UserRole>().Where(a => a.User.Id == model.User.Id).ToList();
                    return PartialView("_StaffResponsibilities", model);
                    //return RedirectToAction("StaffResponsibilities", model);
                }
                else
                {
                    return PartialView("_StaffResponsibilities", model);
                }
            }
            return null;
        }

        public ActionResult EditPostition(int id) //staffposition
        {
            var staffPosition = _coreRepo.Get<InstructorPosition>(id);
            var model = new UserInfoViewModel()
            {
                User = staffPosition.Staff.User,
                StaffPosition = staffPosition
            };

            ViewBag.Positions = _coreRepo.GetAll<Position>();
            return PartialView("_addStaffPosition", model);

        }

        public ActionResult PreloadLevel(UserInfoViewModel model, string level)
        {
            IEnumerable<SelectListItem> campusOptions = new List<SelectListItem>();

            if ((level.IsNullOrWhiteSpace() || level.Equals("0")) && model.StaffPosition != null)
            {
                //if (model.StaffPosition.Id > 0)
                {
                    //Editing take place
                    level = ((int)model.StaffPosition.LevelId).ToString();
                }
            }

            switch (level)
            {
                case "1"://University 
                    {
                        return PartialView("EditorTemplates/UniversitySelectItem"); ;
                    }

                case "2"://Campus 
                    {
                        campusOptions =
                            _coreRepository.GetAll<Campus>()
                                .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });

                        var campusModel = new CampusSelectItem
                        {
                            CampusOptions = campusOptions,
                            CampusId = model.StaffPosition != null ? model.StaffPosition.Campus != null ? model.StaffPosition.Campus.Id : 0 : 0
                        };

                        return PartialView("EditorTemplates/CampusSelectItem", campusModel);
                    }
                case "3"://Faculty
                    {
                        campusOptions =
                            _coreRepository.GetAll<Campus>()
                                .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });

                        var facultyModel = new FacultySelectItem
                        {
                            CampusOptions = campusOptions,
                            CampusId = model.StaffPosition != null ? model.StaffPosition.Campus != null ? model.StaffPosition.Campus.Id : 0 : 0,
                        };

                        return PartialView("EditorTemplates/FacultySelectItem", facultyModel);
                    }
                case "4"://Department
                    {
                        campusOptions =
                            _coreRepository.GetAll<Campus>()
                                .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });

                        var departmentModel = new AdministrativeSelectItem
                        {
                            CampusOptions = campusOptions,
                            CampusId = model.StaffPosition != null ? model.StaffPosition.Campus != null ? model.StaffPosition.Campus.Id : 0 : 0,
                        };

                        return PartialView("EditorTemplates/AdministrativeSelectItem", departmentModel);
                    }
                case "5"://Program
                    {
                        campusOptions =
                            _coreRepository.GetAll<Campus>()
                                .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });

                        var programModel = new ProgramSelectItem(campusOptions);
                        
                        return PartialView("EditorTemplates/ProgramSelectItem", programModel);
                    }
                case "6"://Course
                    {
                        campusOptions =
                           _coreRepository.GetAll<Campus>()
                               .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });

                        //var courseModel = new CourseSelectItem(campusOptions);
                        //if (model.StaffPosition != null && model.StaffPosition.Program != null)
                        //{
                        //    courseModel = new CourseSelectItem(model.StaffPosition.Program, campusOptions);
                        //}

                        var programModel = new ProgramSelectItem(campusOptions);
                        
                        return PartialView("_courseLevelView", programModel);
                    }
                default:
                    return null;
            }
            return null;
        }

        //public ActionResult GetCourses(int id = 0)
        //{
        //    var program = _coreRepo.Get<Program>(id);

        //    return PartialView("_coursesView", program);
        //}

        [UserTypeAccess(UserType = UserTypes.Instructor)]
        [AuthorizeUser(Service = (int)ServiceDetail.CreateStaffAccountsAndRoles,
            Permission = ApplicationConstants.CanRead)]
        public ActionResult StaffAccounts()
        {
            var model = new UserInfoViewModel();

            var staffList = _coreRepo.GetAll<Instructor>().Where(stff => stff.User != null && stff.Person != null);

            List<UserInfo> userInfos = staffList.Select(stf => new UserInfo()
            {
                UserId = stf.User.Id,
                Username = stf.User.Username ?? "",
                FullName = stf.Person.FullName ?? "",
                Positions = stf.InstructorPositions.Where(a => a.IsActive).ToList(),
                StaffCourses = stf.InstructorPositions.Where(a => a.IsActive).SelectMany(a => a.StaffCourses).ToList(),
                Person = stf.Person
            }).ToList();

            model.UserInfos = userInfos;
            return View(model);
        }


        [UserTypeAccess(UserType = UserTypes.Instructor)]
        [AuthorizeUser(AccessLevel = AccessLevel.Administration, Service = (int)ServiceDetail.StudentAccounts,
            Permission = ApplicationConstants.CanRead)]
        public ActionResult Students()
        {
            return View();
        }
    }
}