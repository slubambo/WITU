using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WITU.Models;
using FluentValidation;

namespace WITU.Validations
{
    public class EditCourseValidator :  AbstractValidator<EditCourse>
    {
        public EditCourseValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.SemesterLevelId).GreaterThan(0).WithMessage("Specify Year annd Semester for When the course shall be Offered.");
            RuleFor(x => x.CreditUnits).GreaterThan(0);
            RuleFor(x => x.CourseContent).NotEmpty();
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.SubjectId).GreaterThan(0).WithMessage("Please specify the subject type");

        }
    }
}