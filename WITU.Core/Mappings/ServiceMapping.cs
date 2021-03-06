using System;
using WITU.Core.Model;
using FluentNHibernate.Mapping;

namespace WITU.Core.Mappings
{
	public class ServiceMapping : ClassMap<Service>
	{
		public ServiceMapping()
		{
			Table("`service`");
			Schema("witu");
			Id(x => x.Id, "id")
				.GeneratedBy.Native();
			OptimisticLock.Version();
			Map(x => x.Category, "category");
			Map(x => x.Name, "name");
			HasMany(x => x.Roles)
				.KeyColumn("serviceId")
				.Inverse()
				.Cascade.All()
				.Fetch.Select()
				.AsSet();
		    Cache.ReadWrite();
		}
	}
}