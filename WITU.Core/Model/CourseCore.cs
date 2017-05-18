using System;
using System.Collections.Generic;

namespace WITU.Core.Model
{
	[Serializable]
	public class CourseCore
	{
		public CourseCore()
		{
			
		}
		public virtual string Overview
		{
			get;
			set;
		}

	    public virtual string ShortDescription
	    {
	        get;
            set;
	    }

        public virtual string Aims
        {
            get;
            set;
        }
        public virtual string OutComes
        {
            get;
            set;
        }
        public virtual string Skills
        {
            get;
            set;
        }
        public virtual int Id
		{
			get;
			set;
		}
		public virtual string Name
		{
			get;
			set;
		}

        public virtual string Code { get; set; }
		
		public virtual Subject Subject
		{
			get;
			set;
		}
		
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj))
				return true;
				
			return Equals(obj as CourseCore);
		}
		
		public virtual bool Equals(CourseCore obj)
		{
			if (obj == null) return false;

			if (Equals(Overview, obj.Overview) == false) return false;
			if (Equals(Id, obj.Id) == false) return false;
			if (Equals(Name, obj.Name) == false) return false;
			return true;
		}
		
		public override int GetHashCode()
		{
			int result = 1;

			result = (result * 397) ^ (Overview != null ? Overview.GetHashCode() : 0);
			result = (result * 397) ^ Id.GetHashCode();
			result = (result * 397) ^ (Name != null ? Name.GetHashCode() : 0);
			return result;
		}
	}
}