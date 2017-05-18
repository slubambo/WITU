using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WITU.Validations;

namespace WITU.Models.student
{
    [FluentValidation.Attributes.Validator(typeof(FirstTimeUserModelValidator))]
    public class FirstTimeUserModel
    {
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string OldPassword { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        public string ConfirmPassword { get; set; }

       /* [Display(Name="Email Address")]
        public string EmailAddress { get; set; } */
    }
}