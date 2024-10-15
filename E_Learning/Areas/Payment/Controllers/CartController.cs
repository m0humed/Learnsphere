using E_Learning.Helper;
using E_Learning.Models;
using E_Learning.Repositories.IReposatories;
using E_Learning.Repository.IReposatories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.Areas.Payment.Controllers
{
    //[Authorize]
    [Area("Payment")]
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;

        private readonly ICourseRepository _courseRepository;
        private readonly UserManager<User> _userManager;
        private readonly IViewRenderService view;

        public CartController(ICartRepository cartRepository,
                              ICourseRepository courseRepository , 
                              UserManager<User> userManager,
                              IViewRenderService view)
        {
            _cartRepository = cartRepository;
            _userManager = userManager;
            this.view = view;
            _courseRepository = courseRepository;
        }

        
        public async Task<IActionResult> Index()
        {
            var userId = GetCurrentUserId(); // Get the logged-in user's ID
            if (userId == null)
            {
                return RedirectToAction("ShowLogin", "Account", new { area = "Authentication" });
            }

            var carts = await _cartRepository.GetCartsByUserIdAsync(userId);
            return View(carts);
        }

        // Retrieve the logged-in user's ID
        private string GetCurrentUserId()
        {
            return _userManager.GetUserId(User); // Get the logged-in user's ID
        }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> AddToCart(string courseId)
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            // Add the course to the cart
            await _cartRepository.AddAsync(new Cart { CourseId = courseId, UserId = userId });

            // Get updated cart and course data
            var cart = await _cartRepository.GetCartsByUserIdAsync(userId);
            var courses = await _courseRepository.GetAllAsync();

            //// Return updated partial views for the cart summary and course list
            return Json(new
            {
                //   cartSummary = await view.RenderToStringAsync("_CartSummary", cart),
                courseList = await view.RenderToStringAsync("_CourseList", courses)//new CourseViewModel { Courses = courses, CartCourses = cart })
            });

        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(string courseId)
        {
            var userId = GetCurrentUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            // Remove the course from the cart
            await _cartRepository.DeleteAsync(courseId, userId);

            // Get updated cart and course data
            var cart = await _cartRepository.GetCartsByUserIdAsync(userId);
            var courses = await _courseRepository.GetAllAsync();

            //Return updated partial views for the cart summary and course list
            return Json(new
            {
                //cartSummary = await view.RenderToStringAsync("_CartSummary", cart),
                courseList = await view.RenderToStringAsync("_CourseList", courses )//new CourseViewModel { Courses = courses, CartCourses = cart })
            }); ;
            //return Ok();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteFromCart(string courseId)
        {
            var userId = GetCurrentUserId();

            if (userId == null)
            {
                return RedirectToAction("Login", "Account"); // Redirect to login if not authenticated
            }

            var cartItem = await _cartRepository.GetByIdAsync(courseId, userId);
            if (cartItem == null)
            {
                return NotFound();
            }

            await _cartRepository.DeleteAsync(courseId, userId);

            // After deleting, return the updated cart items as a partial view
            var updatedCartItems = await _cartRepository.GetCartsByUserIdAsync(userId);
            ViewBag.cartsummary = PartialView("_cartsummaryPartialView", updatedCartItems);
            return PartialView("_CartItemsPartialView", updatedCartItems);
        }




    }
}

