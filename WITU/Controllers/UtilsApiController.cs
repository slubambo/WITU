using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.WebPages.Html;
using WITU.Core.Model;
using WITU.Core.Repositories.Interfaces;
using WITU.Filters;
using WITU.Helper.Impls;
using WITU.Helper.Interfaces;
using WITU.Models;
using WITU.Models.UserManagement;
using WITU.UserManagement;
using WITU.UserManagement.Identity;
using WITU.UserManagement.Store;
using WITU.Utils;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using Iesi.Collections.Generic;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Provider;
using Newtonsoft.Json;
using NHibernate.Linq;
using NHibernate.Util;


namespace WITU.Controllers
{
    /// <summary>
    ///     Controller API that contains various utility actions accross the application.
    /// </summary>
    [RoutePrefix("Api/UtilsApi")]
    public class UtilsApiController : ApiController
    {
        private readonly ICoreRepository _coreRepository;
        private readonly IGeneralHelper _generalHelper;
        private readonly IUserManagementRepository _userManagementRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IStudentCourseRepository _studentCourseRepository;
        private readonly ISemesterRepository _semesterRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IStaffRepository _staffRepository;
        
        
        public ApplicationUserManager UserManager { get; private set; }

        public UtilsApiController(ICoreRepository coreRepository, IGeneralHelper generalHelper,
            
            IUserManagementRepository userManagementRepository,
            IStudentRepository studentRepository, IStudentCourseRepository studentCourseRepository, ISemesterRepository semesterRepository, IStaffRepository staffRepository, IAccountRepository accountRepository
            )
        {
            _coreRepository = coreRepository;
            _generalHelper = generalHelper;
            _userManagementRepository = userManagementRepository;
            _studentRepository = studentRepository;
            _semesterRepository = semesterRepository;
            _studentCourseRepository = studentCourseRepository;
            _accountRepository = accountRepository;
            _staffRepository = staffRepository;
            UserManager = new ApplicationUserManager(new UserStore(accountRepository, coreRepository));
             UserManager.UserValidator = new UserValidator<IdentityUser, int>(UserManager)
             {
                 AllowOnlyAlphanumericUserNames = false
             };
        }
        
        #region Information Desk
        [Route("UploadInformationDeskFiles")]
        [HttpPost]
        [AuthorizeUser(AccessLevel = AccessLevel.Campus, Service = (int)ServiceDetail.InformationDesk,
           Permission = ApplicationConstants.CanAddorEdit)]
        public object UploadInformationDeskFiles()
        {
            HttpRequest httpRequest = HttpContext.Current.Request;
            var parentType = (AttachmentType) Enum.Parse(typeof (AttachmentType), httpRequest.Form["type"]);
            var uploadedFiles = new List<Attachment>();

            string folderPath = "";
            switch ((int) parentType)
            {
                case (int) AttachmentType.GeneralInformation:
                    folderPath = ApplicationConstants.InformationDeskAttachmentsFolder;
                    break;
                case (int) AttachmentType.InformationDesk:
                    folderPath = ApplicationConstants.InformationDeskAttachmentsFolder;
                    break;
            }

            if (httpRequest.Files.Any())
            {
                HttpPostedFile httpPostedFile = HttpContext.Current.Request.Files["files[]"];
                if (httpPostedFile != null)
                {
                    string fileNameAssigned = "";

                    bool isFileSaved = _generalHelper.SaveFile(httpPostedFile, folderPath, out fileNameAssigned);

                    string fileType =
                        Path.GetExtension(httpPostedFile.FileName).ToLowerInvariant().Replace(".", "").Trim();
                    bool isImageFile = Attachment.ImageExtensions.Any(x => x.Equals(fileType));
                    //creating thumbnail if its an image...
                    if (isImageFile)
                        _generalHelper.ResizeImage(httpPostedFile, ResourceFolders.Thumbnail,
                            folderPath, fileNameAssigned);

                    if (isFileSaved)
                    {
                        uploadedFiles.Add(new Attachment
                        {
                            DeleteType = "POST",
                            DeleteUrl = "",
                            Name = fileNameAssigned,
                            OriginalFileName = httpPostedFile.FileName,
                            Size = httpPostedFile.ContentLength,
                            FileType = fileType,
                            Url = ApplicationConstants.InformationDeskAttachmentsUrl + fileNameAssigned,
                            ThumbnailUrl = isImageFile
                                ? string.Format("{0}{1}/{2}",
                                    ApplicationConstants.InformationDeskAttachmentsUrl, ResourceFolders.Thumbnail,
                                    fileNameAssigned)
                                : ApplicationConstants.InformationDeskAttachmentsUrl + fileNameAssigned,
                            IsImage = isImageFile,
                            AttachmentType = parentType.ToString()
                        });
                    }
                }
            }

