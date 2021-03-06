using System;
using WITU.Core.Model;
using FluentNHibernate.Mapping;

namespace WITU.Core.Mappings
{
	public class StudentMapping : ClassMap<Student>
	{
		public StudentMapping()
		{
			Table("`student`");
			Schema("witu");
			Id(x => x.Id, "id")
				.GeneratedBy.Native();
			OptimisticLock.Version();
			Map(x => x.CurrentYear, "currentYear");
			Map(x => x.RegistrationNumber, "registrationNumber");
			Map(x => x.Remark, "remark");
			Map(x => x.StudentStatus, "studentStatus");
			Map(x => x.StudentNumber, "studentNumber");
            Map(x => x.ResidenceStatus, "residenceStatus");
            Map(x => x.RegistrationStatus, "registrationStatus");
            Map(x => x.EntryYear, "entryYear");
            Map(x => x.CurrentCGPA, "currentCGPA");
            Map(x => x.CurrentCTCU, "currentCTCU");
            Map(x => x.CurrentAward, "currentAward");
           	References(x => x.Person)
				.Class(typeof(Person))
				.Not.Nullable()	
				.Column("personId")
				.Fetch.Select()
				.Cascade.All();
			References(x => x.ProspectiveStudent)
				.Class(typeof(ProspectiveStudent))	
				.Column("prospectiveStudentId")
				.Fetch.Select()
				.Cascade.All();
			References(x => x.Semester)
				.Class(typeof(Cohort))
				.Nullable()	
				.Column("semesterEntryId")
				.Fetch.Select()
				.Cascade.All();
			HasMany(x => x.SemesterRegistrations)
				.KeyColumn("studentId")
				.Inverse()
				.Cascade.All()
				.Fetch.Select()
				.AsSet();
			References(x => x.User)
				.Class(typeof(User))	
				.Column("userId")
				.Fetch.Select()
				.Cascade.All();
            References(x => x.StartAcademicYear)
                .Class(typeof(CohortYear))
                .Column("startAcademicYearId")
                .Fetch.Select()
                .Cascade.All();
            HasMany(x => x.StudentCourses)
                .KeyColumn("studentId")
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