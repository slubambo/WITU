using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace WITU.API.Utils
{
    public static class ApplicationConstants
    {
        public const string SuccessNotification = "TempSuccess";

        public const string ErrorNotification = "ErrorResult";

        public const string SuccessSaveMessage = "Saved Successfully";

        public const string ErrorSaveMessage = "Something went wrong while saving changes";

        public static readonly string InformationDeskResourcesFolder = 
            WebConfigurationManager.AppSettings["InformationDeskResourcesFolder"];

        public const string UserTypeClaim = "usertype";

        public const string UserPhotoClaim = "userphoto";

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

        public static readonly string InformationDeskAttachmentsUrl =
            WebConfigurationManager.AppSettings["InformationDeskAttachmentsUrl"];

        public static readonly string ProfilePhotoAvartarFolderPath =
            WebConfigurationManager.AppSettings["ProfilePhotoAvartarFolderPath"];

        public static readonly string ProfilePhotoAvartarUrl =
            WebConfigurationManager.AppSettings["ProfilePhotoAvartarUrl"];

        public static readonly string ProfilesFolderPath = WebConfigurationManager.AppSettings["ProfilesFolderPath"];

        public static readonly string ProfilesUrl = WebConfigurationManager.AppSettings["ProfilesUrl"];

        public static readonly string ImgAvator = "avatar.jpg";

        public static readonly string ThumbnailAvator = "avatarThumbnail.jpg";

        //finance related stuff...
        public static readonly string FinanceBankFolder = WebConfigurationManager.AppSettings["FinanceBankFolderPath"];

        public static readonly string FinanceBankUrl = WebConfigurationManager.AppSettings["FinanceBankUrl"];

    }
}