            return new {files = uploadedFiles};
        }

        [Route("DeleteInformationDeskFile")]
        [HttpPost]
        [AuthorizeUser(AccessLevel = AccessLevel.Campus, Service = (int)ServiceDetail.InformationDesk,
           Permission = ApplicationConstants.CanDelete)]
        public bool DeleteInformationDeskFile([FromBody] Attachment fileAttachment)
        {
            if (fileAttachment == null)
                return false;

            //getting the attachment..
            var attachment = (AttachmentType) Enum.Parse(typeof (AttachmentType), fileAttachment.AttachmentType);

            //now lets identify the file and go on to delete..
            string folderPath = "";
            string urlPath = "";
            _generalHelper.GetFolderAndUrlPath(attachment, out folderPath, out urlPath);

            //when its already in the db...
            bool deleteActualFile = true;
            if (fileAttachment.Id > 0)
            {
                switch (attachment)
                {
                    case AttachmentType.GeneralInformation:
                        deleteActualFile = _coreRepository.Delete<GeneralInformationAttachment>(fileAttachment.Id);
                        break;
                    case AttachmentType.InformationDesk:
                        deleteActualFile = _coreRepository.Delete<InformationDeskAttachment>(fileAttachment.Id);
                        break;
                }
            }
            if (!deleteActualFile)
                return false;

            //deleeting files...
            bool isFileDeleted = _generalHelper.DeleteFile(fileAttachment.Name, folderPath);
            if (fileAttachment.IsImage)
                _generalHelper.DeleteFile(fileAttachment.Name,
                    Path.Combine(folderPath, ResourceFolders.Thumbnail.ToString()));

            return isFileDeleted;
        }

        [Route("SaveFileUploadDetails")]
        [HttpPost]
        [AuthorizeUser(AccessLevel = AccessLevel.Campus, Service = (int)ServiceDetail.InformationDesk,
           Permission = ApplicationConstants.CanAddorEdit)]
        public bool SaveFileUploadDetails([FromBody] SaveFileUploadDetails fileUploadDetails)
        {
            bool result = false;
            //getting the elements to upload...
            switch (fileUploadDetails.AttachmentType)
            {
                case AttachmentType.GeneralInformation:
                    var generalInformation = _coreRepository.Get<GeneralInformation>(fileUploadDetails.Id);
                    var attachments = new List<GeneralInformationAttachment>();
                    foreach (Attachment file in fileUploadDetails.Files)
                    {
                        var attach = new GeneralInformationAttachment();
                        if (file.Id > 0)
                            attach = _coreRepository.Get<GeneralInformationAttachment>(file.Id) ??
                                     new GeneralInformationAttachment();
                        if (file.AcademicYear > 0)
                            attach.CohortYear = _coreRepository.Get<CohortYear>(file.AcademicYear);
                        attach.UserFriendlyName = file.FriendlyName;
                        attach.FileName = file.Name;
                        attach.FileType = Path.GetExtension(file.Name).Trim().Replace(".", "").ToLowerInvariant();
                        attach.GeneralInformation = generalInformation;
                        attach.OriginalFileName = file.OriginalFileName;
                        attachments.Add(attach);
                    }

                    result = _coreRepository.SaveOrUpdateAll(attachments);
                    break;

                case AttachmentType.InformationDesk:

                    break;
            }

            return result;
        }

