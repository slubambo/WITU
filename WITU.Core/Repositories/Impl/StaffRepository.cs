using System.Collections.Generic;
using System.Linq;
using WITU.Core.Model;
using WITU.Core.Repositories.Interfaces;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.SqlCommand;

namespace WITU.Core.Repositories.Impl
{
    public class StaffRepository : IStaffRepository
    {
        private readonly ISession _session;

        public StaffRepository(ISession session)
        {
            _session = session;
        }

        public Instructor GetStaffByUserId(int userid)
        {
            Instructor user = _session.Query<Instructor>().Cacheable().CacheMode(CacheMode.Normal).FirstOrDefault(x => x.User.Id == userid);
            return user;
        }

        public Instructor GetStaffByPersonId(int personId)
        {
            Instructor staff = _session.Query<Instructor>().Cacheable().CacheMode(CacheMode.Normal).FirstOrDefault(x => x.Person.Id == personId);
            return staff;
        }

        //List<StaffPosition> IStaffRepository.GetStaffPositionsByUserId(int userId)
        //{
        //    IQueryable<StaffPosition> staffPpsitions = null;

        //    staffPpsitions = _session.Query<StaffPosition>()
        //        .Where(x => x.Staff.User.Id == userId);

        //    return staffPpsitions.Cacheable().CacheMode(CacheMode.Normal).ToList();
        //}

        public List<User> GetStaffUsers(string username)
        {
            IQueryable<User> staffUsers = null;
            if (username != null)
            {
                staffUsers =
                    _session.Query<User>()
                        .Where(x => x.Username.Contains(username) && x.UserType == 1).Cacheable().CacheMode(CacheMode.Normal);
            }
            else
            {
                staffUsers =
                    _session.Query<User>()
                        .Where(x => x.UserType == 1).Cacheable().CacheMode(CacheMode.Normal);
            }

            return staffUsers.ToList();
        }
    }
}
