using System;
using System.Collections.Generic;

namespace WITU.Core.Model
{
	[Serializable]
	public class Instructor
	{
		public Instructor()
		{
			//StaffCourses = new Iesi.Collections.Generic.HashedSet<StaffCourse>();
			InstructorPositions = new Iesi.Collections.Generic.HashedSet<InstructorPosition>();		
		}
		public virtual string About
		{
			get;
			set;
		}
		public virtual DateTime? CreatedOn
		{
			get;
			set;
		}
		public virtual int Id
		{
			get;
			set;
		}
		public virtual string ResearchInterests
		{
			get;
			set;
		}
		public virtual Person Person
		{
			get;
			set;
		}
        //public virtual Iesi.Collections.Generic.ISet<StaffCourse> StaffCourses
        //{
        //    get;
        //    set;
        //}
		public virtual Iesi.Collections.Generic.ISet<InstructorPosition> InstructorPositions
        {
			get;
			set;
		}
		public virtual User User
		{
			get;
			set;
		}

        public virtual int UserId { get; set; }

        
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj))
				return true;
				
			return Equals(obj as Instructor);
		}
		
		public virtual bool Equals(Instructor obj)
		{
			if (obj == null) return false;

			if (Equals(About, obj.About) == false) return false;
			if (Equals(CreatedOn, obj.CreatedOn) == false) return false;
			if (Equals(Id, obj.Id) == false) return false;
			if (Equals(ResearchInterests, obj.ResearchInterests) == false) return false;
			return true;
		}
		
		public override int GetHashCode()
		{
			int result = 1;

			result = (result * 397) ^ (About != null ? About.GetHashCode() : 0);
			result = (result * 397) ^ (CreatedOn != null ? CreatedOn.GetHashCode() : 0);
			result = (result * 397) ^ Id.GetHashCode();
			result = (result * 397) ^ (ResearchInterests != null ? ResearchInterests.GetHashCode() : 0);
			return result;
		}

        
    }
}