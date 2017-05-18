using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WITU.Models
{
    public enum AttachmentType
    {
        InformationDesk,

        GeneralInformation,


    }


    public enum FileTypeExtention
    {
        jpg, jpeg, png, pdf, doc, docx, xls, xlsx, ppt,pptx
    }
    public class UploadAttachment
    {
        public int Id { get; set; }

        public AttachmentType Type { get; set; }

        public List<Attachment> Attachments { get; set; }

        public List<SelectListItem> AcademicYears { get; set; }

        public int TempHolder { get; set; }
    }

    public class UploadRequest
    {
        public AttachmentType Type { get; set; }

        public IList<HttpPostedFileBase> Files { get; set; } 
    }

    public class Attachment
    {
        public static readonly string[] ImageExtensions = new string[]{"jpg", "png", "jpeg"};

        public int Id { get; set; }

        /// <summary>
        /// The Extension of the File, such as jpg, png, doc, etc. Usually is lower case.
        /// </summary>
        public string FileType { get; set; }

        /// <summary>
        /// One which Identifies which kind of file it belongs to, that is, the file can belong to a news/event, General Information 
        /// or even a Calendar.
        /// </summary>
        public string AttachmentType { get; set; }

        public string FolderPath { get; set; }

        public int Size { get; set; }

        public int SizeKb { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public string ThumbnailUrl { get; set; }

        public string DeleteUrl { get; set; }

        public string DeleteType { get; set; }

        public string OriginalFileName { get; set; }

        public bool IsImage { get; set; }

        public int AcademicYear { get; set; }

        public string FriendlyName { get; set; }

        public string AcademicYearName { get; set; }
    }

    public class SaveFileUploadDetails
    {
        public AttachmentType AttachmentType { get; set; }

        public int Id { get; set; }

        public IEnumerable<Attachment> Files { get; set; }
    }

}