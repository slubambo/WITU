using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using WITU.Core.Model;
using WITU.Validations;


namespace WITU.Models.AdminUnits
{
    [FluentValidation.Attributes.Validator(typeof(AddOrEditCampusValidator))]
    public class EditCampus
    {
        public EditCampus()
        {
        }

        public EditCampus(Campus campus)
        {
            Code = campus.Code;
            Description = campus.Description;
            Id = campus.Id;
            Name = campus.Name;
            ShortName = campus.ShortName;

        }
        [DisplayName("Campus Code")]
        public  string Code
        {
            get;
            set;
        }
        public  string Description
        {
            get;
            set;
        }
        public int Id
        {
            get;
            set;
        }
        public  string Name
        {
            get;
            set;
        }

        [Display(Name = "Short Name")]
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
    }
    
}