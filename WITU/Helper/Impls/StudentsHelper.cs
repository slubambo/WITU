using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WITU.Core.Model;
using WITU.Helper.Interfaces;
using WITU.Models.student;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Ninject;

namespace WITU.Helper.Impls
{
    
    public class StudentsHelper : IStudentsHelper
    {
        
        //public byte[] PrintStudentsPdf(StudentPrintModel model, string printedByUser, string ipAddress)
        //{
        //    var body = new Paragraph();

        //    var header = PrintHelper.AddDocumentHeader(string.Format("List of Year {0} Students", model.YearOfStudy), model.Program, model.IntakeAcademicYear, model.Specialisation);
        //    header.SpacingAfter = 2f;
        //    body.Add(header);

        //    //now loading the table itself...
        //    body.Add(ProgressionStudentsTable(model.Students));

        //    //finally we get the  content...
        //    var eventTemplate = PrintHelper.ProcessDefaultEventTemplate(printedByUser, ipAddress, false);
        //    return PrintHelper.MemoryStreamDocument(body, eventTemplate);
        //}
    }
}