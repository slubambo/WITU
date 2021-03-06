using System;
using WITU.Core.Model;
using FluentNHibernate.Mapping;

namespace WITU.Core.Mappings
{
	public class CampusMapping : ClassMap<Campus>
	{
		public CampusMapping()
		{
			Table("`campus`");
			Schema("witu");
			Id(x => x.Id, "id")
				.GeneratedBy.Native();
			OptimisticLock.Version();
			Map(x => x.Code, "code");
			Map(x => x.Description, "description");
			Map(x => x.Name, "name");
			Map(x => x.ShortName, "shortName");
			//HasMany(x => x.Programs)
			//	.KeyColumn("campusId")
			//	.Inverse()
			//	.Cascade.All()
			//	.Fetch.Select()
			//	.AsSet();
			References(x => x.Institution)
				.Class(typeof(Institution))
				.Not.Nullable()	
				.Column("instituteId")
				.Fetch.Select()
				.Cascade.All();
		    Cache.ReadWrite();
		}
	}
}