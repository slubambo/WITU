using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WITU.Core.Helper.Interfaces
{
    public interface IValidationHelper
    {
        /// <summary>
        /// Is used to validate the uploaded file to see if its a photo
        /// </summary>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        bool ValidatePhotoUploaded(HttpPostedFileBase uploadedFile);

        /// <summary>
        /// Validates if the uploaded file is present and is no 0Bytes
        /// </summary>
        /// <param name="uploadedFile">The Uploade File to Validate</param>
        /// <returns>True if Available</returns>
        bool ValidateAvailabilityOfFile(HttpPostedFileBase uploadedFile);

        /// <summary>
        /// Validates if the uploaded file is a valid photo. Does this by checking the photo extention and content type,
        /// ofcourse after ensuring that the the upload is not null or with content length of 0.
        /// </summary>
        /// <param name="uploadedFile">The Uploaded File</param>
        /// <returns>True for valid photo, false otherwise.</returns>
        bool ValidateIsPhoto(HttpPostedFileBase uploadedFile);

        /// <summary>
        /// Validates if the uploaded file is less than <b>0 bytes</b> or greater than <b>4 MB</b>
        /// </summary>
        /// <param name="uploadedFile">The File to upload</param>
        /// <returns>True if validation passes, false otherwise</returns>
        bool ValidateFileUploadSize(HttpPostedFileBase uploadedFile);

        /// <summary>
        /// Validates an exisitance of an excel file.
        /// </summary>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        bool ValidateIsExcelFile(HttpPostedFileBase uploadedFile);
    }
}
