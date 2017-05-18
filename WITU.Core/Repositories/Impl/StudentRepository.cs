using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WITU.Core.Model;
using WITU.Core.Repositories.Interfaces;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Dialect;
using NHibernate.Linq;
using NHibernate.Transform;

namespace WITU.Core.Repositories.Impl
{
    public class StudentRepository: CoreRepository, IStudentRepository
    {
        private readonly ISession _session;

        public StudentRepository(ISession session) : base(session)
        {
            _session = session;
        }
        public Model.Student GetStudent(string username)
        {
            var user = _session.Query<Student>().Cacheable().CacheMode(CacheMode.Normal).FirstOrDefault(x => x.RegistrationNumber == username);
            return user;
        }

       public IEnumerable<Cohort> StudentSemesters(Student student, Cohort givenSemester)
        {
            //bases on the condition that the start academic year is from semester one...
            var startSemester =
                _session.Query<Cohort>().FirstOrDefault(x => x.CohortYear.Id == student.StartAcademicYear.Id
                                                               && x.SemesterSession == 1);
            var criteria = _session.CreateCriteria<Cohort>("sem")
                .CreateAlias("sem.AcademicYear", "ay");
            if (startSemester != null)
            {
                var disjunction = Restrictions.Disjunction();
                for (var i = student.StartAcademicYear.StartYear; i <= givenSemester.CohortYear.StartYear; i++)
                {
                    var conjuction = Restrictions.Conjunction();
                    conjuction.Add(Restrictions.Eq("ay.StartYear", i));
                    conjuction.Add(Restrictions.Eq("sem.SemesterSession", 1));
                    disjunction.Add(conjuction);

                    if (i != givenSemester.CohortYear.StartYear ||
                        (i == givenSemester.CohortYear.StartYear && givenSemester.SemesterSession == 2))
                    {
                        var conjuction2 = Restrictions.Conjunction();
                        conjuction2.Add(Restrictions.Eq("ay.StartYear", i));
                        conjuction2.Add(Restrictions.Eq("sem.SemesterSession", 2));
                        disjunction.Add(conjuction2);
                    }
                }

                criteria.Add(disjunction);
            }

            //finally we populate the conditions...
            var semesters = criteria.SetCacheable(true).SetCacheMode(CacheMode.Normal).List<Cohort>();

            return semesters;
        }

        
        public IList<Student> GetStudents(int[] ids)
        {
            var query = _session.QueryOver<Student>().WhereRestrictionOn(x => x.Id).IsIn(ids);
            return query.Cacheable().CacheMode(CacheMode.Normal).List<Student>();
        }

        
    }
}
