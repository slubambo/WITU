using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using WITU.Core.Model;
using WITU.Helper.Interfaces;
using WITU.Models;
using WITU.Utils;
using FluentNHibernate.Conventions;
using HtmlAgilityPack;
using Itenso.TimePeriod;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Date = WITU.Models.Date;
using Image = System.Drawing.Image;
using Rectangle = System.Drawing.Rectangle;


namespace WITU.Helper.Impls
{
    public enum ResourceFolders
    {
        Normal,
        Thumbnail,
        ThumbnailSmall
    }

    public class GeneralHelper : IGeneralHelper
    {
        private readonly IGeneralHelper _generalHelper;


        public GeneralHelper()
        {
            _generalHelper = this;
        }

        public string ConvertHtmlToPlainText(string htmlString)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(htmlString);

            var sw = new StringWriter();
            _generalHelper.ConvertTo(doc.DocumentNode, sw);
            sw.Flush();
            return sw.ToString().Trim();
        }

        public string ConvertHtmlToPlainText(string htmlString, int summaryLenght)
        {
            var summary = _generalHelper.ConvertHtmlToPlainText(htmlString);
            if (summary.Length <= summaryLenght)
                return summary;
            return summary.Substring(0, summaryLenght);
        }

        public void ConvertTo(HtmlNode node, TextWriter outText)
        {
            string html;
            switch (node.NodeType)
            {
                case HtmlNodeType.Comment:
                    //do nothing;
                    break;
                case HtmlNodeType.Document:
                    _generalHelper.ConvertContentTo(node, outText);
                    break;
                case HtmlNodeType.Text:
                    //ignore script and style..
                    string parentName = node.ParentNode.Name;
                    if ((parentName == "script") || parentName == "style")
                        break;
                    //get text..
                    html = ((HtmlTextNode) node).Text;

                    if (HtmlNode.IsOverlappedClosingElement(html))
                        break;
                    //check if text is meaning ful
                    if (html.Trim().Length > 0)
                        outText.Write(HtmlEntity.DeEntitize(html));
                    break;

                case HtmlNodeType.Element:
                    switch (node.Name)
                    {
                        case "p":
                            //treating paragrh aas crlf..
                            outText.Write("\r\n");
                            break;
                    }

                    if (node.HasChildNodes)
                    {
                        _generalHelper.ConvertContentTo(node, outText);
                    }
                    break;
            }
        }

        public void ConvertContentTo(HtmlNode node, TextWriter outText)
        {
            foreach (HtmlNode subNode in node.ChildNodes)
            {
                _generalHelper.ConvertTo(subNode, outText);
            }
        }

        public string GetEnumDescriptionIrName(Enum value)
        {
            try
            {
                FieldInfo fi = value.GetType().GetField(value.ToString());
                var attributes = (DescriptionAttribute[]) fi.GetCustomAttributes(typeof (DescriptionAttribute), false);
                return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
            }
            catch (Exception)
            {
                return value.ToString();
            }
        }

