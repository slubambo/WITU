using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WITU.Core.Model;
using NHibernate.Criterion;

namespace WITU.Core.Repositories.Interfaces
{
    public interface IUserManagementRepository
    {
        IEnumerable<Position> GetPositionsByCategoryId(int positionCategoryId);

        IEnumerable<Service> GetActiveServices(int[] inactiveServiceCategories);

        AccessRole GetAccessRoleByUserId(int userId);

        IEnumerable<UserRole> GetUserRolesByUserId(int userId);

        UserRole GetUserRoleByRoleId(int roleId);
    }
}
