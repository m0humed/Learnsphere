using Microsoft.AspNetCore.Mvc;

namespace E_Learning.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoursesController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View("Index");
        }

        public async Task<IActionResult> CoursesList()
        {
            return View("CoursesList");
        }

        public async Task<IActionResult> AddCourse()
        {
            return View();
        }
    }
}
