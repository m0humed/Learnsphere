using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        public AdminController()
        {
                
        }

        [Authorize(Roles ="HeadAdmin")]
        public async Task<IActionResult> AddAdmin()
        {
            return View();
        }
        [Authorize(Roles ="HeadAdmin")]
        public async Task<IActionResult> DeleteAdmin()
        {
            return View();
        }
    }
}
