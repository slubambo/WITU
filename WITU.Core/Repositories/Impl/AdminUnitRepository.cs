using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WITU.Core.Model;
using WITU.Core.Repositories.Interfaces;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace WITU.Core.Repositories.Impl
{
    public class AdminUnitRepository : IAdminUnitRepository
    {
         private readonly ISession _session;

         public AdminUnitRepository(ISession session)
        {
            _session = session;
        }
         
        public bool CampusExists(string campusName, int campusId)
        {
            var query =
                 _session.CreateCriteria<Campus>("c")
                     .Add(Restrictions.Eq("c.Name", campusName));

            if (campusId > 0)
                query.Add(Restrictions.Not(Restrictions.Eq("Id", campusId)));

            var campus = query.UniqueResult<Campus>();
            return campus != null;
        }
        
    }
}
