using System;
using WITU.Core.Model;
using FluentNHibernate.Mapping;

namespace WITU.Core.Mappings
{
    public class AccessRoleMapping : ClassMap<AccessRole>
    {
        public AccessRoleMapping()
        {
            Table("`access_role`");
            Schema("witu");
            Id(x => x.Id, "id")
                .GeneratedBy.Native();
            OptimisticLock.Version();
            Map(x => x.Level, "level");
            Map(x => x.LevelId, "levelId");
            Map(x => x.LevelName, "levelName");
            References(x => x.User)
                .Class(typeof(User))
                .Not.Nullable()
                .Column("userId")
                .Fetch.Select()
                .Cascade.All();
            HasMany(x => x.UserRoles)
                .KeyColumn("accessRoleId")
                .Inverse()
                .Cascade.All()
                .Fetch.Select()
                .AsSet();
        }
    }
}