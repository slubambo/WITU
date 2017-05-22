using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;
using WITU.Core.Model;
using WITU.Core.Repositories.Interfaces;
using WITU.Helper.Impls;
using WITU.Helper.Interfaces;
using WITU.Models;
using WITU.Utils;


namespace WITU.Controllers
{
    [RequireHttps]
    [RoutePrefix("InformationDesk")]
    [AllowAnonymous]
    public class InformationDeskController : BaseController
    {
        private const int SummaryLength = 200;

        public InformationDeskController(ICoreRepository coreRepository, IGeneralHelper generalHelper)
            : base(coreRepository, generalHelper)
        {

        }

        public ActionResult Index()
        {
            //return View();
            return RedirectToAction("GeneralInformationAll");
        }

        public ViewResult GeneralInformation(int id)
        {
            var generalnfo = _coreRepository.Get<GeneralInformation>(id);
            if (generalnfo != null)
            {
                var model = new ContentView()
                {
                    ImageUrl =
                        WebConfigurationManager.AppSettings[AppSettings.InformationDeskResourcesUrl.ToString()] +
                        generalnfo.DefaultImageFilelName,
                    Title = generalnfo.Title,
                    Content = generalnfo.Content,
                    Type = InformationDeskType.GeneralInformation,
                    Id = generalnfo.Id
                };

                ViewBag.Showing = model.Title;
                return View("ContentView", model);
            }

            return View();
        }

        [Route("UploadAttachment/{type}/{id}")]
        public ViewResult UploadAttachment(AttachmentType type, int id)
        {
            var model = new UploadAttachment()
            {
                Type = type,
                Id = id,
                Attachments = new List<Attachment>(),
                AcademicYears = _coreRepository.GetAll<CohortYear>()
                    .OrderByDescending(x => x.Name)
                    .Select(x => new SelectListItem() { Text = x.Name, Value = x.Id.ToString() })
                    .ToList()
            };

            return View(model);
        }

        [Route("Information/{id}")]
        public ActionResult InformationDesk(int id)
        {
            var informationDesk = _coreRepository.Get<InformationDesk>(id);
            if (informationDesk != null)
            {
                //var model = new ContentView()
                //{
                //    Content = informationDesk.Content,
                //    Title = informationDesk.Title,
                //    Type = InformationDeskType.InformationDesk,
                //    Id = informationDesk.Id
                //};

                ////setting the image...
                //if (informationDesk.DefaultImageFileName != null)
                //    model.ImageUrl = WebConfigurationManager.AppSettings[AppSettings.InformationDeskResourcesUrl.ToString()] +
                //        informationDesk.DefaultImageFileName;

                //return View("ContentView", model);
            }

            return View();
        }

        [Route("General/{name}")]
        //[OutputCache(Duration = 600, VaryByParam = "name")]
        public ActionResult General(string name)
        {
            var value = name.Trim().Replace("-", " ");
            var infoDesk = _coreRepository.Get<GeneralInformation>("Title", value);
            if (infoDesk != null)
            {
                var model = new ContentView()
                {
                    Title = infoDesk.Title,
                    Content = infoDesk.Content,
                    ImageUrl =
                        WebConfigurationManager.AppSettings[AppSettings.InformationDeskResourcesUrl.ToString()] +
                        infoDesk.DefaultImageFilelName,
                    Id = infoDesk.Id

                };

                ViewBag.Showing = model.Title;
                return View("ContentView", model);
            }

            throw new NullReferenceException("Not Found!!");
        }

        public ViewResult ContentView()
        {
            //var model = new ContentView();
            //return View(model);
            return null;
        }

        [Route("All")]
        public ViewResult InformationDesk()
        {
            var allInformation = _coreRepository.GetAll<InformationDesk>();

            foreach (var id in allInformation)
            {
                if (id.ShortDescription == null)
                {
                    var sd = _generalHelper.ConvertHtmlToPlainText(id.Content);
                    id.ShortDescription = sd.Length > SummaryLength ? sd.Substring(0, SummaryLength) + "... " : sd;
                }
                else
                    id.ShortDescription = id.ShortDescription.Length > SummaryLength ? id.ShortDescription.Substring(0, SummaryLength) + "... " :
                        id.ShortDescription + "...";

                //homeView.InformationDesks.Add(id);
            }

            return View(allInformation);
        }

