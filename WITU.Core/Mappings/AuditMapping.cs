using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WITU.Core.Model;
using FluentNHibernate.Mapping;

namespace WITU.Core.Mappings
{
    public class AuditMapping : ClassMap<Audit>
    {
        public AuditMapping()
        {
            Table("`Audit`");
            Schema("witu");
            Id(x => x.Id, "id")
                .GeneratedBy.Native();
            OptimisticLock.Version();
            Map(x => x.Message, "Message");
            Map(x => x.UserType, "UserType");
            Map(x => x.IpAddress, "IpAddress");
            Map(x => x.UserName, "UserName");
            Map(x => x.UrlAccessed, "UrlAccessed");
            Map(x => x.TimeAccessed, "TimeAccessed");
            Map(x => x.Data, "Data");
        }
    }
}
