using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WITU.Core.Model;

namespace WITU.Core.Repositories.Interfaces
{
    public interface ISemesterRepository
    {
        IEnumerable<Cohort> GetSemestersByAcademicYear(int academicYearId);
        
        Cohort GetCurrentSemester(DateTime dateTime);

        Cohort GetSemester(int academicYearId, int semesterSession);

        Cohort GetCurrentOrLatestSemester();

        /// <summary>
        /// Returns a collection of semesters that correspond to a given academic year with a year of study and corresponding 
        /// semester session.
        /// </summary>
        /// <param name="givenSemester"></param>
        /// <param name="yearOfStudy"></param>
        /// <returns></returns>
        IEnumerable<Cohort> GetSemesters(Cohort givenSemester, int yearOfStudy);

        
    }
}
