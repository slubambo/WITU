using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WITU.Utils
{
    public class BinaryContentResult : ActionResult
    {
        private string ContentType;
        private byte[] ContentBytes;
        private string FileName;


        public BinaryContentResult(byte[] contentBytes, string contentType)
        {
            this.ContentBytes = contentBytes;
            this.ContentType = contentType;
        }

        public BinaryContentResult(byte[] contentBytes, string contentType, string fileName)
        {
            this.ContentBytes = contentBytes;
            this.ContentType = contentType;
            this.FileName = fileName;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;
            response.Clear();
            response.Cache.SetCacheability(HttpCacheability.NoCache);
            response.ContentType = this.ContentType;
            if (FileName != null)
            {
                response.AddHeader("filename", FileName);
                response.AddHeader("content-disposition", string.Format(@"attachment;filename=""{0}""", FileName));
            }
            else
            {
                var fileName = "rms_" + DateTime.Now.ToLongTimeString();
                response.AddHeader("content-disposition", string.Format(@"attachment;filename=""{0}.pdf""", fileName));
            }

            var stream = new MemoryStream(this.ContentBytes);
            stream.WriteTo(response.OutputStream);
            stream.Dispose();
        }
    }
}