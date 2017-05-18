using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WITU.Core.Model;
using FluentValidation;

namespace WITU.Core.Validations
{
    class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            //RuleFor(x => x.GivenName).NotEmpty();
            //RuleFor(x => x.Surname).NotEmpty();
            //RuleFor(x => x.Gender).NotEmpty();
            //RuleFor(x => x.DateOfBirth);
            //RuleFor(x => x.Title).NotEmpty();
            //RuleFor(x => x.MaritalStatus).NotEmpty();
            ////RuleFor(x => x.Religion).NotEmpty();
            //RuleFor(x => x.Country).NotEmpty();
            //RuleFor(x => x.District).NotEmpty();
            //RuleFor(x => x.EmailAddress).EmailAddress();
            //RuleFor(x => x.TelephoneContact).NotEmpty().Length(5, 10).WithMessage("Enter the right Phone Number");
            
        }
    }
}
