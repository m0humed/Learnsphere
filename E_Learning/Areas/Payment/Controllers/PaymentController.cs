using Microsoft.AspNetCore.Mvc;

namespace E_Learning.Areas.Payment.Controllers
{
    public class PaymentController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
