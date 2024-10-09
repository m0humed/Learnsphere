using E_Learning.Areas.Home.Data;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.Areas.Home.Controllers
{
    [Area("Home")]
    public class HomeAreaController : Controller
    {
        ICourseCardService _courseCardService;
        public HomeAreaController(ICourseCardService courseCardService)
        {
            _courseCardService = courseCardService;
        }

        public IActionResult HomeIndex()
        {
            var ccs = _courseCardService.getAll().Result;
            return View("Area_Index", ccs);
        }
    }
}
