using System;
using WITU.Core.Model;
using FluentNHibernate.Mapping;

namespace WITU.Core.Mappings
{
	public class CountyMapping : ClassMap<County>
	{
		public CountyMapping()
		{
			Table("`county`");
			Schema("witu");
			Id(x => x.Id, "id")
				.GeneratedBy.Native();
			OptimisticLock.Version();
			Map(x => x.Code, "code");
			Map(x => x.CreatedOn, "createdOn");
			Map(x => x.LastModified, "lastModified");
			Map(x => x.Name, "name");
			References(x => x.District)
				.Class(typeof(District))
				.Not.Nullable()	
				.Column("districtId")
				.Fetch.Select()
				.Cascade.All();
			HasMany(x => x.People)
				.KeyColumn("countyOfBirthId")
				.Inverse()
				.Cascade.All()
				.Fetch.Select()
				.AsSet();
			HasMany(x => x.People1)
				.KeyColumn("countyOfResidenceId")
				.Inverse()
				.Cascade.All()
				.Fetch.Select()
				.AsSet();
			HasMany(x => x.SubCounties)
				.KeyColumn("countyId")
				.Inverse()
				.Cascade.All()
				.Fetch.Select()
				.AsSet();
		    Cache.ReadOnly();
		}
	}
}