using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.Areas.Admin.Controllers
{
   // [Authorize(Roles ="Admin")]
    [Area("Admin")]
    public class AdminController : Controller
    {
        public AdminController()
        {
                
        }

        public async Task<IActionResult> Index()
        {
            return View("Index");
        }

       // [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AddAdmin()
        {
            return View();
        }

       // [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeleteAdmin()
        {
            return View();
        }
    }
}
