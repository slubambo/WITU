using System;
using System.Collections.Generic;

namespace WITU.Core.Model
{
	[Serializable]
	public class CohortYear
	{
		public CohortYear()
		{
			//Programs = new Iesi.Collections.Generic.HashedSet<Program>();
			//Programs1 = new Iesi.Collections.Generic.HashedSet<Program>();
			Semesters = new Iesi.Collections.Generic.HashedSet<Cohort>();
		    Students = new Iesi.Collections.Generic.HashedSet<Student>();
		    GeneralInformationAttachments = new Iesi.Collections.Generic.HashedSet<GeneralInformationAttachment>();
		    
		}
		public virtual DateTime? CreatedOn
		{
			get;
			set;
		}
		public virtual DateTime? EndDate
		{
			get;
			set;
		}
		public virtual short EndYear
		{
			get;
			set;
		}
		public virtual int Id
		{
			get;
			set;
		}
		public virtual bool IsDefaultAcademicYear
		{
			get;
			set;
		}
		public virtual string Name
		{
			get;
			set;
		}
		public virtual DateTime? StartDate
		{
			get;
			set;
		}
		public virtual short StartYear
		{
			get;
			set;
		}
		
		//public virtual Iesi.Collections.Generic.ISet<Program> Programs
		//{
		//	get;
		//	set;
		//}
		//public virtual Iesi.Collections.Generic.ISet<Program> Programs1
		//{
		//	get;
		//	set;
		//}
		public virtual Iesi.Collections.Generic.ISet<Cohort> Semesters
		{
			get;
			set;
		}
        public virtual Iesi.Collections.Generic.ISet<Student> Students { get; set; }

        public virtual Iesi.Collections.Generic.ISet<GeneralInformationAttachment> GeneralInformationAttachments { get;
            set; }

        public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj))
				return true;
				
			return Equals(obj as CohortYear);
		}
		
		public virtual bool Equals(CohortYear obj)
		{
			if (obj == null) return false;

			if (Equals(CreatedOn, obj.CreatedOn) == false) return false;
			if (Equals(EndDate, obj.EndDate) == false) return false;
			if (Equals(EndYear, obj.EndYear) == false) return false;
			if (Equals(Id, obj.Id) == false) return false;
			if (IsDefaultAcademicYear != obj.IsDefaultAcademicYear) return false;
			if (Equals(Name, obj.Name) == false) return false;
			if (Equals(StartDate, obj.StartDate) == false) return false;
			if (Equals(StartYear, obj.StartYear) == false) return false;
			return true;
		}
		
		public override int GetHashCode()
		{
			int result = 1;

			result = (result * 397) ^ (CreatedOn != null ? CreatedOn.GetHashCode() : 0);
			result = (result * 397) ^ (EndDate != null ? EndDate.GetHashCode() : 0);
			result = (result * 397) ^ EndYear.GetHashCode();
			result = (result * 397) ^ Id.GetHashCode();
			result = (result * 397) ^ (IsDefaultAcademicYear != null ? IsDefaultAcademicYear.GetHashCode() : 0);
			result = (result * 397) ^ (Name != null ? Name.GetHashCode() : 0);
			result = (result * 397) ^ (StartDate != null ? StartDate.GetHashCode() : 0);
			result = (result * 397) ^ StartYear.GetHashCode();
			return result;
		}
	}
}