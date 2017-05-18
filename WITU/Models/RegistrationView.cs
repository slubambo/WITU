using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WITU.Core.Model;
using WITU.Models.widgets;

namespace WITU.Models
{

    public class RegistrationView
    {
        public virtual int StudentId { get; set; }

        public virtual int[] SelectedCourses { get; set; }

        public virtual int ProgramDuration { get; set; }

        public virtual int ResultStatus { get; set; }

        public virtual int ConfirmationStatus { get; set; }

        public virtual int MaximumSemLoad { get; set; }

        public virtual int MinimumSemLoad { get; set; }
        public virtual Student Student { get; set; }


        public virtual String ProgramName { get; set; }

        public virtual IEnumerable<CourseCustom> CoursesList { get; set; }

        public virtual IEnumerable<Course> Courses { get; set; }

        public List<Student> Students { get; set; }

        public virtual IEnumerable<StudentCourse> RegisteredCourses { get; set; }

        public virtual IEnumerable<Course> CoursesToRegister { get; set; }

        public virtual IEnumerable<CohortRegistration> SemesterRegistrations { get; set; }

        public virtual IEnumerable<StudentCourse> SelectedStudentCourses { get; set; }

        public ProgramSelectItem ProgramSelectItem { get; set; }

        public virtual Boolean AppliedForCreditTransfer { get; set; }
    }

    public class CourseCustom
    {
        public virtual string Code { get; set; }

        [Display(Name = "Course Type")]
        public virtual CourseType? CourseType { get; set; }

        [Display(Name = "Course Type")]
        public virtual StudentCoursePerformanceTrack? StudentCoursePerformanceTrack { get; set; }

        [Display(Name = "Credit Units")]
        public virtual int? CreditUnits { get; set; }

        public virtual int SemesterId { get; set; }
        public virtual int Id { get; set; }

        public virtual int StudentCourseId { get; set; }

        [Display(Name = "Semester Registration")]
        public virtual CohortRegistration SemesterRegistration { get; set; }

        public virtual string Name { get; set; }
        public virtual string Remark { get; set; }
        public virtual Boolean IsRetaken3X { get; set; }

        public virtual int Level { get; set; }

        public virtual int Session { get; set; }
    }
}