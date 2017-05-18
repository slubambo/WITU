using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using WITU.Core.Model;
using WITU.Validations;

namespace WITU.Models.AdminUnits
{
    [FluentValidation.Attributes.Validator(typeof(AddOrEditUniversityValidator))]
    public class EditUniversity
    {
        public EditUniversity()
        {
        }

        public EditUniversity(Institution university)
        {
            Id = university.Id;
            LogoPathName = university.LogoPathName;
            Name = university.Name;
            ShortName = university.ShortName;
            logoFile = university.logoFile;
            Description = university.Description;

        }
        [AllowHtml]
        public string Description
        {
            get;
            set;
        }
        public int Id
        {
            get;
            set;
        }
        [Display(Name = "University Logo Path")]
        public string LogoPathName
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        [DisplayName("Short Name")]
        public string ShortName
        {
            get;
            set;
        }
        
        public int ContactOwnerId
        {
            get;
            set;
        }

        public HttpPostedFileBase logoFile { get; set; }

    }
}