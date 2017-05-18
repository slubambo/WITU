using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WITU.Core.Model;
using FluentValidation;

namespace WITU.Core.Validations
{
    public class CampusValidator : AbstractValidator<Campus>
    {
        public CampusValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Code).Must(IsInteger);
        }

        public bool IsInteger(string code)
        {
            int val = 0;
            return int.TryParse(code, out val);
        }
    }
}
