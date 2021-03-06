using System;
using WITU.Core.Model;
using WITU.Core.Utils;
using FluentNHibernate.Mapping;

namespace WITU.Core.Mappings
{
    public class RoleMapping : ClassMap<Role>
    {
        public RoleMapping()
        {
            Table("`role`");
            Schema("witu");
            Id(x => x.Id, "id")
                .GeneratedBy.Native();
            OptimisticLock.Version();
            Map(x => x.RequiresAccessScope, "requiresAccessScope").CustomType<BoolAsTinyInt>();
            Map(x => x.CanCreate, "canCreate").CustomType<BoolAsTinyInt>();
            Map(x => x.CanDelete, "canDelete").CustomType<BoolAsTinyInt>();
            Map(x => x.CanUpdate, "canUpdate").CustomType<BoolAsTinyInt>();
            Map(x => x.CanRead, "canRead").CustomType<BoolAsTinyInt>();
            Map(x => x.Type, "type");
            References(x => x.Service)
                .Class(typeof(Service))
                .Column("serviceId")
                .Fetch.Select()
                .Cascade.All();
            HasMany(x => x.UserRoles)
                .KeyColumn("roleId")
                .Inverse()
                .Cascade.All()
                .Fetch.Select()
                .AsSet();
            //Cache.ReadWrite();
        }
    }
}