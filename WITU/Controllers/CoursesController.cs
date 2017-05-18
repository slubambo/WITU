using System.Linq;
using System.Web.Mvc;
using WITU.Core.Model;
using WITU.Core.Repositories.Interfaces;
using WITU.Helper.Interfaces;
using WITU.Models.Courses;

namespace WITU.Controllers
{
    [RequireHttps]
    public class CoursesController : BaseController
    {
        private readonly ICourseRepository _courseRepository;
        private readonly int summaryLength = 300;

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

            var courseCores = _coreRepository.GetAll<CourseCore>().ToList();

            if (courseCores.Any())
            {
                foreach (var course in courseCores)
                {
                    var shortDescription = course.Overview != null
                        ? _generalHelper.ConvertHtmlToPlainText(course.Overview)
                        : "";
                    course.ShortDescription = shortDescription.Length > summaryLength
                        ? shortDescription.Substring(0, summaryLength) + "... "
                        : shortDescription + "...";
                }
            }

            var model = new CoursesModel
            {
                AllCourses = courses.Select(x => new CourseModel
                {
                    Course = x
                }),
                Campuses = campuses,
                AllCourseCores = courseCores
            };

            return View(model);
        }
    }
}