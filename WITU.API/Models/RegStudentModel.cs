using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WITU.API.Models
{
    public class RegStudentModel
    {
        public int Id
        {
            get;
            set;
        }
        public string RegistrationNumber
        {
            get;
            set;
        }
       
        public string StudentNumber
        {
            get;
            set;
        }
       

        public string FullName
        {
            get;
            set;
        }
        public string Guid { get; set; }
        public int ExamPermitId { get; set; }
        public int SemRegId { get; set; }
        public string PhotoUrl { get; set; }

    }
}