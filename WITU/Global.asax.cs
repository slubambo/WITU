using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using FluentValidation;
using FluentValidation.Mvc;
using Microsoft.Ajax.Utilities;
using Ninject;
using Ninject.Web.Common;
using WITU.Utils;


namespace WITU
{
    public class MvcApplication : System.Web.HttpApplication
    {
        
        protected void Application_Start()
        {

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WITU.WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            FluentValidationModelValidatorProvider.Configure();

            // Removing all the view engines
            ViewEngines.Engines.Clear();

            //Add Razor Engine (which we are using)
            ViewEngines.Engines.Add(new WITUViewEngine());

            //TaskManager.Initialize(new TaskScheduler()); 
            
            //var kernel = (new Bootstrapper()).Kernel;
            //var ninjectValidatorFactory = new NinjectValidatorFactory(kernel);
            //ModelValidatorProviders.Providers.Add(new FluentValidationModelValidatorProvider(ninjectValidatorFactory));
            //DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            //FluentValidationModelValidatorProvider.Configure(x => x.ValidatorFactory = ninjectValidatorFactory);

        }
    }
}
