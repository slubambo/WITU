using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WITU.Core.Helper.Impls;
using WITU.Core.Helper.Interfaces;
using WITU.Models;
using WITU.Models.student;
using FluentValidation;
using Ninject;

namespace WITU.Validations
{
    public class EditPersonValidator : AbstractValidator<EditPerson>
    {
        [Inject]
        public IValidationHelper ValidationHelper { get; set; }

        public EditPersonValidator()
        {
            ValidationHelper = new ValidationHelper();
            RuleFor(x => x.Title).NotEmpty().NotEqual("empty").WithMessage("Title is required");
            RuleFor(x => x.MaritalStatus).NotEmpty().WithMessage("Marital Status is required");
            RuleFor(x => x.EmailAddress).EmailAddress().WithMessage("The Email Address is not valid").NotEmpty().WithMessage("Email address is required");
            RuleFor(x => x.AltEmailAddress).EmailAddress().WithMessage("The Alternative Email Address is not valid");
            RuleFor(x => x.Religion).NotEmpty().WithMessage("Religion is required");
            RuleFor(x => x.NationalityId).GreaterThan(0).WithMessage("Nationality is required");
            RuleFor(x => x.DateOfBirth).NotNull().WithMessage("Please provide the date of birth");
            RuleFor(x => x.TelephoneContact).NotEmpty().WithMessage("Telephone Contact is required");
            RuleFor(x => x.ProfilePhotoFile)
                .Must(ValidationHelper.ValidateIsPhoto)
                .WithMessage("The Uploaded File is not an image.").When(x => (x.ProfilePhotoFile != null && x.ProfilePhotoFile.ContentLength > 0));
            RuleFor(x => x.NextOfKinName).NotEmpty().WithMessage("NOK Name is required");
            RuleFor(x => x.NextOfKinContact).NotEmpty().WithMessage("NOK Contact is required");
            RuleFor(x => x.NextOfKinRelationship).NotEmpty().WithMessage("NOK Relationship is required");
        }
    }
}