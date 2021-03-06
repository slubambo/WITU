using System;
using WITU.Core.Model;
using FluentNHibernate.Mapping;

namespace WITU.Core.Mappings
{
	public class CountryMapping : ClassMap<Country>
	{
		public CountryMapping()
		{
			Table("`country`");
			Schema("witu");
			Id(x => x.Id, "id")
				.GeneratedBy.Native();
			OptimisticLock.Version();
			Map(x => x.CountryCode, "countryCode");
			Map(x => x.CreatedOn, "createdOn");
			Map(x => x.LastModified, "lastModified");
			Map(x => x.Name, "name");
			Map(x => x.Nationality, "nationality");
			HasMany(x => x.People)
				.KeyColumn("countryOfBirthId")
				.Inverse()
				.Cascade.All()
				.Fetch.Select()
				.AsSet();
			HasMany(x => x.People1)
				.KeyColumn("countryOfResidenceId")
				.Inverse()
				.Cascade.All()
				.Fetch.Select()
				.AsSet();
			HasMany(x => x.People2)
				.KeyColumn("nationalityId")
				.Inverse()
				.Cascade.All()
				.Fetch.Select()
				.AsSet();
		    Cache.ReadOnly();
		}
	}
}