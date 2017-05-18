using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WITU.Core.Model;

namespace WITU.Models.widgets
{
    public enum FacultyDisplay
    {
        All,
        Campus,
        Faculty,
        Department,
        Program,
        Specialisation,
        YearOfStudy,
        StudyTimes
    }

    /// <summary>
    /// This Model holds the view of a group drop down selector which presents and implements changes pertaining to selections made in sequence from 
    /// Campus > Faculties > Departments > Programs > Specialisations > Year Of Study > Sttudy Period.
    /// <br/>
    /// It is important to note that <b>Whatever changes made may affect the views using the parital View FacultySelectItem.cshtml</b>. So be
    /// very careful how and what you use in this presentation.
    /// </summary>
    public class FacultySelectItem
    {

        public const string DefaultFirstItemText = "-- All --";

        public FacultySelectItem()
        {
            if(FacultyOptions == null)
                FacultyOptions = new List<SelectListItem>();
 
            if(ItemsToDisplay == null)
                ItemsToDisplay = new List<FacultyDisplay>()
                {
                    FacultyDisplay.Campus, FacultyDisplay.Faculty 
                };

            if (DefaultFirstItem == null)
                DefaultFirstItem = new SelectListItem() {Text = DefaultFirstItemText, Value = "0"};

            StaffId = 0;
        }

        [Display(Name = "Campus")]
        public int CampusId { get; set; }

        
        public int StaffId { get; set; }

        [Display(Name = "Faculty")]
        public int FacultyId { get; set; }
 
        public IEnumerable<SelectListItem> CampusOptions { get; set;}

        public List<SelectListItem> FacultyOptions { get; set; }

        public List<SelectListItem> DepartmentOptions { get; set; }

        public SelectListItem DefaultFirstItem { get; set; }

        /// <summary>
        /// Presents a collection of items to Display in the form.
        /// </summary>
        public List<FacultyDisplay> ItemsToDisplay { get; set; } 
    
    }
}