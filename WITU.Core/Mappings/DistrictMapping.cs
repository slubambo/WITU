using System;
using WITU.Core.Model;
using FluentNHibernate.Mapping;

namespace WITU.Core.Mappings
{
	public class DistrictMapping : ClassMap<District>
	{
		public DistrictMapping()
		{
			Table("`district`");
			Schema("witu");
			Id(x => x.Id, "id")
				.GeneratedBy.Native();
			OptimisticLock.Version();
			Map(x => x.Code, "code");
			Map(x => x.CreatedOn, "createdOn");
			Map(x => x.LastModified, "lastModified");
			Map(x => x.Name, "name");
			HasMany(x => x.Counties)
				.KeyColumn("districtId")
				.Inverse()
				.Cascade.All()
				.Fetch.Select()
				.AsSet();
			HasMany(x => x.People)
				.KeyColumn("districtOfBirthId")
				.Inverse()
				.Cascade.All()
				.Fetch.Select()
				.AsSet();
			HasMany(x => x.People1)
				.KeyColumn("districtOfResidenceId")
				.Inverse()
				.Cascade.All()
				.Fetch.Select()
				.AsSet();
		    Cache.ReadOnly();
		}
	}
}