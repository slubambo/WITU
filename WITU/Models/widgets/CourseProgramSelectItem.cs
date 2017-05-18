using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WITU.Core.Model;

namespace WITU.Models.widgets
{
    public class CourseProgramSelectItem
    {

        public CourseProgramSelectItem()
        {
            
        }

        public CourseProgramSelectItem(IEnumerable<SelectListItem> campusOptions)
        {
            if (YearSemesterPogramSelectItem == null)
            {
                //the semester need to be included, eventually we could set this to null, but for now it is needed...S
                YearSemesterPogramSelectItem = new YearSemesterPogramSelectItem(true, campusOptions);
            }

            if(CourseOptions == null)
                CourseOptions = new List<SelectListItem>();
        }

        
        public YearSemesterPogramSelectItem YearSemesterPogramSelectItem { get; set; }

        [Display(Name = "Course")]
        public int CourseId { get; set; }


        public List<SelectListItem> CourseOptions { get; set; }
    }
}