using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WITU.Core.Helper.Impls;
using WITU.Core.Helper.Interfaces;
using WITU.Core.Model;
using FluentValidation;
using Ninject;

namespace WITU.Core.Validations
{
    public class GeneralInformationValidator : AbstractValidator<GeneralInformation>
    {
        [Inject]
        public readonly IValidationHelper validationHelper;

        public GeneralInformationValidator()
        {
            validationHelper = new ValidationHelper();

            RuleFor(x => x.Content).NotEmpty();
            //RuleFor(x => x.ShortDescription).NotEmpty();
            RuleFor(x => x.UploadImage)
                .NotEmpty()
                .When(x => x.DefaultImageFilelName == null)
                .WithMessage("General Information requires an informative Photo to be uploaded")
                .Must(validationHelper.ValidateIsPhoto)
                .When(x => x.UploadImage != null)
                .WithMessage("The Required File Is Not a Photo or Image")
                .Must(validationHelper.ValidateFileUploadSize)
                .When(x => x.UploadImage != null)
                .WithMessage("The Required File is less than 0 bytes or greater than 4MB");
            
        }
    }
}
