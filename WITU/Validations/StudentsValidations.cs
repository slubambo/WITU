using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WITU.Core.Helper.Impls;
using WITU.Core.Helper.Interfaces;
using WITU.Core.Model;
using WITU.Models;
using WITU.Models.student;
using FluentValidation;
using Ninject;

namespace WITU.Validations
{
    public class StudentsValidations
    {
    }

    #region Student Portal 

    public class FirstTimeUserModelValidator : AbstractValidator<FirstTimeUserModel>
    {
        public FirstTimeUserModelValidator()
        {
            RuleFor(x => x.OldPassword).NotEmpty().WithMessage("Current password cannot be empty").NotEqual(x => x.ConfirmPassword).WithMessage("The new password cannot be the same as the old password");
            RuleFor(x => x.NewPassword).NotEmpty().Must(x => x!=null && x.Length >= 6).WithMessage("Password Must be atleat 6 characters long.");
            RuleFor(x => x.ConfirmPassword).Equal(x => x.NewPassword).WithMessage("Passwords Must Match");
           // RuleFor(x => x.EmailAddress).EmailAddress().NotEmpty().WithMessage("Please enter your email address");
        }
    }

    #endregion

    #region Import Validations
    public class StudentImportTypeValidator : AbstractValidator<StudentImportTypeModel>
    {
        public StudentImportTypeValidator()
        {
            RuleFor(x => x.ImportType)
                .GreaterThan(0)
                .WithMessage("Please specify the import type.");
            
        }
    }

    
#endregion  

    public class StudentListModelValidator : AbstractValidator<StudentListModel>
    {

        public StudentListModelValidator()
        {
            RuleFor(x => x.AcademicYearId).GreaterThan(0).WithMessage("Please Specify the Academic Year");
            RuleFor(x => x.YearSemesterPogramSelectItem.ProgramSelectItem.ProgramId).GreaterThan(0).WithMessage("Please Specify the Academic Program");
            
            RuleFor(x => x.YearSemesterPogramSelectItem.Year)
                .GreaterThan(0)
                .WithMessage("Please specify the current Year of Study");

        }
    }

    
}