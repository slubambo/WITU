using System;
using WITU.Core.Model;
using WITU.Core.Utils;
using FluentNHibernate.Mapping;

namespace WITU.Core.Mappings
{
	public class CohortYearMapping : ClassMap<CohortYear>
	{
		public CohortYearMapping()
		{
			Table("`academic_year`");
			Schema("witu");
			Id(x => x.Id, "id")
				.GeneratedBy.Native();
			OptimisticLock.Version();
			Map(x => x.CreatedOn, "createdOn");
			Map(x => x.EndDate, "endDate");
			Map(x => x.EndYear, "endYear");
            Map(x => x.IsDefaultAcademicYear, "isDefaultAcademicYear").CustomType<BoolAsTinyInt>();
			Map(x => x.Name, "name");
			Map(x => x.StartDate, "startDate");
			Map(x => x.StartYear, "startYear");
			
			//HasMany(x => x.Programs)
			//	.KeyColumn("endAcademicYearId")
			//	.Inverse()
			//	.Cascade.All()
			//	.Fetch.Select()
			//	.AsSet();
			//HasMany(x => x.Programs1)
			//	.KeyColumn("startAcademicYearId")
			//	.Inverse()
			//	.Cascade.All()
			//	.Fetch.Select()
			//	.AsSet();
			HasMany(x => x.Semesters)
				.KeyColumn("academicYearId")
				.Inverse()
				.Cascade.All()
				.Fetch.Select()
				.AsSet();
            HasMany(x => x.Students)
                .KeyColumn("startAcademicYearId")
                .Inverse()
                .Cascade.All()
                .Fetch.Select()
                .AsSet();
            HasMany(x => x.GeneralInformationAttachments)
                .KeyColumn("academicYearId")
                .Inverse()
                .Cascade.All()
                .Fetch.Select()
                .AsSet();
            
		    Cache.ReadWrite();
		}
	}
}