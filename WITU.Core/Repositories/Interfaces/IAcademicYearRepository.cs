using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WITU.Core.Model;

namespace WITU.Core.Repositories.Interfaces
{
    public interface IAcademicYearRepository
    {
        CohortYear GetAcademicYearByName(string name);

        /// <summary>
        /// Retrieves the academic years from a given start year.
        /// </summary>
        /// <param name="startYear"></param>
        /// <param name="noOfYears"></param>
        /// <returns></returns>
        IEnumerable<CohortYear> GetAcademicYears(long startYear, double noOfYears = 1);

        /// <summary>
        /// Retrieves the latest academic year
        /// </summary>
        /// <returns></returns>
        CohortYear GetLatestAcademicYear();
    }
}
