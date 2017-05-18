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
    public class SemesterRepository : CoreRepository, ISemesterRepository
    {
        private readonly ISession _session;

        public SemesterRepository(ISession session) : base(session)
        {
            _session = session;
        }

        public IEnumerable<Cohort> GetSemestersByAcademicYear(int academicYearId)
        {
            var semesters = new List<Cohort>(_session.Query<Cohort>().Where(x => x.CohortYear.Id == academicYearId).Cacheable().CacheMode(CacheMode.Normal));
            return semesters;
        }

        
        public Cohort GetCurrentSemester(DateTime dateTime)
        {
            var semester = _session.Query<Cohort>().Cacheable().CacheMode(CacheMode.Normal).FirstOrDefault(x => x.StartDate <= dateTime && x.EndDate >= dateTime);
            return semester;
        }

        public Cohort GetCurrentOrLatestSemester()
        {
            var semester = GetCurrentSemester(DateTime.Now) ??
                           _session.Query<Cohort>().OrderByDescending(x => x.EndDate).Cacheable().CacheMode(CacheMode.Normal).FirstOrDefault(x =>  x.EndDate <= DateTime.Today);
            return semester;
        }

        public Cohort GetSemester(int academicYearId, int semesterSession)
        {
            var semester =
                _session.Query<Cohort>().Cacheable().CacheMode(CacheMode.Normal)
                    .FirstOrDefault(x => x.CohortYear.Id == academicYearId && x.SemesterSession == semesterSession);
            return semester;
        }

        public IEnumerable<Cohort> GetSemesters(Cohort givenSemester, int yearOfStudy)
        {
            
            //creating a restriction...
            var criteria = _session.CreateCriteria<Cohort>("semester")
                .CreateAlias("semester.AcademicYear", "academicYear");

            var disjunction = Restrictions.Disjunction();
            for (var i = 1; i <= yearOfStudy; i++)
            {
                for (var j = 1; j <= 2; j++)
                {
                    if ((i < yearOfStudy) || (i == yearOfStudy && j <= givenSemester.SemesterSession))
                    {
                        var conjunction = Restrictions.Conjunction();
                        conjunction.Add(Restrictions.Eq("academicYear.StartYear", (short)((givenSemester.CohortYear.StartYear - yearOfStudy) + i)));
                        conjunction.Add(Restrictions.Eq("semester.SemesterSession", j));
                        disjunction.Add(conjunction);
                    }
                }
            }

            //finally we add to criteria...
            criteria.Add(disjunction);

            return criteria.SetCacheable(true).SetCacheMode(CacheMode.Normal).List<Cohort>();
        }
        
    }
}
