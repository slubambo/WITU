using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using WITU.Core.Model;
using WITU.Models;
using FluentValidation;

namespace WITU.Validations
{
    public class EditProgramValidator : AbstractValidator<EditProgram>
    {
        public EditProgramValidator()
        {
            RuleFor(x => x.AdministrativeSelectItem.CampusId).GreaterThan(0).WithMessage("Please Specify Campus");
            RuleFor(x => x.AdministrativeSelectItem.FacultyId).GreaterThan(0).WithMessage("Please Specify Faculty");
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.ProgramNumber).NotEmpty();
            RuleFor(x => x.StartAcademicYear).GreaterThan(0).WithMessage("Please Specify when the Program is Starting or Started.");
            RuleFor(x => x.ProgramCoreId).GreaterThan(0).WithMessage("Please Specify Parent Program");
            
            //RuleFor(x => x.StudyPeriodPrograms)
            //    .Must(StudyPeriodEmptyVaildation) //when no study period is added...
            //    .WithMessage("Specify at least a Study Period with its corresponding tuition")
            //    .Must(DuplicateStudyPeriodsValidaiton)
            //    .WithMessage("{0}", DuplicateStudyPeriodErrorMessage);

            RuleFor(x => x.StudyPeriodPrograms).Must(AtleastOneStudyPeriodSelected)
                .WithMessage("At Least one Study Period Must be Selected.");

            //specialisation Rules...
            RuleFor(x => x.SpecialisationLevel)
                .NotEmpty()
                .When(x => x.ProgramSpecialisations.Any(y => y.SpecialisationCoreId > 0 && y.Code != null))
                .WithMessage("Whenever Specialisation is Selected, a year of Specialisation should be specified.");

            RuleFor(x => x.ProgramSpecialisations)
                .Must(IncompleteSpeciialisationValidation) //whenever a specialisation is added, the code is required
                .WithMessage("{0}", IncompleteSpecialisationErrorMessage)
                .When(x => x.ProgramSpecialisations.Any(y => y.SpecialisationCoreId > 0 && y.Code != null))
                
                .Must(DuplicateSpecialisationValidation)
                .WithMessage("{0}", DuplicateSpecialisationErrorMessage)
                .When(x =>x.ProgramSpecialisations.Any(y => y.SpecialisationCoreId > 0 && y.Code != null));
            RuleFor(x => x.MinimumLoad).GreaterThan(0);

        }

        #region Specialisaiton Validators

        public bool IncompleteSpeciialisationValidation(EditProgram instance,
            List<SpecialisationProgramView> specialisationProgramViews)
        {
            return !InvalidSpecialisations(instance).Any();
        }

        private string IncompleteSpecialisationErrorMessage(EditProgram instance)
        {
            var builder = new StringBuilder("The following Specialisations have errors: <br/><ul>");
            var invalidSpecialisations = InvalidSpecialisations(instance);
            foreach (var x in invalidSpecialisations)
            {
                builder.Append(string.Format("<li>{0} added has not been assigned a code. </li>", x));
            }
            return builder.Append("</ul>").ToString();
        }

        private List<string> InvalidSpecialisations(EditProgram instance)
        {
            var invalidSpecialisations = new List<string>();

            var invalidSpecIds = instance.ProgramSpecialisations.Where(x => x.SpecialisationCoreId > 0 && x.Code == null);
            foreach (var inv in invalidSpecIds)
            {
                var specName = instance.Specialisations.FirstOrDefault(x => x.Value.Equals(inv.SpecialisationCoreId.ToString()));
                if(specName != null)
                    invalidSpecialisations.Add(specName.Text);
            }
            return invalidSpecialisations;
        }

        public bool DuplicateSpecialisationValidation(EditProgram instance,
            List<SpecialisationProgramView> specialisationProgramViews)
        {
            return !DuplicateSpecialisations(instance).Any();
        }

        public string DuplicateSpecialisationErrorMessage(EditProgram instance)
        {
            var builder = new StringBuilder("The following Specialisaitons have been duplicated: <br/><ul>");
            foreach (var dup in DuplicateSpecialisations(instance))
            {
                builder.Append(string.Format("<li><b>{0}</b> has been added <b>{1}</b> times", dup.Key, dup.Value));
            }
            return builder.Append("</ul>").ToString();
        }

        public Dictionary<string, int> DuplicateSpecialisations(EditProgram instance)
        {
            var duplicates = new Dictionary<string, int>();

            var dups = instance.ProgramSpecialisations.Where(x => x.SpecialisationCoreId > 0 && x.Code != null)
                    .GroupBy(x => x.SpecialisationCoreId)
                    .ToDictionary(x => x.Key, v => v.Count());
            foreach (var dup in dups)
            {
                if (dup.Value > 1)
                {
                    var dupName = instance.Specialisations.FirstOrDefault(x => x.Value.Equals(dup.Key.ToString()));
                    if (dupName != null)
                        duplicates.Add(dupName.Text, dup.Value);
                }
            }
            return duplicates;
        } 

        #endregion

        #region Study Period Validation

        public bool AtleastOneStudyPeriodSelected(List<StudyPeriodProgramView> instance)
        {
            return instance.Any(x => x.IsSelected);
        }
        public bool StudyPeriodEmptyVaildation(List<StudyPeriodProgramView> studyPeriodProgramViews)
        {
            return studyPeriodProgramViews.Any(x => x.StudyPeriodId > 0);
        }

        public bool DuplicateStudyPeriodsValidaiton(EditProgram instance, List<StudyPeriodProgramView> studyPeriodProgramViews)
        {
            return !DuplicateStudyTimes(instance).Any();
        }

        public string DuplicateStudyPeriodErrorMessage(EditProgram instance)
        {
            var duplicates = DuplicateStudyTimes(instance);
            var builder = new StringBuilder("The following Study Periods have been duplicated:<br/><ul>");
            foreach (var duplicate in duplicates)
            {
                builder.Append(string.Format("<li> <b>{0}</b> has been added <b>{1}</b> times in Study Period. </li>", duplicate.Key, duplicate.Value));
            }

            return builder.Append("</ul>").ToString();
        }
        private Dictionary<string, int> DuplicateStudyTimes(EditProgram instance)
        {
            var duplicates = new Dictionary<string, int>();

            var rawDups = instance.StudyPeriodPrograms.Where(x => x.StudyPeriodId > 0)
                    .GroupBy(x => x.StudyPeriodId)
                    .ToDictionary(x => x.Key, y => y.Count());
            foreach (var rd in rawDups)
            {
                if (rd.Value > 1)
                {
                    var dupName = instance.StudyPeriods.FirstOrDefault(x => x.Value.Equals(rd.Key.ToString()));
                    if (dupName != null)
                        duplicates.Add(dupName.Text, rd.Value);
                }
            }

            return duplicates;
        }
        #endregion
    }

    public class NullableValidator<T> : AbstractValidator<T> where T : class
    {
        
    }
}