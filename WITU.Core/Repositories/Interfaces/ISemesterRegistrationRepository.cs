using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WITU.Core.Model;

namespace WITU.Core.Repositories.Interfaces
{
    public interface ISemesterRegistrationRepository
    {
        CohortRegistration GetSemesterRegistrationBySemesterAndStudentId(int semesterId, int studentId);
        CohortRegistration GetSemesterRegistrationById(int semesterRegistrationId);
        IEnumerable<CohortRegistration> GetSemesterRegistrationByStudentId(int studentId);
    }
}
