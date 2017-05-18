using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WITU.Core.Model;
using FluentValidation;

namespace WITU.Core.Validations
{
    public class UniversityValidator : AbstractValidator<Institution>
    {
        public UniversityValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.logoFile).Must(ValidateLogo).When(x => x.LogoPathName != null);
        }

        public bool ValidateLogo(Institution uni, HttpPostedFileBase logo)
        {
            if (uni.LogoPathName != null)
                return true;

            //do validation on file  to check if empty or size is 0..
            return false;
        }
    }
}
 