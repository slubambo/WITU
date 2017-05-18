using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WITU.API.Models
{
    public class CourseModel
    {
        public int Id { get; set; }


        public string Code { get; set; }

       
        public int Semester { get; set; }

        public string Name { get; set; }

        
        public int? CreditUnits { get; set; }

        public string AcademicYear { get; set; }

        public string ProgramName { get; set; }
        public string Campus { get; set; }

        public int InvigilatorCourseId { get; set; }
        


    }
}