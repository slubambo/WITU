using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WITU.Core.Model;
using WITU.Core.Repositories.Interfaces;
using WITU.Models;
using WITU.Utils;


namespace WITU.Controllers
{
    [RequireHttps]
    [Authorize]
    public class AuditTrailController : Controller
    {
        private ICoreRepository _coreRepo;
        private IAuditRepository _auditRepository;

        public AuditTrailController(ICoreRepository coreRepo, IAuditRepository auditRepository)
        {
            _coreRepo = coreRepo;
            _auditRepository = auditRepository;
        }
        //
        // GET: /AuditTrail/
        [UserTypeAccess(UserType = UserTypes.Instructor)]
        [AuthorizeUser(AccessLevel = AccessLevel.Administration, Service = (int)ServiceDetail.Audit,
            Permission = ApplicationConstants.CanRead)]
        public ActionResult Index()
        {
            var model = new WITU.Models.AuditViewList()
            {
                AuditRecords = _coreRepo.GetAll<Audit>().Select(x =>
                    new AuditTrail()
                    {
                        UserType = x.UserType,
                        UserName = x.UserName,
                        IpAddress =x.IpAddress,
                        UrlAccessed = x.UrlAccessed,
                        Message = x.Message,
                        Data = x.Data,
                        TimeAccessed = x.TimeAccessed

                    
                    }).ToList()
            };
            @ViewBag.TrailGroup = "All";
            return View(model);
        }

        [UserTypeAccess(UserType = UserTypes.Instructor)]
        [AuthorizeUser(AccessLevel = AccessLevel.Administration, Service = (int)ServiceDetail.Audit,
            Permission = ApplicationConstants.CanRead)]
        public ActionResult StudentTrail()
        {
            var model = new WITU.Models.AuditViewList()
            {
                AuditRecords = _auditRepository.GetStudentTrail().Select(x =>
                    new AuditTrail()
                    {
                        UserType = x.UserType,
                        UserName = x.UserName,
                        IpAddress = x.IpAddress,
                        UrlAccessed = x.UrlAccessed,
                        Message = x.Message,
                        Data = x.Data,
                        TimeAccessed = x.TimeAccessed


                    }).ToList()
            };
            @ViewBag.TrailGroup = "Student";
            return View("Index",model);
        }

        [UserTypeAccess(UserType = UserTypes.Instructor)]
        [AuthorizeUser(AccessLevel = AccessLevel.Administration, Service = (int)ServiceDetail.Audit,
            Permission = ApplicationConstants.CanRead)]
        public ActionResult StaffTrail()
        {
            var model = new WITU.Models.AuditViewList()
            {
                AuditRecords = _auditRepository.GetStaffTrail().Select(x =>
                    new AuditTrail()
                    {
                        UserType = x.UserType,
                        UserName = x.UserName,
                        IpAddress = x.IpAddress,
                        UrlAccessed = x.UrlAccessed,
                        Message = x.Message,
                        Data = x.Data,
                        TimeAccessed = x.TimeAccessed


                    }).ToList()
            };
            @ViewBag.TrailGroup = "Staff";
            return View("Index", model);
        }
	}
}