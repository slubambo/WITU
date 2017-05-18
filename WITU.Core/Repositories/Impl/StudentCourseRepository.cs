using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WITU.Core.Model;
using WITU.Core.Repositories.Interfaces;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.Transform;
using NHibernate.Validator.Engine;

namespace WITU.Core.Repositories.Impl
{
    public class StudentCourseRepository: IStudentCourseRepository
    {
        private readonly ISession _session;

        public StudentCourseRepository(ISession session)
        {
            _session = session;
        }

        public IEnumerable<StudentCourse> GetStudentCoursesByStudentId(int studentId)
        {
            return new List<StudentCourse>(_session.Query<StudentCourse>().Where(x => x.Student.Id == studentId && x.ResultStatus != ResultStatus.DeletedApproved));
        }

        public IEnumerable<StudentCourse> GetCurrentSemesterCreditTransferCourses(int studentId, int semesterId)
        {
            return new List<StudentCourse>(_session.Query<StudentCourse>().Where(x => x.Student.Id == studentId && x.Semester.Id == semesterId && x.PerformanceTrack == StudentCoursePerformanceTrack.CreditTransferPending));
        }

        public IEnumerable<StudentCourse> GetCurrentSemesterCoursesByPerformanceTrack(int studentCoursePerformanceTrack, int studentId, int semesterId)
        {
            return new List<StudentCourse>(_session.Query<StudentCourse>().Where(x => x.Student.Id == studentId && x.Semester.Id == semesterId && x.PerformanceTrack == StudentCoursePerformanceTrack.Audited));
        }

        public IEnumerable<StudentCourse> GetCurrentSemesterExemptionCourses(int studentId, int semesterId)
        {
            //return new List<StudentCourse>(_session.Query<StudentCourse>().Where(x => x.Student.Id == studentId && x.Semester.Id == semesterId && (x.PerformanceTrack == StudentCoursePerformanceTrack.ExemptPendingAR || x.PerformanceTrack == StudentCoursePerformanceTrack.ExemptPendingDean
            //    || x.PerformanceTrack == StudentCoursePerformanceTrack.ExemptPendingHOD || x.PerformanceTrack == StudentCoursePerformanceTrack.Exempted)));
            return new List<StudentCourse>(_session.Query<StudentCourse>().Where(x => x.Student.Id == studentId && x.Semester.Id == semesterId && (x.PerformanceTrack == StudentCoursePerformanceTrack.ExemptPending
                 || x.PerformanceTrack == StudentCoursePerformanceTrack.Exempted)));
        }

        public IEnumerable<StudentCourse> GetStudentCoursesForExemption(int studentId)
        {
            return new List<StudentCourse>(_session.Query<StudentCourse>().Where(x => x.Student.Id == studentId && x.PerformanceTrack != StudentCoursePerformanceTrack.ExemptPending
                 && x.PerformanceTrack != StudentCoursePerformanceTrack.Exempted));
        }

        public StudentCourse GetSemesterStudentCourse(int semesterId, int courseId, int studentId)
        {
            return _session.Query<StudentCourse>().FirstOrDefault(x => x.Semester.Id == semesterId && x.Course.Id == courseId && x.Student.Id == studentId);
        }

        public IQueryable<StudentCourse> GetStudentCourses(int courseId, int studentId)
        {
            return _session.Query<StudentCourse>().Where(x => x.Course.Id == courseId && x.Student.Id == studentId);

        }

        public IEnumerable<StudentCourse> GetStudentCoursesForCourse(int courseId)
        {
            var query = _session.Query<StudentCourse>().Where(x => x.Course.Id == courseId);

            return query;

        }

        public IEnumerable<StudentCourse> GetStudentCoursesBySemRegistrationId(int semRegistrationId)
        {
            return new List<StudentCourse>(_session.Query<StudentCourse>().Where(x => x.SemesterRegistration.Id == semRegistrationId));
        }
        public IEnumerable<StudentCourse> GetStudentCoursesByStaffCourseId(int staffCourseId)
        {
            return new List<StudentCourse>(_session.Query<StudentCourse>().Where(x => x.StaffCourse.Id == staffCourseId));
        }

        public int GetRegisteredCountByStaffCourseId(int staffCourseId)
        {
            return _session.Query<StudentCourse>().Count(x => x.StaffCourse.Id == staffCourseId);
        }

        public IEnumerable<StudentCourse> GetCurrentSemesterCoursesToRetake(List<Course> courses, int studentId, int semesterRegistrationId)
       {
           IQueryable<StudentCourse> studentCourses = (_session.Query<StudentCourse>().Where(x => x.Student.Id == studentId && x.SemesterRegistration.Id != semesterRegistrationId && x.ResultStatus != ResultStatus.DeletedApproved));

           return (from course in courses where studentCourses.Any(m => (m.Course.Id == course.Id)) select (_session.Query<StudentCourse>().FirstOrDefault(x => x.Student.Id == studentId && x.SemesterRegistration.Id == semesterRegistrationId && x.Course.Id == course.Id))).ToList();
       }


    }
}
