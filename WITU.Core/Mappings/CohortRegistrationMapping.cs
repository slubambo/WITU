using System;
using WITU.Core.Model;
using FluentNHibernate.Mapping;

namespace WITU.Core.Mappings
{
	public class CohortRegistrationMapping : ClassMap<CohortRegistration>
	{
		public CohortRegistrationMapping()
		{
			Table("`semester_registration`");
			Schema("witu");
			Id(x => x.Id, "id")
				.GeneratedBy.Native();
			OptimisticLock.Version();
			Map(x => x.AccumulatedCreditUnits, "accumulatedCreditUnits");
			Map(x => x.Cgpa, "cgpa");
			Map(x => x.Gpa, "gpa");
			Map(x => x.RegistrationApprovalDateTime, "registrationApprovalDateTime");
			Map(x => x.RegistrationDateTime, "registrationDateTime");
			Map(x => x.SemesterCreditUnits, "semesterCreditUnits");
			Map(x => x.StudentRemark, "studentRemark");
			Map(x => x.StudentStatus, "studentStatus");
			References(x => x.Cohort)
				.Class(typeof(Cohort))
				.Not.Nullable()	
				.Column("semsterId")
				.Fetch.Select()
				.Cascade.All();
			References(x => x.Student)
				.Class(typeof(Student))
				.Not.Nullable()	
				.Column("studentId")
				.Fetch.Select()
				.Cascade.All();
            HasMany(x => x.StudentCourses)
                .KeyColumn("semesterRegistrationId")
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