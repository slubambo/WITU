using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WITU.Core.Model;
using WITU.Core.Repositories.Interfaces;
using FluentNHibernate.Data;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace WITU.Core.Repositories.Impl
{
    public class AuditRepository : IAuditRepository
    {
        private readonly ISession _session;

        public AuditRepository(ISession session)
        {
            _session = session;
        }


        public IEnumerable<Audit> GetStudentTrail()
        {
            var query = _session.CreateCriteria<Audit>().Add(Restrictions.Eq("UserType", "2"));
            return query.SetCacheable(true).SetCacheMode(CacheMode.Normal).List<Audit>();

        }

        public IEnumerable<Audit> GetStaffTrail()
        {
            var query = _session.CreateCriteria<Audit>().Add(Restrictions.Eq("UserType", "1"));
            return query.SetCacheable(true).SetCacheMode(CacheMode.Normal).List<Audit>();
        }

        public bool SaveAudit(object entity)
        {
            var result = false;
            if (!_session.IsOpen)
            {
                _session.Connection.Open();
            }
            using (var txn = _session.BeginTransaction())
            {
                try
                {
                    _session.SaveOrUpdate(entity);
                    txn.Commit();
                    txn.Dispose();
                    result = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    txn.Rollback();
                    _session.Close();
                }
            }
            return result;
        }
    }
}
