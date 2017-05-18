using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WITU.Core.Model;

namespace WITU.Models
{
    public enum AdministrativeDisplay
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
    /// It is important to note that <b>Whatever changes made may affect the views using the parital View AdministrativeSelectItem.cshtml</b>. So be
    /// very careful how and what you use in this presentation.
    /// </summary>
    public class AdministrativeSelectItem
    {

        public const string DefaultFirstItemText = "-- All --";

        public AdministrativeSelectItem()
        {
//            if(CampusOptions == null)
//                CampusOptions = new List<SelectListItem>();

            if(FacultyOptions == null)
                FacultyOptions = new List<SelectListItem>();

            if(DepartmentOptions == null)
                DepartmentOptions = new List<SelectListItem>();

            if(ItemsToDisplay == null)
                ItemsToDisplay = new List<AdministrativeDisplay>()
                {
                    AdministrativeDisplay.Campus, AdministrativeDisplay.Faculty, AdministrativeDisplay.Department
                };

            if (DefaultFirstItem == null)
                DefaultFirstItem = new SelectListItem() {Text = DefaultFirstItemText, Value = "0"};

            StaffId = 0;
        }

        //public AdministrativeSelectItem(Program program, IEnumerable<SelectListItem> campusOptions)
        //{
        //    if (program != null)
        //    {
        //        CampusId = program.Campus.Id;
        //        FacultyId = program.Faculty.Id;
        //        if (program.Department != null)
        //            DepartmentId = program.Department.Id;
        //    }

        //    CampusOptions = campusOptions;

        //    if (DefaultFirstItem == null)
        //        DefaultFirstItem = new SelectListItem() { Text = DefaultFirstItemText, Value = "0" };

        //    StaffId = 0;
        //}

        //public AdministrativeSelectItem(Program program, IEnumerable<Campus> campus)
        //{
        //    CampusId = program.Campus != null ? program.Campus.Id : 0;
            
        //    FacultyId = program.Faculty != null ? program.Faculty.Id : 0;
            
        //    DepartmentId = program.Department != null ? program.Department.Id : 0;
            
        //    CampusOptions = campus.Select(x => new SelectListItem() {Text = x.Name, Value = x.Id.ToString()}).ToList();
            
        //    FacultyOptions = program.Campus != null
        //        ? program.Campus.Faculties.Select( x => new SelectListItem() {Text = x.FacultyCore.Name, Value = x.Id.ToString()}).ToList()
        //        : new List<SelectListItem>();
            
        //    DepartmentOptions = program.Faculty != null
        //        ? program.Faculty.Departments.Select(x => new SelectListItem() {Text = x.DepartmentCore.Name, Value = x.Id.ToString()}).ToList()
        //        : new List<SelectListItem>();

        //    DefaultFirstItem = new SelectListItem() {Text = DefaultFirstItemText, Value = "0"};

        //    ItemsToDisplay = new List<AdministrativeDisplay>()
        //    {
        //        AdministrativeDisplay.Campus, AdministrativeDisplay.Faculty, AdministrativeDisplay.Department
        //    };

        //    StaffId = 0;
        //}

        //// New Constructors to use when logged staff member
        //public AdministrativeSelectItem(Program program, IEnumerable<SelectListItem> campusOptions, int staffId)
        //{
        //    if (program != null)
        //    {
        //        CampusId = program.Campus.Id;
        //        FacultyId = program.Faculty.Id;
        //        if (program.Department != null)
        //            DepartmentId = program.Department.Id;
        //    }

        //    CampusOptions = campusOptions;

        //    if (DefaultFirstItem == null)
        //        DefaultFirstItem = new SelectListItem() { Text = DefaultFirstItemText, Value = "0" };

        //    // Save staff Id
        //    StaffId = staffId != 0 ? staffId : 0;
        //}

        //public AdministrativeSelectItem(Program program, IEnumerable<Campus> campus, int staffId)
        //{
        //    CampusId = program.Campus != null ? program.Campus.Id : 0;

        //    FacultyId = program.Faculty != null ? program.Faculty.Id : 0;

        //    DepartmentId = program.Department != null ? program.Department.Id : 0;

        //    CampusOptions = campus.Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() }).ToList();

        //    FacultyOptions = program.Campus != null
        //        ? program.Campus.Faculties.Select(x => new SelectListItem() { Text = x.FacultyCore.Name, Value = x.Id.ToString() }).ToList()
        //        : new List<SelectListItem>();

        //    DepartmentOptions = program.Faculty != null
        //        ? program.Faculty.Departments.Select(x => new SelectListItem() { Text = x.DepartmentCore.Name, Value = x.Id.ToString() }).ToList()
        //        : new List<SelectListItem>();

        //    DefaultFirstItem = new SelectListItem() { Text = DefaultFirstItemText, Value = "0" };

        //    ItemsToDisplay = new List<AdministrativeDisplay>()
        //    {
        //        AdministrativeDisplay.Campus, AdministrativeDisplay.Faculty, AdministrativeDisplay.Department
        //    };

        //    //Save staff Id
        //    StaffId = staffId != 0 ? staffId : 0;
        //}

        [Display(Name = "Campus")]
        public int CampusId { get; set; }

        
        public int StaffId { get; set; }

        [Display(Name = "Faculty")]
        public int FacultyId { get; set; }

        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

//        [Display(Name = "Year of Study")]
//        public int YearOfStudy { get; set; }
//
//        [Display(Name = "Specialisation")]
//        public int SpecialisationId { get; set; }
//
//        [Display(Name = "Program")]
//        public int ProgramId { get; set; }
//
//        [Display(Name = "Study Times")]
//        public int StudyTimes { get; set; }

        public IEnumerable<SelectListItem> CampusOptions { get; set;}

        public List<SelectListItem> FacultyOptions { get; set; }

        public List<SelectListItem> DepartmentOptions { get; set; }

        public SelectListItem DefaultFirstItem { get; set; }

        /// <summary>
        /// Presents a collection of items to Display in the form.
        /// </summary>
        public List<AdministrativeDisplay> ItemsToDisplay { get; set; } 
    
    }
}