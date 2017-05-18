using System;
using WITU.Core.Model;
using FluentNHibernate.Mapping;

namespace WITU.Core.Mappings
{
    public class PositionCategoryMapping : ClassMap<PositionCategory>
    {
        public PositionCategoryMapping()
		{
            Table("`position_category`");
			Schema("witu");
			Id(x => x.Id, "id")
				.GeneratedBy.Native();
			OptimisticLock.Version();
			Map(x => x.Name, "name");
			HasMany(x => x.Positions)
                .KeyColumn("positionCategoryId")
				.Inverse()
				.Cascade.All()
				.Fetch.Select()
				.AsSet();
            Cache.ReadWrite();
		}
    }
}
