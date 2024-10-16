using E_Learning.Areas.Home.Data;
using E_Learning.Repository.IReposatories;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.Areas.Home.Controllers
{
    [Area("Home")]
    public class HomeAreaController : Controller
    {
        //ICourseCardService _courseCardService;
        //public HomeAreaController(ICourseCardService courseCardService)
        //{
        //    _courseCardService = courseCardService;
        //}

        ICourseRepository _courseCardService;
        public HomeAreaController(ICourseRepository courseCardService)
        {
            _courseCardService = courseCardService;
        }

        public IActionResult HomeIndex()
        {
            var ccs = _courseCardService.GetAllAsync().Result;
            return View("Area_Index", ccs);
        }
    }
}
