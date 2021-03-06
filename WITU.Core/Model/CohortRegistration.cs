using System;
using System.Collections.Generic;
using WITU.Core.Model;

namespace WITU.Core.Model
{
    public enum RegistrationStatus
    {
        PendingRegistration = 0,
        ApprovedRegistration = 1,
        DeletedRegistration = 2,
        RejectedRegistration = 3,
        NotRegistered = 4,
        Unknown = 5,
        
    }

	[Serializable]
	public class CohortRegistration
	{
		public CohortRegistration()
		{
			StudentCourses = new Iesi.Collections.Generic.HashedSet<StudentCourse>();
		}
		public virtual int? AccumulatedCreditUnits
		{
			get;
			set;
		}
		public virtual decimal? Cgpa
		{
			get;
			set;
		}
		public virtual decimal? Gpa
		{
			get;
			set;
		}
		public virtual int Id
		{
			get;
			set;
		}
		public virtual DateTime? RegistrationApprovalDateTime
		{
			get;
			set;
		}
		public virtual DateTime RegistrationDateTime
		{
			get;
			set;
		}
		public virtual int? SemesterCreditUnits
		{
			get;
			set;
		}
		public virtual string StudentRemark
		{
			get;
			set;
		}
		public virtual RegistrationStatus? StudentStatus
		{
			get;
			set;
		}
		
		public virtual Cohort Cohort
		{
			get;
			set;
		}
		
		public virtual Student Student
		{
			get;
			set;
		}

        public virtual Iesi.Collections.Generic.ISet<StudentCourse> StudentCourses { get; set; }
		
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj))
				return true;
				
			return Equals(obj as CohortRegistration);
		}
		
		public virtual bool Equals(CohortRegistration obj)
		{
			if (obj == null) return false;

			if (Equals(AccumulatedCreditUnits, obj.AccumulatedCreditUnits) == false) return false;
			if (Equals(Cgpa, obj.Cgpa) == false) return false;
			if (Equals(Gpa, obj.Gpa) == false) return false;
			if (Equals(Id, obj.Id) == false) return false;
			if (Equals(RegistrationApprovalDateTime, obj.RegistrationApprovalDateTime) == false) return false;
			if (Equals(RegistrationDateTime, obj.RegistrationDateTime) == false) return false;
			if (Equals(SemesterCreditUnits, obj.SemesterCreditUnits) == false) return false;
			if (Equals(StudentRemark, obj.StudentRemark) == false) return false;
			if (Equals(StudentStatus, obj.StudentStatus) == false) return false;
			return true;
		}
		
		public override int GetHashCode()
		{
			int result = 1;

			result = (result * 397) ^ (AccumulatedCreditUnits != null ? AccumulatedCreditUnits.GetHashCode() : 0);
			result = (result * 397) ^ (Cgpa != null ? Cgpa.GetHashCode() : 0);
			result = (result * 397) ^ (Gpa != null ? Gpa.GetHashCode() : 0);
			result = (result * 397) ^ Id.GetHashCode();
			result = (result * 397) ^ (RegistrationApprovalDateTime != null ? RegistrationApprovalDateTime.GetHashCode() : 0);
			result = (result * 397) ^ RegistrationDateTime.GetHashCode();
			result = (result * 397) ^ (SemesterCreditUnits != null ? SemesterCreditUnits.GetHashCode() : 0);
			result = (result * 397) ^ (StudentRemark != null ? StudentRemark.GetHashCode() : 0);
			result = (result * 397) ^ (StudentStatus != null ? StudentStatus.GetHashCode() : 0);
			return result;
		}
	}
}