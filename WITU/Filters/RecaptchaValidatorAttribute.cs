using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace WITU.Filters
{
    /// <summary>
    /// Validates a captcha.
    /// </summary>
    public class RecaptchaValidatorAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (filterContext.RequestContext.HttpContext.Request["g-recaptcha-response"] != null)
            {

                string privatekey = WebConfigurationManager.AppSettings["RecaptchaPrivateKey"];
                string response = filterContext.RequestContext.HttpContext.Request["g-recaptcha-response"];
                filterContext.ActionParameters["CaptchaValid"] = Validate(response, privatekey);
            }
        }

        public static bool Validate(string mainresponse, string privatekey)
        {

            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create
                ("https://www.google.com/recaptcha/api/siteverify?secret=" +
                privatekey + "&response=" + mainresponse);

                WebResponse response = req.GetResponse();

                using (StreamReader readStream = new StreamReader(response.GetResponseStream()))
                {
                    string jsonResponse = readStream.ReadToEnd();

                    JsonResponseObject jobj = JsonConvert.DeserializeObject<JsonResponseObject>(jsonResponse);

                    return jobj.success;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public class JsonResponseObject
        {
            public bool success { get; set; }
            [JsonProperty("error-codes")]
            public List<string> errorcodes { get; set; }
        }
    }
}