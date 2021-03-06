using System;
using WITU.Core.Model;
using FluentNHibernate.Mapping;

namespace WITU.Core.Mappings
{
	public class CohortMapping : ClassMap<Cohort>
	{
		public CohortMapping()
		{
			Table("`semester`");
			Schema("witu");
			Id(x => x.Id, "id")
				.GeneratedBy.Native();
			OptimisticLock.Version();
			Map(x => x.AssessmentEndDate, "assessmentEndDate");
			Map(x => x.AssessmentStartDate, "assessmentStartDate");
			Map(x => x.EndDate, "endDate");
			Map(x => x.RegistrationDeadline, "registrationDeadline");
			Map(x => x.ResultsUploadDeadline, "resultsUploadDeadline");
			Map(x => x.SemesterSession, "semesterSession");
			Map(x => x.StartDate, "startDate");
			Map(x => x.Status, "status");
            References(x => x.CohortYear)
				.Class(typeof(CohortYear))
				.Not.Nullable()	
				.Column("academicYearId")
				.Fetch.Select()
				.Cascade.All();
			HasMany(x => x.SemesterRegistrations)
				.KeyColumn("semsterId")
				.Inverse()
				.Cascade.All()
				.Fetch.Select()
				.AsSet();
			HasMany(x => x.StaffCourses)
				.KeyColumn("semesterId")
				.Inverse()
				.Cascade.All()
				.Fetch.Select()
				.AsSet();
			HasMany(x => x.Students)
				.KeyColumn("semesterEntryId")
				.Inverse()
				.Cascade.All()
				.Fetch.Select()
				.AsSet();
            HasMany(x => x.StudentCourses)
                .KeyColumn("semesterId")
                .KeyNullable()
                .Inverse()
                .Cascade.All()
                .Fetch.Select()
                .AsSet()
                //deleted courses are not to be included...
                .Where(x => x.ResultStatus != ResultStatus.DeletedApproved)
                ;
		    Cache.ReadWrite();
		}
	}
}