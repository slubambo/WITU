using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WITU.Core.Model
{
    public enum CourseType
    {
        [Description("Core")]
        Core = 0,

        [Description("Elective")]
        Elective = 1,

        [Description("Audited")]
        Audited = 3,

        [Description("Un-known")]
        UnKnown = -1
    }

    public enum CourseStatus
    {
        Active = 1,

        [Description("In-Active")]
        InActive = 2
    }
	[Serializable]
	public class Course
	{
		public Course()
		{
			Courses = new Iesi.Collections.Generic.HashedSet<Course>();
			Courses1 = new Iesi.Collections.Generic.HashedSet<Course>();
			StaffCourses = new Iesi.Collections.Generic.HashedSet<StaffCourse>();
		    StudentCourses = new Iesi.Collections.Generic.HashedSet<StudentCourse>();
		}
		public virtual string Code
		{
			get;
			set;
		}
		
        [Display(Name = "Course Type")]
        public virtual CourseType? CourseType
		{
			get;
			set;
		}
		
        [Display(Name = "Course Weight")]
        public virtual int? Weight
		{
			get;
			set;
		}
		public virtual int Id
		{
			get;
			set;
		}
		public virtual Iesi.Collections.Generic.ISet<Course> Courses
		{
			get;
			set;
		}
		public virtual Iesi.Collections.Generic.ISet<Course> Courses1
		{
			get;
			set;
		}

        
        [AllowHtml]
        public virtual string Content { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual int Status { get; set; }
        public virtual string Name { get; set; }

        public virtual int TimeFrame
        {
            get;
            set;
        }

        public virtual Campus Campus
        {
            get;
            set;
        }

        public virtual CourseCore CourseCore
        {
            get;
            set;
        }

        public virtual CourseCategory CourseCategory
        {
            get;
            set;
        }

        public virtual Iesi.Collections.Generic.ISet<StaffCourse> StaffCourses
		{
			get;
			set;
		}

        public virtual Iesi.Collections.Generic.ISet<StudentCourse> StudentCourses { get; set; } 

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj))
				return true;
				
			return Equals(obj as Course);
		}
		
		public virtual bool Equals(Course obj)
		{
			if (obj == null) return false;

			if (Equals(Code, obj.Code) == false) return false;
			if (Equals(CourseType, obj.CourseType) == false) return false;
			if (Equals(Weight, obj.Weight) == false) return false;
			if (Equals(Id, obj.Id) == false) return false;
			return true;
		}
		
		public override int GetHashCode()
		{
			int result = 1;

			result = (result * 397) ^ (Code != null ? Code.GetHashCode() : 0);
			result = (result * 397) ^ (CourseType != null ? CourseType.GetHashCode() : 0);
			result = (result * 397) ^ (Weight != null ? Weight.GetHashCode() : 0);
			result = (result * 397) ^ Id.GetHashCode();
			return result;
		}
	}
}