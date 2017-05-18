using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WITU.Core.Model;

namespace WITU.Models.AdminUnits
{
    public class UniversityView
    {
        public Institution University { get; set; }

        public bool contactsExist { get; set; }

        public IList<Position> UniversityPositions { get; set; }

        public IList<InstructorPosition> AllStaffPositions { get; set; } 
    
    }

    
    public class CampusView
    {
        public Campus Campus { get; set; }

    }
}