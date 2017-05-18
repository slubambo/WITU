using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WITU.Models;
using FluentValidation;

namespace WITU.Validations
{
    public class AddEditCountryValidation : AbstractValidator<AddEditCountry>
    {
        public AddEditCountryValidation()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.Nationality).NotEmpty()
                .Unless(x => x.Code != null)
                .WithMessage("Gwe!! U don't know ur Nationality!!Be serious")
                .Must(CheckForCountryNameInCountry).Unless(x => string.IsNullOrEmpty(x.Nationality))
                .WithMessage("Country Name must be contained in Nationality!!");
        }

        public bool CheckForCountryNameInCountry(AddEditCountry instance, string nationality)
        {
            if (nationality.Contains(instance.Name))
                return true;
            return false;
        }
    }
}