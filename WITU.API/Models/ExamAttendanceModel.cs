using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WITU.API.Models
{
    public class ExamAttendanceModel
    {
        public long ExamPermitId
        {
            get;
            set;
        }

        public long CourseId
        {
            get;
            set;
        }

        public long CreatedOn
        {
            get;
            set;
        }
    }
}