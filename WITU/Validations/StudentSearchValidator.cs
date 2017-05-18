using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WITU.Models.student;
using FluentValidation;

namespace WITU.Validations
{
    public class StudentSearchValidator : AbstractValidator<StudentSearchModel>
    {
        public StudentSearchValidator()
        {
            RuleFor(x => x.SearchTerm).NotEmpty().WithMessage("Please provide a search term");
            
        }
    }
}