        [Route("Attachments/{type}/{id}/{limit?}")]
        [HttpGet]
        public IEnumerable<Attachment> Attachments(AttachmentType type, int id, int limit = -1)
        {
            string folderPath = "";
            string urlPath = "";

            //get folder and url path...
            _generalHelper.GetFolderAndUrlPath(type, out folderPath, out urlPath);

            var attachments = new List<Attachment>();
            switch (type)
            {
                case AttachmentType.GeneralInformation:
                    attachments.AddRange(
                        _coreRepository.Get<GeneralInformation>(id)
                            .GeneralInformationAttachments.Select(x => new Attachment
                            {
                                Id = x.Id,
                                Name = x.FileName,
                                AttachmentType = type.ToString(),
                                FileType = x.FileType, 
                                //Size = x.
                                OriginalFileName = x.OriginalFileName,
                                FriendlyName = x.UserFriendlyName,
                                AcademicYear = x.CohortYear != null ? x.CohortYear.Id : 0,
                                ThumbnailUrl = Attachment.ImageExtensions.Any(y => y.Equals(x.FileType))
                                    ? string.Format("{0}{1}/{2}", urlPath, ResourceFolders.Thumbnail.ToString(),
                                        x.FileName)
                                    : urlPath + x.FileName,
                                Url = urlPath + x.FileName,
                                IsImage = Attachment.ImageExtensions.Any(y => y.Equals(x.FileType)),
                                AcademicYearName = x.CohortYear != null ? x.CohortYear.Name : ""
                            }));
                    break;
                case AttachmentType.InformationDesk:

                    break;
            }
            if (limit > 0)
                return attachments.Take(limit);

            return attachments;
        }
        #endregion

        [Route("LoadStaffRoles")]
        public string LoadStaffRoles()
        {
            IEnumerable<PositionCategory> staffRoles = _coreRepository.GetAll<PositionCategory>();
            IEnumerable<SelectListItem> loadedroles =
                staffRoles.Select(x => new SelectListItem {Text = x.Name, Value = x.Id.ToString()});
            return JsonConvert.SerializeObject(loadedroles);
        }

        [HttpGet]
        public IEnumerable<SelectListItem> AcademicYears()
        {
            IEnumerable<CohortYear> academicyears = _coreRepository.GetAll<CohortYear>();
            IEnumerable<SelectListItem> LoadedYears =
                academicyears.Select(x => new SelectListItem {Text = x.Name, Value = x.Id.ToString()});
            return LoadedYears;
        }
        
        [Route("GetSemesterProgress")]
        [HttpGet]
        public IEnumerable<object> GetSemesterProgress()
        {
            DateTime today = DateTime.Now;
            Cohort currentSemester = new Cohort();
            currentSemester = _semesterRepository.GetCurrentSemester(today);
            string response = string.Empty;
            if (currentSemester == null)
            {
                return new object[] {false};
            }
            else
            {

                return new object[] {true, GetSemesterProgressSummary(currentSemester, today)};
            }
           
        }
        private SemesterProgressSummary GetSemesterProgressSummary(Cohort sem, DateTime today)
        {
            double _elapsedDays = (today - Convert.ToDateTime(sem.StartDate)).TotalDays;
            double _totalDays = (Convert.ToDateTime(sem.EndDate) - Convert.ToDateTime(sem.StartDate)).TotalDays;
            double _percent = (_elapsedDays / _totalDays) * 100;
            
                return new SemesterProgressSummary() { elapsedDays = (int)_elapsedDays, totalDays = (int)_totalDays, percent = _percent.ToString()};
            

        }

        #region Students Module
        [Route("ResetPassword/{userId}")]
        [HttpPost]
        [AuthorizeUser(AccessLevel = AccessLevel.Campus, Service = (int)ServiceDetail.ResetStudentPassword,
            Permission = ApplicationConstants.CanAddorEdit)]
        public object ResetPassword(int userId)
        {
            var user = _coreRepository.Get<User>(userId);

