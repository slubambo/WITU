using System;
using WITU.Core.Model;
using FluentNHibernate.Mapping;

namespace WITU.Core.Mappings
{
	public class ReferenceMapping : ClassMap<Reference>
	{
		public ReferenceMapping()
		{
			Table("`reference`");
			Schema("witu");
			Id(x => x.Id, "id")
				.GeneratedBy.Native();
			OptimisticLock.Version();
			Map(x => x.AttachedFileName, "attachedFileName");
			Map(x => x.Desigination, "desigination");
			Map(x => x.Organization, "organization");
			Map(x => x.Relationship, "relationship");
			References(x => x.Person)
				.Class(typeof(Person))	
				.Column("personId")
				.Fetch.Select()
				.Cascade.All();
			References(x => x.ProspectiveStudent)
				.Class(typeof(ProspectiveStudent))
				.Not.Nullable()	
				.Column("prospectiveStudentId")
				.Fetch.Select()
				.Cascade.All();
		}
	}
}