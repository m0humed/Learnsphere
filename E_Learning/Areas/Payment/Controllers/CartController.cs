using E_Learning.Models;
using E_Learning.Repositories.IReposatories;
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
        private readonly UserManager<User> _userManager; 

        public CartController(ICartRepository cartRepository, UserManager<User> userManager)
        {
            _cartRepository = cartRepository;
            _userManager = userManager;
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

        public async Task<IActionResult> AddToCart(string courseId)
        {
            var userId = GetCurrentUserId(); // Get the logged-in user's ID
            if (userId == null)
            {
                return RedirectToAction("ShowLogin", "Account", new { area = "Authentication"}); 
            }

            
            var cart = new Cart
            {
                CourseId = courseId,
                UserId = userId, 
               
            };

            await _cartRepository.AddAsync(cart);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFromCart(string courseId)
        {

            var userId = GetCurrentUserId(); // Get the logged-in user's ID

            if (userId == null)
            {
                return RedirectToAction("ShowLogin", "Account", new { area = "Authentication" });
            }


            // Check if the cart item exists for the user
            var cartItem = await _cartRepository.GetByIdAsync(courseId, userId);
            if (cartItem == null)
            {
                // Handle case where the cart item does not exist
                return NotFound(); // Return 404 not found
            }

            // Delete the cart item
            await _cartRepository.DeleteAsync(courseId, userId);

            // Redirect to the cart index or appropriate page
            return RedirectToAction("Index");
        }

    }
}

