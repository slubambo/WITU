using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WITU.Core.Model
{
    public enum StudentStatus
    {
        [Description("Uncertain")]
        Unknown = -1,

        [Description("Normal Progress")]
        NormalProgress = 0,

        [Description("Probationary Progress (CTR)")]
        ProbationaryProgressCourse = 1,

        [Description("Probationary Progress (NL) ")]
        ProbationaryProgressNL = 2,

        [Description("Discontinued (GPA) ")]
        DiscontinuedGPA = 3,

        [Description("Discontinued (C)")]
        DiscontinuedCourse = 4,

        DiscontinuedDuration = 5,

        Withdrawn = 6,
        Suspended = 7,
        Deceased = 8,

        [Description("Absconded ")]
        Absconded = 9,

        AppliedforGraduation = 10,

        [Description("Graduated ")]
        Graduated = 11
    }

    public enum IntakeType
    {
        All,

        Normal,

        Rolling
    }

	[Serializable]
	public class Student
	{
		public Student()
		{
			SemesterRegistrations = new Iesi.Collections.Generic.HashedSet<CohortRegistration>();
		    StudentCourses = new Iesi.Collections.Generic.HashedSet<StudentCourse>();
		}

	    public static List<StudentStatus> ManualStudentStatuses = new List<StudentStatus>
	    {
	        StudentStatus.Withdrawn,
	        StudentStatus.Suspended,
	        StudentStatus.Deceased,
	        StudentStatus.Graduated
	    }; 

        public static List<StudentStatus> ReActivatableStudentStatuses = new List<StudentStatus>()
        {
            StudentStatus.Withdrawn, StudentStatus.Suspended
        }; 

		public virtual int CurrentYear
		{
			get;
			set;
		}
		public virtual int Id
		{
			get;
			set;
		}
		public virtual string RegistrationNumber
		{
			get;
			set;
		}
		public virtual string Remark
		{
			get;
			set;
		}
		public virtual StudentStatus StudentStatus
		{
			get;
			set;
		}
		public virtual string StudentNumber
		{
			get;
			set;
		}
	
		public virtual Person Person
		{
			get;
			set;
		}
		public virtual ProspectiveStudent ProspectiveStudent
		{
			get;
			set;
		}
		public virtual Cohort Semester
		{
			get;
			set;
		}
		public virtual Iesi.Collections.Generic.ISet<CohortRegistration> SemesterRegistrations
		{
			get;
			set;
		}
		
        public virtual int UserId { get; set; }

		public virtual User User
		{
			get;
			set;
		}

        public virtual decimal CurrentCTCU { get; set; }

        public virtual decimal CurrentCGPA { get; set; }

        public virtual double CurrentAward { get; set; }

        //Helper Parameter
        //public virtual string ProgramName
        //{
        //    get
        //    {
        //        var fullName = "";
        //        if (Program != null)
        //            fullName += string.Format("{0} ", Program.ProgramCore.Name.Trim());

        //        return fullName;
        //    }
        //}

        public virtual Iesi.Collections.Generic.ISet<StudentCourse> StudentCourses
        {
            get;
            set;
        }

        public virtual Institution Institution { get; set; }
        public virtual int? ResidenceStatus { get; set; }
        public virtual int? RegistrationStatus { get; set; }
        public virtual CohortYear StartAcademicYear { get; set; }
        public virtual int? EntryYear { get; set; }
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj))
				return true;
				
			return Equals(obj as Student);
		}
		
		public virtual bool Equals(Student obj)
		{
			if (obj == null) return false;

			if (Equals(CurrentYear, obj.CurrentYear) == false) return false;
			if (Equals(Id, obj.Id) == false) return false;
			if (Equals(RegistrationNumber, obj.RegistrationNumber) == false) return false;
			if (Equals(Remark, obj.Remark) == false) return false;
			if (Equals(StudentStatus, obj.StudentStatus) == false) return false;
			if (Equals(StudentNumber, obj.StudentNumber) == false) return false;
            if (Equals(CurrentCGPA, obj.CurrentCGPA) == false) return false;
            if (Equals(CurrentCTCU, obj.CurrentCTCU) == false) return false;
            if (Equals(CurrentAward, obj.CurrentAward) == false) return false;
			return true;
		}
		
		public override int GetHashCode()
		{
			int result = 1;

			result = (result * 397) ^ CurrentYear.GetHashCode();
			result = (result * 397) ^ Id.GetHashCode();
			result = (result * 397) ^ (RegistrationNumber != null ? RegistrationNumber.GetHashCode() : 0);
			result = (result * 397) ^ (Remark != null ? Remark.GetHashCode() : 0);
			result = (result * 397) ^ StudentStatus.GetHashCode();
			result = (result * 397) ^ (StudentNumber != null ? StudentNumber.GetHashCode() : 0);
            result = (result * 397) ^ CurrentCGPA.GetHashCode();
            result = (result * 397) ^ CurrentCTCU.GetHashCode();
            result = (result * 397) ^ CurrentAward.GetHashCode();
			return result;
		}
	}
}