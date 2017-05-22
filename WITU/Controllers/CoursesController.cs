using System.Linq;
using System.Web.Mvc;
using WITU.Core.Model;
using WITU.Core.Repositories.Interfaces;
using WITU.Helper.Interfaces;
using WITU.Models.Courses;
using WITU.Utils;

namespace WITU.Controllers
{
    [RequireHttps]
    public class CoursesController : BaseController
    {
        private readonly ICourseRepository _courseRepository;
        private readonly int summaryLength = 90;

        public CoursesController(ICoreRepository coreRepository, IGeneralHelper generalHelper,
            ICourseRepository courseRepository) : base(coreRepository, generalHelper)
        {
            _courseRepository = courseRepository;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.Showing = "Courses";

            var courses = _courseRepository.GetCourses();
            var campuses = courses.Select(x => x.Campus).Distinct();
            
            var model = new CoursesModel
            {
                AllCourses = courses.Select(x => new CourseModel
                {
                    Course = x,
                    Overview = (x.Overview != null
                      ? _generalHelper.ConvertHtmlToPlainText(x.Overview)
                       : "").Length > summaryLength
                               ? (x.Overview != null
                      ? _generalHelper.ConvertHtmlToPlainText(x.Overview)
                       : "").Substring(0, summaryLength) + "... "
                               : (x.Overview != null
                      ? _generalHelper.ConvertHtmlToPlainText(x.Overview)
                       : "") + "..."
                }),
                Campuses = campuses,
                
            };

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Course(int id)
        {
            var course = _coreRepository.Get<Course>(id);
            if (course != null)
            {
                var model = new CourseView()
                {
                    Course = course,
                    Overview = (course.Overview != null
                      ? _generalHelper.ConvertHtmlToPlainText(course.Overview)
                       : "")
                };

                ViewBag.Showing = "Course";

                return View(model);
            }

            TempData[ApplicationConstants.ErrorNotification] = "Course not found!!";

            return RedirectToAction("Courses");
        }
    }
}