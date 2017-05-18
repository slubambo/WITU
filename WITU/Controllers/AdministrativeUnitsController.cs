using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WITU.Core.Model;
using WITU.Core.Repositories.Interfaces;
using WITU.Helper.Interfaces;
using WITU.Models;
using WITU.Models.AdminUnits;
using WITU.Utils;
using FluentNHibernate.Conventions;
using FluentNHibernate.Testing.Values;
using Microsoft.Ajax.Utilities;
using NHibernate.Mapping;

namespace WITU.Controllers
{
    [RequireHttps]
    [Authorize]
    public class AdministrativeUnitsController : BaseController
    {
        private IAdminUnitRepository _adminUnitRepository;

        public AdministrativeUnitsController(ICoreRepository coreRepository, IAdminUnitRepository adminUnitRepository, IGeneralHelper generalHelper)
            : base(coreRepository, generalHelper)
        {
            _adminUnitRepository = adminUnitRepository;
            
        }
        //
        // GET: /AdministrativeUnits/
        [UserTypeAccess(UserType = UserTypes.Instructor)]
        public ActionResult Index()
        {
//            var model = new AdministrativeUnits
//            {
//                Campuses = _coreRepository.GetAll<Campus>().Count(),
//                Faculties = _coreRepository.GetAll<Faculty>().Count(),
//                Departments = _coreRepository.GetAll<Department>().Count()
//            };
            //var campuses = _coreRepository.GetAll<Campus>();
            //return View(model);
            return RedirectToAction("University");
        }

        [UserTypeAccess(UserType = UserTypes.Instructor)]
        [AuthorizeUser(AccessLevel = AccessLevel.Administration, Service = (int)ServiceDetail.University,
            Permission = ApplicationConstants.CanRead)]
        public ViewResult University()
        {
           
           var university = new Institution();

            university = _coreRepository.GetAll<Institution>().First();
            var model = new UniversityView() {
                                                University = new Institution(), 
                                                contactsExist = false,
                                                AllStaffPositions = _coreRepository.GetAll<InstructorPosition>().Where(x => x.DeactivatedDate == null && x.IsActive == true).ToList(),
                                                UniversityPositions = _coreRepository.GetAll<Position>().Where(x => x.Level.ToLower() == "university").ToList(),
                                                };
            if (university != null)
            {
                
            }
             model.University = university;
            return View(model);
        }

        [UserTypeAccess(UserType = UserTypes.Instructor)]
        [AuthorizeUser(AccessLevel = AccessLevel.Administration, Service = (int)ServiceDetail.University,
            Permission = ApplicationConstants.CanAddorEdit)]
        public ViewResult AddorEditUniversity(int id = 0)
        {
            var model = new EditUniversity();

            if (id > 0)
            {
                var university = _coreRepository.Get<Institution>(id);
                if (university != null)
                    model = new EditUniversity(university);
            }
            
            
            return View("AddorEditUniversity",model);
        }

        [HttpPost]
        [AuthorizeUser(AccessLevel = AccessLevel.Administration, Service = (int)ServiceDetail.University,
            Permission = ApplicationConstants.CanAddorEdit)]
        [Audit(AuditingLevel = AuditLevel.AddOrEdit)]
        public ActionResult AddorEditUniversity(EditUniversity model)
        {
            
            if (ModelState.IsValid)
            {
                var university = _coreRepository.Get<Institution>(model.Id) ?? new Institution();
                university.Name = model.Name;
                university.ShortName = model.ShortName;
                if (!string.IsNullOrWhiteSpace(model.Description))
                {
                    university.Description = _generalHelper.ConvertHtmlToPlainText(model.Description);
                }
                
                if (model.logoFile != null)
                {
                    var fileName = model.logoFile.FileName;
                    model.LogoPathName = fileName;
                    //university.logoFile.SaveAs(ConfigurationManager.AppSettings["BaseResourcesFolder"] + fileName);
                    string newFileName = null;
                    string oldFileName = university.LogoPathName;
                    if (!string.IsNullOrWhiteSpace(oldFileName))
                    {
                        bool isFileDeleted = _generalHelper.DeleteFile(oldFileName, ConfigurationManager.AppSettings["BaseResourcesFolder"]);
                    }
                    if (_generalHelper.SaveFile(model.logoFile, ConfigurationManager.AppSettings["BaseResourcesFolder"],
                        out newFileName, model.LogoPathName))
                    {

                        model.LogoPathName = newFileName;
                    }
                    university.logoFile = model.logoFile;
                    university.LogoPathName = model.LogoPathName;
                }
                if (model.Id == 0)
                {
                    university.CreatedOn = DateTime.Now;
                }

                bool isSaved = _coreRepository.SaveOrUpdate(university);
                if (isSaved)
                {
                    TempData[ApplicationConstants.SuccessNotification] = ApplicationConstants.SuccessSaveMessage;
                    return RedirectToAction("University");
                }
               
            }
            //var errors = ModelState.Values.SelectMany(v => v.Errors);
            TempData[ApplicationConstants.ErrorNotification] = ApplicationConstants.ErrorSaveMessage;
            return View(model);
        }
      
	}
}