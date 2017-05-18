using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WITU.Core.Model;
using WITU.Validations;

namespace WITU.Models.student
{
    [FluentValidation.Attributes.Validator(typeof(StudentSearchValidator))]
    public class StudentSearchModel
    {
        public StudentSearchModel()
        {
           
        }
        public IEnumerable<Student> Students { get; set; }

        public string SearchTerm { get; set; }
    }
}