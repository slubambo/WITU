using System;
using WITU.Core.Model;
using FluentNHibernate.Mapping;

namespace WITU.Core.Mappings
{
	public class SubCountyMapping : ClassMap<SubCounty>
	{
		public SubCountyMapping()
		{
			Table("`sub_county`");
			Schema("witu");
			Id(x => x.Id, "id")
				.GeneratedBy.Native();
			OptimisticLock.Version();
			Map(x => x.Code, "code");
			Map(x => x.CreatedOn, "createdOn");
			Map(x => x.LastModified, "lastModified");
			Map(x => x.Name, "name");
			References(x => x.County)
				.Class(typeof(County))
				.Not.Nullable()	
				.Column("countyId")
				.Fetch.Select()
				.Cascade.All();
			HasMany(x => x.People)
				.KeyColumn("subCountyOfBirthId")
				.Inverse()
				.Cascade.All()
				.Fetch.Select()
				.AsSet();
			HasMany(x => x.People1)
				.KeyColumn("subCountyOfResidenceId")
				.Inverse()
				.Cascade.All()
				.Fetch.Select()
				.AsSet();
		    Cache.ReadOnly();
		}
	}
}