using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WITU.Core.Model;
using FluentNHibernate.Mapping;

namespace WITU.Core.Mappings
{
    public class RefreshTokenMapping: ClassMap<RefreshToken>
    {
        public RefreshTokenMapping()
        {
            Table("`refresh_token`");
            Schema("witu");
            Id(x => x.Id, "id");
            OptimisticLock.Version();
            Map(x => x.Subject, "subject");
            Map(x => x.IssuedUtc, "issuedUtc");
            Map(x => x.ExpiresUtc, "expiresUtc");
            Map(x => x.ProtectedTicket, "protectedTicket");
            Map(x => x.ClientId, "clientId");
            
        }
    }
}
