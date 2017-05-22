using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WITU.Core.Model;
using WITU.Validations;

namespace WITU.Models
{
    [FluentValidation.Attributes.Validator(typeof(EditCourseValidator))]
    public class EditCourse
    {

        public EditCourse()
        {
            Status = (int) CourseStatus.Active;
        }

        public EditCourse(Course course)
        {
            Id = course.Id;
            SpecialisationId =  0;
            //ProgramId = course.Program != null ? course.Program.Id : 0;
            //ProgramName = course.Program != null ? course.ProgramName : "";
            //CampusName = course.Program != null ? course.Program.Campus.Name : "";
            //Code = course.Code;
            CourseType = course.CourseType != null ? (int)course.CourseType : 0;
            //SemesterLevelId = course.SemesterLevel != null ? course.SemesterLevel.Id : 0;
            Name = course.Name;
            //CourseContent = course.Content;
            //CreditUnits = course.Weight ?? 0;
            //SubjectId = course.Subject != null ? course.Subject.Id : 0;
            Status = course.Status > 0 ? course.Status : (int)CourseStatus.Active;
            
        }
        
        public int Id { get; set; }

        [Display(Name = "Specialisation")]
        public int SpecialisationId { get; set; }

        [Display(Name = "Program")]
        public int ProgramId { get; set; }

        public string ProgramName { get; set; }

        public string CampusName { get; set; }

        public string Code { get; set; }

        [Display(Name = "Year and Semester when Course is Offered")]
        public int SemesterLevelId { get; set; }

        [Display(Name = "Activation Status")]
        public int Status { get; set; }

        [Display(Name = "Course Type")]
        public int CourseType { get; set; }

        public string Name { get; set; }

        [AllowHtml]
        [Display(Name = "Course Content")]
        public string CourseContent { get; set; }

        [Display(Name = "Credit Units")]
        public int CreditUnits { get; set; }

        [Display(Name = "Subject")]
        public int SubjectId { get; set; }

        public List<SelectListItem> Subjects { get; set; }

        public IEnumerable<SelectListItem> CourseTypes { get; set; }

        public IEnumerable<SelectListItem> StatusOptions { get; set; } 

        public List<SelectListItem> SemesterLevelOptions { get; set; }

    }
}