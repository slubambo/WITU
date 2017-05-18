using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WITU.Core.Model;
using WITU.Core.Repositories.Interfaces;
using WITU.Helper.Interfaces;
using WITU.Models;
using WITU;

namespace WITU.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private readonly ICoreRepository _coreRepo;
        private readonly IGeneralHelper _generalHelper;
        private readonly int summaryLength = 150;

        public HomeController(ICoreRepository coreRepository, IGeneralHelper generalHelper)
        {
            _coreRepo = coreRepository;
            _generalHelper = generalHelper;
        }

        public ActionResult Index()
        {
            ViewBag.Showing = "Home";

            var homeView = new HomeView()
            {
                GeneralInformations = new List<GeneralInformation>(),
                InformationDesks = new List<InformationDesk>(),
                ContactSummary = new ContactSummary()
            };

            var gses = _coreRepo.GetAll<GeneralInformation>().ToList();
            foreach (var gs in gses)
            {
                if (gs.ShortDescription == null)
                {
                    var sd = _generalHelper.ConvertHtmlToPlainText(gs.Content);
                    gs.ShortDescription = sd.Length > summaryLength ? sd.Substring(0, summaryLength) + "... " : sd;
                }
                else
                    gs.ShortDescription = gs.ShortDescription.Length > summaryLength
                        ? gs.ShortDescription.Substring(0, summaryLength) + "... "
                        : gs.ShortDescription +"...";

                homeView.GeneralInformations.Add(gs);
            }

            var ides = _coreRepo.GetAll<InformationDesk>().ToList();
            foreach (var id in ides)
            {
                if (id.ShortDescription == null)
                {
                    var sd = _generalHelper.ConvertHtmlToPlainText(id.Content);
                    id.ShortDescription = sd.Length > summaryLength ? sd.Substring(0, summaryLength) + "... " : sd;
                }
                else
                    id.ShortDescription = id.ShortDescription.Length > summaryLength ? id.ShortDescription.Substring(0, summaryLength) + "... " :
                        id.ShortDescription + "...";

                homeView.InformationDesks.Add(id);
            }


            //now retrieving contacts...
            var university = _coreRepo.GetAll<Institution>().FirstOrDefault();
            homeView.University = university;
            
            return View(homeView);
        }

        public ActionResult About()
        {
            ViewBag.Showing = "Home";
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //[ChildActionOnly]
        [OutputCache(Duration = 600,VaryByParam = "showing")]
        public PartialViewResult GeneralInformationMenu(string showing = null)
        {
            var generalInfos = _coreRepo.GetAll<GeneralInformation>();
            var menuItems =
                generalInfos.Select(x => new Menu(){UrlAction = x.Title.Trim().Replace(" ", "-"), Name = x.Title});

            //passing the view bag showing...
            ViewBag.Showing = showing;

            return PartialView(menuItems);
        }
    }
}