using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace WITU.Utils
{
    public static class ApplicationConstants
    {
        public const string SuccessNotification = "TempSuccess";

        public const string ErrorNotification = "ErrorResult";

        public const string Error404Notification = "Error404";

        public const string SuccessSaveMessage = "Saved Successfully";

        public const string ErrorSaveMessage = "Something went wrong while saving changes";

        public static readonly string InformationDeskResourcesFolder = 
            WebConfigurationManager.AppSettings["InformationDeskResourcesFolder"];

        public const string StaffUserType = "1";

        public const string StudentUserType = "2";

        //Access Levels
        public const string UniversityLevel = "University";
        public const string CampusLevel = "Campus";
        public const string FacultyLevel = "Faculty";
        public const string DepartmentLevel = "Department";
        public const string CourseLevel = "Course";

        //User Permissions 
        public const int CanRead = 1;
        public const int CanCreate = 2;
        public const int CanEdit = 3;
        public const int CanDelete = 4;
        public const int CanAddorEdit = 5;

        //Student Default Password
        public const string DEFAULT_STUDENT_PASSWORD = "123456";


        public const string ProspectiveStudentUserType = "3";

        public const string UserTypeClaim = "usertype";

        public const string RolesClaim = "userroles";

        public const string UserPhotoClaim = "userphoto";

        public const string StudentProgramIdClaim = "studentprogramid";

        public const string StudentSpecialisationIdClaim = "studentspecialisationid";

        public const string UserAccessLevel = "useraccessLevel";

        public static readonly string StudentResourcesFolder = WebConfigurationManager.AppSettings["StudentsResourcesFolder"];

        public static readonly string ProspectiveStudentsResourcesFolder = WebConfigurationManager.AppSettings["ProspectiveStudentsResourcesFolder"]; 

        public static readonly string StaffResourcesFolder = WebConfigurationManager.AppSettings["StaffResourcesFolder"];

        public static readonly string StudentResourcesFolderUrl = WebConfigurationManager.AppSettings["StudentsResourcesFolderUrl"];

        public static readonly string ProspectiveStudentsResourcesFolderUrl = WebConfigurationManager.AppSettings["ProspectiveStudentsResourcesFolderUrl"];

        public static readonly string StaffResourcesFolderUrl = WebConfigurationManager.AppSettings["StaffResourcesFolderUrl"];

        public static readonly string StudentResourcesThumbnailUrl = WebConfigurationManager.AppSettings["StudentsResourcesThumbnailUrl"];

        public static readonly string ProspectiveStudentsResourcesThumbnailUrl = WebConfigurationManager.AppSettings["ProspectiveStudentsResourcesThumbnailUrl"];

        public static readonly string StaffResourcesThumbnailUrl = WebConfigurationManager.AppSettings["StaffResourcesThumbnailUrl"];

        public static readonly string StudentsResourcesThumbnailSmallUrl = WebConfigurationManager.AppSettings["StudentsResourcesThumbnailSmallUrl"];

        public static readonly string ProspectiveStudentsResourcesThumbnailSmallUrl = WebConfigurationManager.AppSettings["StudentsResourcesThumbnailSmallUrl"];

        public static readonly string StaffResourcesThumbnailSmallUrl = WebConfigurationManager.AppSettings["StaffResourcesThumbnailSmallUrl"];








        public static readonly string InformationDeskAttachmentsFolder =
            WebConfigurationManager.AppSettings["InformationDeskAttachmentsFolder"];

        public static readonly string MessengerAttachmentsFolder =
            WebConfigurationManager.AppSettings["MessengerAttachmentsFolder"];

        public static readonly string InformationDeskAttachmentsUrl =
            WebConfigurationManager.AppSettings["InformationDeskAttachmentsUrl"];

        public static readonly string MessengerAttachmentsUrl =
            WebConfigurationManager.AppSettings["MessengerAttachmentsUrl"];

        public static readonly string ProfilePhotoAvartarFolderPath =
            WebConfigurationManager.AppSettings["ProfilePhotoAvartarFolderPath"];

        public static readonly string ProfilePhotoAvartarUrl =
            WebConfigurationManager.AppSettings["ProfilePhotoAvartarUrl"];

        public static readonly string ProfilesFolderPath = WebConfigurationManager.AppSettings["ProfilesFolderPath"];

        public static readonly string ProfilesUrl = WebConfigurationManager.AppSettings["ProfilesUrl"];

        public static readonly string DocumentsFolderPath = WebConfigurationManager.AppSettings["DocumentsFolderPath"];

        public static readonly string DocumentsFolderUrl = WebConfigurationManager.AppSettings["DocumentsFolderUrl"];

        public static readonly string ImgAvator = "avatar.jpg";

        public static readonly string ThumbnailAvator = "avatarThumbnail.jpg";

        public static readonly string UniversityLogoForSlips = "20140602151808955_NDU.png";

        public static readonly string NoBankLogo = "noBankLogo.jpg";

        //finance related stuff...
        public static readonly string FinanceBankFolder = WebConfigurationManager.AppSettings["FinanceBankFolderPath"];

        public static readonly string FinanceBankUrl = WebConfigurationManager.AppSettings["FinanceBankUrl"];


        //Reading course content from Version 1 resources folder;
        public static readonly string ArmsV1CourseContentFolder =
            WebConfigurationManager.AppSettings["V1CourseContentFolder"];
    }
}