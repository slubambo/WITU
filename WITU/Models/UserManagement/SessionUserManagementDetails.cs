using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WITU.Core.Model;

namespace WITU.Models.UserManagement
{
    public class SessionUserManagementDetails
    {
        public List<UserRole> LoggedInUserRoles { get; set; }

        public int HighestLevel { get; set; }

        public Instructor Instructor { get; set; }
    }
}