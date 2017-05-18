using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WITU.Core.Model;
using FluentNHibernate.Mapping;

namespace WITU.Core.Mappings
{
    public class UserRoleMapping : ClassMap<UserRole>
    {
        public UserRoleMapping()
        {
            Table("`user_role`");
            Schema("witu");
            Id(x => x.Id, "id")
                .GeneratedBy.Native();
            OptimisticLock.Version();
            References(x => x.User)
                .Class(typeof(User))
                .Not.Nullable()
                .Column("userId")
                .Fetch.Select()
                .Cascade.All();
            References(x => x.Role)
                .Class(typeof(Role))
                .Not.Nullable()
                .Column("roleId")
                .Fetch.Select()
                .Cascade.All();
            References(x => x.AccessRole)
                .Class(typeof(AccessRole))
                .Column("accessRoleId")
                .Fetch.Select()
                .Cascade.All();
            //Cache.ReadWrite();
        }
    }
}
