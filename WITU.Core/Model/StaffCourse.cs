using System;
using System.Collections.Generic;

namespace WITU.Core.Model
{
	[Serializable]
	public class StaffCourse
	{
		public StaffCourse()
		{
			//StudentCourses = new Iesi.Collections.Generic.HashedSet<StudentCourse>();		
		}
		public virtual int? AssessmentWindowId
		{
			get;
			set;
		}
		public virtual string CourseSummary
		{
			get;
			set;
		}
		public virtual string Grading
		{
			get;
			set;
		}
		public virtual int Id
		{
			get;
			set;
		}
		public virtual bool IsMainInstructor
		{
			get;
			set;
		}
		public virtual string LecturePlan
		{
			get;
			set;
		}
		public virtual int Status
		{
			get;
			set;
		}
        public virtual Course Course
        {
            get;
            set;
        }

        public virtual Cohort Cohort
        {
            get;
            set;
        }

        public virtual InstructorPosition StaffPosition
		{
			get;
			set;
		}
        
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj))
				return true;
				
			return Equals(obj as StaffCourse);
		}
		
		public virtual bool Equals(StaffCourse obj)
		{
			if (obj == null) return false;

			if (Equals(AssessmentWindowId, obj.AssessmentWindowId) == false) return false;
			if (Equals(CourseSummary, obj.CourseSummary) == false) return false;
			if (Equals(Grading, obj.Grading) == false) return false;
			if (Equals(Id, obj.Id) == false) return false;
			if (Equals(IsMainInstructor, obj.IsMainInstructor) == false) return false;
			if (Equals(LecturePlan, obj.LecturePlan) == false) return false;
			if (Equals(Status, obj.Status) == false) return false;
			return true;
		}
		
		public override int GetHashCode()
		{
			int result = 1;

			result = (result * 397) ^ (AssessmentWindowId != null ? AssessmentWindowId.GetHashCode() : 0);
			result = (result * 397) ^ (CourseSummary != null ? CourseSummary.GetHashCode() : 0);
			result = (result * 397) ^ (Grading != null ? Grading.GetHashCode() : 0);
			result = (result * 397) ^ Id.GetHashCode();
			result = (result * 397) ^ IsMainInstructor.GetHashCode();
			result = (result * 397) ^ (LecturePlan != null ? LecturePlan.GetHashCode() : 0);
			result = (result * 397) ^ Status.GetHashCode();
			return result;
		}
	}
}