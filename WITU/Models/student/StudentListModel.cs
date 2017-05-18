using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WITU.Core.Model;
using WITU.Models.widgets;
using WITU.Validations;
using Ninject.Infrastructure.Language;

namespace WITU.Models.student
{
    [FluentValidation.Attributes.Validator(typeof(StudentListModelValidator))]
    public class StudentListModel
    {
        public StudentListModel()
        {
            if (AcademicYearOptions == null)
                AcademicYearOptions = new List<SelectListItem>();
            Students = new List<Student>();
        }
        public YearSemesterPogramSelectItem YearSemesterPogramSelectItem { get; set; }

        [Display(Name = "Academic Year")]
        public int AcademicYearId { get; set; }
        public List<SelectListItem> AcademicYearOptions { get; set; }

        public IEnumerable<Student> Students { get; set; }

        public RouteValueDictionary RouteObject { get; set; }

    }
}