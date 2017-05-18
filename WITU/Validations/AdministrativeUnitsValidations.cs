using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WITU.Core.Model;
using WITU.Models.AdminUnits;
using FluentValidation;

namespace WITU.Validations
{
    public class AdministrativeUnitsValidations
    {
    }

    #region Campus Validations

    public class AddOrEditCampusValidator : AbstractValidator<EditCampus>
    {
        public AddOrEditCampusValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Please specify the name of the campus.");
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Please enter the description");
            RuleFor(x => x.ShortName)
                .NotEmpty()
                .WithMessage("Please enter the Campus Short Name");
        }
    }

    #endregion


    #region University Validations

    public class AddOrEditUniversityValidator : AbstractValidator<EditUniversity>
    {
        public AddOrEditUniversityValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Please enter the university name.");
            RuleFor(x => x.ShortName)
                .NotEmpty()
                .WithMessage("Please enter the university short name.");
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Please enter description about the university");
        }
    }

    #endregion

}