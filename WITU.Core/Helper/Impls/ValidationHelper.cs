using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WITU.Core.Helper.Interfaces;

namespace WITU.Core.Helper.Impls
{
    public class ValidationHelper : IValidationHelper
    {
        public bool ValidatePhotoUploaded(System.Web.HttpPostedFileBase uploadedFile)
        {
            if (uploadedFile == null)
                return false;

            if (uploadedFile.ContentLength <= 0)
                return false;

            var imageContentTypes = new string[] { "image/png", "image/JPEG" };
            var imageFileExtentions = new string[] { ".png", ".jpg", ".jpeg" };
            var isImage = imageContentTypes.Any(x => x.Equals(uploadedFile.ContentType, StringComparison.CurrentCultureIgnoreCase));
            var isValidExtention = imageFileExtentions.Any(x => x.Equals(System.IO.Path.GetExtension(uploadedFile.FileName) ?? "",
                    StringComparison.CurrentCultureIgnoreCase));
            if (!isImage || !isValidExtention) //if not a valid extention or content type is not image...
                return false;
            if (uploadedFile.ContentLength > 2096 * 1024)//greater than 4MB
                return false;

            return true;
        }

        public bool ValidateAvailabilityOfFile(HttpPostedFileBase uploadedFile)
        {
            if (uploadedFile == null)
                return false;
            return uploadedFile.ContentLength > 0;
        }

        public bool ValidateIsPhoto(HttpPostedFileBase uploadedFile)
        {
            if (uploadedFile == null)
                return false;
            else if (uploadedFile.ContentLength <= 0)
                return false;
            
            //now validating the actual photo...
            var imageContentTypes = new string[] { "image/png", "image/JPEG" };
            var imageFileExtentions = new string[] { ".png", ".jpg", ".jpeg" };
            var isImage = imageContentTypes.Any(x => x.Equals(uploadedFile.ContentType, StringComparison.CurrentCultureIgnoreCase)) 
                || uploadedFile.ContentType.Contains("image/"); // last condition adds support for images in IE8 as they use formats of image/x-png or image/pjpeg
            if (!isImage)
                return false;

            var isValidExtention = imageFileExtentions.Any(x => x.Equals(System.IO.Path.GetExtension(uploadedFile.FileName) ?? "",
                    StringComparison.CurrentCultureIgnoreCase));
            if (!isValidExtention)
                return false;

            return true;
        }

        public bool ValidateFileUploadSize(HttpPostedFileBase uploadedFile)
        {
            if (uploadedFile == null)
                return false;

            if (uploadedFile.ContentLength <= 0)
                return false;

            if (uploadedFile.ContentLength > 2096 * 1024)//greater than 4MB
                return false;

            return true;
        }

        public bool ValidateIsExcelFile(HttpPostedFileBase uploadedFile)
        {
            if (ValidateAvailabilityOfFile(uploadedFile))
            {
                var expectedExtenstions = new List<string> { ".xlsx" };
                if (expectedExtenstions.Contains(Path.GetExtension(uploadedFile.FileName).ToLower()))
                    return true;
            }
            return false;
        }
    }

    
}
