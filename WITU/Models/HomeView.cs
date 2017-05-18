using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WITU.Core.Model;

namespace WITU.Models
{
    public class HomeView
    {
        public IList<GeneralInformation> GeneralInformations { get; set; }

        public IList<InformationDesk> InformationDesks { get; set; }

        public ContactSummary  ContactSummary{ get; set; }

        public Institution University { get; set; }

    }

    public class ContactSummary
    {
        public string Url { get; set; }

        public String City { get; set; }

        public String Country { get; set; }

        public String TelephoneContact { get; set; }

        public String EmailContact { get; set; }
    }


}