            if (user != null)
            {
                UserManager.RemovePassword(user.Id);
                IdentityResult result = UserManager.AddPassword(user.Id, ApplicationConstants.DEFAULT_STUDENT_PASSWORD);

                if (result.Succeeded)
                {
                    //Success
                    return true;
                }
                else
                {
                    //Failure
                    return false;
                } 
            }
            
            //If we get here then no user was found 
            return false;

        }

        [HttpPost]
        [Route("ToggleStudentActivation/{studentId}")]
        [AuthorizeUser(AccessLevel = AccessLevel.Campus, Service = (int)ServiceDetail.StudentAccounts,
            Permission = ApplicationConstants.CanAddorEdit)]
        public object ToggleStudentActivation(int studentId)
        {
            var student = _coreRepository.Get<Student>(studentId);
            if (student != null)
            {
                var isLockedOut = student.User.IsLockedOut;
                if (isLockedOut)
                {
                    student.User.IsLockedOut = false;
                }
                else
                {
                    student.User.IsLockedOut = true;
                    student.User.LastActivityDate = DateTime.Now;

                }
               var isToggled = _coreRepository.SaveOrUpdate(student);
                return isToggled;
            }
            return false;
        }


        [HttpPost]
        [Route("CreateStudentAccount/{studentId}")]
        [AuthorizeUser(AccessLevel = AccessLevel.Campus, Service = (int)ServiceDetail.StudentAccounts,
            Permission = ApplicationConstants.CanAddorEdit)]
        public async Task<object> CreateStudentAccount(int studentId)
        {
            var student = _coreRepository.Get<Student>(studentId);
            if (student != null)
            {
                var user = new IdentityUser
                {
                    UserName = student.RegistrationNumber.Trim(),
                    UserType = (int)UserTypes.Student,
                    IsApproved = true,
                    IsFirstTimeUser = true,
                    Email = student.Person.EmailAddress,
                    IsLockedOut = false


                };

                var userAlreadyExists = false;
                User coreUser = _accountRepository.GetUserByName(student.RegistrationNumber.Trim());
                if (coreUser != null)
                {
                    userAlreadyExists = true;
                }


                IdentityResult result = (!userAlreadyExists) ? await UserManager.CreateAsync(user, ApplicationConstants.DEFAULT_STUDENT_PASSWORD) : null;

                if (userAlreadyExists || result.Succeeded)
                {
                    //from here we create prospective student who shall be attached to this user...

                    //User was newly created, set the coreUser to the db User otherwise use the already existing dbUser
                    if (result != null)
                        coreUser = _accountRepository.GetUserByName(student.RegistrationNumber);

                    student.User = coreUser;

                    var isStudentSaved = _coreRepository.SaveOrUpdate(student);
                    if (isStudentSaved)
                    {
                        //Student successfully updated
                        return true;
                    }
                    //If Code reaches here, then the student wasn't successfully updated
                    //Delete any created student user (@Entity User) account since no students(@Entity Student) were updated
                    if (coreUser.Id > 0)
                    {
                        _coreRepository.Delete<User>(coreUser.Id);
                    }


                }
            }
            return false;
        }

        [HttpPost]
        [Route("ToggleStaffActivation/{userId}")]
        [AuthorizeUser(AccessLevel = AccessLevel.Campus, Service = (int)ServiceDetail.StudentAccounts,
            Permission = ApplicationConstants.CanAddorEdit)]
        public object ToggleStaffActivation(int userId)
        {
            var user = _coreRepository.Get<User>(userId);
            if (user != null)
            {
                var isLockedOut = user.IsLockedOut;
                if (isLockedOut)
                {
                    user.IsLockedOut = false;
                }
                else
                {
                    user.IsLockedOut = true;
                    user.LastActivityDate = DateTime.Now;

                }
                var isToggled = _coreRepository.SaveOrUpdate(user);
                return isToggled;
            }
            return false;
        }

