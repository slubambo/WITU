using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WITU.Core.Model;

namespace WITU.Models
{
    public class CountryIndex
    {
        public IEnumerable<Country> Countries { get; set; }

        public IEnumerable<District> Districts { get; set; }

        public int TotalCounties { get; set; }

        public IEnumerable<TableView> CountryTableViews { get; set; }
    
    }

    public class TableView
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}