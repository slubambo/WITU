using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WITU.Core.Model;

namespace WITU.Models.widgets
{
    public class CourseSelectItem 
    {
        public CourseSelectItem()
        {
            if (AdministrativeSelectItem == null)
            {
                AdministrativeSelectItem = new AdministrativeSelectItem();
            }

            if(ProgramOptions == null)
                ProgramOptions = new List<SelectListItem>();

            if(SpecialisationOptions == null)
                SpecialisationOptions = new List<SelectListItem>();
        }

        public CourseSelectItem(IEnumerable<SelectListItem> campusOptions)
        {
            if (AdministrativeSelectItem == null)
            {
                AdministrativeSelectItem = new AdministrativeSelectItem()
                {
                    CampusOptions = campusOptions
                };
            }

            if (ProgramOptions == null)
                ProgramOptions = new List<SelectListItem>();

            if (SpecialisationOptions == null)
                SpecialisationOptions = new List<SelectListItem>();
        }

        //public CourseSelectItem(Program program, IEnumerable<SelectListItem> campusOptions)
        //{
        //    if (program != null)
        //    {
        //        ProgramId = program.Id;
        //        Program = program;
        //        AdministrativeSelectItem = new AdministrativeSelectItem(program, campusOptions);
        //    }
        //}

        public AdministrativeSelectItem AdministrativeSelectItem { get; set; }

        [Display(Name = "Program")]
        public int ProgramId { get; set; }


        //[Display(Name = "Program")]
        //public Program Program { get; set; }

        [Display(Name = "Specialisation")]
        public int SpecialisationId { get; set; }

        [Display(Name = "Course")]
        public int CourseId { get; set; }

        public List<SelectListItem> ProgramOptions { get; set; }

        public List<SelectListItem> SpecialisationOptions { get; set; } 
    }
}