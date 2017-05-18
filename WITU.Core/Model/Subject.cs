using System;
using System.Collections.Generic;

namespace WITU.Core.Model
{
	[Serializable]
	public class Subject
	{
		public Subject()
		{
			//CourseCores = new Iesi.Collections.Generic.HashedSet<CourseCore>();
   //         Courses = new Iesi.Collections.Generic.HashedSet<Course>();		
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
		//public virtual Iesi.Collections.Generic.ISet<CourseCore> CourseCores
		//{
		//	get;
		//	set;
		//}
		
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj))
				return true;
				
			return Equals(obj as Subject);
		}
        //public virtual Iesi.Collections.Generic.ISet<Course> Courses
        //{
        //    get;
        //    set;
        //}
		
		public virtual bool Equals(Subject obj)
		{
			if (obj == null) return false;

			if (Equals(Id, obj.Id) == false) return false;
			if (Equals(Name, obj.Name) == false) return false;
			return true;
		}
		
		public override int GetHashCode()
		{
			int result = 1;

			result = (result * 397) ^ Id.GetHashCode();
			result = (result * 397) ^ (Name != null ? Name.GetHashCode() : 0);
			return result;
		}
	}
}