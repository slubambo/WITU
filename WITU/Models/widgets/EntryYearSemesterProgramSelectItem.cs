using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WITU.Core.Model;

namespace WITU.Models.widgets
{
    public class EntryYearSemesterProgramSelectItem : YearSemesterPogramSelectItem
    {
        public EntryYearSemesterProgramSelectItem() : base()
        {

        }

        public EntryYearSemesterProgramSelectItem(bool includeSemester,
            IEnumerable<SelectListItem> campusOptions):base(includeSemester, campusOptions)
        {
        }

        [Display(Name = "Year of Entry")]
        public new int  Year { get; set; }
    }
}