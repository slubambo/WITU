using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MultipartDataMediaFormatter.Infrastructure;

namespace WITU.API.Models
{
    public class ExamMalpracticeModel
    {
        public String Comments
        {
            get;
            set;
        }

        public String Title
        {
            get;
            set;
        }

        public long CreatedOn
        {
            get;
            set;
        }

        public long CourseId
        {
            get;
            set;
        }

    }
}