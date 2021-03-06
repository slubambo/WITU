using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WITU.Core.Model
{
    public enum AccessLevel
    {
        Administration = 1,
        Campus = 2
    }

	[Serializable]
	public class InstructorPosition
	{
		public InstructorPosition()
		{
            StaffCourses = new Iesi.Collections.Generic.HashedSet<StaffCourse>();	
		}

        public virtual Iesi.Collections.Generic.ISet<StaffCourse> StaffCourses
        {
            get;
            set;
        }

		public virtual string AcademicQaulification
		{
			get;
			set;
		}
		public virtual DateTime AssignedDate
		{
			get;
			set;
		}
		public virtual DateTime? DeactivatedDate
		{
			get;
			set;
		}
		public virtual int Id
		{
			get;
			set;
		}
		public virtual string IdNumber
		{
			get;
			set;
		}
		public virtual bool IsActive
		{
			get;
			set;
		}
        [Required]
        [Display(Name = "Level")]
		public virtual AccessLevel LevelId
		{
			get;
			set;
		}
		public virtual int WorkStatus
		{
			get;
			set;
		}
        [Required]
		public virtual Position Position
		{
			get;
			set;
		}
		public virtual Instructor Staff
		{
			get;
			set;
		}

        public virtual Campus Campus
        {
            get;
            set;
        }
		
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj))
				return true;
				
			return Equals(obj as InstructorPosition);
		}
		
		public virtual bool Equals(InstructorPosition obj)
		{
			if (obj == null) return false;

			if (Equals(AcademicQaulification, obj.AcademicQaulification) == false) return false;
			if (Equals(AssignedDate, obj.AssignedDate) == false) return false;
			if (Equals(DeactivatedDate, obj.DeactivatedDate) == false) return false;
			if (Equals(Id, obj.Id) == false) return false;
			if (Equals(IdNumber, obj.IdNumber) == false) return false;
			if (Equals(IsActive, obj.IsActive) == false) return false;
			if (Equals(LevelId, obj.LevelId) == false) return false;
			if (Equals(WorkStatus, obj.WorkStatus) == false) return false;
			return true;
		}
		
		public override int GetHashCode()
		{
			int result = 1;

			result = (result * 397) ^ (AcademicQaulification != null ? AcademicQaulification.GetHashCode() : 0);
			result = (result * 397) ^ AssignedDate.GetHashCode();
			result = (result * 397) ^ (DeactivatedDate != null ? DeactivatedDate.GetHashCode() : 0);
			result = (result * 397) ^ Id.GetHashCode();
			result = (result * 397) ^ (IdNumber != null ? IdNumber.GetHashCode() : 0);
			result = (result * 397) ^ (IsActive != null ? IsActive.GetHashCode() : 0);
			result = (result * 397) ^ LevelId.GetHashCode();
			result = (result * 397) ^ (WorkStatus != null ? WorkStatus.GetHashCode() : 0);
			return result;
		}
	}
}