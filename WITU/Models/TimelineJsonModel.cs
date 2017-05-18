using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WITU.Core.Model;

namespace WITU.Models
{
    public class TimelineJsonModel
    {
        public const string DateTimeFormat = "yyyy,MM,dd";

        public Timeline timeline { get; set; }
    }

    public class Timeline
    {
        public Timeline()
        {
            era = new List<Era>();
            date = new List<Date>();
        }
        
        public string headline { get; set; }

        public string type { get; set; }

        public string text { get; set; }

        public string startDate { get; set; }

        public string endDate { get; set; }

        public Asset asset { get; set; }

        public IList<Era> era { get; set; }


        public IList<Date> date { get; set; } 
    }

    public class Date
    {
        public string startDate { get; set; }

        public string endDate { get; set; }

        public string headline { get; set; }

        public string text { get; set; }

        public string tag { get; set; }

        public string classname { get; set; }

        public Asset asset { get; set; }
    }

    public class Asset
    {
        public string media { get; set; }

        public string thumbnail { get; set; }

        public string credit { get; set; }

        public string caption { get; set; }
    }

    public class Era
    {
        public string startDate { get; set; }

        public string endDate { get; set; }

        public string headline { get; set; }

        public string text { get; set; }

        public string tag { get; set; }

    }
}