using System;
using WITU.Core.Model;
using WITU.Core.Utils;
using FluentNHibernate.Mapping;

namespace WITU.Core.Mappings
{
	public class StaffCourseMapping : ClassMap<StaffCourse>
	{
		public StaffCourseMapping()
		{
			Table("`staff_course`");
			Schema("witu");
			Id(x => x.Id, "id")
				.GeneratedBy.Native();
			OptimisticLock.Version();
			Map(x => x.AssessmentWindowId, "assessmentWindowId");
			Map(x => x.CourseSummary, "courseSummary");
			Map(x => x.Grading, "grading");
            Map(x => x.IsMainInstructor, "isMainInstructor").CustomType<BoolAsTinyInt>();
			Map(x => x.LecturePlan, "lecturePlan");
			Map(x => x.Status, "status");
			References(x => x.Course)
				.Class(typeof(Course))
				.Not.Nullable()	
				.Column("courseId")
				.Fetch.Select()
				.Cascade.All();
            //References(x => x.Semester)
            //    .Class(typeof(Semester))
            //    .Not.Nullable()	
            //    .Column("semesterId")
            //    .Fetch.Select()
            //    .Cascade.All();
			References(x => x.StaffPosition)
				.Class(typeof(InstructorPosition))
				.Not.Nullable()	
				.Column("staffPositionId")
				.Fetch.Select()
				.Cascade.All();
            
		    Cache.ReadWrite();
		}
	}
}