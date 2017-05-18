using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WITU.Core.Model;
using FluentNHibernate.Mapping;

namespace WITU.Core.Mappings
{
    public class ClientMapping : ClassMap<Client>
    {
        public ClientMapping()
        {
            Table("`client`");
            Schema("witu");
            Id(x => x.Id, "id");
            OptimisticLock.Version();
            Map(x => x.Active, "active");
            Map(x => x.AllowedOrigin, "allowedOrigin");
            Map(x => x.ApplicationType, "applicationType");
            Map(x => x.Name, "name");
            Map(x => x.RefreshTokenLifeTime, "refreshTokenLifeTime");
            Map(x => x.Secret, "secret");

            Cache.ReadOnly();
        }
    }
}
