using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WITU.Models.widgets
{
    public class StudyPeriodEntryYearProgramSelectItem
    {
        public StudyPeriodEntryYearProgramSelectItem()
        {
            
        }

        public StudyPeriodEntryYearProgramSelectItem(IEnumerable<SelectListItem> campusOptions) : this(true, campusOptions)
        {
            
        }

        public StudyPeriodEntryYearProgramSelectItem(bool includeSemester, IEnumerable<SelectListItem> campusOptions)
        {
            if (YearSemesterPogramSelectItem == null)
            {
                YearSemesterPogramSelectItem = new EntryYearSemesterProgramSelectItem(includeSemester, campusOptions);
            }
        }
         
        public EntryYearSemesterProgramSelectItem YearSemesterPogramSelectItem { get; set; }

        [Display(Name = "Study Period")]
        public int StudyPeriod { get; set; }

        public List<SelectListItem> StudyPeriodOptions { get; set; }
    }
}