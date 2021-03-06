using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WITU.Core.Model
{
	[Serializable]
	public class Cohort
	{
		public Cohort()
		{
			//SemesterRegistrations = new Iesi.Collections.Generic.HashedSet<SemesterRegistration>();
			StaffCourses = new Iesi.Collections.Generic.HashedSet<StaffCourse>();
			Students = new Iesi.Collections.Generic.HashedSet<Student>();
		    StudentCourses = new Iesi.Collections.Generic.HashedSet<StudentCourse>();
		}

        [Display(Name = "Assesement End Date")]
		public virtual DateTime? AssessmentEndDate
		{
			get;
			set;
		}

        [Display(Name = "Assesement Start Date")]
		public virtual DateTime? AssessmentStartDate
		{
			get;
			set;
		}

        [Display(Name = "Semester End Date")]
		public virtual DateTime? EndDate
		{
			get;
			set;
		}
		public virtual int Id
		{
			get;
			set;
		}

        [Display(Name = "Deadline for Registration")]
		public virtual DateTime? RegistrationDeadline
		{
			get;
			set;
		}

        [Display(Name = "Deadline for Uploading Results")]
		public virtual DateTime? ResultsUploadDeadline
		{
			get;
			set;
		}

        [Display(Name = "Semester Session")]
		public virtual int SemesterSession
		{
			get;
			set;
		}

        [Display(Name = "Semester Start Date")]
		public virtual DateTime? StartDate
		{
			get;
			set;
		}

        
		public virtual short? Status
		{
			get;
			set;
		}

        [Display(Name = "Cohort Year")]
		public virtual CohortYear CohortYear
		{
			get;
			set;
		}
		
		public virtual Iesi.Collections.Generic.ISet<CohortRegistration> SemesterRegistrations
		{
			get;
			set;
		}
		public virtual Iesi.Collections.Generic.ISet<StaffCourse> StaffCourses
		{
			get;
			set;
		}
		public virtual Iesi.Collections.Generic.ISet<Student> Students
		{
			get;
			set;
		}

        public virtual Iesi.Collections.Generic.ISet<StudentCourse> StudentCourses { get; set; } 
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj))
				return true;
				
			return Equals(obj as Cohort);
		}
		
		public virtual bool Equals(Cohort obj)
		{
			if (obj == null) return false;

			if (Equals(AssessmentEndDate, obj.AssessmentEndDate) == false) return false;
			if (Equals(AssessmentStartDate, obj.AssessmentStartDate) == false) return false;
			if (Equals(EndDate, obj.EndDate) == false) return false;
			if (Equals(Id, obj.Id) == false) return false;
			if (Equals(RegistrationDeadline, obj.RegistrationDeadline) == false) return false;
			if (Equals(ResultsUploadDeadline, obj.ResultsUploadDeadline) == false) return false;
			if (Equals(SemesterSession, obj.SemesterSession) == false) return false;
			if (Equals(StartDate, obj.StartDate) == false) return false;
			if (Equals(Status, obj.Status) == false) return false;
			return true;
		}
		
		public override int GetHashCode()
		{
			int result = 1;

			result = (result * 397) ^ (AssessmentEndDate != null ? AssessmentEndDate.GetHashCode() : 0);
			result = (result * 397) ^ (AssessmentStartDate != null ? AssessmentStartDate.GetHashCode() : 0);
			result = (result * 397) ^ (EndDate != null ? EndDate.GetHashCode() : 0);
			result = (result * 397) ^ Id.GetHashCode();
			result = (result * 397) ^ (RegistrationDeadline != null ? RegistrationDeadline.GetHashCode() : 0);
			result = (result * 397) ^ (ResultsUploadDeadline != null ? ResultsUploadDeadline.GetHashCode() : 0);
			result = (result * 397) ^ SemesterSession.GetHashCode();
			result = (result * 397) ^ (StartDate != null ? StartDate.GetHashCode() : 0);
			result = (result * 397) ^ (Status != null ? Status.GetHashCode() : 0);
			return result;
		}
	}
}