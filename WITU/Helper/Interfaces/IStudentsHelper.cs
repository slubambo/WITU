using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WITU.Models.student;

namespace WITU.Helper.Interfaces
{
    public interface IStudentsHelper
    {
        /// <summary>
        /// Used to print student lists
        /// </summary>
        /// <param name="model"></param>
        /// <param name="printedByUser"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        //byte[] PrintStudentsPdf(StudentPrintModel model, string printedByUser, string ipAddress);
    }
}