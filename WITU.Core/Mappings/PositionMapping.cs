using System;
using WITU.Core.Model;
using FluentNHibernate.Mapping;

namespace WITU.Core.Mappings
{
    public class PositionMapping : ClassMap<Position>
    {
        public PositionMapping()
        {
            Table("`position`");
            Schema("witu");
            Id(x => x.Id, "id")
                .GeneratedBy.Native();
            OptimisticLock.Version();
            Map(x => x.Level, "level");
            Map(x => x.Name, "name");
            References(x => x.PositionCategory)
                .Class(typeof(PositionCategory))
                .Not.Nullable()
                .Column("positionCategoryId")
                .Fetch.Select()
                .Cascade.All();
            HasMany(x => x.StaffPositions)
                .KeyColumn("positionId")
                .Inverse()
                .Cascade.All()
                .Fetch.Select()
                .AsSet();
            Cache.ReadOnly();
        }
    }
}