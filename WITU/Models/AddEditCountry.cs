using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WITU.Validations;

namespace WITU.Models
{
    [FluentValidation.Attributes.Validator(typeof(AddEditCountryValidation))]
    public class AddEditCountry
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Nationality { get; set; }

        [Display(Name = "Last Modified")]
        public DateTime? LastModified { get; set; }

        public GenderTest Gender
        {
            get
            {
                return new GenderTest(){Value = "Male"};
            }
        }
    }

    public class GenderTest
    {
        public string Value { get; set; }
        public bool Male { get; set; }

        public bool Female { get; set; }
    }
}