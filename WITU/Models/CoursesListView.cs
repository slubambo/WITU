using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WITU.Core.Model;

namespace WITU.Models
{
    public class CoursesListView
    {
        public IEnumerable<Course> Courses { get; set; }

        public int ProgramId { get; set; }

        public int SpecialisationId { get; set; }

        public int Year { get; set; }

        public int Semester { get; set; }

        public bool EnableAddCourse { get; set; }
    }
}