using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace WITU.Utils
{
    public enum AppSettings
    {
        BaseResourcesUrl,
        InformationDeskResourcesUrl
    }
    public abstract class ApplicationViewPages<T> : WebViewPage<T>
    {
        protected override void InitializePage()
        {
            SetViewBagDefaultProperties();
            base.InitializePage();
        }

        private void SetViewBagDefaultProperties()
        {
            ViewBag.BaseResourcesUrl = WebConfigurationManager.AppSettings[AppSettings.BaseResourcesUrl.ToString()];
            ViewBag.InformationDeskResourcesUrl = WebConfigurationManager.AppSettings[AppSettings.InformationDeskResourcesUrl.ToString()];
        }
    }
}