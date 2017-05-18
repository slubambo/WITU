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
    public class AcademicYearRepository : IAcademicYearRepository
    {
        private readonly ISession _session;

        public AcademicYearRepository(ISession session)
        {
            _session = session;
        }

        public CohortYear GetAcademicYearByName(string name)
        {
            var year = _session.Query<CohortYear>().Cacheable().CacheMode(CacheMode.Normal).FirstOrDefault(x => x.Name == name);
            return year;
        }

        public IEnumerable<CohortYear> GetAcademicYears(long startYear, double noOfYears = 1)
        {
            var years = new List<long>();

            for (var i = 0; i < noOfYears; i++)
            {
                years.Add(startYear + i);
            }

            //now retrieve the years...
            var academicYears = _session.QueryOver<CohortYear>().Where(x => x.StartYear.IsIn(years)).Cacheable().CacheMode(CacheMode.Normal).List<CohortYear>();

            return academicYears;
        }

        public CohortYear GetLatestAcademicYear()
        {

            var acYear = _session.Query<CohortYear>().OrderByDescending(x => x.StartYear).Cacheable().CacheMode(CacheMode.Normal).FirstOrDefault(/*x => x.StartDate <= DateTime.Today*/);
            return acYear;
        }
    }
}
