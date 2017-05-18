using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WITU.Core.Model;
using WITU.Core.Repositories.Interfaces;
using WITU.Helper.Impls;
using WITU.Models;
using WITU.UserManagement.Identity;
using HtmlAgilityPack;
using iTextSharp.text;


namespace WITU.Helper.Interfaces
{
    public interface IGeneralHelper
    {
        /// <summary>
        /// Converts a given string in Html format to plain text.
        /// </summary>
        /// <param name="htmlString">Html String to be converted to plain text</param>
        /// <returns></returns>
        string ConvertHtmlToPlainText(string htmlString);

        /// <summary>
        /// Converts a given string in Html format to plain text and returns the substring section from the first character up to predetermined lenght.
        /// </summary>
        /// <param name="htmlString">Html String to convert to plain text.</param>
        /// <param name="summaryLength">The Length of the returned String after Trunacating</param>
        /// <returns></returns>
        string ConvertHtmlToPlainText(string htmlString, int summaryLength);

        void ConvertTo(HtmlNode node, TextWriter outText);

        void ConvertContentTo(HtmlNode node, TextWriter outText);

        /// <summary>
        /// Returns either the description of the enum. If description is not present, then the name of the enum is returned.
        /// </summary>
        /// <param name="value">Enum whose description or name s </param>
        /// <returns>The description of the Enum. If not present the name should be returned!!</returns>
        string GetEnumDescriptionIrName(Enum value);

        /// <summary>
        /// Gets the value of an Enum from the description
        /// </summary>
        /// <typeparam name="T">Type of Enum to return</typeparam>
        /// <param name="description">The description to consider before returning Enum</param>
        /// <returns></returns>
        T GetValueFromDescription<T>(string description);

        /// <summary>
        /// Converts an Enum to a Select List item. Uses the Description Attribute, the value and the string.
        /// This is often used in a dropdown to present values and corresponding Text elements.
        /// </summary>
        /// <typeparam name="T">Type of Enum with variables to use in a drop down</typeparam>
        /// <param name="descriptorAsText">is true by default, meaning that the derscription shall be what is presented, otherwise it is the text itself</param>
        /// <param name="intAsValue">true by default meaning the int value of enum is what is used as the value, otherwise the text is used</param>
        /// <returns></returns>
        IList<SelectListItem> SelectListItemGenerator<T>(bool descriptorAsText = true, bool intAsValue = true)
            where T : struct, IConvertible;

        

        /// <summary>
        /// Used to Save a file to given location or folder. This of course is restricted by the application's maximum upload size requirements
        /// </summary>
        /// <param name="uploadedFile">File to Upload</param>
        /// <param name="folderPath">Path of the folder where to save the file</param>
        /// <param name="isSaved">Parameter to return the upload status</param>
        /// <param name="oldFileName">The (optional) oldFileName which shall be deleted before saving.</param>
        /// <returns></returns>
        
        bool SaveFile(HttpPostedFileBase uploadedFile, string folderPath, out string fileName, string oldFileName = null);

        /// <summary>
        /// Works as the normal SaveFile upload. 
        /// </summary>
        /// <param name="uploadedFile"></param>
        /// <param name="folderPath"></param>
        /// <param name="fileName"></param>
        /// <param name="oldFileName"></param>
        /// <returns></returns>
        bool SaveFile(HttpPostedFile uploadedFile, string folderPath, out string fileName,
            string oldFileName = null);

        /// <summary>
        /// Deletes the file passed it to it fromm the given folder in the folderPath
        /// </summary>
        /// <param name="fileName">Name of the File to delete <b>With Its extension</b></param>
        /// <param name="folderPath">Physical Folder Location Path of the File</param>
        /// <returns></returns>
        bool DeleteFile(string fileName, string folderPath);

        /// <summary>
        /// Retrieves the list of errors generated from validation.
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        List<string> GetErrorList(ModelStateDictionary modelState);

        /// <summary>
        /// Used to rezise images into thumbnails. 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="folderPath"></param>
        /// <param name="newfileName"></param>
        /// <param name="oldFileName"></param>
        void ResizeImage(HttpPostedFileBase file, int width, int height, string folderPath, string newfileName,
            string oldFileName = null);

        /// <summary>
        /// Used to Resize Images
        /// </summary>
        /// <param name="file"></param>
        /// <param name="folderType"></param>
        /// <param name="folderPath"></param>
        /// <param name="newfileName"></param>
        /// <param name="oldFileName"></param>
        void ResizeImage(HttpPostedFileBase file, ResourceFolders folderType, string folderPath, string newfileName,
            string oldFileName = null);

        /// <summary>
        /// Used to Detect Faces and Cropping
        /// </summary>
        /// <param name="file"></param>


        void ResizeImage(HttpPostedFile file, ResourceFolders folderType, string folderPath, string newfileName,
            string oldFileName = null);

        void GetFolderAndUrlPath(AttachmentType attachmentType, out string folderPath, out string urlPath);

        /// <summary>
        /// Converts the string provided to a nullable struct.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        bool ToNullable<T>(string s, out T? t) where T : struct;

        /// <summary>
        /// Used to check the if a photo exists
        /// </summary>
        /// <param name="photoPath">The photo path of the image</param>
        /// <returns></returns>
        bool IsPhotoAvailable(string photoPath);

        /// <summary>
        /// Used to get the StudentPhoto to display
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        string GetStudentProfilePhoto(Student student);

        /// <summary>
        /// Gets the Staff Profile photo to display
        /// </summary>
        /// <param name="staff"></param>
        /// <returns></returns>
        string GetStaffProfilePhoto(Instructor staff);
    }
}
