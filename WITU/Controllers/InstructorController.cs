using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WITU.Core.Model;
using WITU.Core.Repositories.Interfaces;
using WITU.Helper.Interfaces;
using WITU.Models;

namespace WITU.Controllers
{
    [RequireHttps]
    [Authorize]
    public class InstructorController : BaseController
    {
        private readonly ICoreRepository _coreRepo;
        private readonly IAccountRepository _accountRepo;
        private readonly IGeneralHelper _generalHelper;
        private readonly int summaryLength = 150;

        public InstructorController(ICoreRepository coreRepository, IAccountRepository accountRepository,
            IGeneralHelper generalHelper)
            : base(coreRepository, generalHelper)
        {
            _coreRepo = coreRepository;
            _accountRepo = accountRepository;
            _generalHelper = generalHelper;
  
        }

        // GET: Instructor
        [UserTypeAccess(UserType = UserTypes.Instructor)]
        public ActionResult Index()
        {
            return View();
        }
    }
}