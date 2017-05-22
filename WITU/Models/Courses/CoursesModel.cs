using System.Collections.Generic;
using WITU.Core.Model;

namespace WITU.Models.Courses
{
    public class CoursesModel
    {
        public IEnumerable<CourseModel> AllCourses { get; set; }

        public IEnumerable<Campus> Campuses { get; set; }
    }

    public class CourseModel
    {
        public Course Course { get; set; }

        public string Overview { get; set; }
    }

}