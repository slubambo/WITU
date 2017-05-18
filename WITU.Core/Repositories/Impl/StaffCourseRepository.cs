using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WITU.Core.Model;
using WITU.Core.Repositories.Interfaces;
using NHibernate;
using NHibernate.Linq;

namespace WITU.Core.Repositories.Impl
{
    public class StaffCourseRepository:IStaffCourseRepository
    {
        private readonly ISession _session;

        public StaffCourseRepository(ISession session)
        {
            _session = session;
        }

        public StaffCourse GetById(int id)
        {
            return _session.Query<StaffCourse>().FirstOrDefault(x => x.Id == id);
        }
        //public IEnumerable<StaffCourse> GetStaffCoursesBySemId(int semId)
        //{
        //    return new List<StaffCourse>(_session.Query<StaffCourse>().Where(x => x.Semester.Id == semId));
        //}
        public IEnumerable<StaffCourse> GetStaffCoursesByCourseIdandSemester(int courseId, int semesterId)
        {
            //return new List<StaffCourse>(_session.Query<StaffCourse>().Where(x => x.Course.Id == courseId && x.Semester.Id == semesterId));
            return new List<StaffCourse>(_session.Query<StaffCourse>().Where(x => x.Course.Id == courseId));
        }

    }
}
