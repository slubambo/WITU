using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WITU.Core.Model;
using WITU.Models.widgets;

namespace WITU.Models.UserManagement
{
    public class ViewStaffCources
    {
        public Instructor Instructor { get; set; }
        public virtual List<StaffCourse> StaffCourses { get; set; }
    }
    public class StaffCourseModel
    {
        public StaffCourseModel()
        {
            
        }

        public StaffCourseModel(IEnumerable<SelectListItem> campusOptions)
        {
            if (YearSemesterPogramSelectItem == null)
            {
                YearSemesterPogramSelectItem = new YearSemesterPogramSelectItem(true, campusOptions);
            }
        }

        public Instructor Instructor { get; set; }
        public virtual List<StaffCourse> StaffCourses { get; set; }

        public virtual List<Course> Courses { get; set; }

        public YearSemesterPogramSelectItem YearSemesterPogramSelectItem { get; set; }
    }
}