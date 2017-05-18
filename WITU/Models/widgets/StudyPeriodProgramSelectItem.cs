using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WITU.Models.widgets;

namespace WITU.Models.widgets
{
    public class StudyPeriodProgramSelectItem
    {
        public StudyPeriodProgramSelectItem()
        {
            
        }

        public StudyPeriodProgramSelectItem(IEnumerable<SelectListItem> campusOptions) : this(true, campusOptions)
        {
            
        }

        public StudyPeriodProgramSelectItem(bool includeSemester, IEnumerable<SelectListItem> campusOptions)
        {
            if (YearSemesterPogramSelectItem == null)
            {
                YearSemesterPogramSelectItem = new YearSemesterPogramSelectItem(includeSemester, campusOptions);
            }
        }
         
        public YearSemesterPogramSelectItem YearSemesterPogramSelectItem { get; set; }

        [Display(Name = "Study Period")]
        public int StudyPeriod { get; set; }

        public List<SelectListItem> StudyPeriodOptions { get; set; }


    }
}