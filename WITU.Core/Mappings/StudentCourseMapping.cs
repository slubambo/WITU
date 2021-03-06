using System;
using WITU.Core.Model;
using WITU.Core.Utils;
using FluentNHibernate.Mapping;

namespace WITU.Core.Mappings
{
	public class StudentCourseMapping : ClassMap<StudentCourse>
	{
		public StudentCourseMapping()
		{
			Table("`student_course`");
			Schema("witu");
            Id(x => x.Id, "id")
                .GeneratedBy.Native();
			OptimisticLock.Version();
			Map(x => x.ContinousAssessment, "continousAssessment");
			Map(x => x.ExamMark, "examMark");
			Map(x => x.FinalMark, "finalMark");
            Map(x => x.HasAssessed, "hasAssessed").CustomType<NullableBoolAsTinyInt>();
			Map(x => x.LastModified, "lastModified");
			Map(x => x.PerformanceTrack, "performanceTrack");
            Map(x => x.CourseType, "courseType");
			Map(x => x.ResultStatus, "resultStatus");
			Map(x => x.ResultUploadDate, "resultUploadDate");
			References(x => x.StaffCourse)
				.Class(typeof(StaffCourse))
				.Column("staffCourseId")
				.Fetch.Select()
				.Cascade.All();
            References(x => x.Course)
                .Class(typeof(Course))
                .Column("courseId")
                .Fetch.Select()
                .Cascade.All();
		    References(x => x.SemesterRegistration)
		        .Class(typeof (CohortRegistration))
		        .Column("semesterRegistrationId")
                .Nullable()
		        .Fetch.Select()
		        .Cascade.All();
            References(x => x.Semester)
                .Class(typeof(Cohort))
                .Column("semesterId")
                .Fetch.Select()
                .Cascade.All();
            References(x => x.Student)
                .Class(typeof(Student))
                .Column("studentId")
                .Fetch.Select()
                .Cascade.All();
		    Cache.ReadWrite();

//            ApplyFilter<ResultStatusFilter>("ResultStatus != :resultStatus");

		    //Add a where clause for deleting...
		    //Where("resultStatus != 6"); //resultStatus should not be equal to 6 if we are to select an item.
		}
	}
}