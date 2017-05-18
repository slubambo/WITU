using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WITU.Core.Model;

namespace WITU.Core.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        Student GetStudent(string username);

        /// <summary>
        /// returns the list of (possible) semesters that a given student has attended, with
        /// </summary>
        /// <param name="student"></param>
        /// <param name="givenSemester"></param>
        /// <returns></returns>
        IEnumerable<Cohort> StudentSemesters(Student student, Cohort givenSemester);

        /// <summary>
        /// Returns list of students based on ids
        /// </summary>
        /// <param name="ids">ids of students to return</param>
        /// <returns></returns>
        IList<Student> GetStudents(int[] ids);

    }
}
