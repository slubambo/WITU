using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WITU.Models;
using FluentValidation;

namespace WITU.Validations
{
    public class AccountViewModelValidations
    {
    }

    public class LoginViewModelValidation : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidation()
        {
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();

        }
    }

    public class RegisterViewModelValidation : AbstractValidator<RegisterViewModel>
    {
        public RegisterViewModelValidation()
        {
            RuleFor(x => x.Surname).NotEmpty();
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.UserName).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().Must(x => x.Length > 6).WithMessage("Password Must be atleat 6 characters long.");

            RuleFor(x => x.ConfirmEmailAddress).Equal(x => x.UserName).WithMessage("Email Addresses Must Match");
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Passwords did not Match!");
        }
    }
}