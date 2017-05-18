using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WITU.Core.Model;
using WITU.Core.Repositories.Interfaces;
using NHibernate;
using NHibernate.Linq;
using WITU.Core.Model;

namespace WITU.Core.Repositories.Impl
{
    public class SemesterRegistrationRepository:ISemesterRegistrationRepository
    {
        private readonly ISession _session;

        public SemesterRegistrationRepository(ISession session)
        {
            _session = session;
        }
        public CohortRegistration GetSemesterRegistrationBySemesterAndStudentId(int semesterId, int studentId)
        {
            var query = _session.Query<CohortRegistration>().Cacheable().CacheMode(CacheMode.Normal).FirstOrDefault(x => x.Cohort.Id == semesterId && x.Student.Id == studentId);
            return query;
        }

        public CohortRegistration GetSemesterRegistrationById(int semesterRegistrationId)
        {
            var query = _session.Query<CohortRegistration>().Cacheable().CacheMode(CacheMode.Normal).FirstOrDefault(x => x.Id ==semesterRegistrationId);
            return query;
        }

        public IEnumerable<CohortRegistration> GetSemesterRegistrationByStudentId(int studentId)
        {
            var semesterRegistrations = new List<CohortRegistration>(_session.Query<CohortRegistration>().Where(x => x.Student.Id == studentId).Cacheable().CacheMode(CacheMode.Normal));
            return semesterRegistrations;
        }
    }
}
