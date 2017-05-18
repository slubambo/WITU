using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WITU.Core.Model;

namespace WITU.Core.Repositories.Interfaces
{
    public interface IStaffCourseRepository
    {
        //IEnumerable<StaffCourse> GetStaffCoursesBySemId(int semId);
        //IEnumerable<StaffCourse> GetStaffCoursesByCourseIdandSemester(int courseId, int semesterId);
        StaffCourse GetById(int staffCourseId);
    }
}