        [Route("AllAttachedFiles/{type}/{id}")]
        public ViewResult AllAttachedFiles(int id, InformationDeskType type)
        {
            var folderPath = "";
            var urlPath = "";

            var model = new List<Attachment>();
            switch (type)
            {
                case InformationDeskType.GeneralInformation:

                    //get folder and url path...
                    _generalHelper.GetFolderAndUrlPath(AttachmentType.GeneralInformation, out folderPath, out urlPath);

                    model.AddRange(_coreRepository.Get<GeneralInformation>(id).GeneralInformationAttachments.Select(x => new Attachment()
                    {
                        Id = x.Id,
                        Name = x.FileName,
                        AttachmentType = type.ToString(),
                        FileType = x.FileType,
                        OriginalFileName = x.OriginalFileName,
                        FriendlyName = x.UserFriendlyName,
                        ThumbnailUrl = Attachment.ImageExtensions.Any(y => y.Equals(x.FileType)) ?
                            string.Format("{0}{1}/{2}", urlPath, ResourceFolders.Thumbnail.ToString(), x.FileName) :
                            urlPath + x.FileName,
                        Url = urlPath + x.FileName,
                        IsImage = Attachment.ImageExtensions.Any(y => y.Equals(x.FileType)),
                        //AcademicYearName = x.CohortYear != null ? x.CohortYear.Name : ""
                    }));

                    break;
                case InformationDeskType.InformationDesk:

                    break;
            }
            return View(model);
        }

        #region Calendar
        public ViewResult Calendar()
        {
            var timeline = _coreRepository.GetAll<Timeline>().FirstOrDefault();
            return View(timeline);
        }

        [UserTypeAccess(UserType = UserTypes.Instructor)]
        [AuthorizeUser(AccessLevel = AccessLevel.Administration, Service = (int)ServiceDetail.Calender,
            Permission = ApplicationConstants.CanEdit)]
        public ViewResult EditCalendar()
        {
            var timeline = _coreRepository.GetAll<Timeline>().FirstOrDefault();
            return View(timeline);
        }

        [HttpPost]
        [AuthorizeUser(AccessLevel = AccessLevel.Administration, Service = (int)ServiceDetail.Calender,
            Permission = ApplicationConstants.CanEdit)]
        [Audit(AuditingLevel = AuditLevel.AddOrEdit)]
        public ActionResult EditCalendar(Timeline model)
        {
            if (ModelState.IsValid)
            {
                var isSaved = _coreRepository.SaveOrUpdate(model);
                if (isSaved)
                {
                    TempData[ApplicationConstants.SuccessNotification] = ApplicationConstants.SuccessSaveMessage;
                    return RedirectToAction("Calendar");
                }
            }

            TempData[ApplicationConstants.ErrorNotification] = ApplicationConstants.ErrorSaveMessage;
            return View(model);
        }

        public ViewResult GeneralInformationAll()
        {
            var model = _coreRepository.GetAll<GeneralInformation>();
            foreach (var gi in model)
            {
                //adding the short description if not present...
                if (gi.Content != null && gi.ShortDescription == null)
                {
                    var sd = _generalHelper.ConvertHtmlToPlainText(gi.Content);
                    gi.ShortDescription = sd.Length > SummaryLength ? sd.Substring(0, SummaryLength) + "..." : sd;
                }
            }
            return View(model);
        }

        #region Add and Edits
        [Authorize]
        [UserTypeAccess(UserType = UserTypes.Instructor)]
        [AuthorizeUser(AccessLevel = AccessLevel.Administration, Service = (int)ServiceDetail.GeneralInformation,
            Permission = ApplicationConstants.CanEdit)]
        public ViewResult EditGeneralInformation(int id = 0)
        {
            var generalInfo = _coreRepository.Get<GeneralInformation>(id);
            if (generalInfo == null)
                throw new ArgumentNullException("id");

            //adding the short description if not present...
            if (generalInfo.Content != null && generalInfo.ShortDescription == null)
            {
                var sd = _generalHelper.ConvertHtmlToPlainText(generalInfo.Content);
                generalInfo.ShortDescription = sd.Length > SummaryLength ? sd.Substring(0, SummaryLength) + "..." : sd;
            }

            //now for the return...
            return View(generalInfo);
        }

