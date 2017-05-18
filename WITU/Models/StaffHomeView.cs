using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WITU.Core.Model;

namespace WITU.Models
{
    public class StaffHomeView
    {
        public IList<InformationDesk> InformationDesks { get; set; }
        public Instructor staff { get; set; }

        public SemesterProgressSummary semInfo { get; set; }
    }
}