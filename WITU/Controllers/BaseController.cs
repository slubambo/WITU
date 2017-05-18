using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WITU.Core.Repositories.Impl;
using WITU.Core.Repositories.Interfaces;
using WITU.Helper.Interfaces;

namespace WITU.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly ICoreRepository _coreRepository;
        protected readonly IGeneralHelper _generalHelper;
        
        public BaseController(ICoreRepository coreRepository, IGeneralHelper generalHelper)
        {
            _coreRepository = coreRepository;
            _generalHelper = generalHelper;
        }

        public new RedirectToRouteResult RedirectToAction(string action, string controller)
        {
            DependencyResolver.Current.GetService<BaseController>();

            return base.RedirectToAction(action, controller);
        }
    }
}