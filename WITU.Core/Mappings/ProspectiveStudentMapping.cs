using System;
using WITU.Core.Model;
using FluentNHibernate.Mapping;

namespace WITU.Core.Mappings
{
	public class ProspectiveStudentMapping : ClassMap<ProspectiveStudent>
	{
		public ProspectiveStudentMapping()
		{
			Table("`prospective_student`");
			Schema("witu");
			Id(x => x.Id, "id")
				.GeneratedBy.Native();
			OptimisticLock.Version();
			Map(x => x.ApplicationNumber, "applicationNumber");
			Map(x => x.CreatedOn, "createdOn");
			Map(x => x.SignitureName, "signitureName");
			Map(x => x.UnderGradEntryKind, "underGradEntryKind");
			
			References(x => x.Person)
				.Class(typeof(Person))	
				.Column("personId")
				.Fetch.Select()
				.Cascade.All();
			
			HasMany(x => x.References)
				.KeyColumn("prospectiveStudentId")
				.Inverse()
				.Cascade.All()
				.Fetch.Select()
				.AsSet();
			
			HasMany(x => x.Students)
				.KeyColumn("prospectiveStudentId")
				.Inverse()
				.Cascade.All()
				.Fetch.Select()
				.AsSet();
			References(x => x.User)
				.Class(typeof(User))	
				.Column("userId")
				.Fetch.Select()
				.Cascade.All();
		}
	}
}