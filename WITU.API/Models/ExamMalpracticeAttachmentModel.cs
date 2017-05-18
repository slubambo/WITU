using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MultipartDataMediaFormatter.Infrastructure;

namespace WITU.API.Models
{
    public class ExamMalpracticeAttachmentModel
    {
        public string ExamMalpracticeId
        {
            get; set;
        }

        public HttpFile File
        {
            get; set;
        }
    }
}