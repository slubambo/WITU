using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WITU.Core.Model;

namespace WITU.Models.widgets
{
    public class YearSemesterPogramSelectItem
    {
        public YearSemesterPogramSelectItem()
        {
            
        }

        public YearSemesterPogramSelectItem(bool includeSemester, 
            IEnumerable<SelectListItem> campusOptions)
        {
            IncludeSemester = includeSemester;

            if (ProgramSelectItem == null)
            {
                ProgramSelectItem = new ProgramSelectItem(campusOptions);
            }

            if(YearOptions == null)
                YearOptions = new List<SelectListItem>();
        }

        public ProgramSelectItem ProgramSelectItem { get; set; }

        [Display(Name = "Year of Study")]
        public int Year { get; set; }

        [Display(Name = "Semester")]
        public int Semester { get; set; }

        public List<SelectListItem> YearOptions { get; set; }

        public bool IncludeSemester { get; set; }
    }
}