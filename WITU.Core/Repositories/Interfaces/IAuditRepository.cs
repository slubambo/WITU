using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WITU.Core.Model;

namespace WITU.Core.Repositories.Interfaces
{
    public interface IAuditRepository
    {

        IEnumerable<Audit> GetStudentTrail();

        IEnumerable<Audit> GetStaffTrail();

        bool SaveAudit(object entity);


    }

}
