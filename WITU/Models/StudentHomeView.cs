using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WITU.Core.Model;

namespace WITU.Models
{
    public class StudentHomeView
    {
        public IList<InformationDesk> InformationDesks { get; set; }
        public Student student { get; set; }

        public SemesterProgressSummary semInfo {get; set;}

        public bool registrationStatus { get; set; }


    }

    public class SemesterProgressSummary
    {
        public int elapsedDays { get; set; }
        public int totalDays { get; set; }

        public string percent { get; set; }
        
    }
}