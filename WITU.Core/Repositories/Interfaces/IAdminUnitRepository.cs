using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WITU.Core.Model;

namespace WITU.Core.Repositories.Interfaces
{
    public interface IAdminUnitRepository
    {
        bool CampusExists(string campusName, int campusId);
        
    }
}