        public T GetValueFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }
            //throw new ArgumentException("Not found.", "description");
             return default(T);
        }

        public IList<SelectListItem> SelectListItemGenerator<T>(bool descriptorAsText = true, bool intAsValue = true)
            where T : struct, IConvertible
        {
            if (!typeof (T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            //now doing the rest of the work...
            IEnumerable<Enum> listElments = Enum.GetValues(typeof (T)).Cast<Enum>();
            return listElments.Select(a => new SelectListItem
            {
                Text = descriptorAsText ? _generalHelper.GetEnumDescriptionIrName(a) : a.ToString(),
                Value = intAsValue ? (Convert.ToInt32(a)).ToString() : a.ToString()
            }).ToList();
        }

        public bool SaveFile(HttpPostedFileBase uploadedFile, string folderPath, out string fileName,
            string oldFileName = null)
        {
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            if (!string.IsNullOrEmpty(oldFileName) && !(oldFileName.Trim().ToLower().Equals("avatar.jpg")))
                _generalHelper.DeleteFile(oldFileName, folderPath);

            //saving the file given to us...
            if (uploadedFile == null)
            {
                fileName = null;
                return false;
            }

            if (uploadedFile.ContentLength <= 0)
            {
                fileName = null;
                return false;
            }

            if (!Directory.Exists(folderPath))
            {
                // If it doesn't exist, create the directory
                Directory.CreateDirectory(folderPath);
            }

            fileName = Path.GetFileName(DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + uploadedFile.FileName);
            string path = Path.Combine(folderPath, fileName);
            uploadedFile.SaveAs(path); //where the actual saving is done...

            return true;
        }

        public bool SaveFile(HttpPostedFile uploadedFile, string folderPath, out string fileName,
            string oldFileName = null)
        {
            //create directory if it doesn't exisit...
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            if (!string.IsNullOrEmpty(oldFileName) && !(oldFileName.Trim().ToLower().Equals("avatar.jpg")))
                _generalHelper.DeleteFile(oldFileName, folderPath);

            //saving the file given to us...
            if (uploadedFile == null)
            {
                fileName = null;
                return false;
            }

            if (uploadedFile.ContentLength <= 0)
            {
                fileName = null;
                return false;
            }

            fileName = Path.GetFileName(DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + uploadedFile.FileName);
            string path = Path.Combine(folderPath, fileName);
            uploadedFile.SaveAs(path); //where the actual saving is done...

            return true;
        }

        public bool DeleteFile(string fileName, string folderPath)
        {
            string path = Path.Combine(folderPath, fileName);

            if (File.Exists(Path.GetFullPath(path)))
            {
                File.Delete(Path.GetFullPath(path));
                return true;
            }
            return false;
        }

        private static void RemoveFile(string path)
        {
            // Check if our file exists
            if (File.Exists(Path.GetFullPath(path)))
            {
                // Delete our file
                File.Delete(Path.GetFullPath(path));
            }
        }

        public void ResizeImage(HttpPostedFileBase file, int width, int height, string folderPath, string newfileName,
            string oldFileName = null)
        {
            char dirSeparator = Path.DirectorySeparatorChar;
            string studentProfilePhotoDirectory = String.Format(@"{0}{1}{2}", folderPath, dirSeparator);

            // Check if the directory we are saving to exists
            if (!Directory.Exists(studentProfilePhotoDirectory))
            {
                // If it doesn't exist, create the directory
                Directory.CreateDirectory(studentProfilePhotoDirectory);
            }

            //delete old thumbnail, if present...
            if (!string.IsNullOrEmpty(oldFileName) && !(oldFileName.Trim().ToLower().Equals("avatar.jpg")))
                _generalHelper.DeleteFile(oldFileName, studentProfilePhotoDirectory);


            // Final path we will save our thumbnail
            string imagePath = String.Format(@"{0}{1}{2}", studentProfilePhotoDirectory, dirSeparator, newfileName);
            // Create a stream to save the file to when we're done resizing
            var stream = new FileStream(Path.GetFullPath(imagePath), FileMode.OpenOrCreate);

            // Convert our uploaded file to an image
            Image OrigImage = Image.FromStream(file.InputStream);
            // Create a new bitmap with the size of our thumbnail
            var TempBitmap = new Bitmap(width, height);

            // Create a new image that contains are quality information
            Graphics NewImage = Graphics.FromImage(TempBitmap);
            NewImage.CompositingQuality = CompositingQuality.HighQuality;
            NewImage.SmoothingMode = SmoothingMode.HighQuality;
            NewImage.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //Used to crop while maitaininig Aspect Ratio
            double ratioX = (double) width/OrigImage.Width;
            double ratioY = (double) height/OrigImage.Height;
            double ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int) (OrigImage.Width*ratio);
            var newHeight = (int) (OrigImage.Height*ratio);

            // Create a rectangle and draw the image
            var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
            NewImage.DrawImage(OrigImage, imageRectangle);

            // Save the final file
            TempBitmap.Save(stream, OrigImage.RawFormat);

            // Clean up the resources
            NewImage.Dispose();
            TempBitmap.Dispose();
            OrigImage.Dispose();
            stream.Close();
            stream.Dispose();
        }


        // HttpPostedFileBase Method
        public void ResizeImage(HttpPostedFileBase file, ResourceFolders folderType, string folderPath,
            string newfileName,
            string oldFileName = null)
        {
            char dirSeparator = Path.DirectorySeparatorChar;
            string thumbnailDirectory = String.Format(@"{0}{1}{2}", folderPath, dirSeparator, folderType);

            // Check if the directory we are saving to exists
            if (!Directory.Exists(thumbnailDirectory))
            {
                // If it doesn't exist, create the directory
                Directory.CreateDirectory(thumbnailDirectory);
            }

            //delete old thumbnail, if present...
            if (!string.IsNullOrEmpty(oldFileName) && !(oldFileName.Trim().ToLower().Equals("avatar.jpg")))
                _generalHelper.DeleteFile(oldFileName, thumbnailDirectory);


            // Final path we will save our thumbnail
            string imagePath = String.Format(@"{0}{1}{2}", thumbnailDirectory, dirSeparator, newfileName);
            // Create a stream to save the file to when we're done resizing
            var stream = new FileStream(Path.GetFullPath(imagePath), FileMode.OpenOrCreate);

            int width = 0;
            int height = 0;
            switch (folderType)
            {
                case ResourceFolders.Thumbnail:
                    width = 110;
                    height = 110;
                    break;
                case ResourceFolders.ThumbnailSmall:
                    width = 50;
                    height = 50;
                    break;
            }

            // Convert our uploaded file to an image
            Image OrigImage = Image.FromStream(file.InputStream);
            // Create a new bitmap with the size of our thumbnail
            var TempBitmap = new Bitmap(width, height);

            // Create a new image that contains are quality information
            Graphics NewImage = Graphics.FromImage(TempBitmap);
            NewImage.CompositingQuality = CompositingQuality.HighQuality;
            NewImage.SmoothingMode = SmoothingMode.HighQuality;
            NewImage.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // Create a rectangle and draw the image
            var imageRectangle = new Rectangle(0, 0, width, height);
            NewImage.DrawImage(OrigImage, imageRectangle);

            // Save the final file
            TempBitmap.Save(stream, OrigImage.RawFormat);

            // Clean up the resources
            NewImage.Dispose();
            TempBitmap.Dispose();
            OrigImage.Dispose();
            stream.Close();
            stream.Dispose();
        }


        // HttpPostedFile Method
        public void ResizeImage(HttpPostedFile file, ResourceFolders folderType, string folderPath, string newfileName,
            string oldFileName = null)
        {
            char dirSeparator = Path.DirectorySeparatorChar;
            string thumbnailDirectory = String.Format(@"{0}{1}{2}", folderPath, dirSeparator, folderType);

            // Check if the directory we are saving to exists
            if (!Directory.Exists(thumbnailDirectory))
            {
                // If it doesn't exist, create the directory
                Directory.CreateDirectory(thumbnailDirectory);
            }

            //delete old thumbnail, if present...
            if (!string.IsNullOrEmpty(oldFileName) && !(oldFileName.Trim().ToLower().Equals("avatar.jpg")))
                _generalHelper.DeleteFile(oldFileName, thumbnailDirectory);


            // Final path we will save our thumbnail
            string imagePath = String.Format(@"{0}{1}{2}", thumbnailDirectory, dirSeparator, newfileName);
            // Create a stream to save the file to when we're done resizing
            var stream = new FileStream(Path.GetFullPath(imagePath), FileMode.OpenOrCreate);

            int width = 0;
            int height = 0;
            switch (folderType)
            {
                case ResourceFolders.Thumbnail:
                    width = 110;
                    height = 110;
                    break;
                case ResourceFolders.ThumbnailSmall:
                    width = 50;
                    height = 50;
                    break;
            }

            // Convert our uploaded file to an image
            Image OrigImage = Image.FromStream(file.InputStream);
            // Create a new bitmap with the size of our thumbnail
            var TempBitmap = new Bitmap(width, height);

            // Create a new image that contains are quality information
            Graphics NewImage = Graphics.FromImage(TempBitmap);
            NewImage.CompositingQuality = CompositingQuality.HighQuality;
            NewImage.SmoothingMode = SmoothingMode.HighQuality;
            NewImage.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // Create a rectangle and draw the image
            var imageRectangle = new Rectangle(0, 0, width, height);
            NewImage.DrawImage(OrigImage, imageRectangle);

            // Save the final file
            TempBitmap.Save(stream, OrigImage.RawFormat);

            // Clean up the resources
            NewImage.Dispose();
            TempBitmap.Dispose();
            OrigImage.Dispose();
            stream.Close();
            stream.Dispose();
        }

        public List<string> GetErrorList(ModelStateDictionary modelState)
        {
            IEnumerable<string> query = from state in modelState.Values
                from error in state.Errors
                select error.ErrorMessage;
            List<string> errorList = query.ToList();
            return errorList;
        }

        public void GetFolderAndUrlPath(AttachmentType attachmentType, out string folderPath, out string urlPath)
        {
            switch (attachmentType)
            {
                case AttachmentType.GeneralInformation:
                    folderPath = ApplicationConstants.InformationDeskAttachmentsFolder;
                    urlPath = ApplicationConstants.InformationDeskAttachmentsUrl;
                    break;

                case AttachmentType.InformationDesk:
                    folderPath = ApplicationConstants.InformationDeskAttachmentsFolder;
                    urlPath = ApplicationConstants.InformationDeskAttachmentsUrl;
                    break;
                default:
                    folderPath = null;
                    urlPath = null;
                    break;
            }
        }

        public bool ToNullable<T>(string s, out T? t) where T : struct
        {
            Nullable<T> result = new Nullable<T>();
            try
            {
                if (!string.IsNullOrEmpty(s) && s.Trim().Length > 0)
                {
                    TypeConverter conv = TypeDescriptor.GetConverter(typeof (T));
                    result = (T) conv.ConvertFrom(s);
                }
                t = result;
                return true;
            }
            catch
            {
                t = result;
                return false;
            }
            
        }

        public bool IsPhotoAvailable(string photoPath)
        {
            var exists = false;
            try
            {
                
                var request = (HttpWebRequest)WebRequest.Create(photoPath);
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    exists = response.StatusCode == HttpStatusCode.OK;
                }
            }
            catch
            {
            }

            return exists;
        }

        public string GetStudentProfilePhoto(Student student)
        {
            var studentProfilePhotoName = string.Empty;

            if (!string.IsNullOrEmpty(student.Person.ProfilePhotoName))
                studentProfilePhotoName = student.Person.ProfilePhotoName;

            if (string.IsNullOrEmpty(studentProfilePhotoName) && !string.IsNullOrEmpty(student.User.ProfilePhotoName))
                studentProfilePhotoName = student.User.ProfilePhotoName;

            bool exists = false;
            string photoPath = null;
            string existingProfilePhoto = null;
            string avartarUrl = ApplicationConstants.ProfilesUrl + ApplicationConstants.ImgAvator;

            if (!string.IsNullOrEmpty(studentProfilePhotoName))
            {

                try
                {
                    photoPath = Path.Combine(ApplicationConstants.StudentResourcesFolderUrl,
                        studentProfilePhotoName)
                        .Replace("\\", "/");

                    var request = (HttpWebRequest)WebRequest.Create(photoPath);
                    using (var response = (HttpWebResponse)request.GetResponse())
                    {
                        exists = response.StatusCode == HttpStatusCode.OK;
                    }
                }
                catch
                {
                }

                existingProfilePhoto = exists ? photoPath : avartarUrl;
            }

            else
            {
                existingProfilePhoto = avartarUrl;
            }

            return existingProfilePhoto;
        }

        public string GetStaffProfilePhoto(Instructor staff)
        {
            var staffProfilePhotoName = string.Empty;

            if (!string.IsNullOrEmpty(staff.Person.ProfilePhotoName))
                staffProfilePhotoName = staff.Person.ProfilePhotoName;

            if (string.IsNullOrEmpty(staffProfilePhotoName) && !string.IsNullOrEmpty(staff.User.ProfilePhotoName))
                staffProfilePhotoName = staff.User.ProfilePhotoName;

            bool exists = false;
            string photoPath = null;
            string existingProfilePhoto = null;
            string avartarUrl = ApplicationConstants.ProfilesUrl + ApplicationConstants.ImgAvator;

            if (!string.IsNullOrEmpty(staffProfilePhotoName))
            {

                try
                {
                    photoPath = Path.Combine(ApplicationConstants.StaffResourcesFolderUrl,
                        staffProfilePhotoName)
                        .Replace("\\", "/");

                    var request = (HttpWebRequest)WebRequest.Create(photoPath);
                    using (var response = (HttpWebResponse)request.GetResponse())
                    {
                        exists = response.StatusCode == HttpStatusCode.OK;
                    }
                }
                catch
                {
                }

                existingProfilePhoto = exists ? photoPath : avartarUrl;
            }

            else
            {
                existingProfilePhoto = avartarUrl;
            }

            return existingProfilePhoto;
        }
    }
}