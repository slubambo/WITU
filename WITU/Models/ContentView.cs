using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WITU.Models
{
    public enum InformationDeskType
    {
        GeneralInformation,
        InformationDesk
    }
    public class ContentView
    {
        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public string Content { get; set; }

        public int Id { get; set; }

        public InformationDeskType Type { get; set; }
    }
}