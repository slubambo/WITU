using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WITU.Core.Model;

namespace WITU.Core.Repositories.Interfaces
{
    public interface IStaffRepository
    {
        Instructor GetStaffByUserId(int userid);

        Instructor GetStaffByPersonId(int personId);

        //List<StaffPosition> GetStaffPositionsByUserId(int userId);

        //List<StaffCourse> GetStaffCoursesByUserId(int userId);

        List<User> GetStaffUsers(string username);

    }
}
