using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WITU.Core.Model;
using WITU.Validations;

namespace WITU.Models
{
    [FluentValidation.Attributes.Validator(typeof(EditProgramValidator))]
    public class EditProgram
    {
        public EditProgram()
        {
            if(AdministrativeSelectItem == null)
                AdministrativeSelectItem = new AdministrativeSelectItem();

            if(ProgramSpecialisations == null)
                ProgramSpecialisations = new List<SpecialisationProgramView>();

            if(StudyPeriodPrograms == null)
                StudyPeriodPrograms = new List<StudyPeriodProgramView>();
        }
        
        public int ProgramId { get; set; }

        public AdministrativeSelectItem AdministrativeSelectItem { get; set; }

        public List<SelectListItem> StudyPeriods { get; set; }

        [Display(Name = "Parent Program")]
        public int ProgramCoreId { get; set; }

        public string ProgramCoreName { get; set; }

        public string Code { get; set; }

        [Display(Name = "Start Academic Year")]
        public int StartAcademicYear { get; set; }

        public List<SelectListItem> AcademicYears { get; set; }

        [Display(Name = "Admission Status")]
        public bool IsAdmitted { get; set; }

        [Display(Name = "Minimum Load")]
        public int MinimumLoad { get; set; }

        [Display(Name = "Program Number")]
        public int? ProgramNumber { get; set; }

        [Display(Name = "Study Periods")]
        public List<StudyPeriodProgramView> StudyPeriodPrograms { get; set; }

        public List<SelectListItem> ProgramCores { get; set; }

        public List<SpecialisationProgramView> ProgramSpecialisations { get; set; }

        public List<SelectListItem> Specialisations { get; set; }

        [Display(Name = "Year of Specialisation")]
        public int? SpecialisationLevel { get; set; }
    }

    public class StudyPeriodProgramView
    {
        public int StudyPeriodId { get; set; }

        public double? Tuition { get; set; }

        public int StudyPeriodProgramId { get; set; }

        public bool IsSelected { get; set; }

        public string Name { get; set; }
    }

    public class SpecialisationProgramView
    {
        public int SpecialisaitonId { get; set; }

        public int SpecialisationCoreId { get; set; }

        public string Code { get; set; }
    }
}