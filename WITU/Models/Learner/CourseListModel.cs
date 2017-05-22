using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WITU.Core.Model;

namespace WITU.Models.Learner
{
    public class CourseListModel
    {
        public CourseListModel() {
            if (CourseList == null) {
                CourseList = new List<Course>();
            }
        }

        [Display(Name = "Courses")]
        public List<Course> CourseList { get; set; }
    }
}