        [Route("DeleteRegCourses/{ids}")]
        [HttpPost]
        [AuthorizeUser(AccessLevel = AccessLevel.Campus, Service = (int)ServiceDetail.Registration,
            Permission = ApplicationConstants.CanAddorEdit)]
        public string DeleteRegCourses(string ids)
        {
            string courseIds = ids.Replace("\"", "");
            string[] courseIdsArray = courseIds.Split(Convert.ToChar(","));

            foreach (var studentCourseId in courseIdsArray.Select(c => c.Replace("[", "").Replace("]", "")).Select(studentCourseIdString => Convert.ToInt32(studentCourseIdString.Trim(), CultureInfo.InvariantCulture)))
            {
                _coreRepository.Delete<StudentCourse>(studentCourseId);
            }
            return "";
        }

        [Route("ExemptRegCourses/{ids}")]
        [HttpPost]
        [AuthorizeUser(AccessLevel = AccessLevel.Campus, Service = (int)ServiceDetail.Registration,
            Permission = ApplicationConstants.CanAddorEdit)]
        public string ExemptRegCourses(string ids)
        {
            string courseIds = ids.Replace("\"", "");
            string[] courseIdsArray = courseIds.Split(Convert.ToChar(","));

            foreach (var c in courseIdsArray)
            {
                var studentCourseIdString = c.Replace("[", "").Replace("]", "");

                var studentCourseId = Convert.ToInt32(studentCourseIdString.Trim(), CultureInfo.InvariantCulture);

                var studentCourse = _coreRepository.Get<StudentCourse>(studentCourseId);

                studentCourse.PerformanceTrack = StudentCoursePerformanceTrack.Exempted;

                _coreRepository.SaveOrUpdate(studentCourse);
            }
            return "";
        }

        [Route("AbsRegCourses/{ids}")]
        [HttpPost]
        [AuthorizeUser(AccessLevel = AccessLevel.Campus, Service = (int)ServiceDetail.Registration,
            Permission = ApplicationConstants.CanAddorEdit)]
        public string AbsRegCourses(string ids)
        {
            string courseIds = ids.Replace("\"", "");
            string[] courseIdsArray = courseIds.Split(Convert.ToChar(","));

            foreach (var c in courseIdsArray)
            {
                var studentCourseIdString = c.Replace("[", "").Replace("]", "");

                var studentCourseId = Convert.ToInt32(studentCourseIdString.Trim(), CultureInfo.InvariantCulture);

                var studentCourse = _coreRepository.Get<StudentCourse>(studentCourseId);

                studentCourse.PerformanceTrack = StudentCoursePerformanceTrack.Abs;

                _coreRepository.SaveOrUpdate(studentCourse);
            }
            return "";
        }

        [Route("AuditRegCourses/{ids}")]
        [HttpPost]
        [AuthorizeUser(AccessLevel = AccessLevel.Campus, Service = (int)ServiceDetail.Registration,
            Permission = ApplicationConstants.CanAddorEdit)]
        public string AuditRegCourses(string ids)
        {
            string courseIds = ids.Replace("\"", "");
            string[] courseIdsArray = courseIds.Split(Convert.ToChar(","));

            foreach (var studentCourse in from c in courseIdsArray select c.Replace("[", "").Replace("]", "") into studentCourseIdString select Convert.ToInt32(studentCourseIdString.Trim(), CultureInfo.InvariantCulture) into studentCourseId select _coreRepository.Get<StudentCourse>(studentCourseId))
            {
                studentCourse.PerformanceTrack = StudentCoursePerformanceTrack.Audited;

                _coreRepository.SaveOrUpdate(studentCourse);
            }
            return "";
        }
        
        [HttpPost]
        [Route("ApproveSemesterRegistration/{semesterRegistrationId}")]
        [AuthorizeUser(AccessLevel = AccessLevel.Campus, Service = (int)ServiceDetail.Registration,
            Permission = ApplicationConstants.CanAddorEdit)]
        public string ApproveSemesterRegistration(int semesterRegistrationId)
        {
            var semReg = _coreRepository.Get<CohortRegistration>(semesterRegistrationId);

            if (semReg == null) return "";

            semReg.StudentStatus = RegistrationStatus.ApprovedRegistration;
            if (_coreRepository.SaveOrUpdate(semReg))
            {
                
            };

            return "";
        }
        
        #endregion 

    }
}
