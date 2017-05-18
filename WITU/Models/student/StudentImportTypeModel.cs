using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using WITU.Validations;

namespace WITU.Models.student
{
    public enum StudentImportType
    {
        Batched = 1, Interactive = 2
    }

   [FluentValidation.Attributes.Validator(typeof(StudentImportTypeValidator))]
    public class StudentImportTypeModel
    {
        public StudentImportTypeModel()
        {
            ImportType = 0;
        }
        [DisplayName("Type")]
        public int ImportType { get; set; }
    }
}