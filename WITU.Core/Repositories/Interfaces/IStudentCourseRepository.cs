using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WITU.Core.Model;
using NHibernate.Validator.Cfg.Loquacious.Impl;

namespace WITU.Core.Repositories.Interfaces
{
    public interface IStudentCourseRepository
    {
        IEnumerable<StudentCourse> GetStudentCoursesByStudentId(int studentId);
        
        IEnumerable<StudentCourse> GetStudentCoursesByStaffCourseId(int staffCourseId);
        
        StudentCourse GetSemesterStudentCourse(int semesterId, int courseId, int studentId);

        IQueryable<StudentCourse> GetStudentCourses(int courseId, int studentId);

        /// <summary>
        /// Return the list of student courses for a given course
        /// </summary>
        /// <param name="courseId">course Id</param>
        /// <returns>List of Student courses for a given course</returns>
        IEnumerable<StudentCourse> GetStudentCoursesForCourse(int courseId);
    }
}
