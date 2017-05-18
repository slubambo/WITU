using System;
using System.Collections.Generic;
using System.Linq;
using WITU.Core.Model;
using WITU.Core.Repositories.Interfaces;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace WITU.Core.Repositories.Impl
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ISession _session;

        public AccountRepository(ISession session)
        {
            _session = session;
        }

        public string GetUserName(int userId)
        {
            User user = _session.Query<User>().Cacheable().CacheMode(CacheMode.Normal).FirstOrDefault(x => x.Id == userId);
            if (user != null)
                return user.Username;
            return null;
        }

        public int GetUserId(string username)
        {
            User user = _session.Query<User>().Cacheable().CacheMode(CacheMode.Normal).FirstOrDefault(x => x.Username.Equals(username));
            if (user != null)
                return user.Id;
            return 0;
        }

        public User GetUserByName(string username)
        {
            User user = _session.Query<User>().Cacheable().CacheMode(CacheMode.Normal).FirstOrDefault(x => x.Username.Equals(username));
            return user;
        }

        public List<UserRole> UserRoles(int userId)
        {
            IQueryable<UserRole> userRoles = _session.Query<UserRole>().Where(x => x.User.Id == userId).Cacheable().CacheMode(CacheMode.Normal);
            return userRoles.ToList();
        }

        public string GetPasswordHash(int userId)
        {
            var password = _session.CreateCriteria<User>()
                .Add(Restrictions.Eq(Projections.Id(), userId))
                .SetProjection(Projections.ProjectionList().Add(Projections.Property("Password")))
                .UniqueResult<string>();
            return password;
        }

        public string GetRoleName(int roleId)
        {
            Role role = _session.Query<Role>().FirstOrDefault(x => x.Id == roleId);
            if (role != null)
                return role.Service.Name;
            return null;
        }

        public Role GetRoleByName(string roleName)
        {
            Role role = _session.Query<Role>().Cacheable().CacheMode(CacheMode.Normal).FirstOrDefault(x => x.Service.Name.Equals(roleName));
            return role;
        }

        public bool IsUserInRole(int userId, string roleName)
        {
            bool any = _session.Query<UserRole>().Cacheable().CacheMode(CacheMode.Normal).Any(x => x.User.Id == userId && x.Role.Service.Name.Equals(roleName));
            return any;
        }

        public Instructor GetUserInstructor(User user)
        {
            int userid = user.Id;
            Instructor instructor = _session.Query<Instructor>().Cacheable().CacheMode(CacheMode.Normal).FirstOrDefault(x => x.User.Id == userid);
            return instructor;
        }

        public Instructor GetUserInstructor(int userId)
        {
            Instructor instructor = _session.Query<Instructor>().Cacheable().CacheMode(CacheMode.Normal).FirstOrDefault(x => x.User.Id == userId);
            return instructor;
        }

        public Student GetUserStudent(User user)
        {
            int userid = user.Id;
            Student student = _session.Query<Student>().Cacheable().CacheMode(CacheMode.Normal).FirstOrDefault(x => x.User.Id == userid);
            return student;
        }

        public ProspectiveStudent GetUserProseProspectiveStudent(User user)
        {
            throw new NotImplementedException();
        }
    }
}
