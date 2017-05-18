using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WITU.Core.Model;

namespace WITU.Models.widgets
{
    public class ProgramSelectItem:AdministrativeSelectItem
    {
        public ProgramSelectItem()
        {
            if (AdministrativeSelectItem == null)
            {
                AdministrativeSelectItem = new AdministrativeSelectItem();
            }

            if(ProgramOptions == null)
                ProgramOptions = new List<SelectListItem>();

            if(SpecialisationOptions == null)
                SpecialisationOptions = new List<SelectListItem>();

            IncludeOldPrograms = false;
        }

        public ProgramSelectItem(IEnumerable<SelectListItem> campusOptions)
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

            IncludeOldPrograms = false;
        }

        //public ProgramSelectItem(Program program, IEnumerable<SelectListItem> campusOptions)
        //{
        //    if (program != null)
        //    {
        //        ProgramId = program.Id;
        //        AdministrativeSelectItem = new AdministrativeSelectItem(program, campusOptions);
        //        IncludeOldPrograms = !program.IsAdmittion;
        //    }
        //}

        public AdministrativeSelectItem AdministrativeSelectItem { get; set; }

        [Display(Name = "Specialisation")]
        public int SpecialisationId { get; set; }

        [Display(Name = "Program")]
        public int ProgramId { get; set; }

        public List<SelectListItem> ProgramOptions { get; set; }

        public List<SelectListItem> SpecialisationOptions { get; set; }

        [Display(Name = "Include Old Programs")]
        public bool IncludeOldPrograms { get; set; }
    }
}