        [Authorize]
        [HttpPost]
        [AuthorizeUser(AccessLevel = AccessLevel.Administration, Service = (int)ServiceDetail.GeneralInformation,
            Permission = ApplicationConstants.CanEdit)]
        [Audit(AuditingLevel = AuditLevel.AddOrEdit)]
        public ActionResult EditGeneralInformation(GeneralInformation model)
        {
            if (ModelState.IsValid)
            {
                if (model.UploadImage != null)
                {
                    string newFileName = null;
                    if (_generalHelper.SaveFile(model.UploadImage, ApplicationConstants.InformationDeskResourcesFolder,
                        out newFileName, model.DefaultImageFilelName))
                        model.DefaultImageFilelName = newFileName;
                }

                //working with the description..
                if (String.IsNullOrWhiteSpace(model.ShortDescription))
                {
                    //getting the short description from the description...
                    model.ShortDescription = _generalHelper.ConvertHtmlToPlainText(model.Content, SummaryLength);
                }

                var isSaved = _coreRepository.SaveOrUpdate(model);
                if (isSaved)
                {
                    TempData[ApplicationConstants.SuccessNotification] = ApplicationConstants.SuccessSaveMessage;
                    return RedirectToAction("GeneralInformation", new { id = model.Id });
                }

            }

            TempData[ApplicationConstants.ErrorNotification] = ApplicationConstants.ErrorSaveMessage;

            return View(model);
        }

        [Authorize]
        [UserTypeAccess(UserType = UserTypes.Instructor)]
        [AuthorizeUser(AccessLevel = AccessLevel.Administration, Service = (int)ServiceDetail.InformationDesk,
            Permission = ApplicationConstants.CanEdit)]
        public ViewResult EditInformationDesk(int id = 0)
        {
            var infoDesk = _coreRepository.Get<InformationDesk>(id) ?? new InformationDesk();

            if (infoDesk.Content != null && infoDesk.ShortDescription != null)
            {
                var sd = _generalHelper.ConvertHtmlToPlainText(infoDesk.Content);
                infoDesk.ShortDescription = sd.Length > SummaryLength ? sd.Substring(0, SummaryLength) + "..." : sd;
            }

            return View(infoDesk);
        }

        [Authorize]
        [HttpPost]
        [AuthorizeUser(AccessLevel = AccessLevel.Administration, Service = (int)ServiceDetail.InformationDesk,
            Permission = ApplicationConstants.CanEdit)]
        [Audit(AuditingLevel = AuditLevel.AddOrEdit)]
        public ActionResult EditInformationDesk(InformationDesk model)
        {
            if (ModelState.IsValid)
            {
                //lets see if the image is present and then we save it...
                if (model.UploadImage != null)
                {
                    string newFileName = null;
                    if (_generalHelper.SaveFile(model.UploadImage, ApplicationConstants.InformationDeskResourcesFolder,
                        out newFileName, model.DefaultImageFileName))
                    {
                        model.DefaultImageFileName = newFileName;
                    }
                }

                //working with the description..
                if (String.IsNullOrWhiteSpace(model.ShortDescription))
                {
                    //getting the short description from the description...
                    model.ShortDescription = _generalHelper.ConvertHtmlToPlainText(model.Content, SummaryLength);
                }

                var isSaved = _coreRepository.SaveOrUpdate(model);
                if (isSaved)
                {
                    TempData[ApplicationConstants.SuccessNotification] = ApplicationConstants.SuccessSaveMessage;
                    return RedirectToAction("Information", new { id = model.Id });
                }
            }

            TempData[ApplicationConstants.ErrorNotification] = ApplicationConstants.ErrorSaveMessage;

            return View(model);
        }

        #endregion

        #endregion
    }
}