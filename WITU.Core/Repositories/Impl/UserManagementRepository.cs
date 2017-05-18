using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WITU.Core.Model;
using WITU.Core.Repositories.Interfaces;
using FluentNHibernate.Utils;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace WITU.Core.Repositories.Impl
{
    public class UserManagementRepository : IUserManagementRepository
    {
        private readonly ISession _session;

        public UserManagementRepository(ISession session)
        {
            _session = session;
        }

        public IEnumerable<Position> GetPositionsByCategoryId(int positionCategoryId)
        {
            ICriteria query =
                _session.CreateCriteria<Position>("p")
                    .CreateAlias("p.PositionCategory", "pp")
                    .Add(Restrictions.Eq("pp.Id", positionCategoryId));

            return query.SetCacheable(true).SetCacheMode(CacheMode.Normal).List<Position>();
        }

        public IEnumerable<Service> GetActiveServices(int[] inactiveServiceCategories)
        {

            var toremove = inactiveServiceCategories.ToList();

            var services =
                _session.Query<Service>()
                    .Cacheable()
                    .CacheMode(CacheMode.Normal)
                    .ToList();
            
            foreach (var service in services.ToList().Where(service => service.Category != null && toremove.Contains((int) service.Category)))
            {
                services.Remove(service);
            }

            return services;
        }

        public AccessRole GetAccessRoleByUserId(int userId)
        {
            var accessRole = _session.Query<AccessRole>().Cacheable().CacheMode(CacheMode.Normal).FirstOrDefault(x => x.User.Id == userId);
            return accessRole ?? null;
        }

        public IEnumerable<UserRole> GetUserRolesByUserId(int userId)
        {
            IQueryable<UserRole> userRoles = _session.Query<UserRole>().Where(x => x.User.Id == userId).Cacheable().CacheMode(CacheMode.Normal);
            return userRoles;

        }


        public UserRole GetUserRoleByRoleId(int roleId)
        {
            UserRole userRole = _session.Query<UserRole>().Cacheable().CacheMode(CacheMode.Normal).FirstOrDefault(x => x.Role.Id == roleId);
            return userRole;

        }
    }
}
