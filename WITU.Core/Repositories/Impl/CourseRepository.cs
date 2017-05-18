using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using WITU.Core.Model;
using WITU.Core.Repositories.Interfaces;

namespace WITU.Core.Repositories.Impl
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ISession _session;

        public CourseRepository(ISession session)
        {
            _session = session;
        }

        public List<Course> GetCourses()
        {
            var query = _session.CreateCriteria<Course>().SetCacheable(true).SetCacheMode(CacheMode.Normal);
            return (List<Course>) query.List<Course>();
        }
    }
}
