using System;
using WITU.Core.Model;
using FluentNHibernate.Mapping;

namespace WITU.Core.Mappings
{
	public class InstitutionMapping : ClassMap<Institution>
	{
		public InstitutionMapping()
		{
			Table("`institution`");
			Schema("witu");
			Id(x => x.Id, "id")
				.GeneratedBy.Native();
			OptimisticLock.Version();
			Map(x => x.CreatedOn, "createdOn");
			Map(x => x.Description, "description");
			Map(x => x.LogoPathName, "logoPathName");
			Map(x => x.Name, "name");
			Map(x => x.ShortName, "shortName");
			HasMany(x => x.Campus)
				.KeyColumn("universityId")
				.Inverse()
				.Cascade.All()
				.Fetch.Select()
				.AsSet();
			
		    Cache.ReadWrite();
		}
	}
}