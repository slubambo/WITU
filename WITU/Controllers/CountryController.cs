using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WITU.Core.Model;
using WITU.Core.Repositories.Impl;
using WITU.Core.Repositories.Interfaces;
using WITU.Models;
using WITU.Utils;

namespace WITU.Controllers
{
    public class CountryController : Controller
    {
        private readonly ICoreRepository _coreRepository;

        public CountryController(ICoreRepository coreRepository)
        {
            _coreRepository = coreRepository;
        }

        //
        // GET: /Country/
    //    public ActionResult Index()
    //    {
    //        var model = new CountryIndex()
    //        {
    //            TotalCounties = _coreRepository.CountAll<County>(),
    //            Countries = _coreRepository.GetAll<Country>(),
    //            Districts = _coreRepository.GetAll<District>(),
               
    //        };

    //        model.CountryTableViews = model.Countries.Select(x => new TableView()
    //        {
    //            Name = x.Name,
    //            Code = x.CountryCode,
    //            CreatedOn = x.CreatedOn ?? DateTime.Now,
    //            Id = x.Id
    //        });

    //        return View(model);
    //    }

    //    public ViewResult AddEditCountry(int id = 0)
    //    {
    //        var model = new AddEditCountry();
            
    //        var country = _coreRepository.Get<Country>(id);
    //        if(country != null)
    //            model = new AddEditCountry()
    //            {
    //                Name = country.Name,
    //                Nationality = country.Nationality,
    //                Code = country.CountryCode,
    //                Id = country.Id,
    //                LastModified = country.LastModified
    //            };

    //        return View(model);
    //    }

    //    [HttpPost]
    //    public ActionResult AddEditCountry(AddEditCountry model)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            //go ahead and save the country...
    //            var country = new Country();
    //            if (model.Id > 0)
    //            {
    //                var c = _coreRepository.Get<Country>(model.Id);
    //                if (c != null)
    //                    country = c;
    //            }
    //            //modifying accordinlgy...
    //            country.Name = model.Name;
    //            country.CountryCode = model.Code;
    //            country.Nationality = model.Nationality;
    //            country.LastModified = DateTime.Now;

    //            if (_coreRepository.SaveOrUpdate(country))
    //            {
    //                TempData[ApplicationConstants.SuccessNotification] = "Country has been saved.";
    //                return RedirectToAction("Index");
    //            }
    //            else
    //                TempData[ApplicationConstants.ErrorNotification] = ApplicationConstants.ErrorNotification;
    //        }
    //        else 
    //            ModelState.AddModelError("", "Some Errors!!");

    //        return View(model);
    //    }
    }

   
}