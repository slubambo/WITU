using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using WITU.Core.Model;


namespace WITU.Models.student
{
    public class StudentPrintModel
    {
        public CohortYear IntakeAcademicYear { get; set; }

        public int YearOfStudy { get; set; }
       
        public List<Student> Students { get; set; }
    }
}